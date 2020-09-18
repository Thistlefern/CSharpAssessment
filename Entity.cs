using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessment
{
    class Entity
    {
        public string name;

        // Health
        public float health = 20; // TODO set to 0 after creating other monsters
        public virtual float Health()
        {
            return health;
        }

        // Fighting stats
        // Attack is listed as arrays for each subclass
        public float defense = 0;
        public virtual float Defense()
        {
            return defense;
        }

        // Gold
        public int gold;
        public virtual int Gold()
        {
            gold = 0;
            return gold;
        }
    }
}
