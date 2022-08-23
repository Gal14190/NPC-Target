using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace NPC_Target
{
    public partial class Form1 : Form
    {
        private IPEndPoint ServerEndPoint;
        private Socket WinSocket;

        private int[] NPC_VALUE = { 99, 99, 99, 99, 99, 99, 99, 99 };
        

        public void ThreadProc()
        {
            while (true)
            {
                try
                {
                    byte[] data = new byte[1024];


                    Console.Write("Waiting for client");
                    IPEndPoint sender2 = new IPEndPoint(IPAddress.Any, 0);
                    EndPoint Remote = (EndPoint)(sender2);
                    int recv = WinSocket.ReceiveFrom(data, ref Remote);
                    Console.WriteLine("Message received from {0}:", Remote.ToString());
                    Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

                    if (data[0] < 8)
                        NPC_VALUE[data[0]] = data[1];
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();

            ServerEndPoint = new IPEndPoint(IPAddress.Any, 900);
            WinSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            WinSocket.Bind(ServerEndPoint);

            Thread t = new Thread(new ThreadStart(ThreadProc));

            t.Start();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void UPD_CHECK_Tick(object sender, EventArgs e)
        {
            Label[] labels = { PLAYER_DATA, NPC1_DATA, NPC2_DATA, NPC3_DATA, NPC4_DATA, NPC5_DATA, NPC6_DATA, NPC7_DATA };
            Panel[] panels = { panel8, panel1, panel2, panel3, panel4, panel5, panel6, panel7 };
            for (int i = 0; i < 8; i++)
            {
                labels[i].Text = NPC_VALUE[i].ToString();

                if (NPC_VALUE[i] == 0)
                    panels[i].BackColor = Color.Gray;
            }
            
        }
    }
}
