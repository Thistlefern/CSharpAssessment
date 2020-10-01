using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Text;

namespace CSharpAssessment
{
    class Entity
    {
        public string name;
        // Health
        public double health;

        // Fighting stats
        public int attack;
        public int attackBonus;
        static int[] AttackArray = { 0, 1, 2, 3, 4, 5 };

        // initiate random damage amounts
        public virtual void Attack()
        {
            Random attRand = new Random();
            attack = AttackArray[attRand.Next(6)] + attackBonus;
        }

        public Entity()
        {
            name = "stranger";
            health = 20;
        }

    }
}
