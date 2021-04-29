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
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Room
    {
        public Socket SeverSocket;
        public Player[] Players = new Player[2];

        private bool WaitPlayer()
        {
            int remainPlayer = 2;
            Socket client;

            try
            {
                while (remainPlayer != 0)
                {
                    client = SeverSocket.Accept();                  //等待用户连接
                    string endPoint = client.RemoteEndPoint.ToString();

                    Players[remainPlayer - 1].Socket = client;
                    Console.WriteLine($"玩家{remainPlayer}（{endPoint}）连接成功");

                    Thread listener = new Thread(Listen);           //每一个Player建一个线程
                    listener.Start();
                    
                    remainPlayer--;
                }
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        private void Listen(object obj)
        {

        }
    }
}
