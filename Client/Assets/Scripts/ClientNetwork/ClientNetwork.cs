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

    // Start is called before the first frame update
    void Start()
    {
        ClientNetworkInstance = this;
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

    private void Listen()         //�������Է���������Ϣ
    {
        byte[] buffer = new byte[1000];
        while (true)
        {
            int len = Socket.Receive(buffer);
            var msg = Encoding.UTF8.GetString(buffer, 0, len);
            if (msg.Length == 0)        //���Կ���Ϣ
                continue;
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
        lines = lines.Where(s => !string.IsNullOrEmpty(s)).ToArray();       //ɾ������
        foreach (var line in lines)
        {
            string[] tokens = line.Split(' ');
            Type t = typeof(ClientNetwork);
            var method = t.GetMethod(tokens[0]);

            if (method == null)
                Debug.LogError("�޷������ĺ�����" + tokens[0]);
            else
                method.Invoke(this, new object[] { tokens});
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
        Battle.BattleInstance.Win();
    }

    public void Die(object o)
    {
        Battle.BattleInstance.Die();
    }

    public void Lose(object o)
    {
        Battle.BattleInstance.Lose();
    }

    public void StartThisTurn(object o)
    {
        Battle.BattleInstance.StartTurn();
    }

    public void RivalSetLeader(object o)
    {
        Enum.TryParse<Leaders>(((string[])o)[1], out Leaders leaderType);
        PlayerManager.PlayerManagerInstance.Player2SetLeader(leaderType);
    }

    public void TransitionTurn(object o)
    {
        Battle.BattleInstance.StartTransitionGame();
    }
}

public enum Leaders     //����ö������
{
    DefaultLeader,
    Bear,       //ë��
    Hawk        //ӥ��
}

public enum Cards       //����ö��
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
