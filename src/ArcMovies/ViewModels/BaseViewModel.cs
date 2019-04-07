using ArcMovies.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArcMovies.ViewModels
{
    abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region [ INotifyPropertyChanged ]

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetProperty<T>(ref T backingStore, T value, Action onChanged = null, [CallerMemberName]string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyName);
        }

        protected internal void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region [ IsBusy ]

        private bool _IsBusy = false;
        public bool IsBusy
        {
            get => _IsBusy;
            set => SetProperty(ref _IsBusy, value, onChanged: () =>
            {
                OnPropertyChanged(nameof(IsNotBusy));
                ChangeIsBusyCommandCanExecute();
            });
        }

        public bool IsNotBusy { get => !_IsBusy; }

        protected virtual void ChangeIsBusyCommandCanExecute() { }

        #endregion

        #region [ GoBackCommand ]

        Command _GoBackCommand;
        public Command GoBackCommand
            => _GoBackCommand
            ?? (_GoBackCommand = new AsyncCommand(GoBackCommandExecute));

        Task GoBackCommandExecute() => NavigationService.Current.GoBack();

        #endregion
    }
}