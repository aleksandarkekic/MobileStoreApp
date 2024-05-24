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

namespace MobileShop.View
{
    /// <summary>
    /// Interaction logic for AppView.xaml
    /// </summary>
    public partial class AppView : Window
    {
        public AppView(Window parent, String uloga)
        {
            Owner = parent;

            InitializeComponent();
            if (uloga == "admin")
            {
                users.Visibility = Visibility.Visible;
               addUser.Visibility = Visibility.Visible;

            }
            else
            {
               
                users.Visibility = Visibility.Collapsed;
                addUser.Visibility = Visibility.Collapsed;

            }

        }
        private void sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selected = sidebar.SelectedItem as NavButton;

            navframe.Navigate(selected.Navlink);
            
            
        }
    }
}
