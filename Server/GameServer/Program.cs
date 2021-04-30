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
            Room room = new Room();
            if (!room.Start())
            {
                Console.WriteLine("Room establish failed, exit!");
                Environment.Exit(0);
            }

        }
    }

    public class TurnManager
    {

    }
}
