using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessment
{
    struct Item
    {
        string type;
        string name;
        int hungerBoost;
        int thirstBoost;
        string weaponType;
        int damage;
        int handsNeeded;
        string rarity;
        int cost;
        int quantity;
        // TODO add more types of items here and to the inventory
        // TODO adjust costs for god's sake

        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }
        public int HungerBoost { get => hungerBoost; set => hungerBoost = value; }
        public int ThirstBoost { get => thirstBoost; set => thirstBoost = value; }
        public string WeaponType { get => weaponType; set => weaponType = value; }
        public int Damage { get => damage; set => damage = value; }
        public int HandsNeeded { get => handsNeeded; set => handsNeeded = value; }
        public string Rarity { get => rarity; set => rarity = value; }
        public int Cost { get => cost; set => cost = value; }
        public int Quantity { get => quantity; set => quantity = value; }

    }
}
