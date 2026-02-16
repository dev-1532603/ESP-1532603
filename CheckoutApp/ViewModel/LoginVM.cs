using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void Login()
        {
            if (string.IsNullOrEmpty(Username?.Trim())) return;
            if (string.IsNullOrEmpty(Password?.Trim())) return;


        }
    }
}
