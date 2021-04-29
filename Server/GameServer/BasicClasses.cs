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
            switch (leader)
            {
                default:
                    return new Leader(1, 1, 1);
            }
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
            switch (card)
            {
                default:
                    return new Card(0, 0, 0);
            }
        }
    }

    public class Player     //游戏玩家类
    {
        public Socket Socket;
        public Leader Leader;
        public Card[] CardsArray;
        public double MilitaryPoints;
        public double EraPoints;
        public double CarbonPoints;
        public double Health;
    }
}
