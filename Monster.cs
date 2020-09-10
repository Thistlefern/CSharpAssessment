using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessment
{
    class Monster : Entity
    {
        public override bool Hostile()
        {
            hostile = true;
            return hostile;
        }

    }
}
