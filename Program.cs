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
            player.health = player.Health();
            int fightsWon = 0;
            int level = (fightsWon/5) + 1;
            // Player inventory creation
            Item[] myInventory;
            using (var reader = new StreamReader("playerinventory.csv"))
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
                bool monsterAlive = false;
                
                // loop while the player is alive
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

                    if (gameInput == "help")
                    {
                        Console.WriteLine("Command isn't doing anything? Make sure that you're spelling the command right.");
                        Console.WriteLine("Command still isn't doing anything? You likely have written a command that doesn't exist. Try typing commands or command list to see a list of known commands.");
                        Console.WriteLine("Out of a resource? Most resources, including food and drink, can be purchased in town.");
                        Console.WriteLine("Out of money? Try trading some of your wares for coin, or defeating some monsters on the road.");
                    }

                    // command list
                    // TODO AT END check that all commands are listed here
                    if (gameInput == "commands" || gameInput == "command list")
                    {
                        Console.WriteLine("Command List:");
                        Console.WriteLine("Help - if you're especially lost");
                        Console.WriteLine("Inventory - if you're wondering what you have");
                        Console.WriteLine("Inv - if you're not up to typing all of the word 'inventory'");
                        Console.WriteLine("Eat - if you're feeling hungry");
                        Console.WriteLine("Drink - if you're feeling thirsty");
                        Console.WriteLine("Sleep - if you're feeling tired");
                        Console.WriteLine("Travel - if you're ready to continue on your quest");
                        Console.WriteLine("Quit - if you're ready to end your adventure");
                    }

                    if(gameInput == "inventory" || gameInput == "inv")
                    {
                        Console.WriteLine("You take a look through your wagon, and find the following:");
                        foreach (Item tmpItem in myInventory)
                        {
                            if (tmpItem.Quantity > 0)
                            {
                                if(tmpItem.Type == "food" || tmpItem.Type == "drink")
                                {
                                    Console.WriteLine($"{tmpItem.Name}: you have {tmpItem.Quantity} servings left.");
                                }
                                else
                                {
                                    Console.WriteLine($"{tmpItem.Name}: you have {tmpItem.Quantity}.");
                                }
                            }
                        }
                    }

                    // TODO add ability to look at item descriptions

                    // eating
                    if (gameInput == "eat")
                    {
                        Console.WriteLine("What would you like to eat?");
                        foreach (Item tmpFood in myInventory)
                        {
                            if (tmpFood.Type == "food" && tmpFood.Quantity > 0)
                            {
                                Console.WriteLine($"{tmpFood.Name}: you have {tmpFood.Quantity} servings left.");
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
                            if (tmpFood.Type == "drink" && tmpFood.Quantity > 0)
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
                                    if(myInventory[i].Alcohol > 0)
                                    {
                                        player.drunkenness += myInventory[i].Alcohol;
                                        switch (player.drunkenness)
                                        {
                                            case double d when d > 0 && d <= 3:
                                                Console.WriteLine("You feel a slight buzz after that drink!");
                                                break;
                                            case double d when d >= 4 && d <= 9:
                                                Console.WriteLine("You feel great! You're on top of the world! If only the world would stop spinning...");
                                                break;
                                            case double d when d >= 10:
                                                Console.WriteLine("Everything goes dark...");
                                                Random blackOut = new Random();
                                                int blackOutHours = blackOut.Next(8)+4;
                                                player.fatigue -= blackOutHours;
                                                player.hunger += blackOutHours;
                                                player.thirst += blackOutHours;
                                                hoursSinceStart += blackOutHours;
                                                time += blackOutHours;
                                                player.drunkenness -= blackOutHours;
                                                Console.WriteLine("...");
                                                Console.WriteLine($"Ugh...you wake up after being unconcious for {blackOutHours} hours.");
                                                break;
                                                // TODO add while you were unconcious random events, just like when you while you were sleeping
                                        }
                                    }
                                    player.hunger -= myInventory[i].HungerBoost;
                                    player.thirst -= myInventory[i].ThirstBoost;
                                    myInventory[i].Quantity--;
                                }
                            }
                        }
                    }
                    // TODO fix wording to be proper English

                    // sleeping
                    if (gameInput == "sleep")
                    {
                        Console.WriteLine("Sleep for how many hours?");
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
                        player.drunkenness -= sleepInput*0.5;
                        Console.WriteLine($"You sleep for {sleepInput} hours, and awaken feeling more rested.");
                        Random sleepEncounter = new Random();
                        //TODO add random "while you were sleeping" encounters
                    }

                    // travelling
                    if (gameInput == "travel") // TODO adjust random encounters
                                               // TODO add town encounters (consider array for randomizing distances)
                                               // TODO make sure to have a save option that saves to a text file (see Jon's repo)
                    {
                        hoursSinceStart++;
                        time++;
                        player.hunger++;
                        player.thirst++;
                        player.fatigue++;
                        player.drunkenness -= 0.5;
                        Random travelEncounter = new Random();
                        int newEncounter = travelEncounter.Next(20);

                        switch (newEncounter)
                        {
                            case 6:
                                Console.WriteLine("You pass by a river on your travels. You can go fishing here if you want!");
                                // TODO add fishing
                                break;
                            case 7:
                                Console.WriteLine("You pass another traveller on the path.");
                                Console.WriteLine("\"A trade? No, I have everything I need, thank you.\"");
                                break;
                            case 8:
                                Console.WriteLine("A wheel on your wagon broke! It takes you an hour to fix.");
                                time++;
                                hoursSinceStart++;
                                break;
                            case int e when e == 9 || e == 10:
                                Console.WriteLine("A bandit appears!");
                                Console.WriteLine("\"Alright, bud, we can do this the easy way or the hard way.\"");
                                Console.WriteLine("WIll you allow the thief to steal from you, or fight for your cart and wares?"); // TODO add theft of wares or change line
                                bool beingRobbed = true;

                                while (beingRobbed == true)
                                {
                                    string banditInput = Console.ReadLine().ToLower();

                                    if (banditInput == "allow")
                                    {
                                        Console.WriteLine("\"Smart choice, bud.\"");
                                        Random moneyStolenRand = new Random();
                                        double moneyStolenPercent = moneyStolenRand.Next(33);
                                        if (moneyStolenPercent < 10)
                                        {
                                            moneyStolenPercent = 10;
                                        }
                                        double moneyStolen = myInventory[0].Quantity * (moneyStolenPercent / 100);
                                        myInventory[0].Quantity -= Convert.ToInt32(moneyStolen);
                                        Console.WriteLine($"The thief rifles around, and steals {Convert.ToInt32(moneyStolen)} gold. You now have {myInventory[0].Quantity} gold pieces.");
                                        Console.WriteLine("The thief leaves. That was unfortunate...");
                                        beingRobbed = false;
                                    }
                                    else if (banditInput == "fight") // TODO add more text here
                                    {
                                        monsterAlive = true;
                                        beingRobbed = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("That's an unknown command. The thief has a knife pointed at you, so choose to allow or fight quickly!");
                                    }
                                }
                                break;
                            case int e when e == 11 || e == 12:
                                Console.WriteLine("A living skeleton appears, and it's brandishing a weapon!");
                                monsterAlive = true;
                                break;
                            case int e when e == 13:
                                Console.WriteLine("An orc warrior appears!");
                                Console.WriteLine("\"Prepare to die, milkdrinker!\"");
                                monsterAlive = true;
                                break;
                            case int e when e == 14 || e == 15:
                                Console.WriteLine("A snarling wolf appears, and it appears hungry!");
                                monsterAlive = true;
                                break;
                            case int e when e == 16 || e == 17:
                                Console.WriteLine("You pass another merchant on the road.");
                                Console.WriteLine("Ho there, friend. Care to exchange goods?");
                                // TODO Food Vendor encounter
                                break;
                            case int e when e == 18:
                                Console.WriteLine("You pass another merchant on the road.");
                                Console.WriteLine("Ho there, friend. Care to exchange goods?");
                                // TODO Armorer encounter
                                break;
                            case int e when e == 19 || e == 20:
                                Console.WriteLine("You pass another traveller on the path.");
                                Console.WriteLine("\"A trade? Sure, let's see what you have.\"");
                                // TODO Adventurer encounter
                                break;
                        }
                    }
                    // monster creation
                    Entity monster = new Entity();

                    // fight loop
                    while (monsterAlive == true)
                    {
                    //    // initiate random damage amounts
                    //    Random monAttRand = new Random();
                    //    int monAttack = monAttRand.Next(6);
                    //    Random playerAttRand = new Random();
                    //    int playerAttack = playerAttRand.Next(6);

                    //    // choose a weapon and attack with it
                    //    Console.WriteLine("Which of your weapons will you attack with?");
                    //    foreach (Item tmpWeapon in myInventory)
                    //    {
                    //        if (tmpWeapon.Type == "weapon" && tmpWeapon.Quantity > 0)
                    //        {
                    //            if(tmpWeapon.HandsNeeded == 2)
                    //            {
                    //                Console.WriteLine($"{tmpWeapon.Name}, which requires both hands to use, and has a {tmpWeapon.Damage} damage base attack.");
                    //            }
                    //            else
                    //            {
                    //                Console.WriteLine($"{tmpWeapon.Name}, which has a {tmpWeapon.Damage} damage base attack.");
                    //            }
                    //        }
                    //    }
                    //    string weapInput = Console.ReadLine().ToLower();

                    //    for (int i = 0; i < myInventory.Length; i++)
                    //    {
                    //        if (myInventory[i].Name == weapInput)
                    //        {
                    //            if (myInventory[i].Quantity == 0)
                    //            {
                    //                Console.WriteLine($"You don't have a {myInventory[i].Name}!"); // TODO either make sure no weapons start with a vowel, or adjust for proper English
                    //            }
                    //            else
                    //            {
                    //                Console.WriteLine($"You attack with the {myInventory[i].Name}, dealing {(myInventory[i].Damage + player.Attack[playerAttack])*(1-monster.defense)} damage to the {monster.name}.");
                    //                monster.health -= (myInventory[i].Damage + player.Attack[playerAttack]) * (1 - monster.defense);
                    //                if(monster.health < 0)
                    //                {
                    //                    monster.health = 0;
                    //                }
                    //                Console.WriteLine($"The {monster.name} now has {monster.health} health.");
                    //            }
                    //        }
                    //    }
                    //    if(monster.health == 0)
                    //    {
                    //        Console.WriteLine("You won the fight!"); // TODO add loot
                    //        fightsWon++;
                    //        if(fightsWon % 5 == 0)
                    //        {
                    //            Console.WriteLine("You feel stronger after all of these fights! Your attack and health have increased!");
                    //            player.health += 10;
                    //            for(int i = 0; i < player.Attack.Length; i++)
                    //            {
                    //                player.Attack[i]++;
                    //            }
                    //        }
                    //        monsterAlive = false;
                    //    }
                    //    else if (monster.health <= 5 && monster.name == "bandit")
                    //    {
                    //        Console.WriteLine("The bandit flees for their life. Good riddance!");
                    //        monsterAlive = false;
                    //    }
                    //    else
                    //    {
                           
                    //        Console.WriteLine($"The {monster.name} attacks you back!");
                    //        player.health -= monster.Attack[monAttack];
                    //        if(player.health < 0)
                    //        {
                    //            player.health = 0;
                    //        }
                    //        Console.WriteLine($"You take {monster.Attack[monAttack]} damage, and have {player.health} health left."); // TODO adjust when monsters have different stats
                    //        if(player.health == 0)
                    //        {
                    //            Console.WriteLine("You lost the fight! Everything goes black...");
                    //            playerAlive = false;
                    //            monsterAlive = false;
                    //        }
                    //    }
                    //    if (player.health <= (player.health / 4) && monster.name == "bandit")
                    //    {
                    //        Console.WriteLine("\"Don't make me kill you. Just give me what I want and we can go our seperate ways.\"");
                    //        Console.WriteLine("Continue fighting?");
                    //        string killBanditInput = Console.ReadLine().ToLower();
                    //        if (killBanditInput == "n" || killBanditInput == "no")
                    //        {
                    //            Console.WriteLine("\"Smart choice, bud.\"");
                    //            Random moneyStolenRand = new Random();
                    //            double moneyStolenPercent = moneyStolenRand.Next(50);
                    //            if (moneyStolenPercent < 10)
                    //            {
                    //                moneyStolenPercent = 10;
                    //            }
                    //            double moneyStolen = myInventory[0].Quantity * (moneyStolenPercent / 100);
                    //            myInventory[0].Quantity -= Convert.ToInt32(moneyStolen);
                    //            Console.WriteLine($"The thief rifles around, and steals {Convert.ToInt32(moneyStolen)} gold. You now have {myInventory[0].Quantity} gold pieces.");
                    //            Console.WriteLine("The thief leaves. That was unfortunate...");
                    //            monsterAlive = false;
                    //        }
                    //    }
                    }

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
                    switch (player.hunger)
                    {
                        case int h when h >= 8 && h <= 15:
                            Console.WriteLine("Your stomach is rumbling. You should eat something.");
                            break;
                        case int h when h >= 16 && h <= 29:
                            Console.WriteLine("Your stomach is aching from being so empty! You need to eat something soon!"); // TODO add stat penalties
                            break;
                        case int h when h >= 30 && h <= 35:
                            Console.WriteLine("Your stomach is in knots, and you can barely stand. You won't last much longer without something to eat..."); // TODO add stat penalties
                            break;
                        case int h when h >= 36:
                            Console.WriteLine("You have died of starvation.");
                            playerAlive = false;
                            break;
                    }

                    switch (player.thirst)
                    {
                        case int t when t >= 8 && t <= 15:
                            Console.WriteLine("Your mouth feels dry. You should drink something.");
                            break;
                        case int t when t >= 16 && t <= 29:
                            Console.WriteLine("Your head is spinning due to how dehydrated you are! You need to drink something soon!"); // TODO add stat penalties
                            break;
                        case int t when t >= 30 && t <= 35:
                            Console.WriteLine($"Your head throbs, and your throat aches. You won't last much longer without a drink..."); // TODO add stat penalties
                            break;
                        case int t when t >= 36:
                            Console.WriteLine("You have died of dehydration.");
                            playerAlive = false;
                            break;
                    }

                    switch (player.fatigue)
                    {
                        case int f when f >= 16 && f <= 23:
                            Console.WriteLine("You're feeling pretty tired. You should get some sleep.");
                            break;
                        case int f when f >= 24 && f <= 40:
                            Console.WriteLine("You're feeling exhausted! You need to sleep soon!"); // TODO add stat penalties
                            break;
                        case int f when f >= 41 && f <= 47:
                            Console.WriteLine("You can barely keep your eyes open, and your head keeps drooping. You won't be able to stay awake much longer...");
                            break;
                        case int f when f >= 48:
                            Console.WriteLine("Your eyelids droop, and everything goes dark...");
                            Random passOut = new Random();
                            int passOutHours = passOut.Next(12) + 4;
                            player.fatigue -= passOutHours;
                            player.hunger += passOutHours;
                            player.thirst += passOutHours;
                            hoursSinceStart += passOutHours;
                            time += passOutHours;
                            player.drunkenness -= passOutHours;
                            Console.WriteLine("...");
                            Console.WriteLine($"You wake up after being asleep for {passOutHours} hours.");
                            break;
                            // TODO add while you were unconcious random events, just like when you while you were sleeping
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
