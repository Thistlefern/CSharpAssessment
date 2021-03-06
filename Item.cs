﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpAssessment
{
    class Item
    {
        int row;
        string type;
        string name;
        int hungerBoost;
        int thirstBoost;
        int alcohol;
        int damage;
        int handsNeeded;
        int costValue;
        int quantity;
        string description;

        public int Row { get => row; set => row = value; }
        public string Type { get => type; set => type = value; }
        public string Name { get => name; set => name = value; }
        public int HungerBoost { get => hungerBoost; set => hungerBoost = value; }
        public int ThirstBoost { get => thirstBoost; set => thirstBoost = value; }
        public int Alcohol { get => alcohol; set => alcohol = value; }
        public int Damage { get => damage; set => damage = value; }
        public int HandsNeeded { get => handsNeeded; set => handsNeeded = value; }
        public int CostValue { get => costValue; set => costValue = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string Description { get => description; set => description = value; }
    }
}
