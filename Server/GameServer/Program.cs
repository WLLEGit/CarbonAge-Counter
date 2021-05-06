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
            TurnManager turnManager = new TurnManager();
            turnManager.StartGame(room);
        }
    }

    public enum Era { Ancient, Modern, Contemporary };
    public class TurnManager
    {
        const int TURNSEACHERA = 5;
        public Player CurPlayer;
        public Era CurEra = Era.Ancient;
        public int TurnCount = 0;
        public Room Room;

        public void StartGame(Room room)
        {
            Room = room;
            CurPlayer = Room.Players[0];
            while (Room.Players[0].Msg.Length == 0 || Room.Players[1].Msg.Length == 0) ;  //等待两个玩家初始化信息
            Room.Players[0].ProcessMessage(Room.Players[0].Msg);
            Room.Players[1].ProcessMessage(Room.Players[1].Msg);
            for(CurEra = Era.Ancient; CurEra <= Era.Contemporary; ++CurEra)
            {
                for (TurnCount = 0; TurnCount < TURNSEACHERA; ++TurnCount)
                {
                    CurPlayer.StartTurn();
                    CurPlayer = Room.AnotherPlayer(CurPlayer);
                    CurPlayer.StartTurn();
                    EndTurn();
                }
                if(CurEra != Era.Contemporary)
                    TransitionTurn();
            }
            EndGame();
        }
        private void EndTurn()  //结算本回合的点数，启用卡牌，判断特殊事件
        {

        }
        private void TransitionTurn()   
        {

        }
        private void EndGame()
        {

        }
    }
}
