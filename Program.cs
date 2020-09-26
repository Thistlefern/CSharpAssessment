using System;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;

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
            int fightsWon = 0;
            int level = 1;
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
            // Check for how many hours you've TRAVELLED, to determine how far you've gone and how close you are to a town.
            int hoursTravelled = 0;

            Console.WriteLine("So, d'ya want me ta run ya through how this works, or d'ya know the ropes?");
            Console.WriteLine("1: Tutorial");
            Console.WriteLine("2: Play Game");
            string tutorialInput = Console.ReadLine().ToLower();
            if (tutorialInput == "1" || tutorialInput == "tutorial")
            {
                Console.WriteLine("Sure thing, happy to help a new adventurer!");
                tutorial = true;
            }
            else if(tutorialInput == "2" || tutorialInput == "play")
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
                bool trading = false;
                string traderName = "A trader";
                bool monsterAlive = false;
                string monsterName = "A monster";
                player.attackBonus = (5 * (level-1)) + 2;
                if(myInventory[7].Quantity >= 1) // if the player owns chain mail, they have a defence of 50%
                {
                    player.defense = .5;
                } else if (myInventory[25].Quantity >= 1) // if the player owns scail mail, they have a defence of 35%
                {
                    player.defense = .35;
                } else if(myInventory[18].Quantity >= 1) // if the player owns leather armor, they have a defence of 25%
                {
                    player.defense = .25;
                }
                else
                {
                    player.defense = 0; // if the player owns no armor, they have a defence of 0%
                }

                // loop while the player is alive
                while (playerAlive == true)
                {
                    // clock and day counter
                    if (time >= 24)
                    {
                        time -= 24;
                    }

                    int day = ((hoursSinceStart + 8) / 24) + 1;

                    Console.WriteLine($"\nIt is {Hours[time]} on day {day} of your journey.");

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
                    if(player.health < player.maxHealth / 4)
                    {
                        Console.WriteLine($"You have lost a lot of blood from the fights you've been in. I know that rationally sleep won't fix that, but this is a game, so maybe get some sleep!");
                    }
                    else if (player.health < player.maxHealth / 2)
                    {
                        Console.WriteLine($"You are in quite a bit of pain. It might be a good idea to sleep for a little while.");
                    }


                    Console.WriteLine($"Your current health: {player.health}/{player.maxHealth}");

                    if (hoursTravelled == 36)
                    {
                        // reaching a town
                        Console.WriteLine("\nYou've made it to a small town! Here, you can visit a general store, and you can also sleep without the worry of bandits appearing!"); // TODO edit if I don't make sleeping encounters
                        Console.WriteLine("1: Visit store");
                        Console.WriteLine("2: Sleep");
                        Console.WriteLine("3: Continue travelling");
                        int townInput = int.Parse(Console.ReadLine());
                        switch (townInput)
                        {
                            case 1:
                                Console.WriteLine("\"Welcome, traveller! How can I help you today?\"");
                                traderName = "The general store merchant";
                                trading = true;
                                break;
                            case 2:
                                Console.WriteLine("Sleep for how long?");
                                int townSleepInput = int.Parse(Console.ReadLine());
                                player.fatigue -= townSleepInput;
                                if (player.fatigue < 0)
                                {
                                    player.fatigue = 0;
                                }

                                time += townSleepInput;
                                hoursSinceStart += townSleepInput;
                                player.hunger += townSleepInput;
                                player.thirst += townSleepInput;
                                player.drunkenness -= townSleepInput * 0.5;
                                player.health += townSleepInput * 0.5;
                                if (player.health > player.maxHealth)
                                {
                                    player.health = player.maxHealth;
                                }
                                Console.WriteLine($"You sleep for {townSleepInput} hours, and awaken feeling more rested.");
                                break;
                            case 3:
                                hoursTravelled = 0;
                                // TODO save option here (see Jon's repo)
                                break;
                        }
                    }

                    // =================== CODE FOR PLAYER COMMANDS START =================== //
                    // player input
                    // TODO add options - after basic commands
                    Console.WriteLine("What would you like to do?");
                    string gameInput = Console.ReadLine().ToLower().Trim();
                    Console.WriteLine("\n");

                    if (gameInput == "help")
                    {
                        Console.WriteLine("Command isn't doing anything? Make sure that you're spelling the command right.");
                        Console.WriteLine("Command still isn't doing anything? You likely have written a command that doesn't exist. Try typing commands or command list to see a list of usable commands.");
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
                        Console.WriteLine("Sleep - if you're feeling tired or your health is low");
                        Console.WriteLine("Travel - if you're ready to continue on your quest");
                        Console.WriteLine("Quit - if you're ready to end your adventure");
                    }

                    // check inventory
                    if(gameInput == "inventory" || gameInput == "inv")
                    {
                        Console.WriteLine("You take a look through your wagon, and find the following:");
                        Console.WriteLine("---------------------------------------------------");
                        foreach (Item tmpItem in myInventory)
                        {
                            if (tmpItem.Quantity > 0) // makes sure only items you have at least 1 of are printed
                            {
                                if(tmpItem.Type == "food" || tmpItem.Type == "drink")
                                {
                                    Console.WriteLine($"{tmpItem.Name}: you have {tmpItem.Quantity} servings left."); // adds the word 'servings' to food and drink items when printing
                                }
                                else
                                {
                                    Console.WriteLine($"{tmpItem.Name}: you have {tmpItem.Quantity}.");
                                }
                            }
                        }
                        Console.WriteLine("---------------------------------------------------");
                    }

                    // TODO add ability to look at item descriptions

                    if(gameInput == "check health") // TODO adjust later, currently debug
                    {
                        Console.WriteLine($"Player max health: {player.maxHealth}");
                        Console.WriteLine($"Player current health: {player.health}");
                        Console.WriteLine($"Player level: {level}");
                        Console.WriteLine($"Fights won: {fightsWon}");
                        Console.WriteLine($"Defense: {player.defense}");
                        if(myInventory[26].Quantity >= 1)
                        {
                            Console.WriteLine("Plus a shield, so if you use a one-handed weapon thats an extra 10%");
                        }
                        player.Attack();
                        Console.WriteLine($"Player attack: {player.attack}");
                        player.Attack();
                        Console.WriteLine($"Player attack: {player.attack}");
                        player.Attack();
                        Console.WriteLine($"Player attack: {player.attack}");
                    }

                    // eating
                    if (gameInput == "eat")
                    {
                        bool letMeEat = false;
                        if (player.hunger == 0)
                        {
                            Console.WriteLine("You don't feel hungry! Are you sure you want to eat?");
                            Console.WriteLine("1: Yes");
                            Console.WriteLine("2: No");
                            string eatAnyways = Console.ReadLine();
                            if (eatAnyways == "yes" || eatAnyways == "y" || eatAnyways == "drink" || Convert.ToString(eatAnyways) == "1")
                            {
                                letMeEat = true;
                            }
                        }
                        if (player.hunger != 0 || letMeEat == true)
                        {
                            int eatRow = 0;
                            Console.WriteLine("What would you like to eat?");
                            for (int i = 0; i < myInventory.Length; i++)
                            {
                                myInventory[i].Row = 0;
                                if (myInventory[i].Type == "food" && myInventory[i].Quantity > 0)
                                {
                                    eatRow++;
                                    myInventory[i].Row = eatRow;
                                    Console.WriteLine($"{myInventory[i].Row}: {myInventory[i].Name}, of which you have {myInventory[i].Quantity} servings left.");
                                }
                            }
                            string eatInput = Console.ReadLine().ToLower();

                            // change quantity of food
                            for (int i = 0; i < myInventory.Length; i++)
                            {
                                if (myInventory[i].Name == eatInput || Convert.ToString(myInventory[i].Row) == eatInput)
                                {
                                    if (myInventory[i].Quantity == 0)
                                    {
                                        Console.WriteLine($"You don't have any servings of {myInventory[i].Name}.");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"You eat {myInventory[i].Name} to satiate your hunger.");
                                        bool eating = true;
                                        while (eating == true)
                                        {
                                            player.hunger -= myInventory[i].HungerBoost;
                                            player.thirst -= myInventory[i].ThirstBoost;
                                            myInventory[i].Quantity--;

                                            if (myInventory[i].Quantity == 0)
                                            {
                                                eating = false;
                                            }
                                            if (player.hunger <= 4)
                                            {
                                                eating = false;
                                            }
                                        }
                                        if (myInventory[i].Quantity < 0)
                                        {
                                            myInventory[i].Quantity = 0;
                                        }

                                        if (player.hunger >= 8)
                                        {
                                            Console.WriteLine("That wasn't quite enough food...");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    // TODO fix wording to be proper English

                    // drinking
                    if (gameInput == "drink")
                    {
                        bool letMeDrink = false;
                        if (player.thirst == 0)
                        {
                            Console.WriteLine("You don't feel thirsty! Are you sure you want to drink?");
                            Console.WriteLine("1: Yes");
                            Console.WriteLine("2: No");
                            string drinkAnyways = Console.ReadLine();
                            if (drinkAnyways == "yes" || drinkAnyways == "y" || drinkAnyways == "drink" || Convert.ToString(drinkAnyways) == "1")
                            {
                                letMeDrink = true;
                            }
                        }
                        if (player.thirst != 0 || letMeDrink == true)
                        {
                            int drinkRow = 0;
                            Console.WriteLine("What would you like to drink?");
                            for (int i = 0; i < myInventory.Length; i++)
                            {
                                myInventory[i].Row = 0;
                                if (myInventory[i].Type == "drink" && myInventory[i].Quantity > 0)
                                {
                                    drinkRow++;
                                    myInventory[i].Row = drinkRow;
                                    Console.WriteLine($"{myInventory[i].Row}: {myInventory[i].Name}, of which you have {myInventory[i].Quantity} servings left."); ;
                                }
                            }
                            string drinkInput = Console.ReadLine().ToLower();

                            // change quantity of beverage
                            for (int i = 0; i < myInventory.Length; i++)
                            {
                                if (myInventory[i].Name == drinkInput || Convert.ToString(myInventory[i].Row) == drinkInput)
                                {
                                    if (myInventory[i].Quantity == 0)
                                    {
                                        Console.WriteLine($"You don't have any servings of {myInventory[i].Name}.");
                                    }
                                    else
                                    {
                                        if (myInventory[i].Name == "health potion")
                                        {
                                            Console.WriteLine($"You drink a healing potion.");
                                            player.health += 20;
                                            if (player.health > player.maxHealth)
                                            {
                                                player.health = player.maxHealth;
                                            }
                                            Console.WriteLine($"You regain 20 health, and are now at {player.health} health!");
                                            player.hunger -= myInventory[i].HungerBoost;
                                            player.thirst -= myInventory[i].ThirstBoost;
                                            myInventory[i].Quantity--;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"You drink {myInventory[i].Name} to quench your thirst.");
                                            bool drinking = true;
                                            while (drinking == true)
                                            {
                                                player.hunger -= myInventory[i].HungerBoost;
                                                player.thirst -= myInventory[i].ThirstBoost;
                                                player.drunkenness += myInventory[i].Alcohol;
                                                myInventory[i].Quantity--;

                                                if(myInventory[i].Quantity == 0)
                                                {
                                                    drinking = false;
                                                }
                                                if(player.thirst <= 4)
                                                {
                                                    drinking = false;
                                                }
                                            }
                                            if(myInventory[i].Quantity < 0)
                                            {
                                                myInventory[i].Quantity = 0;
                                            }
                                        }
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
                                                int blackOutHours = blackOut.Next(8) + 4;
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
                                        if (player.thirst >= 8)
                                        {
                                            Console.WriteLine("That wasn't quite enough to drink...");
                                        }
                                    }
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
                        player.health += sleepInput * 2;
                        if(player.health > player.maxHealth)
                        {
                            player.health = player.maxHealth;
                        }
                        Console.WriteLine($"You sleep for {sleepInput} hours, and awaken feeling more rested.");
                        Random sleepEncounter = new Random();
                        //TODO add random "while you were sleeping" encounters
                    }

                    // allows the player to quit the game
                    if (gameInput == "quit")
                    {
                        Console.WriteLine("Are you sure you want to quit?");
                        string quitInput = Console.ReadLine().ToLower();
                        if (quitInput == "yes" || quitInput == "y")
                        {
                            goto LoopEnd;
                        }
                    }

                    // travelling
                    if (gameInput == "travel")
                    {
                        hoursSinceStart++;
                        time++;
                        player.hunger++;
                        player.thirst++;
                        player.fatigue++;
                        player.drunkenness -= 0.5;
                        hoursTravelled++;

                        // random travel encounter if you don't make it to a town
                        Random travelEncounter = new Random();
                        int newEncounter = travelEncounter.Next(20);

                        switch (newEncounter)
                        {
                            case 6:
                                Console.WriteLine("You pass by a river on your travels. You can go fishing or collect some water here!");
                                bool nearRiver = true;
                                while (nearRiver == true)
                                {
                                    int stayNearRiver = 0;
                                    Console.WriteLine("1: Go fishing");
                                    Console.WriteLine("2: Collect water");
                                    Console.WriteLine("3: Continue travelling");
                                    int riverInput = int.Parse(Console.ReadLine());
                                    switch (riverInput)
                                    {
                                        case 1:
                                            if(myInventory[14].Quantity == 0)
                                            {
                                                Console.WriteLine("You don't have a fishing pole...maybe buy one next time you see one!");
                                            }
                                            else
                                            {
                                                Random fishRand = new Random();
                                                int fishCaught = fishRand.Next(3);
                                                time++;
                                                hoursSinceStart++;
                                                player.hunger++;
                                                player.thirst++;
                                                player.fatigue++;
                                                myInventory[13].Quantity += fishCaught;
                                                if (fishCaught == 0)
                                                {
                                                    Console.WriteLine("You fish for an hour, but don't catch anything...");
                                                }
                                                else if (fishCaught == 1)
                                                {
                                                    Console.WriteLine($"You fish for an hour, and catch {fishCaught} serving worth of fish!");
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"You fish for an hour, and catch {fishCaught} servings worth of fish!");
                                                }
                                                Console.WriteLine("Stay near river?");
                                                Console.WriteLine("1: Stay");
                                                Console.WriteLine("2: Continue travelling");
                                                stayNearRiver = int.Parse(Console.ReadLine());
                                                if (stayNearRiver == 2)
                                                {
                                                    nearRiver = false;
                                                }
                                            }
                                            break;
                                        case 2:
                                            Console.WriteLine("You can get 5 servings of water per hour, as it takes time to make the water drinkable.");
                                            Console.WriteLine("How many hours would you like to spend collecting water?");
                                            int waterCollect = int.Parse(Console.ReadLine());
                                            time += waterCollect;
                                            hoursSinceStart += waterCollect;
                                            player.hunger += waterCollect;
                                            player.thirst += waterCollect;
                                            player.fatigue += waterCollect;
                                            myInventory[30].Quantity += waterCollect * 5;
                                            Console.WriteLine($"You collect {waterCollect * 5} servings of drinking water.");
                                            Console.WriteLine("Stay near river?");
                                            Console.WriteLine("1: Stay");
                                            Console.WriteLine("2: Continue travelling");
                                            stayNearRiver = int.Parse(Console.ReadLine());
                                            if (stayNearRiver == 2)
                                            {
                                                nearRiver = false;
                                            }
                                            break;
                                        case 3:
                                            nearRiver = false;
                                            break;
                                    }
                                }
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
                                bool beingRobbed = true;

                                while (beingRobbed == true)
                                {
                                    Console.WriteLine("Will you allow the thief to steal from you, or fight for your cart and wares?"); // TODO add theft of wares or change line
                                    Console.WriteLine("1: Allow");
                                    Console.WriteLine("2: Fight");
                                    string banditInput = Console.ReadLine().ToLower();

                                    if (banditInput == "allow" || Convert.ToString(banditInput) == "1")
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
                                    else if (banditInput == "fight" || Convert.ToString(banditInput) == "2")
                                    {
                                        Console.WriteLine("\n\"So you want to do this the hard way, eh?\"");
                                        monsterName = "bandit";
                                        monsterAlive = true;
                                        beingRobbed = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("That's an unknown command. The thief has a knife pointed at you, so choose to allow or fight quickly!");
                                    }
                                }
                                break;
                            case 11:
                                Console.WriteLine("An orc warrior appears!");
                                Console.WriteLine("\"Prepare to die, milkdrinker!\"");
                                if (level == 1)
                                {
                                    Console.WriteLine("He's way more powerful than you are! You feel the strong urge to flee from this fight before it starts.");
                                    Console.WriteLine("1: Flee");
                                    Console.WriteLine("2: Fight");
                                    string orcLevelOne = Console.ReadLine().ToLower();
                                    if (orcLevelOne == "flee" || Convert.ToString(orcLevelOne) == "1")
                                    {
                                        Console.WriteLine("You turn your cart on a dime and take another path! The orc gives chase for a while, but finally leaves you alone.");
                                        Console.WriteLine("This detour costs you 2 hours, but saves you your skin.");
                                        time += 2;
                                        hoursSinceStart += 2;
                                    }
                                    else if (orcLevelOne == "fight" || Convert.ToString(orcLevelOne) == "2")
                                    {
                                        Console.WriteLine("You were warned...");
                                        monsterName = "orc";
                                        monsterAlive = true;
                                    }
                                    break;
                                }
                                else
                                {
                                    monsterName = "orc";
                                    monsterAlive = true;
                                    break;
                                }
                            case int e when e == 12 || e == 13:
                                Console.WriteLine("A living skeleton appears, and it's brandishing a weapon!");
                                monsterName = "skeleton";
                                monsterAlive = true;
                                break;
                            case int e when e == 14 || e == 15:
                                Console.WriteLine("A snarling wolf appears, and it appears hungry!");
                                monsterName = "wolf";
                                monsterAlive = true;
                                break;
                            case int e when e == 16 || e == 17:
                                Console.WriteLine("You pass another merchant on the road.");
                                Console.WriteLine("Ho there, friend. Care to exchange goods?");
                                traderName = "A food vendor";
                                Console.WriteLine($"{traderName} wants to trade with you!");
                                trading = true;
                                break;
                            case int e when e == 18:
                                Console.WriteLine("You pass another merchant on the road.");
                                Console.WriteLine("Ho there, friend. Care to exchange goods?");
                                traderName = "An armor and weapons vendor";
                                Console.WriteLine($"{traderName} wants to trade with you!");
                                trading = true;
                                break;
                            case int e when e == 19 || e == 20:
                                Console.WriteLine("You pass another traveller on the path.");
                                Console.WriteLine("\"A trade? Sure, let's see what you have.\"");
                                traderName = "An adventurer";
                                Console.WriteLine($"{traderName} wants to trade with you!");
                                trading = true;
                                break;
                            default:
                                Console.WriteLine("The road ahead is clear ahead.");
                                break;
                        }
                    }
                    // =================== CODE FOR PLAYER COMMANDS END =================== //

                    // =================== CODE FOR TRADING START =================== //

                    // NPC creation
                    Entity npc = new Entity()
                    {
                        name = traderName
                    };
                    NPCinv: // a spot for a later 'goto' method to find in case the vendor's inventory is empty
                    int maxQTY = 0;
                    string willBuy = "FALSE";

                    // NPC inventory creation
                    StoreItem[] storeInventory;
                    using (var reader = new StreamReader("storeinventory.csv"))
                    {
                        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                        System.Collections.Generic.IEnumerable<StoreItem> records = csv.GetRecords<StoreItem>();
                        storeInventory = records.ToArray<StoreItem>();
                    }

                    //NPC inventory quantity randomizer
                    Random storeRandom = new Random();

                    for (int i = 0; i < storeInventory.Length; i++)
                    {
                        switch (traderName)
                        {
                            case "The general store merchant":
                                maxQTY = storeInventory[i].GenStoreMax;
                                willBuy = storeInventory[i].GenStoreWillBuy;
                                break;
                            case "A food vendor":
                                maxQTY = storeInventory[i].FoodVendorMax;
                                willBuy = storeInventory[i].FoodVendorWillBuy;
                                break;
                            case "An armor and weapons vendor":
                                maxQTY = storeInventory[i].ArmorerMax;
                                willBuy = storeInventory[i].ArmorerWillBuy;
                                break;
                            case "An adventurer":
                                maxQTY = storeInventory[i].AdventurerMax;
                                willBuy = storeInventory[i].AdventurerWillBuy;
                                break;
                        }
                        int storeQTY = storeRandom.Next(maxQTY);
                        storeInventory[i].Quantity = storeQTY;
                        storeInventory[i].WillBuy = willBuy;
                    }

                    // checks to make sure that the store isn't empty
                    int storeEmpty = 0;
                    for (int i = 2; i < storeInventory.Length; i++)
                    {
                        storeEmpty += storeInventory[i].Quantity;
                    }

                    // trade loop
                    while (trading == true)
                    {
                        // restarts inventory creation if the store is empty
                        if (storeEmpty == 0)
                        {
                            goto NPCinv;
                        }
                        // ensures that any store always has at least 5 gold
                        if(storeInventory[0].Quantity < 5)
                        {
                            storeInventory[0].Quantity = 5;
                        }

                        Console.WriteLine("\nWhat would you like to do?");
                        Console.WriteLine("1: Buy");
                        Console.WriteLine("2: Sell");
                        Console.WriteLine("3: View their inventory");
                        Console.WriteLine("4: View your inventory");
                        Console.WriteLine("5: End trade");
                        string tradeInput = Console.ReadLine().ToLower();
                        switch (tradeInput)
                        {
                            // purchase wares
                            case string s when s == "1" || s == "buy":
                                Console.WriteLine("What would you like to buy?");
                                Console.WriteLine($"Your current gold: {myInventory[0].Quantity}");
                                int buyRow = 0;
                                Console.WriteLine("---------------------------------------------------");
                                for (int i = 1; i < storeInventory.Length; i++)
                                {
                                    storeInventory[i].Row = 0;
                                    if (storeInventory[i].Quantity > 0)
                                    {
                                        buyRow++;
                                        storeInventory[i].Row = buyRow;
                                        if (storeInventory[i].Type == "drink" || storeInventory[i].Type == "food")
                                        {
                                            Console.WriteLine($"{buyRow}: {storeInventory[i].Name} ({storeInventory[i].Quantity} servings available to buy for {Math.Round(storeInventory[i].CostValue*1.25)} gold each.");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{buyRow}: {storeInventory[i].Name} ({storeInventory[i].Quantity} available to buy for {Math.Round(storeInventory[i].CostValue * 1.25)} gold each.");
                                        }
                                    }
                                }
                                Console.WriteLine("---------------------------------------------------");
                                // allow player to exit trade
                                Console.WriteLine("Type 'back' to exit purchase.");
                                string storeBuyInput = Console.ReadLine().ToLower();
                                if(storeBuyInput == "back")
                                {
                                    break;
                                }

                                // check what can be bought, and buy if possible
                                for (int i = 0; i < storeInventory.Length; i++)
                                {
                                    if (storeInventory[i].Name == storeBuyInput || Convert.ToString(storeInventory[i].Row) == storeBuyInput)
                                    {
                                        if (storeInventory[i].Quantity == 0)
                                        {
                                            Console.WriteLine($"This vendor doesn't have any {storeBuyInput} for sale.");
                                        }
                                        else
                                        {
                                            if(Convert.ToInt32(storeInventory[i].CostValue * 1.25) > myInventory[0].Quantity)
                                            {
                                                Console.WriteLine($"You can't afford the {storeInventory[i].Name}! You are short {Convert.ToInt32(storeInventory[i].CostValue * 1.25) - myInventory[0].Quantity} gold to buy it. Try selling something if you really want this!");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Buy how many?");
                                                int storeBuyQTY = int.Parse(Console.ReadLine());
                                                if(Convert.ToInt32(storeBuyQTY * storeInventory[i].CostValue * 1.25) > myInventory[0].Quantity)
                                                {
                                                    Console.WriteLine($"You can't afford {storeBuyQTY} {storeInventory[i].Name}! The most you can afford to buy is {myInventory[0].Quantity / Convert.ToInt32(storeInventory[i].CostValue * 1.25)}.");
                                                }
                                                else
                                                {
                                                    if (storeBuyQTY > storeInventory[i].Quantity)
                                                    {
                                                        Console.WriteLine("That is more than this vendor has! Buy them out of this item?");
                                                        string buyOut = Console.ReadLine().ToLower();
                                                        if (buyOut == "y" || buyOut == "yes")
                                                        {
                                                            myInventory[0].Quantity -= Convert.ToInt32(storeInventory[i].Quantity * Convert.ToInt32(storeInventory[i].CostValue * 1.25));
                                                            storeInventory[0].Quantity += Convert.ToInt32(storeInventory[i].Quantity * Convert.ToInt32(storeInventory[i].CostValue * 1.25));
                                                            myInventory[i].Quantity += storeInventory[i].Quantity;
                                                            Console.WriteLine($"You give the vendor {Convert.ToInt32(storeInventory[i].Quantity * Convert.ToInt32(storeInventory[i].CostValue * 1.25))} gold, and now have {myInventory[i].Quantity} {myInventory[i].Name}.");
                                                            storeInventory[i].Quantity = 0;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        myInventory[0].Quantity -= Convert.ToInt32(storeBuyQTY * Convert.ToInt32(storeInventory[i].CostValue * 1.25));
                                                        storeInventory[0].Quantity += Convert.ToInt32(storeBuyQTY * Convert.ToInt32(storeInventory[i].CostValue * 1.25));
                                                        myInventory[i].Quantity += storeBuyQTY;
                                                        storeInventory[i].Quantity -= storeBuyQTY;
                                                        Console.WriteLine($"You give the vendor {Convert.ToInt32(storeBuyQTY * Convert.ToInt32(storeInventory[i].CostValue * 1.25))} gold, and now have {myInventory[i].Quantity} {myInventory[i].Name}.");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                break;

                            // sell wares
                            case string s when s == "2" || s == "sell":
                                Console.WriteLine("What would you like to sell?");
                                Console.WriteLine($"Vendor's current gold: {storeInventory[0].Quantity}");
                                int sellRow = 0;
                                for (int i = 1; i < myInventory.Length; i++)
                                {
                                    storeInventory[i].Row = 0;
                                    if (storeInventory[i].WillBuy == "TRUE" && myInventory[i].Quantity > 0)
                                    {
                                        sellRow++;
                                        storeInventory[i].Row = sellRow;
                                        if (storeInventory[i].Type == "drink" || storeInventory[i].Type == "food")
                                        {
                                            Console.WriteLine($"{sellRow}: {myInventory[i].Name} ({myInventory[i].Quantity} servings available to sell for {myInventory[i].CostValue} gold each.");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{sellRow}: {myInventory[i].Name} ({myInventory[i].Quantity} available to sell for {myInventory[i].CostValue} gold each.");
                                        }
                                    }
                                }

                                // allow plyer to exit trade
                                Console.WriteLine("Type 'back' to exit sale.");
                                string storeSellInput = Console.ReadLine().ToLower();
                                if (storeSellInput == "back")
                                {
                                    break;
                                }

                                // check what can be bought, and buy if possible
                                for (int i = 0; i < myInventory.Length; i++)
                                {
                                    if (myInventory[i].Name == storeSellInput || Convert.ToString(storeInventory[i].Row) == storeSellInput)
                                    {
                                        if (myInventory[i].Quantity == 0)
                                        {
                                            Console.WriteLine($"You don't have any {storeSellInput} to sell.");
                                        }
                                        else
                                        {
                                            if (Convert.ToInt32(myInventory[i].CostValue) > storeInventory[0].Quantity)
                                            {
                                                Console.WriteLine($"This trader can't afford the {storeInventory[i].Name}! They only have {storeInventory[0].Quantity} gold left.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Sell how many?");
                                                int storeSellQTY = int.Parse(Console.ReadLine());
                                                if (storeSellQTY * myInventory[i].CostValue > storeInventory[0].Quantity)
                                                {
                                                    Console.WriteLine($"This trader can't afford {storeSellQTY} {myInventory[i].Name}. The most they can afford to buy is {storeInventory[0].Quantity / myInventory[i].CostValue}.");
                                                }
                                                else
                                                {
                                                    if (storeSellQTY > myInventory[i].Quantity)
                                                    {
                                                        Console.WriteLine("That is more than you have! Sell all you have of this item?");
                                                        string buyOut = Console.ReadLine().ToLower();
                                                        if (buyOut == "y" || buyOut == "yes")
                                                        {
                                                            storeInventory[0].Quantity -= Convert.ToInt32(myInventory[i].Quantity * myInventory[i].CostValue);
                                                            myInventory[0].Quantity += Convert.ToInt32(myInventory[i].Quantity * myInventory[i].CostValue);
                                                            storeInventory[i].Quantity += myInventory[i].Quantity;
                                                            Console.WriteLine($"The vendor gives you {Convert.ToInt32(myInventory[i].Quantity * storeInventory[i].CostValue)} gold, and takes your entire {myInventory[i].Name} stock.");
                                                            myInventory[i].Quantity = 0;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        storeInventory[0].Quantity -= Convert.ToInt32(storeSellQTY * storeInventory[i].CostValue);
                                                        myInventory[0].Quantity += Convert.ToInt32(storeSellQTY * storeInventory[i].CostValue);
                                                        storeInventory[i].Quantity += storeSellQTY;
                                                        myInventory[i].Quantity -= storeSellQTY;
                                                        Console.WriteLine($"The vendor gives you {Convert.ToInt32(storeSellQTY * storeInventory[i].CostValue)} gold, and takes {storeSellQTY} {myInventory[i].Name}.");
                                                    }
                                                    // TODO fix for proper English, the whole buying/selling process
                                                }
                                            }
                                        }
                                    }
                                }
                                break;

                            // check the store's inventory
                            case string s when s == "3" || s == "store":
                                Console.WriteLine("---------------------------------------------------");
                                for (int i = 1; i<storeInventory.Length; i++)
                                {
                                    if (storeInventory[i].Quantity > 0)
                                    {
                                        if(storeInventory[i].Type == "drink" || storeInventory[i].Type == "food")
                                        {
                                            Console.WriteLine($"{storeInventory[i].Name}: they have {storeInventory[i].Quantity} servings.");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{storeInventory[i].Name}: they have {storeInventory[i].Quantity}.");
                                        }
                                    }
                                }
                                Console.WriteLine("---------------------------------------------------");
                                break;

                            // check player inventory
                            case string s when s == "4" || s == "inv":
                                Console.WriteLine("---------------------------------------------------");
                                foreach (Item tmpItem in myInventory)
                                {
                                    if (tmpItem.Quantity > 0)
                                    {
                                        if (tmpItem.Type == "food" || tmpItem.Type == "drink")
                                        {
                                            Console.WriteLine($"{tmpItem.Name}: you have {tmpItem.Quantity} servings left.");
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{tmpItem.Name}: you have {tmpItem.Quantity}.");
                                        }
                                    }
                                }
                                Console.WriteLine("---------------------------------------------------");
                                break;

                            case string s when s == "5" || s == "end" || s == "leave":
                                Console.WriteLine("\"Safe travels, friend!\"");
                                if (hoursTravelled == 50)
                                {
                                    Console.WriteLine("Save inventory?");

                                }
                                else
                                {
                                Console.WriteLine("The merchant continues on their way, and soon disappears from sight.");
                                }
                                trading = false;
                                break;
                            default:
                                Console.WriteLine("That is an unknown option. Please try again.");
                                break;
                        }
                    }
                    // =================== CODE FOR TRADING END =================== //

                    // =================== CODE FOR FIGHTING START =================== //

                    // monster creation
                    Entity monster = new Entity();
                    switch (monsterName)
                    {
                        case "bandit":
                            monster = new Bandit();
                            break;
                        case "orc":
                            monster = new Orc();
                            break;
                        case "skeleton":
                            monster = new Skeleton();
                            break;
                        case "wolf":
                            monster = new Wolf();
                            break;
                    }


                    // NPC inventory creation
                    LootItem[] monsterInventory;
                    using (var reader = new StreamReader("monsterinventory.csv"))
                    {
                        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                        System.Collections.Generic.IEnumerable<LootItem> records = csv.GetRecords<LootItem>();
                        monsterInventory = records.ToArray<LootItem>();
                    }

                    //NPC inventory quantity randomizer
                    Random lootRandom = new Random();

                    for (int i = 0; i < monsterInventory.Length; i++)
                    {
                        switch (monsterName)
                        {
                            case "bandit":
                                maxQTY = monsterInventory[i].BanditMax;
                                break;
                            case "orc":
                                maxQTY = monsterInventory[i].OrcMax;
                                break;
                            case "skeleton":
                                maxQTY = monsterInventory[i].SkeletonMax;
                                break;
                            case "wolf":
                                maxQTY = monsterInventory[i].WolfMax;
                                break;
                        }
                        int lootQTY = lootRandom.Next(maxQTY);
                        monsterInventory[i].Quantity = lootQTY;
                    }

                    // checks whether any loot will be dropped (utilized after the fight is won)
                    int noLoot = 0;
                    for (int i = 0; i < monsterInventory.Length; i++)
                    {
                        noLoot += monsterInventory[i].Quantity;
                    }

                    // fight loop
                    while (monsterAlive == true)
                    {
                        // choose a weapon and attack with it
                        Console.WriteLine("\nWhich of your weapons will you attack with?");
                        Console.WriteLine("---------------------------------------------------");
                        int weapRow = 0;
                        for (int w = 0; w <myInventory.Length; w++)
                        {
                            storeInventory[w].Row = 0;
                            if (myInventory[w].Type == "weapon" && myInventory[w].Quantity > 0)
                            {
                                weapRow++;
                                storeInventory[w].Row = weapRow;
                                if (myInventory[w].HandsNeeded == 2)
                                {
                                    Console.WriteLine($"{weapRow}: {myInventory[w].Name}, which requires both hands to use, and has a {myInventory[w].Damage} damage base attack.");
                                }
                                else
                                {
                                    Console.WriteLine($"{weapRow}: {myInventory[w].Name}, which has a {myInventory[w].Damage} damage base attack.");
                                    if(myInventory[26].Quantity >= 1)
                                    {
                                        player.defense += .10;
                                    }
                                }
                            }
                        }
                        Console.WriteLine("---------------------------------------------------");
                        string weapInput = Console.ReadLine().ToLower();
                        Console.WriteLine("\n");

                        for (int i = 0; i < myInventory.Length; i++)
                        {
                            if (myInventory[i].Name == weapInput || Convert.ToString(storeInventory[i].Row) == weapInput)
                            {
                                if (myInventory[i].Quantity == 0)
                                {
                                    Console.WriteLine($"You don't have a {myInventory[i].Name}!");
                                }
                                else
                                {
                                    player.Attack();
                                    Console.WriteLine($"You attack with the {myInventory[i].Name}, dealing {Convert.ToInt32((myInventory[i].Damage + player.attack) * (1 - monster.defense))} damage to the {monster.name}.");
                                    monster.health -= Convert.ToInt32((myInventory[i].Damage + player.attack) * (1 - monster.defense));
                                    if (monster.health < 0)
                                    {
                                        monster.health = 0;
                                    }
                                    Console.WriteLine($"The {monster.name} now has {monster.health} health.");
                                }
                            }
                        }
                        if (monster.health == 0)
                        {
                            Console.WriteLine("You won the fight!");
                            fightsWon++;
                            if (fightsWon % 5 == 0)
                            {
                                Console.WriteLine("You feel stronger after all of these fights! Your attack and health have increased!");
                                player.health += 10;
                                player.maxHealth += 10;
                                level++;
                            }
                            if(noLoot == 0)
                            {
                                Console.WriteLine("\nThe body has nothing worth looting...");
                            }
                            else
                            {
                                Console.WriteLine($"\nOn the dead {monster.name}, you find the following:");
                                for (int i = 0; i < monsterInventory.Length; i++)
                                {
                                    if (monsterInventory[i].Quantity > 0)
                                    {
                                        Console.WriteLine($"{monsterInventory[i].Quantity} {monsterInventory[i].Name}");
                                        myInventory[i].Quantity += monsterInventory[i].Quantity;
                                    }
                                }
                            }
                            monsterAlive = false;
                        }
                        else if (monster.health <= 5 && monster.name == "bandit")
                        {
                            Console.WriteLine("The bandit flees for their life. Good riddance!");
                            monsterAlive = false;
                        }
                        else
                        {

                            monster.Attack();
                            Console.WriteLine($"The {monster.name} attacks you back!");
                            player.health -= Convert.ToInt32(monster.attack * (1 - player.defense));
                            if (player.health < 0)
                            {
                                player.health = 0;
                            }
                            Console.WriteLine($"You take {Convert.ToInt32(monster.attack * (1 - player.defense))} damage, and have {player.health} health left.");
                            if (player.health == 0)
                            {
                                Console.WriteLine("You lost the fight! Everything goes black...");
                                playerAlive = false;
                                monsterAlive = false;
                            }
                        }
                        if (monster.name == "bandit" && player.health <= (50 + ((level-1) * 10)) / 4 && monsterAlive == true)
                        {
                            Console.WriteLine("\n\"Don't make me kill you. Just give me what I want and we can go our seperate ways.\"");
                            Console.WriteLine("Continue fighting?");
                            string killBanditInput = Console.ReadLine().ToLower();
                            if (killBanditInput == "n" || killBanditInput == "no")
                            {
                                Console.WriteLine("\"Smart choice, bud.\"");
                                Random moneyStolenRand = new Random();
                                double moneyStolenPercent = moneyStolenRand.Next(50);
                                if (moneyStolenPercent < 10)
                                {
                                    moneyStolenPercent = 10;
                                }
                                double moneyStolen = myInventory[0].Quantity * (moneyStolenPercent / 100);
                                myInventory[0].Quantity -= Convert.ToInt32(moneyStolen);
                                Console.WriteLine($"The thief rifles around, and steals {Convert.ToInt32(moneyStolen)} gold. You now have {myInventory[0].Quantity} gold pieces.");
                                Console.WriteLine("The thief leaves. That was unfortunate...");
                                monsterAlive = false;
                            }
                        }
                    }

                    // =================== CODE FOR FIGHTING END =================== //

                }
                Console.WriteLine($"You travelled for {hoursSinceStart} hours before dying.");
                Console.WriteLine("Play again?");
                string playAgain = Console.ReadLine().ToLower();
                switch (playAgain)
                {
                    case string s when s == "yes" || s == "y":
                        // starting everything fresh after death
                        player = new Player
                        {
                            name = nameInput
                        };
                        hoursSinceStart = 0;
                        time = 8;
                        hoursTravelled = 0;
                        fightsWon = 0;

                        // resetting inventory to how it was when the game started
                        for(int i = 0; i < myInventory.Length; i++)
                        {
                            myInventory[i].Quantity = 0;
                        }
                        myInventory[0].Quantity = 100;
                        myInventory[2].Quantity = 6;
                        myInventory[4].Quantity = 3;
                        myInventory[6].Quantity = 2;
                        myInventory[8].Quantity = 3;
                        myInventory[9].Quantity = 2;
                        myInventory[10].Quantity = 2;
                        myInventory[14].Quantity = 1;
                        myInventory[23].Quantity = 2;
                        myInventory[30].Quantity = 5;
                        playerAlive = true;
                        break;
                    case string s when s == "no" || s == "n":
                        // exits game loop
                        isGameRunning = false;
                        break;
                    default:
                        Console.WriteLine("Unknown command. Please type either yes/y or no/n.");
                        break;
                }
            }
            // farewell text
            LoopEnd:
            Console.WriteLine($"Farewell, {player.name}!");
        }
    }
}
