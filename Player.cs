using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessment
{
    class Player : Entity
    {
        // Basic needs
        public int hunger = 0; // increases by 1 each hour, hungry at 8, starving at 16 (penalty to stats), death at 36
        public int thirst = 0; // increases by 1 each hour, thirsty at 8, parched at 16 (penalty to stats), death at 36
        public int fatigue = 0; // increases by 1 each hour, tired at 16, exhausted at 24 (penalty to stats), pass out at 48
        public double drunkenness = 0; // increases when alcohol is consumed, and gradually decreases over time. Sober at 0, buzzed at 1, drunk at 4 (penalty to stats), blackout at 10 (unconciousness)
        public override float Health()
        {
            health = 50;
            return health;
        }

        public int[] Attack = { 0, 1, 2, 3, 4, 5 };
    }
}
