using ArcMovies.Clients;
using ArcMovies.Data.Dtos;
using System.Threading.Tasks;

namespace ArcMovies.ViewModels
{
    sealed class MovieViewModel : BasePageViewModel
    {
        public MovieDto Movie { get; private set; }

        public string Poster { get => $"{TmdbConstants.MoviePosterPrefix}{Movie?.PosterPath}"; }
        public string ReleaseDateLabel { get => Movie?.ReleaseDateLabel; }
        public string Genres { get => Movie?.Genres; }
        public string Overview { get => Movie?.Overview; }

        public override Task InitializeAsync(object args)
        {
            if (args is MovieDto movie)
            {
                Movie = movie;

                Title = movie.Title;
                OnPropertyChanged(nameof(Poster));
                OnPropertyChanged(nameof(ReleaseDateLabel));
                OnPropertyChanged(nameof(Genres));
                OnPropertyChanged(nameof(Overview));
            }

            return Task.CompletedTask;
        }
    }
}
