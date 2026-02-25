using CommunityToolkit.Mvvm.ComponentModel;
using SuperCchicAPI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutApp.ViewModel
{
    public partial class TransactionVM : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<TransactionItem> transactionItems = new ObservableCollection<TransactionItem>();
        [ObservableProperty]
        private decimal subtotal, tps, tvq, transactionTotal;
        public struct TransactionItem
        {
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal TotalPrice => UnitPrice * Quantity;
        }
        public void AddToTransaction(ProductDTO product)
        {
            var item = new TransactionItem
            {
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = product.QuantityInStock,
            };
            TransactionItems.Add(item);
        }
        public void ApplyTransactionDiscount(EmployeeDTO employee)
        {
            
        }
        public TransactionVM() { }

    }
}
