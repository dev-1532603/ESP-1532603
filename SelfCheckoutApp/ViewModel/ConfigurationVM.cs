using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SelfCheckoutApp.View;
using SuperCchicLibrary.Service;
using System.Windows;

namespace SelfCheckoutApp.ViewModel
{
    public partial class ConfigurationVM : ObservableObject
    {
        [ObservableProperty]
        private string _apiBaseUrl;

        public ConfigurationVM()
        {
            _apiBaseUrl = string.Empty;
        }

        [RelayCommand]
        public async void SaveConfig()
        {
            if (string.IsNullOrEmpty(ApiBaseUrl)) return;

            if (await ApiHelper.SetupConfig(ApiBaseUrl))
            {
                MessageBox.Show("Connexion à l'api établie.");
                ApiHelper.InitializeClient();
                (Application.Current.MainWindow as MainWindow).ShowTransactionView();
            }
            else
            {
                MessageBox.Show("Connexion échouée.");
            }
        }
    }
}
