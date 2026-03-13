using CheckoutApp.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using SuperCchicLibrary.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CheckoutApp.ViewModel
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
        public void SaveConfig()
        {
            if (string.IsNullOrEmpty(ApiBaseUrl)) return;

            if(ApiHelper.SetupConfig(ApiBaseUrl))
            {
                MessageBox.Show("Connexion à l'api établie.");
                ApiHelper.InitializeClient();
                (Application.Current.MainWindow as MainWindow).ShowLoginView();
            }
            else
            {
                MessageBox.Show("Connexion échouée.");
            }
        }
    }
}
