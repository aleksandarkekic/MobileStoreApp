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
using MobileShop.MySql;
using System.Collections.Generic;
using MobileShop.Model;



namespace MobileShop.View
{
    /// <summary>
    /// Interaction logic for MainStoreView.xaml
    /// </summary>
    public partial class MainStoreView : Window
    {
        public MainStoreView(Window parent, String uloga)
        {
            Owner = parent;
            InitializeComponent();
            if (uloga == "admin")
            {
                adminButton.Visibility = Visibility.Visible;
            }
            else
            {
                adminButton.Visibility = Visibility.Collapsed;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                DragMove();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void btnAddPhone_Click(object sender, RoutedEventArgs e)
        {

        }

   

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            List<MobilniTelefonDTO> list = MySqlTelefoni.getModels();
           
            dataGrid1.ItemsSource = list;
            
        }

        private void adminButton_Click(object sender, RoutedEventArgs e)
        {
            List<KorisnikDTO> list = MySqlKorisnik.GetAllUsers();
            dataGrid1.ItemsSource = list;
        }

      
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            List<ZastitnaMaskaDTO> list = MySQLZastitnaMaska.getZastineMaske();
            dataGrid1.ItemsSource = list;
        }

        private void MenuItem_Click3(object sender, RoutedEventArgs e)
        {
            List<ZastitnoStakloDTO> list = MySqlZastitnoStaklo.getZastinaStakla();
            dataGrid1.ItemsSource = list;
        }
    }
}
