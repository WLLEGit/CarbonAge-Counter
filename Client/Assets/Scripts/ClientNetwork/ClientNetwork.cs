using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using System.Linq;

public class ClientNetwork : MonoBehaviour
{
    //测试用
    public Button TestButton;
    public Text TestText;

    public void TestSendMsg()
    {
        string msg = "Test Message";
        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(msg);
        Socket.Send(bytes);
    }

    public static ClientNetwork ClientNetworkInstance;
    private Socket Socket;

    // Start is called before the first frame update
    void Start()
    {
        ClientNetworkInstance = this;
        ConnectServer("172.27.145.234", 1357);
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
        Debug.Log("服务器连接成功");
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
            Debug.Log("来自服务器的信息：\n" + msg);
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
        lines = lines.Where(s => !string.IsNullOrEmpty(s)).ToArray();       //删除空行
        foreach (var line in lines)
        {
            string[] tokens = line.Split(' ');
            Type t = typeof(ClientNetwork);
            var method = t.GetMethod(tokens[0]);

            if (method == null)
                Debug.LogError("无法解析的函数：" + tokens[0]);
            else
                method.Invoke(this, new object[] { tokens});
        }
    }
    //提供的接口
    public void ChangeCardBoard(List<string> cardNames) //玩家改变卡牌槽
    {
        string msg = "SetCardBoard ";
        foreach(string cardName in cardNames)
            msg += cardName + " ";
        msg += "\n";
        Send(msg);
    }  
    public void DealDamage()    //玩家进行攻击
    {
        Send("DealDamage\n");
    }   
    public void EndThisTurn()   //玩家结束本回合
    {
        Send("EndThisTurn\n");
    }   
    public void EnterGame(string LeaderName)    //主界面进入游戏
    {
        Send("SetLeader " + LeaderName + "\n");
    }
    public void Exit()  //玩家退出游戏
    {
        Send("Exit\n");
    }          
    public void PlayerChangePoints(double deltaMiliPoints, double deltaEraPoints, double deltaCarbonPoints) //改变点数
    {
        Send("ChangePoints " + deltaMiliPoints + " " + deltaEraPoints + " " + deltaCarbonPoints + "\n");
    }

    //需要的接口
    private void RivalChangeCardBoard(object o)
    {

    }

    private void RivalDealDamage(object o)
    {

    }

    private void ChangePoints(object o)
    {

    }

    private void EndGame(object o)
    {

    }

    private void Win(object o)
    {

    }

    private void Die(object o)
    {

    }

    private void Lose(object o)
    {

    }

    private void StartThisTurn(object o)
    {

    }

    private void RivalSetLeader(object o)
    {

    }

    private void RivalChangePoints(object o)
    {

    }
}

public enum Leaders     //领袖枚举类型
{
    DefaultLeader,
    Bear,       //毛熊
    Hawk        //鹰酱
}

public enum Cards       //卡牌枚举
{
    DefaultCard,
    DaoGengHuoZhong,
    TanBuJi
}
