using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SelfCheckoutApp.View;
using SuperCchicLibrary;
using SuperCchicLibrary.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SelfCheckoutApp.ViewModel
{
    public partial class AuthorizeVM : ObservableObject
    {
        [ObservableProperty]
        private string? _username, _password;

        public AuthorizeVM()
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
                    MessageBox.Show("Autorisation réussie.");
                    (Application.Current.MainWindow as MainWindow).ShowTransactionView();

                    Username = string.Empty;
                    Password = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Autorisation invalide");
            }
        }
    }
}
