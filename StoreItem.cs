using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessment
{
    class StoreItem : Item
    {
        string willBuy;
        int genStoreMax;
        string genStoreWillBuy;
        int foodVendorMax;
        string foodVendorWillBuy;
        int armorerMax;
        string armorerWillBuy;
        int adventurerMax;
        string adventurerWillBuy;

        public string WillBuy { get => willBuy; set => willBuy = value; }
        public int GenStoreMax { get => genStoreMax; set => genStoreMax = value; }
        public string GenStoreWillBuy { get => genStoreWillBuy; set => genStoreWillBuy = value; }
        public int FoodVendorMax { get => foodVendorMax; set => foodVendorMax = value; }
        public string FoodVendorWillBuy { get => foodVendorWillBuy; set => foodVendorWillBuy = value; }
        public int ArmorerMax { get => armorerMax; set => armorerMax = value; }
        public string ArmorerWillBuy { get => armorerWillBuy; set => armorerWillBuy = value; }
        public int AdventurerMax { get => adventurerMax; set => adventurerMax = value; }
        public string AdventurerWillBuy { get => adventurerWillBuy; set => adventurerWillBuy = value; }
    }
}
