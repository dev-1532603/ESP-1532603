using SelfCheckoutApp.ViewModel;
using SuperCchicLibrary.Service;
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

namespace SelfCheckoutApp.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AuthorizeVM _authorizeVM;
        public TransactionVM _transactionVM;
        public ProductSearchVM _productSearchVM;
        public MainWindow()
        {
            ApiHelper.InitializeClient();

            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            _authorizeVM = new AuthorizeVM();
            _transactionVM = new TransactionVM();
            _productSearchVM = new ProductSearchVM(_transactionVM._products);

            authorizeV.DataContext = _authorizeVM;
            transactionV.DataContext = _transactionVM;
            productSearchV.DataContext = _productSearchVM;

            _productSearchVM.SetAddToCartAction(_transactionVM.AddToTransaction);
        }
        private void HideAllViews()
        {
            authorizeV.Visibility = Visibility.Collapsed;
            transactionV.Visibility = Visibility.Collapsed;
            productSearchV.Visibility = Visibility.Collapsed;
        }

        public void ShowAuthorizeView()
        {
            HideAllViews();
            authorizeV.Visibility = Visibility.Visible;
        }

        public void ShowTransactionView()
        {
            HideAllViews();
            transactionV.Visibility = Visibility.Visible;
            // fonction pour attendre que le rendu soit loaded, pour focus sur la scanbox, sinon problème d'ordre de load avec le focus
            //Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, new Action(() =>
            //{
            //    transactionV.ScanBox.Focus();
            //}));
        }

        public void ShowProductSearchView()
        {
            HideAllViews();
            productSearchV.Visibility = Visibility.Visible;
        }
    }
}
