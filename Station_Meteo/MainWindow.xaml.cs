﻿using System;
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
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Station_Meteo
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Grid> list_grid = new List<Grid>();
        private TcpClient clientSocket = new TcpClient();
        private TcpClient client;
        private SocketClient sk_client;
        private string m_address;
        private int m_port;
        private ThreadClientSocket th;
        private List<TextBlock> list_textblock_data = new List<TextBlock>();

        public MainWindow()
        {
            InitializeComponent();
            list_grid.Add(Grid_Mesure);
            list_grid.Add(Grid_Serveur);
            list_textblock_data.Add(TextBlock_type);
            list_textblock_data.Add(TextBlock_bartrend);
            list_textblock_data.Add(TextBlock_barometric);
            list_textblock_data.Add(TextBlock_temp_interieur);
            list_textblock_data.Add(TextBlock_humidite_interieur);
            list_textblock_data.Add(TextBlock_temp_exterieur);
        }

        private void Button_Click_connexion(object sender, RoutedEventArgs e)
        {
            FenServ serv = new FenServ();
            serv.ShowDialog();
            if (serv.getAction() == "check")
            {
                Label_ip_serveur.Content = String.Format("IP du serveur : {0}", serv.getAddress());
                Label_port_serveur.Content = String.Format("Port du serveur : {0}", serv.getPort());
                client = serv.getClient();
                if (client.Connected)
                {
                    set_button_connexion_green();
                    sk_client = new SocketClient(client);
                    m_address = serv.getAddress();
                    m_port = serv.getPort();
                    th = new ThreadClientSocket(this, client);
                }
                else
                    set_button_connexion_red();
            }
        }

        public void set_button_connexion_red()
        {
            Button_connexion.BorderBrush = Brushes.Red;
        }

        public void set_button_connexion_green()
        {
            Button_connexion.BorderBrush = Brushes.Green;
        }

        public string getAddress()
        {
            return m_address;
        }

        public int getPort()
        {
            return m_port;
        }

        private void Button_Click_Mesure(object sender, RoutedEventArgs e)
        {
            visibility(Grid_Mesure);
        }

        private void Button_Click_Serveur(object sender, RoutedEventArgs e)
        {
            visibility(Grid_Serveur);
        }

        private void visibility(Grid grid)
        {
            foreach (Grid g in list_grid)
                if (g == grid)
                    g.Visibility = Visibility.Visible;
                else
                    g.Visibility = Visibility.Hidden;
        }

        public List<TextBlock> getTextBlockData()
        {
            return list_textblock_data;
        }

        public void displayMessageBox(string message)
        {
            MessageBox.Show(message);
        }

    }
}