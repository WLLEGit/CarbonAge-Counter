using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

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
}
