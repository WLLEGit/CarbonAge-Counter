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

        private readonly int criticalCarbonPoints = 100;

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
            List<Command> commands = new List<Command>();
            var resCommands = Room.Players[0].EvaluatePointsChange();
            foreach (var command in resCommands)
                commands.Add(command);
            resCommands = Room.Players[1].EvaluatePointsChange();
            foreach (var command in resCommands)
                commands.Add(command);

            if(Room.Players[0].CarbonPoints > Room.Players[1].CarbonPoints)
            {
                if (Room.Players[0].CarbonPoints >= criticalCarbonPoints)
                {
                    commands.Add(new Command(Room.Players[0], "Lose"));
                    commands.Add(new Command(Room.Players[1], "Win"));
                }
            }
            else if(Room.Players[0].CarbonPoints == Room.Players[1].CarbonPoints)
            {
                if (Room.Players[0].CarbonPoints >= criticalCarbonPoints)
                {
                    commands.Add(new Command(Room.Players[0], "Lose"));
                    commands.Add(new Command(Room.Players[1], "Lose"));
                }
            }
            else
                if (Room.Players[1].CarbonPoints >= criticalCarbonPoints)
                {
                    commands.Add(new Command(Room.Players[1], "Lose"));
                    commands.Add(new Command(Room.Players[0], "Win"));
                }

            var tmpEvent = Room.Players[0].GenerateSpecialEvent();
            if (tmpEvent != SpecialEvents.DefaultSpecialEvent)
                commands.Add(new Command(Room.Players[0], "StartSpecialEvent " + tmpEvent.ToString()));
            tmpEvent = Room.Players[1].GenerateSpecialEvent();
            if (tmpEvent != SpecialEvents.DefaultSpecialEvent)
                commands.Add(new Command(Room.Players[1], "StartSpecialEvent " + tmpEvent.ToString()));

            Room.SendCommands(commands);
        }
        private void TransitionTurn()   
        {
            List<Command> commands = new List<Command>();
            commands.Add(new Command(Room.Players[0], "TransitionTurn"));
            commands.Add(new Command(Room.Players[1], "TransitionTurn"));
            Room.SendCommands(commands);
        }
        private void EndGame()
        {
            List<Command> commands = new List<Command>();
            if (Room.Players[0].score > Room.Players[1].score)
            {
                commands.Add(new Command(Room.Players[1], "Lose"));
                commands.Add(new Command(Room.Players[0], "Win"));
            }
            else if (Room.Players[0].score == Room.Players[1].score)
            {
                commands.Add(new Command(Room.Players[0], "Lose"));
                commands.Add(new Command(Room.Players[1], "Lose"));
            }
            else
            {
                commands.Add(new Command(Room.Players[0], "Lose"));
                commands.Add(new Command(Room.Players[1], "Win"));
            }
        }
    }
}
