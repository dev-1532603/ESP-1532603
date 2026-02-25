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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CheckoutApp.View
{
    /// <summary>
    /// Interaction logic for DiscountV.xaml
    /// </summary>
    public partial class DiscountV : UserControl
    {
        public DiscountV()
        {
            InitializeComponent();
        }

        private void BackToTransaction_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).ShowTransactionView();
        }
    }
}
