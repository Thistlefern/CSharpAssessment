using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessment
{
    class Orc : Entity
    {
        public Orc()
        {
            name = "orc";
            health = 40;
            attackBonus = 10;
            defense = 0.25;
        }
    }
}
