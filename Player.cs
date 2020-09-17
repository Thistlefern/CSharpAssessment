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
        public string name;

    }
}
