using CommunityToolkit.Mvvm.ComponentModel;
using SuperCchicLibrary;
using SuperCchicLibrary.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutApp.ViewModel
{
    public partial class ProductSearchVM : ObservableObject
    {
        [ObservableProperty]
        private string? _searchText;
        [ObservableProperty]
        private ObservableCollection<Product> _searchResults = new ObservableCollection<Product>();
        [ObservableProperty]
        private Product? _selectedProduct;

        public ProductSearchVM()
        {
            SearchText = string.Empty;
            SearchResults = new ObservableCollection<Product>();
            SelectedProduct = null;
        }

        partial void OnSearchTextChanged(string? oldValue, string? newValue)
        {
            if (string.IsNullOrEmpty(newValue?.Trim()))
            {
                SearchResults.Clear();
                return;
            }
            //var results = ApiProcessor.GetProducts(SearchText);
            //SearchResults = new ObservableCollection<Product>(results);
        }
    }
}
