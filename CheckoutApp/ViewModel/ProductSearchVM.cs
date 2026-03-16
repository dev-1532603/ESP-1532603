using CheckoutApp.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SuperCchicLibrary;
using SuperCchicLibrary.Service;
using System.Collections.ObjectModel;
using System.Windows;

namespace CheckoutApp.ViewModel
{
    public partial class ProductSearchVM : ObservableObject
    {
        private Action<Product> _addToCart;
        [ObservableProperty]
        private ObservableCollection<Product> _products = new ObservableCollection<Product>(); 
        [ObservableProperty]
        private string? _searchText;
        [ObservableProperty]
        private ObservableCollection<Product> _searchResults = new ObservableCollection<Product>();
        [ObservableProperty]
        private Product? _selectedProduct;

        public ProductSearchVM(ObservableCollection<Product> products)
        {
            SearchText = string.Empty;
            Products = products;
            SearchResults = new ObservableCollection<Product>(products);
        }
        public void SetAddToCartAction(Action<Product> callback)
        {
            _addToCart = callback;

        }
        [RelayCommand]
        public void AddToTransaction()
        {
            if(SelectedProduct == null)
            {
                MessageBox.Show("Veuillez sélectionner un produit avant de l'ajouter à la transaction.", "Aucun produit sélectionné", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SelectedProduct != null && _addToCart != null)
            {
                _addToCart(SelectedProduct);
                (Application.Current.MainWindow as MainWindow).ShowTransactionView();
                SelectedProduct = null;
            }
        }

        partial void OnSearchTextChanged(string? oldValue, string? newValue)
        {
            if (string.IsNullOrEmpty(newValue?.Trim()))
            {
                SearchResults = new ObservableCollection<Product>(_products);
                return;
            }

            SearchProducts(SearchText);            
        }
        
        private void SearchProducts(string searchText)
        {
            var filtered = _products.Where(p => p.Name.ToLower().Contains(searchText.ToLower()) || p.Code.Contains(searchText.ToLower())).ToList();

            SearchResults.Clear();
            foreach (var product in filtered)
            {
                SearchResults.Add(product);
            }
        }
    }
}
