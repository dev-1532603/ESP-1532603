using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SuperCchicAPI.Models;
using SuperCchicLibrary;
using SuperCchicLibrary.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CheckoutApp.ViewModel
{
    public partial class ProductSearchVM : ObservableObject
    {
        private Action<ProductDTO> _addToCart;
        private List<ProductDTO> _products = new();

        [ObservableProperty]
        private string? _searchText;
        [ObservableProperty]
        private ObservableCollection<ProductDTO> _searchResults = new ObservableCollection<ProductDTO>();
        [ObservableProperty]
        private ProductDTO? _selectedProduct;

        public ProductSearchVM()
        {
            SearchText = string.Empty;

            InitializeProductsAsync();
        }
        public void SetAddToCartAction(Action<ProductDTO> callback)
        {
            _addToCart = callback;
        }
        [RelayCommand]
        public void ConfirmCartAddition()
        {
            if(SelectedProduct != null && _addToCart != null)
            {
                _addToCart(SelectedProduct);
            }
        }

        partial void OnSearchTextChanged(string? oldValue, string? newValue)
        {
            if (string.IsNullOrEmpty(newValue?.Trim()))
            {
                SearchResults = new ObservableCollection<ProductDTO>(_products);
                return;
            }

            SearchProducts(SearchText);            
        }
        private async Task InitializeProductsAsync()
        {
            try
            {
                _products = await ApiProcessor.GetAllProductsInfos() ?? new List<ProductDTO>();
                SearchResults = new ObservableCollection<ProductDTO>(_products);
            }
            catch (Exception ex)
            {
                // Gérer l'erreur (log, message utilisateur, etc.)
                Console.WriteLine($"Erreur lors du chargement des produits: {ex.Message}");
            }
        }
        private void SearchProducts(string searchText)
        {
            var filtered = _products.Where(p => p.Name.ToLower().Contains(searchText.ToLower())).ToList();

            SearchResults.Clear();
            foreach (var product in filtered)
            {
                SearchResults.Add(product);
            }
        }
    }
}
