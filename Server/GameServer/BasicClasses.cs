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
    public enum SpecialEvents
    {
        DefaultSpecialEvent = -1,
        BingDong = 100,
        DaHaiXiao = 400,
        DaMianJiTingDian = 600,
        FengBaoChao = 800,
        YuanGuBingDu = 900
    }

    public enum Leaders     //领袖枚举类型
    {
        DefaultLeader,
        Bear,
        Hawk
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
                Leaders.Bear => new Leader(leader, 1.1, 0.9, 1),
                Leaders.Hawk => new Leader(leader, 0.9, 1.1, 1),
                _ => new Leader(leader, 1, 1, 1),
            };
        }
    }

    public enum Cards       //卡牌枚举
    {
        DefaultCard,
        DaoGengHuoZhong,
        TanBuJi,
        BaoHuDiQiu,
        YuanZiNeng,
        XinNengYuan,
        ZhiShuZaoLin,
        DianLi,
        XuMuYe,
        KuangChanKaiCai,
        JieYueLiYong,
        ZhengQiDongLi,
        ZiYuanXunHuan
    }

    public class Card       //卡牌类
    {
        public double MilitaryDelta;
        public double EraDelta;
        public double CarbonDelta;
        public double CarbonRate;

        public Card(double miliDelta, double eraDelta, double carbonDelta, double carbonRate=1)
        {
            MilitaryDelta = miliDelta;
            EraDelta = eraDelta;
            CarbonDelta = carbonDelta;
            CarbonRate = carbonRate;
        }
        public static Card GetCard(Cards card)
        {
            return card switch
            {
                Cards.DefaultCard => new Card(0, 0, 0),
                Cards.DaoGengHuoZhong => new Card(8, 20, 25),
                Cards.TanBuJi => new Card(-20, 20, -100),
                Cards.BaoHuDiQiu => new Card(0, 50, -50),
                Cards.YuanZiNeng => new Card(90, 30, 60),
                Cards.XinNengYuan => new Card(20, 35, -10),
                Cards.ZhiShuZaoLin => new Card(0, 20, -40),
                Cards.DianLi => new Card(60, 20, 60),
                Cards.XuMuYe => new Card(6, 10, 8),
                Cards.KuangChanKaiCai => new Card(15, 15, 25),
                Cards.JieYueLiYong => new Card(0, 20, 0),
                Cards.ZhengQiDongLi => new Card(20, 40, 40),
                Cards.ZiYuanXunHuan => new Card(0, 30, 0, 0.5),
                _ => throw new NotImplementedException(),
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
        public List<Card> Cards = new List<Card>();

        public double score=0;
        public double MilitaryPoints;
        public double EraPoints;
        public double CarbonPoints;
        public double DeltaMilitaryPoints;
        public double DeltaEraPoints;
        public double DeltaCarbonPoints;
        public double CardCarbonRate;

        public double Health = 100;
        public double Attack = 0;

        public string Msg;

        private  bool isTurnEnd;
        private SpecialEvents preEvent = SpecialEvents.DefaultSpecialEvent;

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
        public List<Command> EvaluatePointsChange()
        {
            List<Command> commands = new List<Command>();
            MilitaryPoints += DeltaMilitaryPoints * Leader.MilitaryBonusRate;
            CarbonPoints += DeltaCarbonPoints * Leader.CarbonBonusRate * CardCarbonRate;
            EraPoints += DeltaEraPoints * Leader.EraBonusRate;
            score = MilitaryPoints + EraPoints * 2 + CarbonPoints * 3;
            commands.Add(new Command(this, String.Format("ChangePoints {0} {1} {2}", MilitaryPoints, EraPoints, CarbonPoints)));
            commands.Add(new Command(Room.AnotherPlayer(this), String.Format("RivalChangePoints {0} {1} {2}", MilitaryPoints, EraPoints, CarbonPoints)));
            return commands;
        }
        public SpecialEvents GenerateSpecialEvent()
        {
            if (DeltaCarbonPoints <= 0)
                return SpecialEvents.DefaultSpecialEvent;
            foreach(SpecialEvents specialEvents in Enum.GetValues(typeof(SpecialEvents)))
            {
                if(specialEvents > preEvent && (int)specialEvents < CarbonPoints)
                {
                    preEvent = specialEvents;
                    return specialEvents;
                }
            }
            return SpecialEvents.DefaultSpecialEvent;
        }
        private List<Command> EvaluateCardsProperty()
        {
            List<Command> commands = new List<Command>();
            DeltaCarbonPoints = DeltaEraPoints = DeltaMilitaryPoints = 0;
            CardCarbonRate = 1;
            foreach(var card in Cards)
            {
                DeltaCarbonPoints += card.CarbonDelta;
                DeltaEraPoints += card.EraDelta;
                DeltaMilitaryPoints += card.MilitaryDelta;
                CardCarbonRate *= card.CarbonRate;
            }
            commands.Add(new Command(this, String.Format("ChangeDeltaPoints {0} {1} {2}", DeltaMilitaryPoints, DeltaEraPoints, DeltaCarbonPoints)));
            commands.Add(new Command(Room.AnotherPlayer(this), String.Format("RivalChangeDeltaPoints {0} {1} {2}", DeltaMilitaryPoints, DeltaEraPoints, DeltaCarbonPoints)));
            return commands;
        }

        public void ProcessMessage(string msg)
        {
            List<Command> commands = new List<Command>();
            string[] lines = msg.Split('\n');
            lines = lines.Where(s => !string.IsNullOrEmpty(s)).ToArray();       //删除空行
            foreach (var line in lines)
            {
                string[] tokens = line.Split(" ");
                tokens = tokens.Where(s => !string.IsNullOrEmpty(s)).ToArray();
                Type t = typeof(Player);
                var method = t.GetMethod(tokens[0]);        //根据tokens的第一个词查找需要调用的函数

                if (method == null)
                    Console.WriteLine("Can't find function: " + tokens[0]);
                else
                {
                    List<Command> resCommands = method.Invoke(this, new object[] { tokens}) as List<Command>;       //每一个函数调用完后返回客户端需要执行的command
                    foreach (var command in resCommands)
                        commands.Add(command);
                }
            }
            Room.SendCommands(commands);
        }

        public List<Command> SetCardBoard(object o)
        {
            string[] args = o as string[];
            List<Command> commands = new List<Command>();
            Cards.Clear();
            foreach (string cardName in args)
            {
                if (cardName == "SetCardBoard")
                    continue;
                Cards.Add(Card.GetCard((Cards)System.Enum.Parse(typeof(Cards), cardName)));
            }
            var returnedCommands = EvaluateCardsProperty();
            foreach (var cmd in returnedCommands)
                commands.Add(cmd);
            return commands;
        }
        public List<Command> DealDamage(object o)
        {
            List<Command> commands = new List<Command>
            {
                new Command() { Target = Room.AnotherPlayer(this), CommandMsg = "RivalDealDamage " + Attack }
            };
            Room.AnotherPlayer(this).Health -= Attack;
            if (Room.AnotherPlayer(this).Health <= 0)
                commands.Add(new Command(Room.AnotherPlayer(this), "Die"));
            return commands;
        }
        public void StartThisTurn()
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
        public List<Command> EndThisTurn(object o)
        {
            isTurnEnd = true;
            return new List<Command>();
        }
        public List<Command> SetLeader(object o)
        {
            string[] args = o as string[];
            Leader = Leader.GetLeader((Leaders)System.Enum.Parse(typeof(Leaders), args[1]));
            List<Command> commands = new List<Command>
            {
                new Command() { Target = Room.AnotherPlayer(this), CommandMsg = "RivalSetLeader " + Leader.Type }
            };
            return commands;
        }
        public List<Command> Exit(object o)
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
        public List<Command> ChangePoints(object o)
        {
            string[] args = o as string[];
            MilitaryPoints += double.Parse(args[1]);
            EraPoints += double.Parse(args[2]);
            CarbonPoints += double.Parse(args[3]);
            List<Command> commands = new List<Command>
            {
                new Command { Target = Room.AnotherPlayer(this), CommandMsg = "ChangePoints " + args[1] + " " + args[2] + " " + args[3] }
            };
            return commands;
        }
    }
}
