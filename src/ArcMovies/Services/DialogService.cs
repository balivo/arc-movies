using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArcMovies.Services
{
    sealed class DialogService
    {
        private static Lazy<DialogService> _Lazy = new Lazy<DialogService>(() => new DialogService());
        public static DialogService Current => _Lazy.Value;

        private DialogService() { }

        public Task Alert(string title, string message, string buttonLabel = "Ok")
            => App.Current.MainPage.DisplayAlert(title, message, buttonLabel);

        public async Task Alert(Exception ex)
        {

            if (ex is UnauthorizedAccessException)
            {
                await Alert("Ooops", "Você não possui acesso a esse recurso ou sua sessão expirou por inatividade. Redirecionando para o login.");

                NavigationService.Current.Initialize();
            }
            else if (ex is InvalidOperationException)
                await Alert("Ooops", ex.Message);
            else if (ex.Message.Contains("transport connection") || ex.Message.Contains("target"))
                await Alert("Ooops", "Não foi possível completar essa operação, verifique sua conexão com a internet e/ou tente novamente mais tarde");
            else
                await Alert("Ah não!", ex.Message);
        }

        public Task<string> ActionSheet(string title, string cancel, string destruction, params string[] buttons)
            => App.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);

        internal async Task<bool> Confirm(string message, string simLabel = "Sim", string naoLabel = "Não")
            => (await App.Current.MainPage.DisplayActionSheet(message, null, null, buttons: new[] { simLabel, naoLabel })).Equals(simLabel, StringComparison.InvariantCultureIgnoreCase);
    }
}
