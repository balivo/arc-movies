using ArcMovies.Clients;
using ArcMovies.Data.Dtos;
using ArcMovies.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArcMovies.ViewModels
{
    sealed class MovieItemViewModel : BaseViewModel
    {
        public MovieItemViewModel(MovieDto firstItem, MovieDto secondItem = null)
        {
            FirstItem = firstItem;
            SecondItem = secondItem;
        }

        public MovieDto FirstItem { get; }

        public bool HasFirstItem => !(FirstItem is null);
        public string FirstItemPoster => $"{TmdbConstants.MoviePosterPrefix}{FirstItem?.PosterPath}";

        public MovieDto SecondItem { get; }

        public bool HasSecondItem => !(SecondItem is null);
        public string SecondItemPoster => $"{TmdbConstants.MoviePosterPrefix}{SecondItem?.PosterPath}";

        #region [ DetailsCommand ]

        Command _DetailsCommand;
        public Command DetailsCommand
            => _DetailsCommand
            ?? (_DetailsCommand = new AsyncCommand<MovieDto>(DetailsCommandExecute));

        Task DetailsCommandExecute(MovieDto selected)
            => NavigationService.Current.Navigate<MovieViewModel>(selected);

        #endregion
    }
}
