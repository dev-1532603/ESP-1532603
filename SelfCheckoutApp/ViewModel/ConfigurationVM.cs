using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SelfCheckoutApp.View;
using SuperCchicLibrary.Service;
using System.Windows;

namespace SelfCheckoutApp.ViewModel
{
    public partial class ConfigurationVM : ObservableObject
    {
        private bool _urlValid;
        [ObservableProperty]
        private string _apiBaseUrl;
        public ConfigurationVM()
        {
            _apiBaseUrl = string.Empty;
        }
        partial void OnApiBaseUrlChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            bool valid = ApiBaseUrl.TrimEnd().EndsWith("/api/") || ApiBaseUrl.TrimEnd().EndsWith("/api");

            _urlValid = valid;
        }
        [RelayCommand]
        public async void SaveConfig()
        {

            if (!_urlValid)
            {
                MessageBox.Show("L'URL doit être au format https://monapi.exemple.com/api/", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (await ApiHelper.SetupConfig(ApiBaseUrl))
                {
                    MessageBox.Show("Configuration sauvegardée avec succès!", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    (Application.Current.MainWindow as MainWindow).ApiSetup();
                }
                else
                {
                    MessageBox.Show("Échec de la configuration. Veuillez vérifier l'URL et réessayer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la communication : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
