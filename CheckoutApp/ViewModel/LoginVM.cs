using SuperCchicLibrary.Service;
using CheckoutApp.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using SuperCchicLibrary;

namespace CheckoutApp.ViewModel
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
        private async void Login()
        {
            if (string.IsNullOrEmpty(Username?.Trim())) return;
            if (string.IsNullOrEmpty(Password?.Trim())) return;

            try
            {
                LoginResponseDTO response = await ApiProcessor.Login(Username, Password);
                MessageBox.Show("Connexion réussie.");
                (Application.Current.MainWindow as MainWindow).ShowTransactionView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login invalide");
            }
        }
    }
}
