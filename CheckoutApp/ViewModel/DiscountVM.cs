using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SuperCchicAPI.Models;
using SuperCchicLibrary;
using SuperCchicLibrary.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CheckoutApp.ViewModel
{
    public partial class DiscountVM : ObservableObject
    {
        private Action<EmployeeDTO> _applyTransactionDiscount;
        private List<EmployeeDTO> _employees = new();
        [ObservableProperty]
        private string? _searchText;
        [ObservableProperty]
        private ObservableCollection<EmployeeDTO> _searchResults = new ObservableCollection<EmployeeDTO>();
        [ObservableProperty]
        private EmployeeDTO? _selectedEmployee;

        public DiscountVM() 
        {
            SearchText = string.Empty;
            SearchResults = new ObservableCollection<EmployeeDTO>();
            SelectedEmployee = null;

            InitializeEmployeesAsync();
        }
        public void SetApplyTransactionDiscount(Action<EmployeeDTO> callback)
        {
            _applyTransactionDiscount = callback;
        }
        [RelayCommand]
        private void ApplyEmployeeDiscount()
        {
            if(SelectedEmployee != null && _applyTransactionDiscount != null)
            {
                _applyTransactionDiscount(SelectedEmployee);
            }
        }
        partial void OnSearchTextChanged(string? oldValue, string? newValue)
        {
            if (string.IsNullOrEmpty(newValue?.Trim()))
            {
                SearchResults = new ObservableCollection<EmployeeDTO>(_employees);
                return;
            }

            SearchEmployees(SearchText);
        }
        private async Task InitializeEmployeesAsync()
        {
            try
            {
                _employees = await ApiProcessor.GetEmployees() ?? new List<EmployeeDTO>();
                SearchResults = new ObservableCollection<EmployeeDTO>(_employees);
            }
            catch (Exception ex)
            {
                // Gérer l'erreur (log, message utilisateur, etc.)
                Console.WriteLine($"Erreur lors du chargement des employés: {ex.Message}");
            }
        }
        private void SearchEmployees(string searchText)
        {
            var filtered = _employees.Where(p => p.Name.ToLower().Contains(searchText.ToLower())).ToList();

            SearchResults.Clear();
            foreach (var product in filtered)
            {
                SearchResults.Add(product);
            }
        }
    }
}
