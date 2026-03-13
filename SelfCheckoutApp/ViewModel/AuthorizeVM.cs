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

        public Action<bool>? OnAuthorizationComplete;

        [RelayCommand]
        private async void Authorize()
        {
            if (string.IsNullOrEmpty(Username?.Trim())) return ;
            if (string.IsNullOrEmpty(Password?.Trim())) return ;

            try
            {
                EmployeeDTO employee = await ApiProcessor.Login(Username, Password);

                if (employee != null)
                {
                    Username = string.Empty;
                    Password = string.Empty;
                    OnAuthorizationComplete?.Invoke(true);
                    OnAuthorizationComplete = null;
                    (Application.Current.MainWindow as MainWindow).ShowTransactionView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Autorisation invalide");
                Username = string.Empty;
                Password = string.Empty;
                OnAuthorizationComplete?.Invoke(false);
                OnAuthorizationComplete = null;
                (Application.Current.MainWindow as MainWindow).ShowTransactionView();
            }
        }
    }
}
