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
    /// Interaction logic for ProductSearchV.xaml
    /// </summary>
    public partial class ProductSearchV : UserControl
    {
        public ProductSearchV()
        {
            InitializeComponent();
        }
        private void BackToTransaction_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).ShowTransactionView();
        }
    }
}
