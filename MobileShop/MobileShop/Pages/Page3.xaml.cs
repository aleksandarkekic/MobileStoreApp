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
using MobileShop.Model;
using MobileShop.MySql;

namespace MobileShop.Pages
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
            List<ZastitnoStakloDTO> list = MySqlZastitnoStaklo.getZastinaStakla();
            dataGrid1.ItemsSource = list;
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null)
            {
                try
                {
                    ZastitnoStakloDTO maska = (ZastitnoStakloDTO)dataGrid1.SelectedItem;
                    string model = maska.model;
                    int id = (int)MySqlZastitnoStaklo.getIdZastinogStakla(model);
                    MySqlZastitnoStaklo.deleteStaklo(model,id);
                    List<ZastitnoStakloDTO> list = MySqlZastitnoStaklo.getZastinaStakla();
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
