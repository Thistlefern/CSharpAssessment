using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessment
{
    struct Food
    {
        string type;
        string name;
        int hungerBoost;
        int thirstBoost;
        int cost;
        int quantity;

        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }
        public int HungerBoost { get => hungerBoost; set => hungerBoost = value; }
        public int ThirstBoost { get => thirstBoost; set => thirstBoost = value; }
        public int Cost { get => cost; set => cost = value; }
        public int Quantity { get => quantity; set => quantity = value; }

    }
}
