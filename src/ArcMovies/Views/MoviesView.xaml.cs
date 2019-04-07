using ArcMovies.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArcMovies.Views
{
    public partial class MoviesView : ContentPage
    {
        MoviesViewModel ViewModel { get => (MoviesViewModel)BindingContext; }

        public MoviesView()
        {
            InitializeComponent();
        }

        void ItemTapped(object sender, ItemTappedEventArgs e)
            => ((ListView)sender).SelectedItem = null;

        void ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (!(e.Item is MovieItemViewModel item))
                return;

            if (ViewModel.Movies.IndexOf(item) == (ViewModel.Movies.Count - 2))
            {
                ViewModel.LoadingPage = true;
                ViewModel.RefreshCommand.Execute(null);
            }
        }

        void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.LoadingPage = false;
            ViewModel.RefreshCommand.Execute(null);
        }
    }
}
