using System.Threading.Tasks;

namespace ArcMovies.ViewModels
{
    abstract class BasePageViewModel : BaseViewModel
    {
        string _Title = "Title";
        public string Title
        {
            get => _Title;
            set => SetProperty(ref _Title, value);
        }

        string _Icon;
        public string Icon
        {
            get => _Icon;
            set => SetProperty(ref _Icon, value);
        }

        public virtual Task InitializeAsync(object args) => Task.CompletedTask;
    }
}