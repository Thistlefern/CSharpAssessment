using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessment
{
    class Entity
    {
        // Health
        public float health;
        public virtual float Health()
        {
            health = 100;
            return health;
        }

        // Fighting stats
        public float attackBase;
        public virtual float AttackBase()
        {
            attackBase = 10;
            return attackBase;
        }
        public float defense = 0;

        // Is the entity hostile towards the player?
        public bool hostile;
        public virtual bool Hostile()
        {
            hostile = false;
            return hostile;
        }

        // Inventory
        // TODO add inventory items/files
        public int gold;
        public virtual float Gold()
        {
            gold = 0;
            return gold;
        }
    }
}
