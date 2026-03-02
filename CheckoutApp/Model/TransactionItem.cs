using CommunityToolkit.Mvvm.ComponentModel;
using SuperCchicLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace CheckoutApp
{
    public partial class TransactionItem : ObservableObject
    {
        [ObservableProperty]
        public int productId;
        [ObservableProperty]
        public bool taxable;
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

        public TransactionItem(int productId, string code, string productName, int quantity, decimal unitPrice, bool taxable)
        {
            ProductId = productId; 
            Code = code;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Taxable = taxable;
        }
        public static List<OrderDetailDTO> ConvertToOrderDetails(List<TransactionItem> items)
        {
            List<OrderDetailDTO> list = new List<OrderDetailDTO>();

            foreach (TransactionItem item in items)
            {
                OrderDetailDTO orderdetail = new OrderDetailDTO
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                };

                list.Add(orderdetail);
            }

            return list;
        }
    }
}
