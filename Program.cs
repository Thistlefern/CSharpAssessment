using System;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Runtime.CompilerServices;

namespace CSharpAssessment
{
    class Program
    {
        static void Main()
        {
            // Introduction text
            Console.WriteLine("Welcome, adventurer! What should I call ya?");
            string nameInput = Console.ReadLine();

            // Player creation
            Player player = new Player();
            player.gold = 25;
            // Player inventory creation
            Item[] myInventory;
            using (var reader = new StreamReader("inventory.csv"))
            {
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                System.Collections.Generic.IEnumerable<Item> records = csv.GetRecords<Item>();
                myInventory = records.ToArray<Item>();
            }
            player.name = nameInput;
            Console.WriteLine($"So you're {player.name}? Nice to meetcha! I'm Humphrey, the leader of this here merchant's guild.");
        
            // Tutorial check for tutorial loop
            bool tutorial = false;
            // Game running check for game loop
            bool isGameRunning = false;
            // Player alive check for game loop
            bool playerAlive = true;
            // Hours will be used for travel in game loop
            int hoursSinceStart = 0;
            string[] Hours = { "12:00 am", "1:00 am", "2:00 am", "3:00 am", "4:00 am", "5:00 am", "6:00 am", "7:00 am", "8:00 am", "9:00 am", "10:00 am", "11:00 am", "12:00 pm", "1:00 pm", "2:00 pm", "3:00 pm", "4:00 pm", "5:00 pm", "6:00 pm", "7:00 pm", "8:00 pm", "9:00 pm", "10:00 pm", "11:00 pm" };
            int time = 8;

            Console.WriteLine("So, d'ya want me to run ya through how this works, or d'ya know the ropes?");
            Console.WriteLine("1: Tutorial");
            Console.WriteLine("2: Continue to Game");
            int tutorialInput = int.Parse(Console.ReadLine());
            if (tutorialInput == 1)
            {
                Console.WriteLine("Sure thing, happy to help a new adventurer!");
                tutorial = true;
            }
            else if(tutorialInput == 2)
            {
                Console.WriteLine($"Safe travels then, {player.name}!");
                isGameRunning = true;
            }
            else
            {
                Console.WriteLine("There were two clear choices there, friend. Seems ya could use some help...");
                tutorial = true;
            }

            // Tutorial loop
            // TODO add tutorial, remove placeholder lines - after basic commands
            while (tutorial)
            {
                Console.WriteLine("tutorial here"); // placeholder
                tutorial = false;                   // placeholder
                isGameRunning = true;               // placeholder
            }

            // Main game loop
            while (isGameRunning == true)
            {
                bool isMonsterAlive = false;
                while (playerAlive == true)
                {
                    // clock and day counter
                    if (time >= 24)
                    {
                        time -= 24;
                    }

                    int day = ((hoursSinceStart + 8) / 24) + 1;

                    Console.WriteLine($"It is {Hours[time]} on day {day} of your journey. Hours travelled: {hoursSinceStart}");

                    // player input
                    // TODO add options - after basic commands
                    Console.WriteLine("What would you like to do?");
                    string gameInput = Console.ReadLine().ToLower().Trim();

                    // eating
                    if (gameInput == "eat")
                    {
                        Console.WriteLine("What would you like to eat?");
                        foreach (Item tmpFood in myInventory)
                        {
                            if (tmpFood.Type == "food")
                            {
                                if (tmpFood.Quantity > 0)
                                {
                                    Console.WriteLine($"{tmpFood.Name}: you have {tmpFood.Quantity} servings left.");
                                }
                            }
                        }
                        string eatInput = Console.ReadLine().ToLower();

                        // change quantity of food
                        for (int i = 0; i < myInventory.Length; i++)
                        {
                            if (myInventory[i].Name == eatInput)
                            {
                                if (myInventory[i].Quantity == 0)
                                {
                                    Console.WriteLine($"You don't have any servings of {myInventory[i].Name}.");
                                }
                                else
                                {
                                    if (myInventory[i].HungerBoost <= 2)
                                    {
                                        Console.WriteLine($"You eat a serving of {myInventory[i].Name}. It isn't very filling.");
                                    }
                                    else if (myInventory[i].HungerBoost >= 6)
                                    {
                                        Console.WriteLine($"You eat a serving of {myInventory[i].Name}. You feel stuffed!");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"You eat a serving of {myInventory[i].Name}.");
                                    }
                                    player.hunger -= myInventory[i].HungerBoost;
                                    player.thirst -= myInventory[i].ThirstBoost;
                                    myInventory[i].Quantity--;
                                }
                            }
                        }
                    }
                    // TODO fix wording to be proper English

                    // drinking
                    if (gameInput == "drink")
                    {
                        Console.WriteLine("What would you like to drink?");
                        foreach (Item tmpFood in myInventory)
                        {
                            if (tmpFood.Type == "drink")
                            {
                                if (tmpFood.Quantity > 0)
                                {
                                    Console.WriteLine($"{tmpFood.Name}: you have {tmpFood.Quantity} servings left.");
                                }
                            }
                            string drinkInput = Console.ReadLine().ToLower();

                            // change quantity of beverage
                            for (int i = 0; i < myInventory.Length; i++)
                            {
                                if (myInventory[i].Name == drinkInput)
                                {
                                    if (myInventory[i].Quantity == 0)
                                    {
                                        Console.WriteLine($"You don't have any servings of {myInventory[i].Name}.");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"You drink a serving of {myInventory[i].Name}.");
                                        player.hunger -= myInventory[i].HungerBoost;
                                        player.thirst -= myInventory[i].ThirstBoost;
                                        myInventory[i].Quantity--;
                                    }
                                }
                            }
                        }
                    }
                    // TODO fix wording to be proper English
                    // TODO consider drunkenness?

                    // sleeping
                    if (gameInput == "sleep") // TODO adjust for realism
                    {
                        Console.WriteLine($"Sleep for how many hours? Sleeping for {player.fatigue} hours will leave you fully rested.");
                        int sleepInput = int.Parse(Console.ReadLine());
                        player.fatigue -= sleepInput;
                        if (player.fatigue < 0)
                        {
                            player.fatigue = 0;
                        }

                        time += sleepInput;
                        hoursSinceStart += sleepInput;
                        player.hunger += sleepInput;
                        player.thirst += sleepInput;
                        Console.WriteLine($"You sleep for {sleepInput} hours, and awaken feeling more rested.");
                        Random sleepEncounter = new Random();
                        //TODO add random "while you were sleeping" encounters
                    }

                    // travelling
                    if (gameInput == "travel") // TODO add random encounters
                                               // TODO add town encounters
                    {
                        hoursSinceStart++;
                        time++;
                        player.hunger++;
                        player.thirst++;
                        player.fatigue++;
                        Random travelEncounter = new Random();
                        int newEncounter = travelEncounter.Next(20);
                        Console.WriteLine($"New encounter number: {newEncounter}"); // TODO remove later, debug line
                        if (newEncounter >= 6 && newEncounter <= 9)
                        {
                            Console.WriteLine("A monster appears!");
                            Monster monster = new Monster();
                            isMonsterAlive = true; // TODO add different types of monsters (name, stats)
                        }
                        else if(newEncounter >= 10) // newEncounter >= 10 && newEncounter <= 12
                        {
                            NPC bandit = new NPC(); // TODO if/else for choosing to fight or allow theft
                            Console.WriteLine("A bandit appears!");
                            Console.WriteLine("\"Alright, bud, we can do this the easy way or the hard way.\"");
                            Console.WriteLine("WIll you allow this theft, or fight for your cart and wares?");
                            string banditInput = Console.ReadLine().ToLower();

                            if(banditInput == "allow")
                            {
                                Console.WriteLine("\"Smart choice, bud.\"");
                                Random moneyStolenRand = new Random();
                                double moneyStolenPercent = moneyStolenRand.Next(50);
                                if(moneyStolenPercent < 10)
                                {
                                    moneyStolenPercent = 10;
                                }
                                double moneyStolen = player.gold * (moneyStolenPercent/100);
                                player.gold -= Convert.ToInt32(moneyStolen);
                                Console.WriteLine($"The thief rifles around, and steals {moneyStolen} gold. You now have {player.gold} gold pieces.");
                                Console.WriteLine("The thief leaves. That was unfortunate...");
                            }
                            else if (banditInput == "fight")
                            {
                                isMonsterAlive = true;
                            }
                            else
                            {
                                Console.WriteLine("That's an unknown command. The thief has a knife pointed at you, so type allow or fight quickly!");
                            }
                        }
                        else if(newEncounter >= 13)
                        {
                            Console.WriteLine("Another traveller comes up the path.");
                            NPC npc = new NPC();
                        }
                    }

                    // fight loop
                    //while(isMonsterAlive == true)
                    //{

                    //}

                    if (gameInput == "quit")
                    {
                        Console.WriteLine("Are you sure you want to quit?");
                        string quitInput = Console.ReadLine().ToLower();
                        if (quitInput == "yes" || quitInput == "y")
                        {
                            goto LoopEnd;
                        }
                    }
                    // basic needs tracker
                    if (player.hunger >= 8 && player.hunger <= 16)
                    {
                        Console.WriteLine("Your stomach is rumbling. You should eat something.");
                    }
                    else if (player.hunger >= 16 && player.hunger < 36)
                    {
                        Console.WriteLine($"Your stomach is aching from being so empty! You need to eat something soon!");
                        // TODO add stat penalties
                    }
                    else if (player.hunger >= 30 && player.hunger < 36)
                    {
                        Console.WriteLine($"Your stomach is in knots, and you can barely stand. You won't last much longer without something to eat...");
                        // TODO add stat penalties
                    }
                    else if (player.hunger >= 36)
                    {
                        Console.WriteLine("You have died of starvation.");
                        playerAlive = false;
                    }
                    // TODO adjust so there is a try again option

                    if (player.thirst >= 8 && player.hunger < 16)
                    {
                        Console.WriteLine("Your mouth feels dry. You should drink something.");
                    }
                    else if (player.thirst >= 16 && player.thirst < 30)
                    {
                        Console.WriteLine($"Your head is spinning due to how dehydrated you are! You need to drink something soon!");
                        // TODO add stat penalties
                    }
                    else if (player.thirst >= 30 && player.thirst < 36)
                    {
                        Console.WriteLine($"Your head throbs, and your throat aches. You won't last much longer without a drink...");
                        // TODO add stat penalties
                    }
                    else if (player.thirst >= 36)
                    {
                        Console.WriteLine("You have died of dehydration.");
                        playerAlive = false;
                    }
                    // TODO adjust so there is a try again option

                    if (player.fatigue >= 16 && player.fatigue <= 24)
                    {
                        Console.WriteLine("You're feeling pretty tired. You should get some sleep.");
                    }
                    else if (player.fatigue >= 24)
                    {
                        Console.WriteLine($"You're feeling exhausted! You need to sleep soon!");
                        // TODO add stat penalties
                    }
                }
                Console.WriteLine($"You travelled for {hoursSinceStart} hours before dying.");
                Console.WriteLine("Play again?");
                string playAgain = Console.ReadLine().ToLower();
                switch (playAgain)
                {
                    case "yes":
                        player = new Player
                        {
                            name = nameInput
                        };
                        player.gold = 25;
                        hoursSinceStart = 0;
                        time = 8;
                        using (var reader = new StreamReader("inventory.csv"))
                        {
                            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                            System.Collections.Generic.IEnumerable<Item> records = csv.GetRecords<Item>();
                            myInventory = records.ToArray<Item>();
                        }
                        playerAlive = true;
                        break;
                    case "y":
                        player = new Player
                        {
                            name = nameInput
                        };
                        player.gold = 25;
                        hoursSinceStart = 0;
                        time = 8;
                        using (var reader = new StreamReader("inventory.csv"))
                        {
                            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                            System.Collections.Generic.IEnumerable<Item> records = csv.GetRecords<Item>();
                            myInventory = records.ToArray<Item>();
                        }
                        playerAlive = true;
                        break;
                    case "no":
                        isGameRunning = false;
                        break;
                    case "n":
                        isGameRunning = false;
                        break;
                    default:
                        Console.WriteLine("Unknown command. Please type either yes/y or no/n.");
                        break;
                }
            }
            LoopEnd:
            Console.WriteLine($"Farewell, {player.name}!");
        }
    }
}
