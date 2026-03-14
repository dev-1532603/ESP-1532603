using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ManagerApp.View;
using SuperCchicLibrary;
using SuperCchicLibrary.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManagerApp.ViewModel
{
    public partial class LoginVM : ObservableObject
    {
        [ObservableProperty]
        private string? _username, _password;

        public LoginVM()
        {
            Username = string.Empty;
            Password = string.Empty;
        }

        [RelayCommand]
        private async void LoginAdmin()
        {
            if (string.IsNullOrEmpty(Username?.Trim())) return;
            if (string.IsNullOrEmpty(Password?.Trim())) return;

            try
            {
                //test
                AuthenticationService.Instance.CurrentEmployee = await ApiProcessor.LoginAdmin(Username, Password);
                MessageBox.Show("Connexion réussie.");
                (Application.Current.MainWindow as MainWindow).ShowProductView();

                Username = string.Empty;
                Password = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login invalide");
            }
        }
    }
}
