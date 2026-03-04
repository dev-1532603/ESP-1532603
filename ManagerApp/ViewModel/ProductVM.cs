using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

namespace ManagerApp.ViewModel
{
    public partial class ProductVM : ObservableObject
    {
        private Func<Task>? _onDialogConfirm;
        private List<Product> _products = new List<Product>();
        [ObservableProperty]
        private ObservableCollection<Product> _searchResults = new ObservableCollection<Product>();
        [ObservableProperty]
        private ObservableCollection<Subcategory> _subcategories = new ObservableCollection<Subcategory>();
        [ObservableProperty]
        private string? _searchText;
        [ObservableProperty]
        private Product? _selectedProduct;
        [ObservableProperty]
        private bool _isDialogOpen, _isDialogReadOnly, _isDialogToggleable;
        [ObservableProperty]
        private string _dialogTitle = string.Empty;
        [ObservableProperty]
        private string _dialogMessage = string.Empty;
        [ObservableProperty]
        private string _dialogName = string.Empty;
        [ObservableProperty]
        private decimal _dialogPrice;
        [ObservableProperty]
        private int _dialogQuantityInStock;
        [ObservableProperty]
        private bool _dialogTaxable;
        [ObservableProperty]
        private Subcategory? _dialogSubcategory;
        [ObservableProperty]
        private int _dialogSubcategoryId;

        public ProductVM()
        {
            InitializeProductsAsync();

            SearchResults = new ObservableCollection<Product>(_products);
        }
        private async Task InitializeProductsAsync()
        {
            try
            {
                _subcategories = new ObservableCollection<Subcategory>(await ApiProcessor.GetSubcategories() ?? new List<Subcategory>());
                _products = await ApiProcessor.GetProducts() ?? new List<Product>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement des produits: {ex.Message}");
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
            var filtered = _products.Where(p => p.Name.ToLower().Contains(searchText.ToLower())).ToList();

            SearchResults.Clear();
            foreach (var product in filtered)
            {
                SearchResults.Add(product);
            }
        }

        [RelayCommand]
        public void OpenAddDialog()
        {
            DialogTitle = "Ajouter un produit";
            DialogMessage = "Veuillez remplir les informations du produit.";
            DialogName = string.Empty;
            DialogPrice = 0;
            DialogQuantityInStock = 0;
            DialogTaxable = false;
            DialogSubcategory = null;
            //DialogSubcategoryId = Subcategories?.FirstOrDefault()?.Id ?? 0; 
            IsDialogReadOnly = false;
            IsDialogToggleable = true;
            _onDialogConfirm = () => AddProduct();
            IsDialogOpen = true;
        }
        [RelayCommand]
        public void OpenEditDialog()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Veuillez sélectionner un produit.", "Aucun produit sélectionné",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DialogTitle = "Modifier un produit";
            DialogMessage = "Modifiez les informations du produit.";
            DialogName = SelectedProduct.Name;
            DialogPrice = SelectedProduct.Price;
            DialogQuantityInStock = SelectedProduct.QuantityInStock;
            DialogTaxable = SelectedProduct.Taxable;
            DialogSubcategory = SelectedProduct.Subcategory;
            //DialogSubcategoryId = SelectedProduct.IdSubcategory;
            IsDialogReadOnly = false;
            IsDialogToggleable = true;
            _onDialogConfirm = () => EditProduct();
            IsDialogOpen = true;
        }
        [RelayCommand]
        public void OpenDeleteDialog()
        {
            if (SelectedProduct == null)
            {
                MessageBox.Show("Veuillez sélectionner un produit.", "Aucun produit sélectionné",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DialogTitle = $"Supprimer — {SelectedProduct.Name}";
            DialogMessage = "Êtes-vous sûr de vouloir supprimer ce produit ?";
            DialogName = SelectedProduct.Name;
            DialogPrice = SelectedProduct.Price;
            DialogQuantityInStock = SelectedProduct.QuantityInStock;
            DialogTaxable = SelectedProduct.Taxable;
            DialogSubcategory = SelectedProduct.Subcategory;
            IsDialogReadOnly = true;
            IsDialogToggleable = false;
            _onDialogConfirm = () => DeleteProduct();
            IsDialogOpen = true;
        }
        [RelayCommand]
        public async Task GenerateReport()
        {
            var result = MessageBox.Show(
                "Voulez-vous générer le rapport des ventes du mois ?",
                "Générer un rapport",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                //await ApiProcessor.GetReport();
            }
        }
        [RelayCommand]
        public async Task ConfirmDialog()
        {
            if (!ValidateDialog()) return;
            if (_onDialogConfirm != null)
            {
                await _onDialogConfirm.Invoke();
            }
            IsDialogOpen = false;
        }

        [RelayCommand]
        public void CancelDialog()
        {
            IsDialogOpen = false;
        }
        private async Task AddProduct()
        {
            var newProduct = new Product
            {
                Name = DialogName,
                Price = DialogPrice,
                QuantityInStock = DialogQuantityInStock,
                Taxable = DialogTaxable,
                IdSubcategory = DialogSubcategoryId,
                Subcategory = Subcategories?.FirstOrDefault(s => s.Id == DialogSubcategoryId),
            };
            newProduct.Code = BarcodeService.GenerateBarcode(newProduct);

            try
            {
                await ApiProcessor.PostProduct(newProduct);

                _products.Add(newProduct);
                SearchResults = new ObservableCollection<Product>(_products);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout du produit: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private async Task EditProduct()
        {
            if (SelectedProduct == null) return;

            Product? product = _products.FirstOrDefault(p => p.Id == SelectedProduct.Id);

            if (product == null) return;

            product.Name = DialogName;
            product.Price = DialogPrice;
            product.QuantityInStock = DialogQuantityInStock;
            product.Taxable = DialogTaxable;
            product.IdSubcategory = DialogSubcategoryId;
            product.Subcategory = Subcategories?.FirstOrDefault(s => s.Id == DialogSubcategoryId);

            try
            {
                await ApiProcessor.PutProduct(product);

                SelectedProduct = null;
                SearchResults = new ObservableCollection<Product>(_products);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la modification du produit: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async Task DeleteProduct()
        {
            if (SelectedProduct == null) return;

            try
            {
                await ApiProcessor.DeleteProduct(SelectedProduct.Id);

                _products.Remove(SelectedProduct);
                SelectedProduct = null;
                SearchResults = new ObservableCollection<Product>(_products);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la suppression du produit: {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool ValidateDialog()
        {
            // si le dialog est en mode lecture, one ne valide pas les champs
            if (IsDialogReadOnly) return true; 

            //if (string.IsNullOrWhiteSpace(DialogCode))
            //{
            //    MessageBox.Show("Le code est obligatoire.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return false;
            //}
            if (string.IsNullOrWhiteSpace(DialogName))
            {
                MessageBox.Show("Le nom est obligatoire.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (DialogPrice <= 0)
            {
                MessageBox.Show("Le prix doit être supérieur à 0.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (DialogQuantityInStock < 0)
            {
                MessageBox.Show("La quantité ne peut pas être négative.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (DialogSubcategory == null)
            {
                MessageBox.Show("Veuillez sélectionner une sous-catégorie.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (DialogSubcategoryId == 0)
            {
                MessageBox.Show("Veuillez sélectionner une sous-catégorie.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
