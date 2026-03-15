using SelfCheckoutApp.ViewModel;
using SuperCchicLibrary.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public AuthorizeVM authorizeVM;
        public TransactionVM transactionVM;
        public ProductSearchVM productSearchVM;
        public MainWindow()
        {
            InitializeComponent();
            ApiSetup();
        }
        private async void ApiSetup()
        {
            bool apiInitialized = await ApiHelper.InitializeClient();

            if (!apiInitialized)
            {
                ConfigurationVM configurationVM = new ConfigurationVM();
                configurationV.DataContext = configurationVM;

                ShowConfigurationView();

                return;
            }
            await Initialize();
            ShowTransactionView();
        }
        private async Task Initialize()
        {
            authorizeVM = new AuthorizeVM();
            transactionVM = new TransactionVM();
            productSearchVM = new ProductSearchVM(await transactionVM.InitializeProductsAsync());

            authorizeV.DataContext = authorizeVM;
            transactionV.DataContext = transactionVM;
            productSearchV.DataContext = productSearchVM;

            productSearchVM.SetAddToCartAction(transactionVM.AddToTransaction);
        }
        private void HideAllViews()
        {
            authorizeV.Visibility = Visibility.Collapsed;
            transactionV.Visibility = Visibility.Collapsed;
            productSearchV.Visibility = Visibility.Collapsed;
            configurationV.Visibility = Visibility.Collapsed;
        }

        public void ShowAuthorizeView(Action<bool> onComplete)
        {
            authorizeVM.OnAuthorizationComplete = onComplete;
            HideAllViews();
            authorizeV.Visibility = Visibility.Visible;
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
        public void ShowConfigurationView()
        {
            HideAllViews();
            configurationV.Visibility = Visibility.Visible;
        }
    }
}
