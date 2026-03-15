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
        public ConfigurationVM configurationVM;
        public LoginVM loginVM;
        public TransactionVM transactionVM;
        public ProductSearchVM productSearchVM;
        public DiscountVM discountVM;
        public MainWindow()
        {
            InitializeComponent();
            ApiSetup();
        }
        public async void ApiSetup()
        {
            bool apiInitialized = await ApiHelper.InitializeClient();

            if (!apiInitialized)
            {
                configurationVM = new ConfigurationVM();
                configurationV.DataContext = configurationVM;

                ShowConfigurationView();

                return;
            }
            await Initialize();
            ShowLoginView();
        }
        private async Task Initialize()
        {
            loginVM = new LoginVM();
            transactionVM = new TransactionVM();
            productSearchVM = new ProductSearchVM(await transactionVM.InitializeProductsAsync());
            discountVM = new DiscountVM();

            loginV.DataContext = loginVM;
            transactionV.DataContext = transactionVM;
            productSearchV.DataContext = productSearchVM;
            discountV.DataContext = discountVM;

            productSearchVM.SetAddToCartAction(transactionVM.AddToTransaction);
            discountVM.SetApplyTransactionDiscount(transactionVM.ApplyTransactionDiscount);
        }
        
        private void HideAllViews()
        {
            loginV.Visibility = Visibility.Collapsed;
            transactionV.Visibility = Visibility.Collapsed;
            productSearchV.Visibility = Visibility.Collapsed;
            discountV.Visibility = Visibility.Collapsed;
            configurationV.Visibility = Visibility.Collapsed;
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
        public void ShowConfigurationView()
        {
            HideAllViews();
            configurationV.Visibility = Visibility.Visible;
        }
    }
}
