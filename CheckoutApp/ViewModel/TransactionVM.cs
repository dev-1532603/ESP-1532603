using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using IronBarCode;
using IronSoftware.Drawing;
using SuperCchicLibrary;
using SuperCchicLibrary.Service;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.Net.Http;


namespace CheckoutApp.ViewModel
{
    public partial class TransactionVM : ObservableObject
    {
        const decimal TPSAMOUNT = 0.05m, TVQAMOUNT = 0.09975m, DISCOUNT = 0.9m;
        public List<Product> _products = new();
        private bool _isDiscountApplied;
        private Action<string>? _onDialogConfirm;
        private List<OrderReceipt> _transactionHistory = new();
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
        [ObservableProperty]
        private string _scannedBarcode = string.Empty;

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

                //C'est pour avoir accès aux barcodes dans le bin debug
                foreach (var item in _products)
                {
                    BarcodeService.GenerateBarcodeLabel(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement des produits: {ex.Message}");
            }
        }
        private void UpdateTransaction()
        {
            decimal taxableSubtotal = TransactionItems.Where(i => i.Taxable).Sum(i => i.TotalPrice);
            Subtotal = TransactionItems.Sum(i => i.TotalPrice);
            if (_isDiscountApplied)
            {
                Subtotal *= DISCOUNT;
            }
            Tps = taxableSubtotal * TPSAMOUNT;
            Tvq = taxableSubtotal * TVQAMOUNT;
            TransactionTotal = Subtotal + Tps + Tvq;
        }
        [RelayCommand]
        public void ClearTransaction()
        {
            TransactionItems.Clear();
            Subtotal = 0;
            Tps = 0;
            Tvq = 0;
            TransactionTotal = 0;
            TransactionComment = string.Empty;
            _isDiscountApplied = false;
        }
        //Callback method
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
        //Callback method
        public void ApplyTransactionDiscount(EmployeeDTO employee)
        {
            _isDiscountApplied = true;
            UpdateTransaction();
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
            
            _transactionHistory.Add(OrderReceipt.GenerateReceipt(orderdetails, TransactionComment, Subtotal, Tps, Tvq, TransactionTotal, order.Date));

            ClearTransaction();
        }
        [RelayCommand]
        public void PrintLastReceipt()
        {
            OrderReceipt.PrintReceipt(_transactionHistory.Last());
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
        [RelayCommand]
        public void ScanItem()
        {
            if (!string.IsNullOrEmpty(ScannedBarcode))
            {
                Product scannedProduct = _products.FirstOrDefault(p => p.Code == ScannedBarcode);
                if(scannedProduct != null)
                {
                    AddToTransaction(scannedProduct);
                }
            }
            ScannedBarcode = "";
        }
    }
}
