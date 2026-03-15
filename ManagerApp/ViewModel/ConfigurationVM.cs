using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ManagerApp.View;
using SuperCchicLibrary.Service;
using System.Windows;

namespace ManagerApp.ViewModel
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

            if(await ApiHelper.SetupConfig(ApiBaseUrl))
            {
                MessageBox.Show("Configuration sauvegardée avec succès!", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                (Application.Current.MainWindow as MainWindow).ApiSetup();
            }
            else
            {
                MessageBox.Show("Échec de la configuration. Veuillez vérifier l'URL et réessayer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
