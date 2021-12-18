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
using System.Diagnostics;

namespace Investment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            content.Content = new BondAnalyzer.BondPage();
        }
        /// <summary>
        /// This function will change all StackPanel objects
        /// of the navGrid to its inactive style which consists
        /// of opacity .1
        /// </summary>
        private void SetAllInactive() {
            string childType;
            foreach(UIElement child in navGrid.Children) {
                childType = child.GetType().ToString();
                // Check if child object of navGrid is a StackPanel
                if(childType == "System.Windows.Controls.StackPanel") {
                    child.Opacity = 0.1;
                }
            }
        }
        private void SetCurrentPage(string page) {
            SetAllInactive();

            page = page.ToLower();

            if(page == "bondpage") {
                bondsStack.Opacity = 1;
            } else if(page == "stockpage") {
                stocksStack.Opacity = 1;
            }
        }
        private void BondPageClick(object sender, RoutedEventArgs e) {
            content.Content = new BondAnalyzer.BondPage();
            SetCurrentPage("BondPage");
        }
        private void StockPageClick(object sender, RoutedEventArgs e) {
            //content.Content = new StockAnalyzer.StockPage();
            SetCurrentPage("StockPage");
        }
    }
}
