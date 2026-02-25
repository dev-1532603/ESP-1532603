using CheckoutApp.Service;
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
        public MainWindow()
        {
            ApiHelper.InitializeClient();
            InitializeComponent();
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
