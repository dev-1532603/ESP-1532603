using MaterialDesignThemes.Wpf;
using SelfCheckoutApp.ViewModel;
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

namespace SelfCheckoutApp.View
{
    /// <summary>
    /// Interaction logic for TransactionV.xaml
    /// </summary>
    public partial class TransactionV : UserControl
    {
        public TransactionV()
        {
            InitializeComponent();
        }
        private void SearchProductButton_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Window.GetWindow(this)).ShowProductSearchView();
        }
        private void UserControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (DialogHost.IsDialogOpen("dialogs")) return;
            if (e.Key != Key.Enter) return;

            e.Handled = true;
            ScanBox.Text = ScanBox.Text.Trim('\r', '\n');

            if (!string.IsNullOrWhiteSpace(ScanBox.Text))
            {
                var vm = DataContext as TransactionVM;
                vm.ScanItemCommand.Execute(null);
                ScanBox.Clear();
            }

            ScanBox.Focus();
        }
        private void UserControl_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Si un dialog est ouvert, laisse passer
            //var window = Window.GetWindow(this);
            if (DialogHost.IsDialogOpen("dialogs")) return;

            // Redirige le texte vers scanTextBox
            int caret = ScanBox.CaretIndex;
            ScanBox.Text = ScanBox.Text.Insert(caret, e.Text);
            ScanBox.CaretIndex = caret + e.Text.Length;
            e.Handled = true;
        }
    }
}
