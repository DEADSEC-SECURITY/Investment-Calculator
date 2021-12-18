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

namespace Investment.ToggleSwitch
{
    /// <summary>
    /// Interaction logic for ToggleSwitch.xaml
    /// </summary>
    public partial class ToggleSwitch : UserControl
    {
        public bool switchOn = false;

        public ToggleSwitch()
        {
            InitializeComponent();
        }

        private void switchBtnClick(object sender, RoutedEventArgs e)
        {
            if (switchBtn.HorizontalAlignment == HorizontalAlignment.Left) 
            {
                switchBtn.HorizontalAlignment = HorizontalAlignment.Right;
                timesIcon.Visibility = Visibility.Hidden;
                checkIcon.Visibility = Visibility.Visible;
                switchOn = true;
            } else
            {
                switchBtn.HorizontalAlignment = HorizontalAlignment.Left;
                timesIcon.Visibility = Visibility.Visible;
                checkIcon.Visibility = Visibility.Hidden;
                switchOn = false;
            }
        }
    }
}
