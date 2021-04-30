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
        public Leaders Type;
        public double MilitaryBonusRate;
        public double EraBonusRate;
        public double CarbonBonusRate;

        public Leader(Leaders type, double miliRate, double eraRate, double carbonRate)
        {
            Type = type;
            MilitaryBonusRate = miliRate;
            EraBonusRate = eraRate;
            CarbonBonusRate = carbonRate;
        }
        static public Leader GetLeader(Leaders leader)
        {
            return leader switch
            {
                _ => new Leader(leader, 1, 1, 1),
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
        public static Card GetCard(Cards card)
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

        public Command() { }
        public Command(Player p, string msg) { Target = p; CommandMsg = msg; }
    }
    public class Player     //游戏玩家类
    {
        public Socket Socket;
        public Room Room;

        public Leader Leader;
        public List<Card> Cards;

        public double score;
        public double MilitaryPoints;
        public double EraPoints;
        public double CarbonPoints;
        public double DeltaMilitaryPoints;
        public double DeltaEraPoints;
        public double DeltaCarbonPoints;

        public double Health = 100;
        public double Attack = 0;

        public string Msg;

        private bool isTurnEnd;

        public void StartTurn()
        {
            isTurnEnd = false;
            StartThisTurn();

            while (!isTurnEnd)
            {
                if (Msg.Length != 0)
                {
                    ProcessMessage(Msg);
                    Msg = "";
                }
            }
        }

        public void ProcessMessage(string msg)
        {
            List<Command> commands = new List<Command>();
            string[] lines = msg.Split('\n');
            foreach (var line in lines)
            {
                string[] tokens = line.Split(" ");
                Type t = typeof(Player);
                var method = t.GetMethod(tokens[0]);        //根据tokens的第一个词查找需要调用的函数

                if (method == null)
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

        private List<Command> SetCardBoard(params string[] args)
        {
            List<Command> commands = new List<Command>();
            Cards.Clear();
            string cmdMsg = new string("RivalSetCardBoard ");
            foreach (string cardName in args)
            {
                if (cardName == "SetCardBoard")
                    continue;
                cmdMsg += cardName + " ";
                Cards.Add(Card.GetCard((Cards)System.Enum.Parse(typeof(Cards), cardName)));
            }
            commands.Add(new Command() { Target = Room.AnotherPlayer(this), CommandMsg = cmdMsg });
            return commands;
        }
        private List<Command> DealDamage(params string[] args)
        {
            List<Command> commands = new List<Command>
            {
                new Command() { Target = Room.AnotherPlayer(this), CommandMsg = "RivalDealDamage " + Attack }
            };
            Room.AnotherPlayer(this).Health -= Attack;
            return commands;
        }
        private void StartThisTurn()
        {
            List<Command> commands = new List<Command>();
            if (Health > 0)
            {
                commands.Add(new Command { Target = this, CommandMsg = "StartThisTurn" });
            }
            else
            {
                commands.Add(new Command { Target = this, CommandMsg = "Die" });
                commands.Add(new Command { Target = Room.AnotherPlayer(this), CommandMsg = "Win" });
            }
            Room.SendCommands(commands);
        }
        private List<Command> EndThisTurn(params string[] args)
        {
            isTurnEnd = true;
            return new List<Command>();
        }
        private List<Command> SetLeader(params string[] args)
        {
            Leader = Leader.GetLeader((Leaders)System.Enum.Parse(typeof(Leaders), args[1]));
            List<Command> commands = new List<Command>
            {
                new Command() { Target = Room.AnotherPlayer(this), CommandMsg = "RivalSetLeader " + Leader.Type }
            };
            return commands;
        }
        public List<Command> Exit(params string[] args)
        {
            List<Command> commands = new List<Command>();
            Player anotherPlayer = Room.AnotherPlayer(this);
            commands.Add(new Command(anotherPlayer, "Win"));
            Room.SendCommands(commands);
            anotherPlayer.Socket.Shutdown(SocketShutdown.Both);
            anotherPlayer.Socket.Close();
            Console.WriteLine("{0}退出", Socket.RemoteEndPoint);
            Environment.Exit(0);
            return new List<Command>();
        }
        public List<Command> ChangePoints(params string[] args)
        {
            MilitaryPoints += double.Parse(args[1]);
            EraPoints += double.Parse(args[2]);
            CarbonPoints += double.Parse(args[3]);
            List<Command> commands = new List<Command>();
            commands.Add(new Command { Target = Room.AnotherPlayer(this), CommandMsg = "ChangePoints " + args[1] + " " + args[2] + " " + args[3] });
            return commands;
        }
    }
}
