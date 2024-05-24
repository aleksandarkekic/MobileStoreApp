using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MobileShop.Model;
using MobileShop.MySql;

namespace MobileShop.Pages
{
    /// <summary>
    /// Interaction logic for Page4.xaml
    /// </summary>
    public partial class Page4 : Page
    {
        public Page4()
        {
            InitializeComponent();
            List<KorisnikDTO> list = MySqlKorisnik.GetAllUsers();
            dataGrid1.ItemsSource = list;

        }



        private void delete_Click_1(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                try
                {
                    KorisnikDTO korisnik = (KorisnikDTO)dataGrid1.SelectedItem;
                    int id = korisnik.id;
                    MySqlKorisnik.DeleteKorisnikById(id);
                    List<KorisnikDTO> list = MySqlKorisnik.GetAllUsers();
                    dataGrid1.ItemsSource = list;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

    }
}