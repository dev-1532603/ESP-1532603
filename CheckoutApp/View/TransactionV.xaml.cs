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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CheckoutApp.ViewModel;

namespace CheckoutApp.View
{
    /// <summary>
    /// Logique d'interaction pour TransactionV.xaml
    /// </summary>
    public partial class TransactionV : UserControl
    {
        public TransactionV()
        {
            InitializeComponent();
        }

        private void DiscountButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).ShowDiscountView();
        }

        private void SearchProductButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).ShowProductSearchView();
        }
    }
}
