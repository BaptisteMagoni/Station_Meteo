using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Media;
using System.Windows.Controls;

namespace Station_Meteo
{
    
    class ThreadClientSocket
    {
        private SocketClient sk_client;
        private TcpClient client;
        private MainWindow main;
        private Thread m_thread;
        private string[] rx_data;
        private string[] unite = { "", "", " inches", " °C", " %", " °C", " %" };

        public ThreadClientSocket(MainWindow mw, TcpClient cl)
        {
            client = cl;
            main = mw;
            sk_client = new SocketClient(client);
            m_thread = new Thread(new ThreadStart(run_thread));
            m_thread.Start();
        }

        public void run_thread()
        {
            while (true)
            {
                if (!client.Connected)
                {
                    try
                    {

                        client = new TcpClient(main.getAddress(), main.getPort());
                        sk_client = new SocketClient(client);
                    }
                    catch
                    {
                        Console.WriteLine("Error connection Client !");
                    }
                }
                try
                {
                    sk_client.send_data("GET:ALL");
                    try
                    {
                        rx_data = sk_client.getData();
                        main.Dispatcher.Invoke(() =>
                        {
                            try
                            {
                                for (int i = 0; i < main.getTextBlockData().Count; i++)
                                    main.getTextBlockData()[i].Text = rx_data.GetValue(i).ToString() + unite[i];
                            }
                            catch
                            {
                                Console.WriteLine("Problème sur la boucle de lecture des données");
                            }
                        });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error getData() : " + e.Message);
                    }
                    client.Close();
                    Thread.Sleep(4500);
                }
                catch
                {
                    Console.WriteLine("Serveur inacessible !");
                    init_object_mainwin();
                    m_thread.Abort();
                    
                }
            }
        }

        public void init_object_mainwin()
        {
            main.Dispatcher.Invoke(() =>
            {
                main.Button_connexion.BorderBrush = Brushes.Red;
                main.Label_ip_serveur.Content = "IP du serveur : ?";
                main.Label_port_serveur.Content = "Port du serveur : ?";
                foreach (TextBlock tb in main.getTextBlockData())
                    tb.Text = "None";
                main.displayMessageBox("Le serveur semble ne plus répondre. Réessayer de vous reconnecter !"); 
            });
        }
    }
}