using SuperCchicLibrary.Service;
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
        public LoginVM _loginVM;
        public TransactionVM _transactionVM;
        public ProductSearchVM _productSearchVM;
        public DiscountVM _discountVM;
        public MainWindow()
        {
            ApiHelper.InitializeClient();

            InitializeComponent();    
            Initialize();
        }
        private void Initialize()
        {
            _loginVM = new LoginVM();
            _transactionVM = new TransactionVM();
            _productSearchVM = new ProductSearchVM(_transactionVM._products);
            _discountVM = new DiscountVM();

            loginV.DataContext = _loginVM;
            transactionV.DataContext = _transactionVM;
            productSearchV.DataContext = _productSearchVM;
            discountV.DataContext = _discountVM;

            _productSearchVM.SetAddToCartAction(_transactionVM.AddToTransaction);
            _discountVM.SetApplyTransactionDiscount(_transactionVM.ApplyTransactionDiscount);
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
            transactionV.Visibility = Visibility.Visible;
            // fonction pour attendre que le rendu soit loaded, pour focus sur la scanbox, sinon problème d'ordre de load avec le focus
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, new Action(() =>
            {
                transactionV.ScanBox.Focus();
            }));
        }

        public void ShowProductSearchView()
        {
            HideAllViews();
            productSearchV.Visibility = Visibility.Visible;
        }

        public void ShowDiscountView()
        {
            HideAllViews();
            discountV.Visibility = Visibility.Visible;
        }
    }
}
