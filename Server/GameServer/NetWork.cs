using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GameServer
{
    class NetWork
    {
        public static string GetLocalIPv4()     //获取本机ipv4地址
        {
            string host = Dns.GetHostName();
            IPHostEntry iPHostEntries = Dns.GetHostEntry(host);
            for (int i = 0; i < iPHostEntries.AddressList.Length; i++)
                if (iPHostEntries.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    return iPHostEntries.AddressList[i].ToString();
            return null;
        }
    }

    public class Room
    {
        //测试用
        int TestInt = 0;
        private void TestSendMsg(Player p)
        {
            List<Command> commands = new List<Command>();
            Command command = new Command
            {
                Target = p,
                CommandMsg = "Message" + TestInt
            };
            commands.Add(command);
            TestInt++;
            SendCommands(commands);
        }

        public Socket SeverSocket;
        public Player[] Players = new Player[2];

        public bool Start()
        {
            string ip = NetWork.GetLocalIPv4();
            IPEndPoint point = new IPEndPoint(IPAddress.Parse(ip), 1357);           //地址和端口
            SeverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);  //建立服务器socket
            SeverSocket.Bind(point);
            SeverSocket.Listen(0);

            Console.WriteLine($"Socket建立成功，监听{point.Address}:{point.Port}");
            return WaitPlayer();
        }

        public Player AnotherPlayer(Player p)
        {
            if (p == Players[0])
                return Players[1];
            else
                return Players[0];
        }

        private bool WaitPlayer()
        {
            int remainPlayer = 2;
            Socket client = null;

            try
            {
                while (remainPlayer != 0)
                {
                    client = SeverSocket.Accept();                  //等待用户连接
                    string endPoint = client.RemoteEndPoint.ToString();

                    Players[remainPlayer - 1] = new Player
                    {
                        Socket = client, Room = this, Msg = new string("")
                    };
                    Console.WriteLine($"玩家{remainPlayer}（{endPoint}）连接成功");

                    Thread listener = new Thread(Listen) { IsBackground = true };           //每一个Player建一个线程
                    listener.Start(Players[remainPlayer - 1]);

                    remainPlayer--;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        private void Listen(object obj)
        {
            var player = obj as Player;
            byte[] buffer = new byte[1000];
            Socket socket = player.Socket;
            try
            {
                while (true)
                {
                    int len = socket.Receive(buffer);
                    var msg = Encoding.UTF8.GetString(buffer, 0, len);
                    Console.WriteLine("接收到来自{0}的消息：\n{1}", player.Socket.RemoteEndPoint, msg);
                    player.Msg = msg;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                player.Exit(new object[] { 1});
            }
        }
        public void SendCommands(List<Command> commands)
        {
            string toPlayer1 = new string("");
            string toPlayer2 = new string("");
            foreach (var command in commands)
            {
                if (command.Target == Players[0])
                    toPlayer1 += command.CommandMsg + '\n';
                else if (command.Target == Players[1])
                    toPlayer2 += command.CommandMsg + '\n';
            }
            byte[] toPlayer1Bytes = System.Text.Encoding.UTF8.GetBytes(toPlayer1);
            byte[] toPlayer2Bytes = System.Text.Encoding.UTF8.GetBytes(toPlayer2);

            Players[1].Socket.Send(toPlayer2Bytes);
            Players[0].Socket.Send(toPlayer1Bytes);
        }
    }
}
