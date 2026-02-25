using CheckoutApp.Service;
using CheckoutApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CheckoutApp.View
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public TransactionVM TransactionVM { get; }
        public ProductSearchVM ProductSearchVM { get; }
        public DiscountVM DiscountVM { get; }
        public MainWindow()
        {
            ApiHelper.InitializeClient();
            InitializeComponent(); 

            TransactionVM = new TransactionVM();
            ProductSearchVM = new ProductSearchVM();
            DiscountVM = new DiscountVM();

            ProductSearchVM.SetAddToCartAction(TransactionVM.AddToTransaction);
            DiscountVM.SetApplyTransactionDiscount(TransactionVM.ApplyTransactionDiscount);
        }
        private void HideAllViews()
        {
            loginV.Visibility = Visibility.Collapsed;
            transactionV.Visibility = Visibility.Collapsed;
            productSearchV.Visibility = Visibility.Collapsed;
            discountV.Visibility = Visibility.Collapsed;
        }

        public void ShowLoginView()
        {
            HideAllViews();
            loginV.Visibility = Visibility.Visible;
        }

        public void ShowTransactionView()
        {
            HideAllViews();
            transactionV.DataContext = TransactionVM;
            transactionV.Visibility = Visibility.Visible; 
        }

        public void ShowProductSearchView()
        {
            HideAllViews();
            productSearchV.DataContext = ProductSearchVM;
            productSearchV.Visibility = Visibility.Visible;
        }

        public void ShowDiscountView()
        {
            HideAllViews();
            discountV.DataContext = DiscountVM;
            discountV.Visibility = Visibility.Visible;
        }
    }
}
