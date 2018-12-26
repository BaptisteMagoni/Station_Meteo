using SimpleTCP;
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
using System.Net;
using System.Net.Sockets;

namespace Station_Meteo
{
    /// <summary>
    /// Logique d'interaction pour FenServ.xaml
    /// </summary>
    public partial class FenServ : Window
    {
        private String address;
        private String port;
        private String action;
        private TcpClient client;

        public FenServ()
        {
            InitializeComponent();
            action = "";
        }

        public String getAddress()
        {
            return address;
        }

        public int getPort()
        {
            return Int32.Parse(port);
        }

        private void Ip_serveur_TextChanged(object sender, TextChangedEventArgs e)
        { 
            this.address = ip_serveur.Text;
        }

        private void Port_serveur_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.port = port_serveur.Text;
        }

        private bool is_null(string[] list)
        {
            bool isnull = false;
            foreach (string line in list)
                if (line == null) isnull = true;
                else isnull = false;
            return isnull;
        }

        private void Button_Click_Check(object sender, RoutedEventArgs e)
        {
            if (ip_serveur.Text != "" && port_serveur.Text != "")
            {
                try
                {
                    client = new TcpClient(ip_serveur.Text, Int32.Parse(port_serveur.Text));
                    action = "check";
                    this.Close();
                }
                catch
                {
                    label_error.Content = "L'IP n'est pas bonne. Ressayer!";
                }
            }
            else
                label_error.Content = "Veuillez compléter tous les champs !";
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            action = "close";
            this.Close();
        }

        public String getAction()
        {
            return action;
        }

        public TcpClient getClient()
        {
            return client;
        }
    }
}