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
    /// Logique d'interaction pour LoginV.xaml
    /// </summary>
    public partial class LoginV : UserControl
    {
        public LoginV()
        {
            InitializeComponent();
            this.DataContext = new LoginVM();
        }
    }
}
