using SuperCchicLibrary.Service;
using CheckoutApp.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using SuperCchicLibrary;
using System.Windows.Controls;

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
                EmployeeDTO employee = await ApiProcessor.Login(Username, Password);

                if (employee != null)
                {
                    AuthenticationService.Instance.CurrentEmployee = employee;
                    MessageBox.Show($"Bienvenue {AuthenticationService.Instance.CurrentEmployee.Name}!", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    (Application.Current.MainWindow as MainWindow).ShowTransactionView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la connexion : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}
