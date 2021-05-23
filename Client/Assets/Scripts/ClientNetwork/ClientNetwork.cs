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
    [HideInInspector]

    public TimeClasses CurrentTime;

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
        Debug.Log(lines.Length);
        foreach (var line in lines)
        {
            Debug.Log("Start Process:" + line);
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
        Debug.Log("ChangePoints Called");
        string[] strs = (string[])o;
        PlayerManager.PlayerManagerInstance.Actions.Add(PlayerManager.PlayerManagerInstance.Player1SetPoints);
        PlayerManager.PlayerManagerInstance.arg1.Add(double.Parse(strs[2]));
        PlayerManager.PlayerManagerInstance.arg2.Add(double.Parse(strs[1]));
        PlayerManager.PlayerManagerInstance.arg3.Add(double.Parse(strs[3]));
    }
    public void RivalChangePoints(object o)
    {
        string[] strs = (string[])o;
        PlayerManager.PlayerManagerInstance.Actions.Add(PlayerManager.PlayerManagerInstance.Player2SetPoints);
        PlayerManager.PlayerManagerInstance.arg1.Add(double.Parse(strs[2]));
        PlayerManager.PlayerManagerInstance.arg2.Add(double.Parse(strs[1]));
        PlayerManager.PlayerManagerInstance.arg3.Add(double.Parse(strs[3]));
    }

    public void ChangeDeltaPoints(object o)
    {
        Debug.Log("ChangeDeltaPoints Called");
        string[] strs = (string[])o;
        PlayerManager.PlayerManagerInstance.Actions.Add(PlayerManager.PlayerManagerInstance.Player1SetDeltaPoints);
        PlayerManager.PlayerManagerInstance.arg1.Add(double.Parse(strs[2]));
        PlayerManager.PlayerManagerInstance.arg2.Add(double.Parse(strs[1]));
        PlayerManager.PlayerManagerInstance.arg3.Add(double.Parse(strs[3]));
    }
    public void RivalChangeDeltaPoints(object o)
    {
        string[] strs = (string[])o;
        foreach (var str in strs)
            Debug.Log(str);
        PlayerManager.PlayerManagerInstance.Actions.Add(PlayerManager.PlayerManagerInstance.Player2SetDeltaPoints);
        PlayerManager.PlayerManagerInstance.arg1.Add(double.Parse(strs[2]));
        PlayerManager.PlayerManagerInstance.arg2.Add(double.Parse(strs[1]));
        PlayerManager.PlayerManagerInstance.arg3.Add(double.Parse(strs[3]));
    }
    public void SetEra(object o)
    {
        string[] strs=(string[])o;
        if(strs[1]=="Time1")
        {
            CurrentTime=TimeClasses.Time1;
        }
        else if(strs[1]=="Time2")
        {
            CurrentTime=TimeClasses.Time2;
        }
        else if(strs[1]=="Time3")
        {
            CurrentTime=TimeClasses.Time3;
        }
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

    public void StartSpecialEvent(object o)
    {
        Enum.TryParse<SpecialEvents>(((string[])o)[1], out SpecialEvents specialEvents);
        SpeicalEventManager.SpeicalEventManagerInstance.ActivateSpecialEvent(specialEvents);
    }
}

public enum Leaders     //Leaders For Players to Choose
{
    DefaultLeader,
    Bear,       
    Hawk        
}
public enum SpecialEvents
{
    DefaultSpecialEvent = -1,
    BingDong = 100,
    DaHaiXiao = 400,
    DaMianJiTingDian = 600,
    FengBaoChao = 800,
    YuanGuBingDu = 900
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
