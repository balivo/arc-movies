using ArcMovies.ViewModels;
using ArcMovies.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArcMovies.Services
{
    sealed class NavigationService
    {
        private static Lazy<NavigationService> _Lazy = new Lazy<NavigationService>(() => new NavigationService());

        public static NavigationService Current { get => _Lazy.Value; }

        NavigationService()
        {
            _Mappings = new Dictionary<Type, Type>();

            CreateViewModelMappings();
        }

        Application CurrentApplication => Application.Current;

        INavigation Navigation => ((NavigationPage)CurrentApplication.MainPage).Navigation;

        readonly Dictionary<Type, Type> _Mappings;

        void CreateViewModelMappings()
        {
            _Mappings.Add(typeof(MoviesViewModel), typeof(MoviesView));
            _Mappings.Add(typeof(MovieViewModel), typeof(MovieView));
        }

        internal Task GoBack(bool toRoot = false, bool animated = true)
        {
            if (Navigation.ModalStack.Count > 0)
                return Navigation.PopModalAsync(animated);

            if (toRoot)
                return Navigation.PopToRootAsync(animated);

            return Navigation.PopAsync(animated);
        }

        public Task Navigate<TViewModel>(object parameter = null, bool fromMenu = false, bool toRoot = false) where TViewModel : BaseViewModel
            => InternalNavigateToAsync(typeof(TViewModel), parameter: parameter, fromMenu: fromMenu, toRoot: toRoot);

        async Task InternalNavigateToAsync(Type viewModelType, object parameter = null, bool fromMenu = false, bool toRoot = false)
        {
            try
            {
                Page page = CreateAndBindPage(viewModelType);

                if (viewModelType.BaseType == typeof(BaseRootPageViewModel))
                {
                    if (CurrentApplication.MainPage is null)
                        CurrentApplication.MainPage = new NavigationPage(page);
                    else
                        await Navigation.PopToRootAsync();
                }
                else if (viewModelType.BaseType == typeof(BaseModalViewModel))
                    await Navigation.PushModalAsync(page);
                else
                    await Navigation.PushAsync(page);

                await (page.BindingContext as BasePageViewModel)?.InitializeAsync(parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Type GetPageTypeForViewModel(Type viewModelType)
        {
            try
            {
                if (!_Mappings.ContainsKey(viewModelType))
                    throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");

                return _Mappings[viewModelType];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Page CreateAndBindPage(Type viewModelType)
        {
            try
            {
                Type pageType = GetPageTypeForViewModel(viewModelType);

                if (pageType == null)
                    throw new Exception($"Mapping type for {viewModelType} is not a page");

                Page page = Activator.CreateInstance(pageType) as Page;
                page.BindingContext = Activator.CreateInstance(viewModelType) as BaseViewModel;

                return page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Initialize() => Navigate<MoviesViewModel>().Wait();

        public Task RemovePage(Type page)
        {
            var listaPagina = new List<Page>();

            var pagina = Application.Current.MainPage.Navigation.NavigationStack;

            foreach (var item in pagina)
            {
                if (item.GetType() == page)
                    listaPagina.Add(item);
            }

            foreach (var item in listaPagina)
                Application.Current.MainPage.Navigation.RemovePage(item);

            return Task.CompletedTask;
        }
    }
}

