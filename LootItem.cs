using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessment
{
    class LootItem : Item
    {
        int banditMax;
        int orcMax;
        int skeletonMax;
        int wolfMax;

        public int BanditMax { get => banditMax; set => banditMax = value; }
        public int OrcMax { get => orcMax; set => orcMax = value; }
        public int SkeletonMax { get => skeletonMax; set => skeletonMax = value; }
        public int WolfMax { get => wolfMax; set => wolfMax = value; }
    }
}
