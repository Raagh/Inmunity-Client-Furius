using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneiFurius
{
    public class Character
    {
        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public int posX;
        public int posY;
        public int maxLife;
        public int currentLife;
        public int maxMana;
        public int currentMana;
        public int faction;
        public bool isInvi = false;
        public static int cheaterPosX;
        public static int cheaterPosY;
        public static string cheaterName;
        public static int cheaterId; //public static int cheaterId;
        public static int cheaterFaction;
        public static int cheaterAsociatedFaction;
        public static int cheaterAgility;
        public static bool cheaterParalizado = false;
    }
}
