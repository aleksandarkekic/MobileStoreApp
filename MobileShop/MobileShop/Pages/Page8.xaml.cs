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
using MobileShop.MySql;

namespace MobileShop.Pages
{
    /// <summary>
    /// Interaction logic for Page8.xaml
    /// </summary>
    public partial class Page8 : Page
    {
        public Page8()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            MySqlTelefoni.insertTelefon(Model.Text, (short)int.Parse(price.Text),screen.Text,battery.Text,os.Text,int.Parse(weight.Text), DateTime.Parse(date.Text), cpu.Text, camera.Text, color.Text, memory.Text, memoryRAM.Text);
        }
    }
}
