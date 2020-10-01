using System;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace CSharpAssessment
{
    class Program
    {
        static void Main()
        {
            // Introduction text
            Console.WriteLine("Welcome, adventurer! What should I call ya?");
            string nameInput = Console.ReadLine();
            Console.WriteLine("\n");

            // Player creation
            Player player = new Player();
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
            int townNumber = 0;

            Console.WriteLine("So, d'ya want me ta run ya through how this works, or d'ya know the ropes?");
            Console.WriteLine("1: Tutorial");
            Console.WriteLine("2: Play Game");
            string tutorialInput = Console.ReadLine().ToLower();
            Console.WriteLine("\n");
            if (tutorialInput == "1" || tutorialInput == "tutorial")
            {
                Console.WriteLine("Sure thing, happy to help a new adventurer!");
                tutorial = true;
            }
            else if(tutorialInput == "2" || tutorialInput == "play")
            {
                isGameRunning = true;
            }
            else
            {
                Console.WriteLine("There were two clear choices there, friend. Seems ya could use some help...");
                tutorial = true;
            }

            // Tutorial loop
            while (tutorial)
            {
                // explanation of how to play
                Console.WriteLine("This here game is a text-based adventure only. No fancy visuals here!");
                Console.WriteLine("Any time ya need to do somethin', you'll be prompted to type somethin'.");
                Console.WriteLine("Many choices give ya the option ta type a number instead of the whole word ya need.");
                Console.WriteLine("Most of the time, though, ya can still type a word or two and get the results ya want.");
                Console.WriteLine("\nYa with me so far?");
                Console.WriteLine("1: Yes");
                Console.WriteLine("2: No");
                string tutorialOne = Console.ReadLine().ToLower();
                Console.WriteLine("\n");
                if (tutorialOne == "1" || tutorialOne == "yes")
                {
                    Console.WriteLine("Good ta hear!");
                }
                else if (tutorialOne == "2" || tutorialOne == "no")
                {
                    Console.WriteLine("Ah, gotta comedian eh? Har har.");
                }
                else
                {
                    Console.WriteLine("I'm...gonna take that as a 'no'. Maybe yer not cut out fer this...");
                    tutorial = false;
                    isGameRunning = false;
                }
                Console.WriteLine("\nMoving on! ");
            TutorialFighting: // used for a goto later in the tutorial, if the player wants this bit repeated
                // explanation of combat
                Console.WriteLine("While on the road, yer gonna have ta keep an eye on yer provisions.");
                Console.WriteLine("Yer top priority has gotta be having a weapon at all times. Never know what kinds of monsters or ne'er-do-wells ya might meet on the road!");
                Console.WriteLine("When ya meet a threat, be ready ta deal, and ta take, some damage.");
                Console.WriteLine("During a fight, both yer health and yer opponent's health will be displayed after each hit. Ya might get the chance to run, but don't count on that!");
                Console.WriteLine("Between fights, if yer health is low, get some sleep and you'll perk right up.");
                Console.WriteLine("\nStill with me?");
                Console.WriteLine("1: Yes");
                Console.WriteLine("2: No");
                string tutorialTwo = Console.ReadLine().ToLower();
                Console.WriteLine("\n");
                if (tutorialTwo == "1" || tutorialTwo == "yes")
                {
                    Console.WriteLine("Glad I'm not goin' too fast for ya!"); // this prints if the player says that they understand the tutorial so far
                }
                else if (tutorialTwo == "2" || tutorialTwo == "no")
                {
                    if (tutorialOne == "1" || tutorialOne == "yes")
                    {
                        Console.WriteLine("Oh, I'm sorry, I do talk pretty fast sometimes.");
                        goto TutorialFighting; // if the player needs combat re-explained, this takes them back to the top of the combat tutorial
                    }
                    if (tutorialOne == "2" || tutorialTwo == "no") // Humphrey, the tutorial narrator, doesn't like when people waste his time, so this checks if the player has said they don't understand twice
                    {
                        Console.WriteLine("Ya better not be messing with me. This is the second time you've said no. I'll repeat myself, but consider this a warning.");
                        goto TutorialFighting; // takes the player back to the top of the combat tutorial, begrudgingly
                    }
                }
                TutorialNeeds: // used for a goto later in the tutorial, if the player wants this bit repeated
                // explanation of basic needs
                Console.WriteLine("\nThe second most important thing ta keep in yer wagon is...well, two things. Food and water.");
                Console.WriteLine("Ya gotta take care of yourself by drinking plenty of liquids and eating food when yer hungry!");
                Console.WriteLine("(When's the last time you had a drink in real life, player?)");
                Console.WriteLine("Sometimes, you'll get a prompt sayin' that yer hungry or thirsty. Be sure to drink and/or eat when ya see this warnin'!");
                Console.WriteLine("Not doin' so will affect yer performance on the road. Sleep is the same way, though ya don't need that as often, and ya won't die from not sleepin'.");
                Console.WriteLine("Although ya can't check how yer needs are faring whenever, ya CAN check yer wares whenever by typing 'inventory'.");
                Console.WriteLine("Check yer inventory frequently, and be sure ta buy whatever ya need from any wandering traders ya might run into!");
                Console.WriteLine("\nStill with me?");
                Console.WriteLine("1: Yes");
                Console.WriteLine("2: No");
                string tutorialThree = Console.ReadLine().ToLower();
                Console.WriteLine("\n");
                if (tutorialThree == "1" || tutorialThree == "yes")
                {
                    Console.WriteLine("Yer a fast learner!"); // this prints if the player says that they understand the tutorial so far
                }
                else if (tutorialThree == "2" || tutorialThree == "no")
                {
                    if (tutorialOne == "1" || tutorialOne == "yes")
                    {
                        Console.WriteLine("Oh, I'm sorry, I do talk pretty fast sometimes."); // if the player needs basic needs re-explained, this takes them back to the top of the basic needs tutorial
                        goto TutorialNeeds;
                    }
                    if (tutorialOne == "2" || tutorialTwo == "no") // Humphrey, the tutorial narrator, doesn't like when people waste his time, so this checks if the player has said they don't understand twice
                    {
                            Console.WriteLine("Ya better not be messing with me. This is the second time you've said no. I'll repeat myself, but consider this a warning.");
                            goto TutorialNeeds; // takes the player back to the top of the basic needs tutorial, begrudgingly
                    }
                }
                Console.WriteLine("You'll get the chance to do all sorts of things on the road, like fish in rivers, trade with other travellers, and gain loot from monsters!");
                Console.WriteLine("Every so often, you'll reach a town, and the general store there should have a big selection of items to buy!");
                Console.WriteLine("It's also in towns that ya can save yer game. No implementation for starting from a save right now, but I'm sure this game maker will get there someday.");
                Console.WriteLine("\nIn any case, if ya need an idea of what ta do or what ta type as you travel, just remember ya can type 'help' or 'command list' any time to see yer possibilities.");
                Console.WriteLine("\nWell, that's about all I've got for ya. I wish you the best out there!");
                tutorial = false;
            }


            Console.WriteLine($"Safe travels then, {player.name}!");

            // Main game loop
            while (isGameRunning == true)
            {
                bool trading = false;
                string traderName = "A trader";
                bool monsterAlive = false;
                string monsterName = "A monster";

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
                    int tooHungry = 0;
                    int tooThirsty = 0;
                    int tooTired = 0;
                    switch (player.hunger) // stages of hunger
                    {
                        case int h when h >= 8 && h <= 15:
                            Console.WriteLine("Your stomach is rumbling. You should eat something.");
                            break;
                        case int h when h >= 16 && h <= 29:
                            Console.WriteLine("Your stomach is aching from being so empty! You need to eat something soon!");
                            player.attackBonus--; // the hungrier you are, the weaker you become in battle
                            tooHungry++; // used for reverting attackBonus when the player eats
                            break;
                        case int h when h >= 30 && h <= 35:
                            Console.WriteLine("Your stomach is in knots, and you can barely stand. You won't last much longer without something to eat...");
                            player.attackBonus--; // the hungrier you are, the weaker you become in battle
                            tooHungry++; // used for reverting attackBonus when the player eats
                            break;
                        case int h when h >= 36:
                            Console.WriteLine("You have died of starvation.");
                            playerAlive = false; // player dies, and this line takes the player to the option to play again
                            break;
                    }

                    switch (player.thirst)
                    {
                        case int t when t >= 8 && t <= 15:
                            Console.WriteLine("Your mouth feels dry. You should drink something.");
                            break;
                        case int t when t >= 16 && t <= 29:
                            Console.WriteLine("Your head is spinning due to how dehydrated you are! You need to drink something soon!");
                            player.health--; // the thirstier you are, the less healthy you become
                            tooThirsty++; // used for reverting health when the player drinks
                            break;
                        case int t when t >= 30 && t <= 35:
                            Console.WriteLine($"Your head throbs, and your throat aches. You won't last much longer without a drink...");
                            player.health--; // the thirstier you are, the less healthy you become
                            tooThirsty++; // used for reverting health when the player drinks
                            break;
                        case int t when t >= 36:
                            Console.WriteLine("You have died of dehydration.");
                            playerAlive = false; // player dies, and this line takes the player to the option to play again
                            break;
                    }

                    switch (player.fatigue)
                    {
                        case int f when f >= 16 && f <= 23:
                            Console.WriteLine("You're feeling pretty tired. You should get some sleep.");
                            break;
                        case int f when f >= 24 && f <= 40:
                            Console.WriteLine("You're feeling exhausted! You need to sleep soon!");
                            player.fightsWon--; // the more tired you are, the more your level suffers
                            tooTired++; // used for reverting fightsWon, and subsequently level, when the player sleeps
                            break;
                        case int f when f >= 41 && f <= 47:
                            Console.WriteLine("You can barely keep your eyes open, and your head keeps drooping. You won't be able to stay awake much longer...");
                            player.fightsWon--; // the more tired you are, the more your level suffers
                            tooTired++; // used for reverting fightsWon, and subsequently level, when the player sleeps
                            break;
                        case int f when f >= 48:
                            Console.WriteLine("Your eyelids droop, and everything goes dark..."); // the player can't die of fatigue, but they will pass out
                            Random passOut = new Random(); // the number of hours that the player is unconcious is random, between 4 and 16
                            int passOutHours = passOut.Next(12) + 4;
                            player.fatigue -= passOutHours;
                            // THE PLAYER CAN DIE 
                            player.hunger += passOutHours;
                            player.thirst += passOutHours;
                            hoursSinceStart += passOutHours;
                            time += passOutHours;
                            player.drunkenness -= passOutHours;
                            Console.WriteLine("...");
                            Console.WriteLine($"You wake up after being asleep for {passOutHours} hours.");
                            break;
                    }
                    if(player.health < player.maxHealth / 4) // a warning about the player's low health
                    {
                        Console.WriteLine($"You have lost a lot of blood from the fights you've been in. I know that rationally sleep won't fix that, but this is a game, so maybe get some sleep!");
                    }
                    else if (player.health < player.maxHealth / 2) // a warning about the player's VERY low health
                    {
                        Console.WriteLine($"You are in quite a bit of pain. It might be a good idea to sleep for a little while.");
                    }


                    Console.WriteLine($"Your current health: {player.health}/{player.maxHealth}"); // health is always printed here before asking the player what they want to do, so they have an idea if they should spend time healing

                    if (hoursTravelled == 24)
                    {
                        // reaching a town
                        Console.WriteLine("\nYou've made it to a small town! Here, you can visit a general store.");
                        townNumber++;
                        Console.WriteLine("1: Visit store");
                        Console.WriteLine("2: Continue travelling");
                        int townInput = int.Parse(Console.ReadLine());
                        Console.WriteLine("\n");
                        switch (townInput)
                        {
                            case 1:
                                Console.WriteLine("\"Welcome, traveller! How can I help you today?\"");
                                traderName = "The general store merchant";
                                trading = true;
                                goto Trading;
                            case 2:
                                break;
                        }
                    }

                    // =================== CODE FOR PLAYER COMMANDS START =================== //
                    // player input
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
                    if (gameInput == "commands" || gameInput == "command list")
                    {
                        Console.WriteLine("Command List:");
                        Console.WriteLine("Help - if you're especially lost");
                        Console.WriteLine("Inventory - if you're wondering what you have");
                        Console.WriteLine("Inv - if you're not up to typing all of the word 'inventory'");
                        Console.WriteLine("Check Stats - if you're curious about your health or fighting stats");
                        Console.WriteLine("Stats - if you're not wanting to type 'check' as well as 'stats'");
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
                                    Console.WriteLine($"{tmpItem.Quantity}x servings of {tmpItem.Name}: {tmpItem.Description}."); // adds the word 'servings' to food and drink items when printing
                                }
                                else
                                {
                                    Console.WriteLine($"{tmpItem.Quantity}x {tmpItem.Name}: {tmpItem.Description}.");
                                }
                            }
                        }
                        Console.WriteLine("---------------------------------------------------");
                    }

                    if(gameInput == "check stats" || gameInput == "stats")
                    {
                        Console.WriteLine($"Player max health: {player.maxHealth}");
                        Console.WriteLine($"Player current health: {player.health}");
                        Console.WriteLine($"Player level: {player.levelsGained+1}");
                        Console.WriteLine($"Player attack bonus: {player.attackBonus}");
                        Console.WriteLine($"Fights won: {player.fightsWon}");
                    }
                    
                    // eating
                    if (gameInput == "eat")
                    {
                        bool letMeEat = false;
                        if (player.hunger == 0) // checks if the player is NOT hungry
                        {
                            Console.WriteLine("You don't feel hungry. Are you sure you want to eat?"); // checks if the player insists on eating when not hungry
                            Console.WriteLine("1: Yes");
                            Console.WriteLine("2: No");
                            string eatAnyways = Console.ReadLine();
                            Console.WriteLine("\n");
                            if (eatAnyways == "yes" || eatAnyways == "y" || eatAnyways == "drink" || Convert.ToString(eatAnyways) == "1")
                            {
                                letMeEat = true; // if the player insists on eating, the dialogue will continue as normal
                            }
                        }
                        if (player.hunger != 0 || letMeEat == true) // checks if the player IS hungry, or if they insisted on eating if they AREN'T hungry
                        {
                            int eatRow = 0;
                            Console.WriteLine("What would you like to eat?");
                            for (int i = 0; i < myInventory.Length; i++)
                            {
                                myInventory[i].Row = 0;
                                if (myInventory[i].Type == "food" && myInventory[i].Quantity > 0) // checks inventory for food, and prints all food items that the player has available
                                {
                                    eatRow++;
                                    myInventory[i].Row = eatRow;
                                    Console.WriteLine($"{myInventory[i].Row}: {myInventory[i].Name}, of which you have {myInventory[i].Quantity} servings left.");
                                }
                            }
                            string eatInput = Console.ReadLine().ToLower();
                            Console.WriteLine("\n");

                            // change quantity of food
                            for (int i = 0; i < myInventory.Length; i++)
                            {
                                if (myInventory[i].Name == eatInput || Convert.ToString(myInventory[i].Row) == eatInput)
                                {
                                    if (myInventory[i].Quantity == 0) // in case the player types in a food they don't possess
                                    {
                                        Console.WriteLine($"You don't have any servings of {myInventory[i].Name}.");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"You eat {myInventory[i].Name} to satiate your hunger.");
                                        bool eating = true;
                                        while (eating == true) // this while loop ensures that the player eats the food that they've chosen until their hunger level is <= 4, or until they've eaten all they have of said item
                                        {
                                            player.hunger -= myInventory[i].HungerBoost;
                                            player.thirst -= myInventory[i].ThirstBoost;
                                            myInventory[i].Quantity--;
                                            player.attackBonus += tooHungry;
                                            tooHungry = 0;

                                            if (myInventory[i].Quantity == 0) // the player stops eating if they run out of the item that they are eating
                                            {
                                                eating = false;
                                            }
                                            if (player.hunger <= 4) // the player stops eating if their hunger becomes 4 or less
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
                                            Console.WriteLine("That wasn't quite enough food..."); // if the player ate all that they have of a food item, and they are still hungry, this will print
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // drinking
                    if (gameInput == "drink")
                    {
                        bool letMeDrink = false;
                        if (player.thirst == 0) // checks if the player is NOT thirsty
                        {
                            Console.WriteLine("You don't feel thirsty. Are you sure you want to drink?"); // checks if the player insists on drinking when not thirsty
                            Console.WriteLine("1: Yes");
                            Console.WriteLine("2: No");
                            string drinkAnyways = Console.ReadLine();
                            Console.WriteLine("\n");
                            if (drinkAnyways == "yes" || drinkAnyways == "y" || drinkAnyways == "drink" || Convert.ToString(drinkAnyways) == "1")
                            {
                                letMeDrink = true; // if the player insists on drinking, the dialogue will continue as normal
                            }
                        }
                        if (player.thirst != 0 || letMeDrink == true) // checks if the player IS thirsty, or if they insisted on drinking if they AREN'T thirsty
                        {
                            int drinkRow = 0;
                            Console.WriteLine("What would you like to drink?");
                            for (int i = 0; i < myInventory.Length; i++)
                            {
                                myInventory[i].Row = 0;
                                if (myInventory[i].Type == "drink" && myInventory[i].Quantity > 0) // checks inventory for drinks, and prints all drink items that the player has available
                                {
                                    drinkRow++;
                                    myInventory[i].Row = drinkRow;
                                    Console.WriteLine($"{myInventory[i].Row}: {myInventory[i].Name}, of which you have {myInventory[i].Quantity} servings left."); ;
                                }
                            }
                            string drinkInput = Console.ReadLine().ToLower();
                            Console.WriteLine("\n");

                            // change quantity of beverage
                            for (int i = 0; i < myInventory.Length; i++)
                            {
                                if (myInventory[i].Name == drinkInput || Convert.ToString(myInventory[i].Row) == drinkInput)
                                {
                                    if (myInventory[i].Quantity == 0) // in case the player choose a drink they don't possess
                                    {
                                        Console.WriteLine($"You don't have any servings of {myInventory[i].Name}.");
                                    }
                                    else
                                    {
                                        if (myInventory[i].Name == "health potion") // checks if what was drunk is a health potion
                                        {
                                            Console.WriteLine($"You drink a healing potion.");
                                            player.health += 20; // 20 health is added to the player's current health for consuming a health potion
                                            if (player.health > player.maxHealth)
                                            {
                                                player.health = player.maxHealth; // in case the player's health was less than 20 below their max, so they don't go over their max
                                            }
                                            Console.WriteLine($"You regain 20 health, and are now at {player.health} health!");
                                            player.hunger -= myInventory[i].HungerBoost;
                                            player.thirst -= myInventory[i].ThirstBoost;
                                            myInventory[i].Quantity--;
                                            player.health += tooThirsty;
                                            if(player.health > player.maxHealth)
                                            {
                                                player.health = player.maxHealth;
                                            }
                                            tooThirsty = 0;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"You drink {myInventory[i].Name} to quench your thirst.");
                                            bool drinking = true;
                                            while (drinking == true) // this while loop ensures that the player drinks the drink that they've chosen until their thirst level is <= 4, or until they've drunk all they have of said item
                                            {
                                                player.hunger -= myInventory[i].HungerBoost;
                                                player.thirst -= myInventory[i].ThirstBoost;
                                                player.drunkenness += myInventory[i].Alcohol;
                                                myInventory[i].Quantity--;

                                                if(myInventory[i].Quantity == 0) // the player stops drinking if they run out of the item that they are drinking
                                                {
                                                    drinking = false;
                                                }
                                                if(player.thirst <= 4) // the player stops drinking if their thirst becomes 4 or less
                                                {
                                                    drinking = false;
                                                }
                                            }
                                            if(myInventory[i].Quantity < 0)
                                            {
                                                myInventory[i].Quantity = 0;
                                            }
                                        }
                                        switch (player.drunkenness) // checks the alcohol content of a drink, and adds to the players drunkenness score
                                        {
                                            case double d when d > 0 && d <= 3:
                                                Console.WriteLine("You feel a slight buzz after that drink!");
                                                break;
                                            case double d when d >= 4 && d <= 9:
                                                Console.WriteLine("You feel great! You're on top of the world! If only the world would stop spinning..."); // a warning to stop drinking alcohol
                                                break;
                                            case double d when d >= 10:
                                                Console.WriteLine("Everything goes dark..."); // after drinking way too much alcohol, the player will pass out for a random number of hours between 4 and 12
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
                                        }
                                        if (player.thirst >= 8)
                                        {
                                            Console.WriteLine("That wasn't quite enough to drink..."); // if the player drank all that they have of a drink item, and they are still thirsty, this will print
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // sleeping
                    if (gameInput == "sleep")
                    {
                        Console.WriteLine("Sleep for how many hours?");
                        int sleepInput = int.Parse(Console.ReadLine());
                        Console.WriteLine("\n");
                        player.fatigue -= sleepInput;
                        if (player.fatigue < 0)
                        {
                            player.fatigue = 0;
                        }

                        // although the player is becoming less fatigued, time is still going by, and causing hunger and thirst to go up, while also increasing health and decreasing drunkenness
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
                        player.fightsWon += tooTired;
                        tooTired = 0;
                        Console.WriteLine($"You sleep for {sleepInput} hours, and awaken feeling more rested.");
                        Random sleepEncounter = new Random();
                    }

                    // allows the player to quit the game
                    if (gameInput == "quit")
                    {
                        Console.WriteLine("Are you sure you want to quit?");
                        string quitInput = Console.ReadLine().ToLower();
                        Console.WriteLine("\n");
                        if (quitInput == "yes" || quitInput == "y")
                        {
                            goto LoopEnd;
                        }
                    }

                    // travelling
                    if (gameInput == "travel")
                    {
                        // increases time, need for food, need for water, need for sleep, and health, while decreasing drunkenness
                        hoursSinceStart++;
                        time++;
                        player.hunger++;
                        player.thirst++;
                        player.fatigue++;
                        player.drunkenness -= 0.5;
                        hoursTravelled++;
                        player.health++;
                        if(player.health > player.maxHealth)
                        {
                            player.health = player.maxHealth;
                        }

                        // random travel encounter if you don't make it to a town
                        Random travelEncounter = new Random();
                        int newEncounter = travelEncounter.Next(20);

                        switch (newEncounter)
                        {
                            case 6: // when the random encounter number is 6, they pass a river where they can collect water or fish for food
                                Console.WriteLine("You pass by a river on your travels. You can go fishing or collect some water here!");
                                bool nearRiver = true;
                                while (nearRiver == true)
                                {
                                    int stayNearRiver = 0;
                                    Console.WriteLine("1: Go fishing");
                                    Console.WriteLine("2: Collect water");
                                    Console.WriteLine("3: Continue travelling");
                                    int riverInput = int.Parse(Console.ReadLine());
                                    Console.WriteLine("\n");
                                    switch (riverInput)
                                    {
                                        case 1: // the below happens if the player chooses to go fishing
                                            if(myInventory[14].Quantity == 0) // if the player doesn't have a fishing pole, they can't fish
                                            {
                                                Console.WriteLine("You don't have a fishing pole...maybe buy one next time you see one!");
                                            }
                                            else
                                            {
                                                Random fishRand = new Random(); // creates a random number of fish for the player to catch in one hour
                                                int fishCaught = fishRand.Next(3);
                                                time++;
                                                hoursSinceStart++;
                                                player.hunger++;
                                                player.thirst++;
                                                player.fatigue++;
                                                myInventory[13].Quantity += fishCaught;
                                                if (fishCaught == 0)
                                                {
                                                    Console.WriteLine("You fish for an hour, but don't catch anything..."); // if the random number of fish is 0
                                                }
                                                else if (fishCaught == 1)
                                                {
                                                    Console.WriteLine($"You fish for an hour, and catch {fishCaught} serving worth of fish!"); // if the random number of fish is 1 (seperate from more than 1 for the wording of serving vs servings)
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"You fish for an hour, and catch {fishCaught} servings worth of fish!"); // if the random number of fish is more than 1
                                                }
                                                Console.WriteLine("Stay near river?"); // allows player to choose if they want to stay by the river, to keep collecting water or continue fishing
                                                Console.WriteLine("1: Stay");
                                                Console.WriteLine("2: Continue travelling");
                                                stayNearRiver = int.Parse(Console.ReadLine());
                                                Console.WriteLine("\n");
                                                if (stayNearRiver == 2)
                                                {
                                                    nearRiver = false;
                                                }
                                            }
                                            break;
                                        case 2: // the below happens if the player chooses to collect drinking water
                                            Console.WriteLine("You can get 5 servings of water per hour, as it takes time to make the water drinkable.");
                                            Console.WriteLine("How many hours would you like to spend collecting water?");
                                            int waterCollect = int.Parse(Console.ReadLine()); // after telling the player that they get 5 servings of water each hour, it asks the player how much time they want to spend getting water
                                            Console.WriteLine("\n");
                                            // time passes, and needs increase, as the player collects water
                                            time += waterCollect;
                                            hoursSinceStart += waterCollect;
                                            player.hunger += waterCollect;
                                            player.thirst += waterCollect;
                                            player.fatigue += waterCollect;
                                            myInventory[26].Quantity += waterCollect * 5;
                                            Console.WriteLine($"You collect {waterCollect * 5} servings of drinking water.");
                                            Console.WriteLine("\nStay near river?"); // allows player to choose if they want to stay by the river, to keep collecting water or continue fishing
                                            Console.WriteLine("1: Stay");
                                            Console.WriteLine("2: Continue travelling");
                                            stayNearRiver = int.Parse(Console.ReadLine());
                                            Console.WriteLine("\n");
                                            if (stayNearRiver == 2)
                                            {
                                                nearRiver = false;
                                            }
                                            break;
                                        case 3: // in case the player doesn't want to stop by the river at all
                                            nearRiver = false;
                                            break;
                                    }
                                }
                                break;
                            case 7: // when the random encounter number is 7, a traveller passes by, but doesn't want to trade. I felt this added some realism
                                Console.WriteLine("You pass another traveller on the path.");
                                Console.WriteLine("\"A trade? No, I have everything I need, thank you.\"");
                                break;
                            case 8: // when the random encounter number is 8, a wagon wheel breaks, and wasts an hour of your time. An omage to Oregon Trail that is nicer than dysentary
                                Console.WriteLine("A wheel on your wagon broke! It takes you an hour to fix.");
                                time++;
                                hoursSinceStart++;
                                break;
                            case int e when e == 9 || e == 10: // when the random encounter number is 9 or 10, a bandit appears, and will steal gold or fight the player
                                Console.WriteLine("A bandit appears!");
                                Console.WriteLine("\"Alright, bud, we can do this the easy way or the hard way.\"");
                                bool beingRobbed = true;

                                while (beingRobbed == true)
                                {
                                    Console.WriteLine("Will you allow the thief to steal from you, or fight for your gold?"); // the choice to fight or be stolen from
                                    Console.WriteLine("1: Allow");
                                    Console.WriteLine("2: Fight");
                                    string banditInput = Console.ReadLine().ToLower();
                                    Console.WriteLine("\n");

                                    if (banditInput == "allow" || Convert.ToString(banditInput) == "1") // if the player chooses to let the bandit have what they want
                                    {
                                        Console.WriteLine("\"Smart choice, bud.\"");
                                        Random moneyStolenRand = new Random();
                                        double moneyStolenPercent = moneyStolenRand.Next(33); // a random number between 0 and 33 is chosen
                                        if (moneyStolenPercent < 10) // in case the random number above is less than 10, it is made to be 10
                                        {
                                            moneyStolenPercent = 10;
                                        }
                                        double moneyStolen = myInventory[0].Quantity * (moneyStolenPercent / 100); // the number between 10 andn33 is a % of the player's gold to steal, so the player never loses more than a third of their gold to the bandit
                                        myInventory[0].Quantity -= Convert.ToInt32(moneyStolen);
                                        Console.WriteLine($"The thief rifles around, and steals {Convert.ToInt32(moneyStolen)} gold. You now have {myInventory[0].Quantity} gold pieces."); // gold is removed from the players inventory
                                        Console.WriteLine("The thief leaves. That was unfortunate...");
                                        beingRobbed = false;
                                    }
                                    else if (banditInput == "fight" || Convert.ToString(banditInput) == "2") // if the player chooses to fight
                                    {
                                        Console.WriteLine("\n\"So you want to do this the hard way, eh?\"");
                                        monsterName = "bandit";
                                        monsterAlive = true; // the fight loop begins
                                        beingRobbed = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("That's an unknown command. The thief has a knife pointed at you, so choose to allow or fight quickly!"); // in case the player doesn't choose to fight or allow the theft, the beingRobbed loop doesn't end
                                    }
                                }
                                break;
                            case 11: // when the random encounter number is 11, an orc appears
                                Console.WriteLine("An orc warrior appears!");
                                Console.WriteLine("\"Prepare to die, milkdrinker!\"");
                                if (player.levelsGained == 0) // a level 1 player literally cannot beat an orc without dying, so a warning is printed
                                {
                                    Console.WriteLine("He's way more powerful than you are! You feel the strong urge to flee from this fight before it starts.");
                                    Console.WriteLine("1: Flee");
                                    Console.WriteLine("2: Fight");
                                    string orcLevelOne = Console.ReadLine().ToLower();
                                    Console.WriteLine("\n");
                                    if (orcLevelOne == "flee" || Convert.ToString(orcLevelOne) == "1") // if the level 1 player flees, 2 hours go by but they lose no health
                                    {
                                        Console.WriteLine("You turn your cart on a dime and take another path! The orc gives chase for a while, but finally leaves you alone.");
                                        Console.WriteLine("This detour costs you 2 hours, but saves you your skin.");
                                        time += 2;
                                        hoursSinceStart += 2;
                                    }
                                    else if (orcLevelOne == "fight" || Convert.ToString(orcLevelOne) == "2") // if the level 1 player decides to fight, the fight begins
                                    {
                                        Console.WriteLine("You were warned..."); // the level 1 player is told that they were warned, before their certain death
                                        monsterName = "orc";
                                        monsterAlive = true; // the fight loop begins
                                    }
                                    break;
                                }
                                else // if the player isn't level 1, they don't get the option to flee
                                {
                                    monsterName = "orc";
                                    monsterAlive = true; // the fight loop begins
                                    break;
                                }
                            case int e when e == 12 || e == 13: // when the random encounter number is 12 or 13, a skeleton appears, and a fight starts
                                Console.WriteLine("A living skeleton appears, and it's brandishing a weapon!");
                                monsterName = "skeleton";
                                monsterAlive = true; // the fight loop begins
                                break;
                            case int e when e == 14 || e == 15: // when the random encounter number is 14 or 15, a wolf appears, and a fight starts
                                Console.WriteLine("A snarling wolf appears, and it appears hungry!");
                                monsterName = "wolf";
                                monsterAlive = true; // the fight loop begins
                                break;
                            case int e when e == 16 || e == 17: // // when the random encounter number is 16 or 17, a food vendor appears
                                Console.WriteLine("You pass another merchant on the road.");
                                Console.WriteLine("\"Ho there, friend. Care to exchange goods?\"");
                                traderName = "A food vendor";
                                Console.WriteLine($"{traderName} wants to trade with you!"); // letting the player know this is a food vendor
                                trading = true; // the trading loop begins
                                break;
                            case int e when e == 18: // when the random encounter number is 18, a weapons vendor appears
                                Console.WriteLine("You pass another merchant on the road.");
                                Console.WriteLine("\"Ho there, friend. Care to exchange goods?\"");
                                traderName = "A weapons vendor";
                                Console.WriteLine($"{traderName} wants to trade with you!"); // letting the player know this is a weapons vendor
                                trading = true; // the trading loop begins
                                break;
                            case int e when e == 19 || e == 20: // when the random encounter number is 19 or 20, an adventurer appears
                                Console.WriteLine("You pass another traveller on the path.");
                                Console.WriteLine("\"A trade? Sure, let's see what you have.\"");
                                traderName = "An adventurer";
                                Console.WriteLine($"{traderName} wants to trade with you!"); // letting the player know that this is an adventurer
                                trading = true; // the trading loop begins
                                break;
                            default: // when the random encounter number is 0-5, nothing happens
                                Console.WriteLine("The road is clear ahead.");
                                break;
                        }
                    }
                    // =================== CODE FOR PLAYER COMMANDS END =================== //

                    // =================== CODE FOR TRADING START =================== //

                    Trading:
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
                        switch (traderName) // determines the store inventory based on the type of vendor
                        {
                            case "The general store merchant":
                                maxQTY = storeInventory[i].GenStoreMax;
                                willBuy = storeInventory[i].GenStoreWillBuy;
                                break;
                            case "A food vendor":
                                maxQTY = storeInventory[i].FoodVendorMax;
                                willBuy = storeInventory[i].FoodVendorWillBuy;
                                break;
                            case "A weapons vendor":
                                maxQTY = storeInventory[i].SmithMax;
                                willBuy = storeInventory[i].SmithWillBuy;
                                break;
                            case "An adventurer":
                                maxQTY = storeInventory[i].AdventurerMax;
                                willBuy = storeInventory[i].AdventurerWillBuy;
                                break;
                        }
                        int storeQTY = storeRandom.Next(maxQTY); // randomizes the amount of each item in the store, where the max the random number can be is the max that they particular type of vendor can have in their store
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
                        if (traderName == "The general store merchant" && storeInventory[0].Quantity < 100)
                        {
                            storeInventory[0].Quantity = 100; // ensures that a general store always has at least 100 gold, as they can buy anything and it makes reaching them nicer as the player can get rid of unwanted items
                        }
                        else if (storeInventory[0].Quantity < 5)
                        {
                            storeInventory[0].Quantity = 5; // ensures that any store always has at least 5 gold
                        }

                        Console.WriteLine("\nWhat would you like to do?"); // options for what to do while in the trading loop
                        Console.WriteLine("1: Buy");
                        Console.WriteLine("2: Sell");
                        Console.WriteLine("3: View their inventory");
                        Console.WriteLine("4: View your inventory");
                        Console.WriteLine("5: End trade");
                        string tradeInput = Console.ReadLine().ToLower();
                        Console.WriteLine("\n");
                        switch (tradeInput)
                        {
                            // purchase wares
                            case string s when s == "1" || s == "buy":
                                Console.WriteLine("What would you like to buy?");
                                Console.WriteLine($"Your current gold: {myInventory[0].Quantity}"); // prints the player's current gold, so they know what they can afford
                                int buyRow = 0;
                                Console.WriteLine("---------------------------------------------------");
                                for (int i = 1; i < storeInventory.Length; i++) // prints each item that is in the store, excluding gold
                                {
                                    storeInventory[i].Row = 0;
                                    if (storeInventory[i].Quantity > 0)
                                    {
                                        buyRow++;
                                        storeInventory[i].Row = buyRow;
                                        if (storeInventory[i].Type == "drink" || storeInventory[i].Type == "food")
                                        {
                                            Console.WriteLine($"{buyRow}: {storeInventory[i].Name} ({storeInventory[i].Quantity} servings available to buy for {Math.Round(storeInventory[i].CostValue*1.25)} gold each.)"); // adjusts wording for food and drink items to contain the word 'servings'
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{buyRow}: {storeInventory[i].Name} ({storeInventory[i].Quantity} available to buy for {Math.Round(storeInventory[i].CostValue * 1.25)} gold each.)");
                                        }
                                    }
                                }
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine("Type 'description' to see an item's description."); // allows player to check item descriptions
                                Console.WriteLine("Type 'back' to exit purchase."); // allows player to exit trade
                                string storeBuyInput = Console.ReadLine().ToLower();
                                Console.WriteLine("\n");
                                if (storeBuyInput == "description")
                                {
                                    Console.WriteLine("Check description for which item?"); // allows the player to check item descriptions individually
                                    string checkDescription = Console.ReadLine();
                                    Console.WriteLine("\n");
                                    for (int i = 1; i < storeInventory.Length; i++)
                                    {
                                        if (storeInventory[i].Name == checkDescription)
                                        {
                                            Console.WriteLine($"\n{storeInventory[i].Description}"); // prints description of item chosen by the player
                                            if(storeInventory[i].HungerBoost > 0) // if the item has a hunger reduction, it prints that amount here
                                            {
                                                Console.WriteLine($"Hunger reduction: {storeInventory[i].HungerBoost}");
                                            }
                                            if (storeInventory[i].ThirstBoost > 0) // if the item has a thirst reduction, it prints that amount here
                                            {
                                                Console.WriteLine($"Thirst reduction: {storeInventory[i].ThirstBoost}");
                                            }
                                            if (storeInventory[i].Alcohol > 0) // if the item has an alcohol content, it prints that amount here
                                            {
                                                Console.WriteLine($"Alcohol content: storeInventory[i].Alcohol");
                                            }
                                            if (storeInventory[i].Damage > 0) // if the item has a base damage, it prints that amount here
                                            {
                                                Console.WriteLine($"Base damage: {storeInventory[i].Damage}");
                                            }
                                            if (storeInventory[i].HandsNeeded > 0) // if the item is a weapon, it prints the number of hands required here. I WAS GOING TO HAVE THIS AFFECT HOW A SHIELD WOULD WORK, BUT REMOVED THAT SO THIS HAS NO IN-GAME PURPOSE CURRENTLY
                                            {
                                                Console.WriteLine($"Number of hands required to use: {storeInventory[i].HandsNeeded}");
                                            }
                                        }
                                    }
                                }
                                if (storeBuyInput == "back") // allows the player to exit the sale
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
                                            Console.WriteLine($"This vendor doesn't have any {storeBuyInput} for sale."); // if the player chooses an item that isn't in the store's inventory, the player is told so
                                        }
                                        else
                                        {
                                            if(Convert.ToInt32(storeInventory[i].CostValue * 1.25) > myInventory[0].Quantity)
                                            {
                                                Console.WriteLine($"You can't afford the {storeInventory[i].Name}! You are short {Convert.ToInt32(storeInventory[i].CostValue * 1.25) - myInventory[0].Quantity} gold to buy it. Try selling something if you really want this!"); // if the player can't afford the item they want, this tells them how much more moeny they need
                                            }
                                            else
                                            {
                                                Console.WriteLine("Buy how many?"); // allows the player to buy more than 1 of an item
                                                int storeBuyQTY = int.Parse(Console.ReadLine());
                                                Console.WriteLine("\n");
                                                if (Convert.ToInt32(storeBuyQTY * storeInventory[i].CostValue * 1.25) > myInventory[0].Quantity)
                                                {
                                                    Console.WriteLine($"You can't afford {storeBuyQTY} {storeInventory[i].Name}!"); // if the player can't afford the number they want of the item they chose, they are told so
                                                }
                                                else
                                                {
                                                    if (storeBuyQTY > storeInventory[i].Quantity)
                                                    {
                                                        Console.WriteLine("That is more than this vendor has! Buy them out of this item?"); // if the player wants more of an item than the vendor has, they can buy the store's entire stock
                                                        string buyOut = Console.ReadLine().ToLower();
                                                        Console.WriteLine("\n");
                                                        if (buyOut == "y" || buyOut == "yes") // if the player decides to buy the vendor out of the item they chose, the total number of that item from the store's inventory is moved to the player's inventory, and gold is removed from the player and given to the store
                                                        {
                                                            myInventory[0].Quantity -= Convert.ToInt32(storeInventory[i].Quantity * Convert.ToInt32(storeInventory[i].CostValue * 1.25));
                                                            storeInventory[0].Quantity += Convert.ToInt32(storeInventory[i].Quantity * Convert.ToInt32(storeInventory[i].CostValue * 1.25));
                                                            myInventory[i].Quantity += storeInventory[i].Quantity;
                                                            Console.WriteLine($"You give the vendor {Convert.ToInt32(storeInventory[i].Quantity * Convert.ToInt32(storeInventory[i].CostValue * 1.25))} gold, and now have {myInventory[i].Quantity} {myInventory[i].Name}.");
                                                            storeInventory[i].Quantity = 0;
                                                        }
                                                    }
                                                    else // if the player can afford the number of the item that they want, and the store has enough of said item, the item is moved into the player's inventory, and the appropriate amount of gold is given to the store
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
                                Console.WriteLine($"Vendor's current gold: {storeInventory[0].Quantity}"); // prints the vendor's gold so that the player knows what they can afford
                                int sellRow = 0;
                                Console.WriteLine("---------------------------------------------------");
                                for (int i = 1; i < myInventory.Length; i++)
                                {
                                    storeInventory[i].Row = 0;
                                    if (storeInventory[i].WillBuy == "TRUE" && myInventory[i].Quantity > 0) // prints each item that the player has in their inventory that the vendor is intrested in buying, per the storeInventory csv file
                                    {
                                        sellRow++;
                                        storeInventory[i].Row = sellRow;
                                        if (storeInventory[i].Type == "drink" || storeInventory[i].Type == "food")
                                        {
                                            Console.WriteLine($"{sellRow}: {myInventory[i].Name} ({myInventory[i].Quantity} servings available to sell for {myInventory[i].CostValue} gold each.)"); // adds the word 'servings' to food and drink items
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{sellRow}: {myInventory[i].Name} ({myInventory[i].Quantity} available to sell for {myInventory[i].CostValue} gold each.)");
                                        }
                                    }
                                }
                                Console.WriteLine("---------------------------------------------------");
                                Console.WriteLine("Type 'description' to check an item's description"); // allows player to check item description
                                Console.WriteLine("Type 'back' to exit sale."); // allows player to exit trade
                                string storeSellInput = Console.ReadLine().ToLower();
                                Console.WriteLine("\n");
                                if (storeSellInput == "description")
                                {
                                    Console.WriteLine("Check description for which item?"); // prints the description for the item the player chose
                                    string checkDescription = Console.ReadLine();
                                    Console.WriteLine("\n");
                                    for (int i = 1; i < storeInventory.Length; i++)
                                    {
                                        if (storeInventory[i].Name == checkDescription)
                                        {
                                            Console.WriteLine($"\n{storeInventory[i].Description}");
                                            if (storeInventory[i].HungerBoost > 0)
                                            {
                                                Console.WriteLine($"Hunger reduction: {storeInventory[i].HungerBoost}"); // if the item has a hunger reduction, it prints that amount here
                                            }
                                            if (storeInventory[i].ThirstBoost > 0)
                                            {
                                                Console.WriteLine($"Thirst reduction: {storeInventory[i].ThirstBoost}"); // if the item has a thirst reduction, it prints that amount here
                                            }
                                            if (storeInventory[i].Alcohol > 0) // if the item has an alcohol content, it prints that amount here
                                            {
                                                Console.WriteLine($"Alcohol content: storeInventory[i].Alcohol");
                                            }
                                            if (storeInventory[i].Damage > 0) // if the item has a base damage, it prints that amount here
                                            {
                                                Console.WriteLine($"Base damage: {storeInventory[i].Damage}");
                                            }
                                            if (storeInventory[i].HandsNeeded > 0) // if the item is a weapon, it prints the number of hands required here. I WAS GOING TO HAVE THIS AFFECT HOW A SHIELD WOULD WORK, BUT REMOVED THAT SO THIS HAS NO IN-GAME PURPOSE CURRENTLY
                                            {
                                                Console.WriteLine($"Number of hands required to use: {storeInventory[i].HandsNeeded}");
                                            }
                                        }
                                    }
                                }
                                if (storeSellInput == "back") // allows the player to exit the purchase
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
                                            Console.WriteLine($"You don't have any {storeSellInput} to sell."); // if the player chooses an item that isn't in their inventory, the player is told so
                                        }
                                        else
                                        {
                                            if (Convert.ToInt32(myInventory[i].CostValue) > storeInventory[0].Quantity)
                                            {
                                                Console.WriteLine($"This trader can't afford the {storeInventory[i].Name}! They only have {storeInventory[0].Quantity} gold left."); // if the vendor can't afford the item the player wants to sell, this tells them how much gold the vendor has available again
                                            }
                                            else
                                            {
                                                Console.WriteLine("Sell how many?"); // allows the player to sell more than 1 of an item
                                                int storeSellQTY = int.Parse(Console.ReadLine());
                                                Console.WriteLine("\n");
                                                if (storeSellQTY * myInventory[i].CostValue > storeInventory[0].Quantity)
                                                {
                                                    Console.WriteLine($"This trader can't afford {storeSellQTY} {myInventory[i].Name}."); // if the vendor can't afford the number the player wants to sell of the item they chose, the player is told so
                                                }
                                                else
                                                {
                                                    if (storeSellQTY > myInventory[i].Quantity)
                                                    {
                                                        Console.WriteLine("That is more than you have! Sell all you have of this item?"); // if the player wants to sell more of an item than they have, they can sell their entire stock
                                                        string buyOut = Console.ReadLine().ToLower();
                                                        Console.WriteLine("\n");
                                                        if (buyOut == "y" || buyOut == "yes") // if the player decides to sell all they have of item they chose, the total number of that item from the player's inventory is moved to the store's inventory, and gold is removed from the store and given to the player
                                                        {
                                                            storeInventory[0].Quantity -= Convert.ToInt32(myInventory[i].Quantity * myInventory[i].CostValue);
                                                            myInventory[0].Quantity += Convert.ToInt32(myInventory[i].Quantity * myInventory[i].CostValue);
                                                            storeInventory[i].Quantity += myInventory[i].Quantity;
                                                            Console.WriteLine($"The vendor gives you {Convert.ToInt32(myInventory[i].Quantity * storeInventory[i].CostValue)} gold, and takes your entire {myInventory[i].Name} stock.");
                                                            myInventory[i].Quantity = 0;
                                                        }
                                                    }
                                                    else // if the store can afford the number of the item that the player wants to sell, and the player has enough of said item, the item is moved into the store's inventory, and the appropriate amount of gold is given to the player
                                                    {
                                                        storeInventory[0].Quantity -= Convert.ToInt32(storeSellQTY * storeInventory[i].CostValue);
                                                        myInventory[0].Quantity += Convert.ToInt32(storeSellQTY * storeInventory[i].CostValue);
                                                        storeInventory[i].Quantity += storeSellQTY;
                                                        myInventory[i].Quantity -= storeSellQTY;
                                                        Console.WriteLine($"The vendor gives you {Convert.ToInt32(storeSellQTY * storeInventory[i].CostValue)} gold, and takes {storeSellQTY} {myInventory[i].Name}.");
                                                    }
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
                                    if (storeInventory[i].Quantity > 0) // prints all the items that the store has available
                                    {
                                        if(storeInventory[i].Type == "drink" || storeInventory[i].Type == "food")
                                        {
                                            Console.WriteLine($"{storeInventory[i].Name}: they have {storeInventory[i].Quantity} servings."); // adds the word 'servings' to food and drink items
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
                                    if (tmpItem.Quantity > 0) // prints all the items that the player has available
                                    {
                                        if (tmpItem.Type == "food" || tmpItem.Type == "drink")
                                        {
                                            Console.WriteLine($"{tmpItem.Name}: you have {tmpItem.Quantity} servings left."); // adds the word 'servings' to food and drink items
                                        }
                                        else
                                        {
                                            Console.WriteLine($"{tmpItem.Name}: you have {tmpItem.Quantity}.");
                                        }
                                    }
                                }
                                Console.WriteLine("---------------------------------------------------");
                                break;
                                 // allows player to leave the trade
                            case string s when s == "5" || s == "end" || s == "leave":
                                Console.WriteLine("\"Safe travels, friend!\""); // merchants are nice!

                                // allows the player to save their inventory if they are in a town
                                if (hoursTravelled == 24)
                                {
                                    Console.WriteLine("Save inventory?");
                                    Console.WriteLine("1: Yes");
                                    Console.WriteLine("2: No");
                                    string saveInput = Console.ReadLine();
                                    Console.WriteLine("\n");

                                    if (saveInput == "1" || saveInput.ToLower() == "yes" || saveInput.ToLower() == "save") // if the player chooses to save...
                                    {
                                        string saveName = "Player inventory save " + townNumber; // ...the player's inventory is saved with the number of the town that this is...
                                        string saveTownName = "Town inventory save " + townNumber; // ...and the town's inventory is saved with the same number

                                        using (var myFile = new StreamWriter(saveName)) // each item in the player's inventory are saved, even if their inventory has 0 of said item
                                        {
                                            for (int i = 0; i < myInventory.Length; i++)
                                            {
                                                myFile.WriteLine(myInventory[i].Quantity + "x " + myInventory[i].Name);
                                            }
                                        }

                                        using (var myFile = new StreamWriter(saveTownName)) // each item in the store's inventory are saved, even if their inventory has 0 of said item
                                        {
                                            for (int i = 0; i < storeInventory.Length; i++)
                                            {
                                                myFile.WriteLine(storeInventory[i].Quantity + "x " + storeInventory[i].Name);
                                            }
                                        }
                                        Console.WriteLine($"Player inventory saved as {saveName}"); // the player is informed of the save
                                        Console.WriteLine($"General store inventory saved as {saveTownName}"); // the player is informed of the save
                                    }
                                    
                                    // if the player is in town, after they save or if they choose not to, they can choose to stay in town
                                    Console.WriteLine("Stay in town?");
                                    Console.WriteLine("1: Stay");
                                    Console.WriteLine("2: Continue travelling");
                                    string stayInTown = Console.ReadLine();
                                    Console.WriteLine("\n");
                                    if (stayInTown == "2" || stayInTown.ToLower() == "travel")
                                    {
                                        hoursTravelled = 0; // resets the hoursTravelled number, so that when it reaches 24 again, another town will be reached
                                    }
                                    trading = false; // trading loop ends
                                }
                                else
                                {
                                Console.WriteLine("The merchant continues on their way, and soon disappears from sight."); // a non general store merchant continues on the road
                                }
                                trading = false; // trading loop ends
                                break;
                            default:
                                Console.WriteLine("That is an unknown option. Please try again."); // if the player 
                                break;
                        }
                    }
                    // =================== CODE FOR TRADING END =================== //

                    // =================== CODE FOR FIGHTING START =================== //

                    // monster creation
                    Entity monster = new Entity();
                    switch (monsterName) // adjusts what type of monster is created based on the name of said monster
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


                    // monster inventory creation
                    LootItem[] monsterInventory;
                    using (var reader = new StreamReader("monsterinventory.csv"))
                    {
                        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                        System.Collections.Generic.IEnumerable<LootItem> records = csv.GetRecords<LootItem>();
                        monsterInventory = records.ToArray<LootItem>();
                    }

                    // monster inventory quantity randomizer
                    Random lootRandom = new Random();

                    for (int i = 0; i < monsterInventory.Length; i++)
                    {
                        switch (monsterName) // adjusts max of random number for each loot item based on what type ofmonster is being fought
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
                        for (int w = 0; w <myInventory.Length; w++) // prints each weapon that the player has available, plus the weapon's base damage
                        {
                            storeInventory[w].Row = 0;
                            if (myInventory[w].Type == "weapon" && myInventory[w].Quantity > 0)
                            {
                                weapRow++;
                                storeInventory[w].Row = weapRow;
                                if (myInventory[w].HandsNeeded == 2)
                                {
                                    Console.WriteLine($"{weapRow}: {myInventory[w].Name}, which requires both hands to use, and has a {myInventory[w].Damage} damage base attack."); // if the weapon requires 2 hands, this is noted
                                }
                                else
                                {
                                    Console.WriteLine($"{weapRow}: {myInventory[w].Name}, which has a {myInventory[w].Damage} damage base attack.");
                                }
                            }
                        }
                        Console.WriteLine("---------------------------------------------------");
                        string weapInput = Console.ReadLine().ToLower();

                        // weapon choice
                        Console.WriteLine("\n");

                        for (int i = 0; i < myInventory.Length; i++)
                        {
                            if (myInventory[i].Name == weapInput || Convert.ToString(storeInventory[i].Row) == weapInput)
                            {
                                if (myInventory[i].Quantity == 0)
                                {
                                    Console.WriteLine($"You don't have a {myInventory[i].Name}!"); // if the player doesn't have the item they chose to use
                                }
                                else
                                {
                                    player.Attack();
                                    Console.WriteLine($"You attack with the {myInventory[i].Name}, dealing {myInventory[i].Damage + player.attack} damage to the {monster.name}."); // if the player has the weapon that they chose, they attack with is, dealing the weapon's base damage plus the player's attack bonus plus a random number between 0 and 5
                                    monster.health -= myInventory[i].Damage + player.attack; // the monster's health is decreased by the damage dealt above
                                    if (monster.health < 0) // resets the monster's health to 0 if it goes below 0
                                    {
                                        monster.health = 0;
                                    }
                                    Console.WriteLine($"The {monster.name} now has {monster.health} health."); // prints the monster's current health
                                }
                            }
                        }
                        if (monster.health == 0) // if the monster's health is 0 after the attack, this is noted, and the number of fights the player has won is increased
                        {
                            Console.WriteLine("You won the fight!");
                            player.fightsWon++;
                            if (player.fightsWon % 5 == 0)
                            {
                                Console.WriteLine("You feel stronger after all of these fights! Your attack and health have increased!"); // theplayer's level goes up for each 5 fights they win.
                                player.health += 10; // leveling up increases the player's current health
                                player.maxHealth += 10; // leveling up increases the player's max health
                                player.levelsGained++;
                                player.attackBonus += 5; // leveling up increases the player's attack bonus
                            }
                            if(noLoot == 0)
                            {
                                Console.WriteLine("\nThe body has nothing worth looting..."); // notes if the monster has nothing to loot
                            }
                            else
                            {
                                Console.WriteLine($"\nOn the dead {monster.name}, you find the following:"); // notes what is looted from the monster, and adds said items to the player's inventory
                                for (int i = 0; i < monsterInventory.Length; i++)
                                {
                                    if (monsterInventory[i].Quantity > 0)
                                    {
                                        Console.WriteLine($"{monsterInventory[i].Quantity} {monsterInventory[i].Name}");
                                        myInventory[i].Quantity += monsterInventory[i].Quantity;
                                    }
                                }
                            }
                            monsterAlive = false; // fighting loop ends
                        }
                        else if (monster.health <= 5 && monster.name == "bandit") // bandit's won't fight to the death if they realize that they are losing
                        {
                            Console.WriteLine("The bandit flees for their life. Good riddance!");
                            player.fightsWon++; // the player still won the fight, even though the bandit didn't die
                            monsterAlive = false; // fighting loop ends
                        }
                        else
                        {

                            monster.Attack(); // the monster attacks if they are still alive and in the fight
                            Console.WriteLine($"The {monster.name} attacks you back!");
                            player.health -= monster.attack;
                            if (player.health < 0) // resets the player's health to 0 if it goes below 0
                            {
                                player.health = 0;
                            }
                            Console.WriteLine($"You take {monster.attack} damage, and have {player.health} health left."); // the monster deals damage, the amount of which is the monsters base attack plus a random number between 0 and 5
                            if (player.health == 0)
                            {
                                Console.WriteLine("\nYou lost the fight! Everything goes black..."); // if the player has 0 health, they die
                                monsterAlive = false; // fighting loop ends
                                playerAlive = false; // takes the player to the option to play again
                            }
                        }
                        if (monster.name == "bandit" && player.health <= player.maxHealth / 4 && monsterAlive == true)
                        {
                            Console.WriteLine("\n\"Don't make me kill you. Just give me what I want and we can go our seperate ways.\""); // the player gets the option to give up when fighting a bandit if their health is low
                            Console.WriteLine("Continue fighting?");
                            string killBanditInput = Console.ReadLine().ToLower();
                            Console.WriteLine("\n");
                            if (killBanditInput == "n" || killBanditInput == "no")
                            {
                                Console.WriteLine("\"Smart choice, bud.\""); // the bandit steals gold from the player if they choose to stop fighting
                                Random moneyStolenRand = new Random();
                                double moneyStolenPercent = moneyStolenRand.Next(33); // a random number between 0 and 33 is chosen
                                if (moneyStolenPercent < 10)
                                {
                                    moneyStolenPercent = 10; // if the random number is less than 10, it is set to 10
                                }
                                double moneyStolen = myInventory[0].Quantity * (moneyStolenPercent / 100); // the random number between 10 and 33 is the percentage of the player's gold that is stolen, so that the player never loses more than a third of their gold from the bandit
                                myInventory[0].Quantity -= Convert.ToInt32(moneyStolen);
                                Console.WriteLine($"The thief rifles around, and steals {Convert.ToInt32(moneyStolen)} gold. You now have {myInventory[0].Quantity} gold pieces.");
                                Console.WriteLine("The thief leaves. That was unfortunate...");
                                monsterAlive = false;
                            }
                        }
                    }

                    // =================== CODE FOR FIGHTING END =================== //

                }
                // the option to play again
                Console.WriteLine($"You travelled for {hoursSinceStart} hours before dying.");
                Console.WriteLine("\nPlay again?");
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

                        // resetting inventory to how it was when the game started
                        for(int i = 0; i < myInventory.Length; i++)
                        {
                            myInventory[i].Quantity = 0;
                        }
                        myInventory[0].Quantity = 100;
                        myInventory[2].Quantity = 6;
                        myInventory[4].Quantity = 3;
                        myInventory[6].Quantity = 2;
                        myInventory[7].Quantity = 3;
                        myInventory[8].Quantity = 2;
                        myInventory[9].Quantity = 2;
                        myInventory[13].Quantity = 1;
                        myInventory[21].Quantity = 2;
                        myInventory[26].Quantity = 5;
                        playerAlive = true;
                        break;
                    case string s when s == "no" || s == "n":
                        isGameRunning = false; // exits game loop
                        break;
                    default:
                        Console.WriteLine("Unknown command. Please type either yes/y or no/n."); // in case the player didn't type yesor no
                        break;
                }
            }
            // farewell text
            LoopEnd:
            Console.WriteLine($"Farewell, {player.name}!");
        }
    }
}
