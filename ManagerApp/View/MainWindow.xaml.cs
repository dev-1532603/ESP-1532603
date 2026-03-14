
using ManagerApp.ViewModel;
using SuperCchicLibrary.Service;
using System.Configuration;
using System.Transactions;
using System.Windows;

namespace ManagerApp.View
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ApiSetup();

            InitializeComponent();
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
            Initialize();
            ShowLoginView();
        }
        private void Initialize()
        {
            LoginVM loginVM = new LoginVM();
            ProductVM productVM = new ProductVM();

            loginV.DataContext = loginVM;
            productV.DataContext = productVM;
        }
        private void HideAllViews()
        {
            loginV.Visibility = Visibility.Collapsed;
            configurationV.Visibility = Visibility.Collapsed;
            productV.Visibility = Visibility.Collapsed;

        }

        public void ShowLoginView()
        {
            HideAllViews();
            loginV.Visibility = Visibility.Visible;
        }
        public void ShowConfigurationView()
        {
            HideAllViews();
            configurationV.Visibility = Visibility.Visible;
        }
        public void ShowProductView()
        {
            HideAllViews();
            productV.Visibility = Visibility.Visible;
        }
    }
}
