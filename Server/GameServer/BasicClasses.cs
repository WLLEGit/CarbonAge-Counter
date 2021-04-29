using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace GameServer
{
    class BasicClasses
    {
    }

    public enum Leaders     //领袖枚举类型
    {
        DefaultLeader
    }

    public class Leader            //领袖类
    {
        public double MilitaryBonusRate;
        public double EraBonusRate;
        public double CarbonBonusRate;

        public Leader(double miliRate, double eraRate, double carbonRate)
        {
            MilitaryBonusRate = miliRate;
            EraBonusRate = eraRate;
            CarbonBonusRate = carbonRate;
        }
        public Leader GetLeader(Leaders leader)
        {
            return leader switch
            {
                _ => new Leader(1, 1, 1),
            };
        }
    }

    public enum Cards       //卡牌枚举
    {
        DefaultCard
    }

    public class Card       //卡牌类
    {
        public double MilitaryDelta;
        public double EraDelta;
        public double CarbonDelta;

        public Card(double miliDelta, double eraDelta, double carbonDelta)
        {
            MilitaryDelta = miliDelta;
            EraDelta = eraDelta;
            CarbonDelta = carbonDelta;
        }
        public Card GetCard(Cards card)
        {
            return card switch
            {
                _ => new Card(0, 0, 0),
            };
        }
    }
    public class Command
    {
        public Player Target;
        public string CommandMsg;
    }
    public class Player     //游戏玩家类
    {
        public Socket Socket;
        public Room Room;

        public Leader Leader;
        public Card[] CardsArray;

        public double MilitaryPoints;
        public double EraPoints;
        public double CarbonPoints;
        public double DeltaMilitaryPoints;
        public double DeltaEraPoints;
        public double DeltaCarbonPoints;

        public double Health;
        public double Attack;


        public void ProcessMessage(string msg)
        {
            List<Command> commands = new List<Command>();
            string[] lines = msg.Split('\n');
            foreach (var line in lines)
            {
                string[] tokens = line.Split(" ");
                Type t = typeof(Player);
                var method = t.GetMethod(tokens[0]);        //根据tokens的第一个词查找需要调用的函数

                if(method == null)
                    Console.WriteLine("Can't find function: " + tokens[0]);
                else
                {
                    List<Command> resCommands = method.Invoke(this, tokens) as List<Command>;       //每一个函数调用完后返回客户端需要执行的command
                    foreach (var command in resCommands)
                        commands.Add(command);
                }
            }
            Room.SendCommands(commands);
        }

        private void SetCardBoard(params string[] args)
        {

        }
        private void DealDamage(params string[] args)
        {

        }
        private void Exit(params string[] args)
        {

        }
    }
}
