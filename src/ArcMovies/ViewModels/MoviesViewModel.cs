using ArcMovies.Clients;
using ArcMovies.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArcMovies.ViewModels
{
    sealed class MoviesViewModel : BaseRootPageViewModel
    {
        public MoviesViewModel()
        {
            Title = "Upcoming";
        }

        public ObservableCollection<MovieItemViewModel> Movies { get; } = new ObservableCollection<MovieItemViewModel>();

        TmdbMoviesResult _TmdbMoviesResult;

        public bool CanLoadMore { get => (_TmdbMoviesResult?.Page ?? 0) < (_TmdbMoviesResult?.Pages ?? 1); }

        public bool IsSearching { get => !string.IsNullOrWhiteSpace(_Filter); }
        public bool IsNotSearching { get => string.IsNullOrWhiteSpace(_Filter); }

        string _Filter = null;
        public string Filter
        {
            get => _Filter;
            set => SetProperty(ref _Filter, value, () =>
            {
                OnPropertyChanged(nameof(IsSearching));
                OnPropertyChanged(nameof(IsNotSearching));
            });
        }

        public bool LoadingPage { get; set; } = false;

        #region [ RefreshCommand ]

        Command _RefreshCommand;
        public Command RefreshCommand
            => _RefreshCommand
            ?? (_RefreshCommand = new AsyncCommand(RefreshCommandExecute, RefreshCommandCanExecute));

        private bool RefreshCommandCanExecute() => true;

        async Task RefreshCommandExecute()
        {
            IsBusy = true;

            try
            {
                var genresResult = await TmdbHttpClient.Current.GetGenres();

                int page = _TmdbMoviesResult?.Page ?? 0;

                if (!LoadingPage)
                {
                    page = 0;
                    Movies.Clear();
                }

                _TmdbMoviesResult = await TmdbHttpClient.Current.GetMovies(_Filter, ++page);

                if ((_TmdbMoviesResult?.Movies?.Count ?? 0) > 0)
                {
                    var movies = _TmdbMoviesResult.Movies.ToList();

                    while (movies.Count > 0)
                    {
                        if (movies.Count >= 2)
                        {
                            var firstItem = movies.ElementAt(0);
                            var secondItem = movies.ElementAt(1);

                            firstItem.Genres = string.Join(", ", genresResult.Genres.Where(genre => firstItem.GenreIds.Contains(genre.Id)).Select(genre => genre.Name));
                            secondItem.Genres = string.Join(", ", genresResult.Genres.Where(genre => secondItem.GenreIds.Contains(genre.Id)).Select(genre => genre.Name));

                            Movies.Add(new MovieItemViewModel(firstItem, secondItem));

                            movies.Remove(firstItem);
                            movies.Remove(secondItem);
                        }
                        else
                        {
                            var firstItem = movies.FirstOrDefault();

                            if (!(firstItem is null))
                            {
                                firstItem.Genres = string.Join(", ", genresResult.Genres.Where(genre => firstItem.GenreIds.Contains(genre.Id)).Select(genre => genre.Name));

                                Movies.Add(new MovieItemViewModel(firstItem));

                                movies.Remove(firstItem);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await DialogService.Current.Alert(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        public override Task InitializeAsync(object args)
        {
            RefreshCommand.Execute(null);

            return Task.CompletedTask;
        }
    }
}
