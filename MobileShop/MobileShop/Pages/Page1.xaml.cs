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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            List<MobilniTelefonDTO> list = MySqlTelefoni.getModels();

            dataGrid1.ItemsSource = list;
            dataGrid1.Items.Refresh();

        }

        private void btn_details_Click(object sender, RoutedEventArgs e)
        {
            expander.IsExpanded = !expander.IsExpanded;
            if (dataGrid1.SelectedItem != null)
            {
                MobilniTelefonDTO telefonDTO = (MobilniTelefonDTO)dataGrid1.SelectedItem;
                string model = telefonDTO.model;
                List<MobilniTelefon2DTO> lista = MySqlTelefoni.getAllByModel(model);
                if (lista.Count > 0) {
                    string modelTelefona = lista[0].model;
                    modelBlock.Text = "Model: " + modelTelefona;
                    operativniSistemBlock.Text = "Operativni sistem: " + lista[0].operativniSistem;
                    procesorBlock.Text="Procesor: " + lista[0].procesor;
                    kapacitetBaterijeBlock.Text="Kapacitet baterije: " + lista[0].kapacitetBaterije;
                    dijagonalaEkranaBlock.Text="Dijagonala ekrana: " + lista[0].dijagonalaEkrana;
                    tezinaBlock.Text="Tezina: " + lista[0].tezina;
                    kameraBlock.Text="Kamera: " + lista[0].kamera;
                    internaMemorijaBlock.Text="Interna memorija[GB]: " + lista[0].internaMemorija;
                    radnaMemorijaBlock.Text="Radna memorija[GB]: " + lista[0].radnaMemorija;
                    datumBlock.Text="Datum objavljivanja: " + lista[0].datumObjavljivanja;
                    bojaBlock.Text="Boja: " + lista[0].boja;
                    cijenaBlock.Text="Cijena: " + lista[0].cijena +" KM";
                }
            }

        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedItem != null) {
                try
                {
                    MobilniTelefonDTO telefon = (MobilniTelefonDTO)dataGrid1.SelectedItem;
                    string model = telefon.model;
                    int id = (int)MySqlTelefoni.getIdMobilnogTelefona(model);
                    MySqlTelefoni.deleteTelefon(model, id);
                    List<MobilniTelefonDTO> list = MySqlTelefoni.getModels();
                    dataGrid1.ItemsSource = list;
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                    Console.WriteLine(ex.StackTrace);
                }
            }

        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
