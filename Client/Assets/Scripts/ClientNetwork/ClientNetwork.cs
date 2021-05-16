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
    public static ClientNetwork ClientNetworkInstance;
    private Socket Socket;

    private void Awake()
    {
        ClientNetworkInstance = this;
        Debug.Log("ClientNetwork Loaded");
    }

    public bool ConnectServer(string ip, int port)
    {
        IPEndPoint point = new IPEndPoint(IPAddress.Parse(ip), port);
        Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket.Connect(point);
        if (!Socket.Connected)
            return false;
        Debug.Log("Sever Connected Successfully!");
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
            if (msg.Length == 0)        //Ignore Empty Message
                continue;
            Debug.Log("Receive Message From Sever: \n" + msg);
            ProcessMsg(msg);
        }
    }

    private void Send(string msg)
    {
        Debug.Log("Send: " + msg);
        Socket.Send(System.Text.Encoding.UTF8.GetBytes(msg));
    }

    private void ProcessMsg(string msg)
    {
        string[] lines = msg.Split('\n');
        lines = lines.Where(s => !string.IsNullOrEmpty(s)).ToArray();       //Remove Empty Line
        foreach (var line in lines)
        {
            string[] tokens = line.Split(' ');
            Type t = typeof(ClientNetwork);
            var method = t.GetMethod(tokens[0]);

            if (method == null)
                Debug.LogError("Can't Resolve Fuction: " + tokens[0]);
            else
                method.Invoke(this, new object[] { tokens});
        }
    }
    
    //Offerred Interface
    public void ChangeCardBoard(List<string> cardNames) //Player Change Cards
    {
        string msg = "SetCardBoard ";
        foreach(string cardName in cardNames)
            msg += cardName + " ";
        msg += "\n";
        Send(msg);
    }  
    public void DealDamage()    //Player Deal Damage
    {
        Send("DealDamage\n");
    }   
    public void EndThisTurn()   //Player End Turn
    {
        Send("EndThisTurn\n");
    }   
    public void EnterGame(string LeaderName)    //Begin Game from Menu
    {
        Send("SetLeader " + LeaderName + "\n");
    }
    public void Exit()  //Player Quit Game
    {
        Send("Exit\n");
    }          
    public void PlayerChangePoints(double deltaMiliPoints, double deltaEraPoints, double deltaCarbonPoints) //Change Score
    {
        Send("ChangePoints " + deltaMiliPoints + " " + deltaEraPoints + " " + deltaCarbonPoints + "\n");
    }

    //Call Funcs in Other Scenes

    public void RivalDealDamage(object o)
    {
        int attack = int.Parse(((string[])o)[1]);
        PlayerManager.PlayerManagerInstance.Player1BeenAttacked(attack);
    }

    public void ChangePoints(object o)
    {
        string[] strs = (string[])o;
        PlayerManager.PlayerManagerInstance.Player1SetPoints(int.Parse(strs[2]), int.Parse(strs[1]), int.Parse(strs[0]));
    }
    public void RivalChangePoints(object o)
    {
        string[] strs = (string[])o;
        PlayerManager.PlayerManagerInstance.Player2SetPoints(int.Parse(strs[2]), int.Parse(strs[1]), int.Parse(strs[0]));
    }

    public void Win(object o)
    {
        battle.BattleInstance.Win();
    }

    public void Die(object o)
    {
        battle.BattleInstance.Die();
    }

    public void Lose(object o)
    {
        battle.BattleInstance.Lose();
    }

    public void StartThisTurn(object o)
    {
        battle.BattleInstance.StartTurn();
    }

    public void RivalSetLeader(object o)
    {
        Enum.TryParse<Leaders>(((string[])o)[1], out Leaders leaderType);
        PlayerManager.PlayerManagerInstance.Player2SetLeader(leaderType);
    }

    public void TransitionTurn(object o)
    {
        battle.BattleInstance.StartTransitionGame();
    }
}

public enum Leaders     //Leaders For Players to Choose
{
    DefaultLeader,
    Bear,       
    Hawk        
}

public enum Cards       //All Cards in Game
{
    DefaultCard,
    DaoGengHuoZhong,
    TanBuJi,
    BaoHuDiQiu,
    XuMuYe,
    DianLi,
    ZhiShuZaoLin,
    YuanZiNeng,
    XinNengYuan,
    KuangChanKaiCai,
    JieYueLiYong,
    ZhengQiDongLi,
    ZiYuanXunHuan
}
