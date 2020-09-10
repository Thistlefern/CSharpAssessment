using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessment
{
    class Player : Entity
    {
        // Basic needs
        public int hunger = 0;
        public int thirst = 0;
        public int fatigue = 0;
        public string name = "stranger";

        // Inventory
        public override float Gold()
        {
            gold = 10;
            return gold;
        }


    }
}
