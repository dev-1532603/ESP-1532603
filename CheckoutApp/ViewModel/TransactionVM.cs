using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using SuperCchicAPI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace CheckoutApp.ViewModel
{
    public partial class TransactionVM : ObservableObject
    {
        private bool _isDiscountApplied;
        [ObservableProperty]
        private ObservableCollection<TransactionItem> _transactionItems = new ObservableCollection<TransactionItem>();
        [ObservableProperty]
        private TransactionItem? _selectedTransactionItem;
        [ObservableProperty]
        private decimal subtotal, tps, tvq, transactionTotal;

        [ObservableProperty]
        private bool _isDialogOpen;

        private Action<string>? _onDialogConfirm;

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
            Subtotal = TransactionItems.Sum(i => i.TotalPrice);
            if (_isDiscountApplied)
            {
                Subtotal *= 0.9m;
            }
            Tps = Subtotal * 0.05m;
            Tvq = Subtotal * 0.09975m;
            TransactionTotal = Subtotal + Tps + Tvq;
        }
        public void AddToTransaction(ProductDTO product)
        {
            TransactionItem item = new TransactionItem(product.Code, product.Name, 1, product.Price);

            TransactionItem existingItem = TransactionItems.FirstOrDefault(i => i.ProductName == item.ProductName);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                TransactionItems.Add(item);
            }
            //UpdateTransaction();
        }
        public void ApplyTransactionDiscount(EmployeeDTO employee)
        {
            _isDiscountApplied = true;
            //UpdateTransaction();
        }
        [RelayCommand]
        public void CompleteTransaction()
        {
            // Logique pour finaliser la transaction (envoi au backend, impression du reçu, etc.)
            // Après la finalisation, réinitialiser la transaction
            TransactionItems.Clear();
            Subtotal = 0;
            Tps = 0;
            Tvq = 0;
            TransactionTotal = 0;
            _isDiscountApplied = false;
        }
        [RelayCommand]
        public void RemoveFromTransaction()
        {
            if(SelectedTransactionItem != null && TransactionItems.Contains(SelectedTransactionItem)) 
            {
                TransactionItems.Remove(SelectedTransactionItem);
                //UpdateTransaction();
            }
        }
        private void UpdateItemQuantity(int newQuantity)
        {
            if (SelectedTransactionItem != null && TransactionItems.Contains(SelectedTransactionItem))
            {
                SelectedTransactionItem.Quantity = newQuantity;
                //UpdateTransaction();
            }
        }
    }
}
