using CommunityToolkit.Mvvm.ComponentModel;
using SuperCchicLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutApp
{
    public partial class TransactionItem : ObservableObject
    {
        [ObservableProperty]
        private string code;

        [ObservableProperty]
        private string productName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalPrice))]
        private int quantity;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TotalPrice))]
        private decimal unitPrice;

        public decimal TotalPrice => UnitPrice * Quantity;

        public TransactionItem(string code, string productName, int quantity, decimal unitPrice)
        {
            Code = code;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
