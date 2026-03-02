using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SuperCchicLibrary;
using SuperCchicLibrary.Service;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Net.Http;


namespace CheckoutApp.ViewModel
{
    public partial class TransactionVM : ObservableObject
    {
        public List<Product> _products = new();
        private bool _isDiscountApplied;
        private Action<string>? _onDialogConfirm;
        [ObservableProperty]
        private ObservableCollection<TransactionItem> _transactionItems = new ObservableCollection<TransactionItem>();
        [ObservableProperty]
        private TransactionItem? _selectedTransactionItem;
        [ObservableProperty]
        private decimal subtotal, tps, tvq, transactionTotal;
        [ObservableProperty]
        private bool _isDialogOpen;
        [ObservableProperty]
        private string _dialogTitle = string.Empty;
        [ObservableProperty]
        private string _dialogInputText = string.Empty;
        [ObservableProperty]
        private string _dialogHint = string.Empty;
        [ObservableProperty]
        private string _transactionComment = string.Empty;

        public TransactionVM()
        {
            TransactionItems = new ObservableCollection<TransactionItem>();

            TransactionItems.CollectionChanged += (s, e) => UpdateTransaction();

            InitializeProductsAsync();
        }
        private async Task InitializeProductsAsync()
        {
            try
            {
                _products = await ApiProcessor.GetProducts() ?? new List<Product>();
                //SearchResults = new ObservableCollection<Product>(_products);
            }
            catch (Exception ex)
            {
                // Gérer l'erreur (log, message utilisateur, etc.)
                Console.WriteLine($"Erreur lors du chargement des produits: {ex.Message}");
            }
        }
        [RelayCommand]
        public void OpenQuantityDialog()
        {
            if (SelectedTransactionItem == null) return;
            DialogTitle = $"Modifier la quantité — {SelectedTransactionItem.ProductName}";
            DialogHint = "Nouvelle quantité";
            DialogInputText = SelectedTransactionItem.Quantity.ToString();
            _onDialogConfirm = result =>
            {
                if (int.TryParse(result, out int qty) && qty > 0)
                    UpdateItemQuantity(qty);
            };
            IsDialogOpen = true;
        }
        [RelayCommand]
        public void OpenCommentDialog()
        {
            DialogTitle = "Ajouter un commentaire";
            DialogHint = "Commentaire";
            DialogInputText = string.Empty;
            _onDialogConfirm = result =>
            {
                if (!string.IsNullOrWhiteSpace(result))
                    TransactionComment = result;
            };
            IsDialogOpen = true;
        }

        [RelayCommand]
        public void ConfirmDialog()
        {
            _onDialogConfirm?.Invoke(DialogInputText);
            IsDialogOpen = false;
        }

        [RelayCommand]
        public void CancelDialog()
        {
            IsDialogOpen = false;
        }
        private void UpdateTransaction()
        {
            decimal taxableSubtotal = TransactionItems.Where(i => i.Taxable).Sum(i => i.TotalPrice);
            Subtotal = TransactionItems.Sum(i => i.TotalPrice);
            if (_isDiscountApplied)
            {
                Subtotal *= 0.9m;
            }
            Tps = taxableSubtotal * 0.05m;
            Tvq = taxableSubtotal * 0.09975m;
            TransactionTotal = Subtotal + Tps + Tvq;
        }
        public void AddToTransaction(Product product)
        {
            TransactionItem item = new TransactionItem(product.Id, product.Code, product.Name, 1, product.Price, product.Taxable);

            TransactionItem? existingItem = TransactionItems.FirstOrDefault(i => i.ProductName == item.ProductName);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                TransactionItems.Add(item);
            }
        }
        public void ApplyTransactionDiscount(EmployeeDTO employee)
        {
            _isDiscountApplied = true;
        }
        [RelayCommand]
        public async Task CompleteTransaction()
        {
            List<OrderDetailDTO> orderdetails = TransactionItem.ConvertToOrderDetails(TransactionItems.ToList());
            
            OrderDTO orderDTO = new OrderDTO
            { 
                TotalPrice = TransactionTotal, 
                EmployeeId = 1, 
                OrderDetails = orderdetails 
            };

            var order = await ApiProcessor.PostOrder(orderDTO);

            // reset si ca marche
            TransactionItems.Clear();
            Subtotal = 0;
            Tps = 0;
            Tvq = 0;
            TransactionTotal = 0;
            _isDiscountApplied = false;

            OrderReceipt.GenerateReceipt(orderdetails, TransactionComment);
        }
        [RelayCommand]
        public void RemoveFromTransaction()
        {
            if(SelectedTransactionItem != null && TransactionItems.Contains(SelectedTransactionItem)) 
            {
                TransactionItems.Remove(SelectedTransactionItem);
            }
        }
        private void UpdateItemQuantity(int newQuantity)
        {
            if (SelectedTransactionItem != null && TransactionItems.Contains(SelectedTransactionItem))
            {
                SelectedTransactionItem.Quantity = newQuantity;
                UpdateTransaction();
            }
        }
    }
}
