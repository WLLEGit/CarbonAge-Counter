using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using System.Text;

public class ClientNetwork : MonoBehaviour
{
    private Network Network;
    // Start is called before the first frame update
    void Start()
    {
        Network = new Network();
        Network.ConnectServer("172.27.153.209", 1357);
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void TestSendMsg()
    {
        string msg = "Test Message";
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(msg);
        Network.Socket.Send(bytes);
    }
}

public enum Leaders     //����ö������
{
    DefaultLeader
}

public enum Cards       //����ö��
{
    DefaultCard
}

public class Network
{
    public Socket Socket;

    public bool ConnectServer(string ip, int port)
    {
        IPEndPoint point = new IPEndPoint(IPAddress.Parse(ip), port);
        Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket.Connect(point);
        if (!Socket.Connected)
            return false;
        Console.WriteLine("���������ӳɹ�");
        Thread listener = new Thread(Listen);
        listener.Start();
        return true;
    }
    
    private void Listen()
    {
        byte[] buffer = new byte[1000];
        while (true)
        {
            int len = Socket.Receive(buffer);
            var msg = Encoding.UTF8.GetString(buffer, 0, len);
            Console.WriteLine("���յ�����{0}����Ϣ��\n{1}", Socket.RemoteEndPoint, msg);
        }
    }
    //�ṩ�Ľӿ�
    public void PlayerChangeCardBoard( )
    {

    }

    public void PlayerDealDamage()
    {

    }

    public void Exit()
    {

    }
    
    //��Ҫ�Ľӿ�
    private void RivalChangeCardBoard(params string[] args)
    {

    }

    private void RivalDealDamage(params string[] args)
    {

    }

    private void RivalExit(params string[] args)
    {

    }

    private void ChangePoints(params string[] args)
    {

    }

    private void EndGame(params string[] args)
    {

    }
}