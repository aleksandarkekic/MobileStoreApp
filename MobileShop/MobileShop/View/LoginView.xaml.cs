using MobileShop.MySql;
using System;
using System.Windows;
using static MobileShop.Hash.Hash;
using  MobileShop.MySql;
using  MobileShop.Model;
using System.Collections.Generic;

namespace MobileShop.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
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

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            string hash = Hash_SHA1(txtPass.Password);
            Console.WriteLine("HASH ZA KORISNIKA JE: " + hash);
            List<Korisnik> korisnici= MySqlKorisnik.GetKorisnik(txtUser.Text);
            
            string hash2 = "";
            if (korisnici.Count > 0)
             hash2 = korisnici[0].sifra;
            if (hash == hash2)
            {
                //MainStoreView mw = new MainStoreView(this, korisnici[0].uloga);
                AppView app = new AppView(this, korisnici[0].uloga);
                app.ShowDialog();
            }
            else {
                string errorMessage = "Pogresno ime ili lozinka!";
                string caption = "ERROR!";
               MessageBoxResult result= MessageBox.Show(errorMessage, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
