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
        public int maxHealth = 50;
        public int fightsWon = 0;
        public int levelsGained = 0;
        public Player()
        {
            // overriding health
            health = 50;
    }
    }
}
