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
    /// Interaction logic for ProductSearchV.xaml
    /// </summary>
    public partial class ProductSearchV : UserControl
    {
        public ProductSearchV()
        {
            InitializeComponent();
        }

        private void AppendText(string text)
        {
            int caretIndex = SearchTextBox.CaretIndex;
            SearchTextBox.Text = SearchTextBox.Text.Insert(caretIndex, text);
            SearchTextBox.CaretIndex = caretIndex + text.Length;
        }

        private void Key_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is string key)
                AppendText(key);
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (SearchTextBox.Text.Length == 0) return;
            int caretIndex = SearchTextBox.CaretIndex;
            if (caretIndex > 0)
            {
                SearchTextBox.Text = SearchTextBox.Text.Remove(caretIndex - 1, 1);
                SearchTextBox.CaretIndex = caretIndex - 1;
            }
        }

        private void Space_Click(object sender, RoutedEventArgs e) => AppendText(" ");

        //private void Clear_Click(object sender, RoutedEventArgs e) => SearchTextBox.Clear();
    }
}
