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
    /// Interaction logic for Page5.xaml
    /// </summary>
    public partial class Page5 : Page
    {
        public Page5()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            List<Mjesto> lista = MySqlMjesto.GetMjesto(town.Text);
            int idMjesta = -1;
            if (lista.Count == 0)
            {
                Mjesto mjesto = new Mjesto(int.Parse(posta.Text), town.Text);
                MySqlMjesto.InsertMjesto(mjesto);
                Adresa adresa = new Adresa(streetNumb.Text, street.Text, int.Parse(posta.Text));
                MySqlAdresa.InsertAdresa(adresa);
               
            }
            else {
                idMjesta = MySqlMjesto.getPostaByNaziv(town.Text);
                Adresa adresa = new Adresa(streetNumb.Text, street.Text, idMjesta);
                MySqlAdresa.InsertAdresa(adresa);
            }
            int idAdrese = MySqlAdresa.getIdAdresa(streetNumb.Text, street.Text, int.Parse(posta.Text));
            MySqlKorisnik.InsertKorisnik(name.Text, surname.Text, password.Password, username.Text, idAdrese, role.Text);
        }
    }
}
