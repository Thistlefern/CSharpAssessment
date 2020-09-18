using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessment
{
    class Player : Entity
    {
        // Basic needs
        // TODO consider drunkenness?
        public int hunger = 0; // increases by 1 each hour, hungry at 8, starving at 16 (penalty to stats), death at 36
        public int thirst = 0; // increases by 1 each hour, thirsty at 8, parched at 16 (penalty to stats), death at 36
        public int fatigue = 0; // increases by 1 each hour, tired at 16, exhausted at 24 (penalty to stats)
        public override float Health()
        {
            health = 50;
            return health;
        }

        public int[] Attack = { 0, 1, 2, 3, 4, 5 };

        public override int Gold()
        {
            gold = 25;
            return gold;
        }
    }
}
