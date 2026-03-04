using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SuperCchicLibrary.Service
{
    public interface IEmployeeService
    {
        Employee? CurrentEmployee { get; set; }
        bool IsLoggedIn { get; }
    }
    public class AuthenticationService : IEmployeeService, INotifyPropertyChanged
    {
        private static readonly Lazy<AuthenticationService> _instance = new(() => new AuthenticationService());
        public static AuthenticationService Instance { get { return _instance.Value; } }
        private Employee? _currentEmployee;
        public Employee? CurrentEmployee
        {
            get => _currentEmployee;
            set
            {
                _currentEmployee = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }
        public bool IsLoggedIn => CurrentEmployee != null;
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
