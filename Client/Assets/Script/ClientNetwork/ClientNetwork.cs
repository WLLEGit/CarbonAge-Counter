using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using System.Text;
using UnityEngine.UI;

public class ClientNetwork : MonoBehaviour
{
    //������
    public Button TestButton;
    public Text TestText;

    public void TestSendMsg()
    {
        string msg = "Test Message";
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(msg);
        Socket.Send(bytes);
    }

    private Socket Socket;

    // Start is called before the first frame update
    void Start()
    {
        ConnectServer("172.27.153.209", 1357);
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public bool ConnectServer(string ip, int port)
    {
        IPEndPoint point = new IPEndPoint(IPAddress.Parse(ip), port);
        Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket.Connect(point);
        if (!Socket.Connected)
            return false;
        Debug.Log("���������ӳɹ�");
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
            Debug.Log("���Է���������Ϣ��\n" + msg);
            ProcessMsg(msg);
        }
    }

    private void Send(string msg)
    {
        Socket.Send(System.Text.Encoding.UTF8.GetBytes(msg));
    }

    private void ProcessMsg(string msg)
    {
        string[] lines = msg.Split('\n');
        foreach (var line in lines)
        {
            string[] tokens = line.Split(' ');
            Type t = typeof(ClientNetwork);
            var method = t.GetMethod(tokens[0]);

            if (method == null)
                Debug.LogError("�޷������ĺ�����" + tokens[0]);
            else
                method.Invoke(this, tokens);
        }
    }
    //�ṩ�Ľӿ�
    public void ChangeCardBoard(List<string> cardNames) //��Ҹı俨�Ʋ�
    {
        string msg = "SetCardBoard ";
        foreach(string cardName in cardNames)
            msg += cardName + " ";
        msg += "\n";
        Send(msg);
    }  
    public void DealDamage()    //��ҽ��й���
    {
        Send("DealDamage\n");
    }   
    public void EndThisTurn()   //��ҽ������غ�
    {
        Send("EndThisTurn\n");
    }   
    public void EnterGame(string LeaderName)    //�����������Ϸ
    {
        Send("SetLeader " + LeaderName + "\n");
    }   
    public void Exit()  //����˳���Ϸ
    {
        Send("Exit\n");
    }          
    public void PlayerChangePoints(double deltaMiliPoints, double deltaEraPoints, double deltaCarbonPoints) //�ı����
    {
        Send("ChangePoints " + deltaMiliPoints + " " + deltaEraPoints + " " + deltaCarbonPoints + "\n");
    }

    //��Ҫ�Ľӿ�
    private void RivalChangeCardBoard(params string[] args)
    {

    }

    private void RivalDealDamage(params string[] args)
    {

    }

    private void ChangePoints(params string[] args)
    {

    }

    private void EndGame(params string[] args)
    {

    }

    private void Win(params string[] args)
    {

    }

    private void Die(params string[] args)
    {

    }

    private void Lose(params string[] args)
    {

    }

    private void StartThisTurn(params string[] args)
    {

    }

    private void RivalSetLeader(params string[] args)
    {

    }

    private void RivalChangePoints(params string[] args)
    {

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
