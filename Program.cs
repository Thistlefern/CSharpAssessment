using System;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

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
            player.name = nameInput;
            Console.WriteLine($"So you're {player.name}? Nice to meetcha! I'm Humphrey, the leader of this here merchant's guild.");
            
            // Player inventory
            Food[] myFood;
            using (var reader = new StreamReader("foodinv.csv"))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    System.Collections.Generic.IEnumerable<Food> records = csv.GetRecords<Food>();
                    myFood = records.ToArray<Food>();
                }
            }

            // Tutorial check for tutorial loop
            bool tutorial = false;
            // Game running check for game loop
            bool isGameRunning = false;
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
            // TODO add tutorial, remove placeholder lines
            while (tutorial)
            {
                Console.WriteLine("tutorial here"); // placeholder
                tutorial = false;                   // placeholder
                isGameRunning = true;               // placeholder
            }

            // Main game loop
            while (isGameRunning)
            {
                // clock and day counter
                if (time >= 24)
                {
                    time -=24;
                }

                int day = ((hoursSinceStart + 8) / 24) + 1;

                Console.WriteLine($"It is {Hours[time]} on day {day} of your journey. Hours travelled: {hoursSinceStart}");

                // basic needs tracker
                if(player.hunger >= 8 && player.hunger <= 16)
                {
                    Console.WriteLine("Your stomach is rumbling. You should eat something.");
                } else if(player.hunger >= 16 && player.hunger < 36)
                {
                    Console.WriteLine($"Your stomach is aching from being so empty! You need to eat something soon!");
                    // TODO add stat penalties
                } // TODO add an else that kills the player at 36 hours of not eating

                if (player.thirst >= 8 && player.hunger <= 16)
                {
                    Console.WriteLine("Your mouth feels dry. You should drink something.");
                }
                else if (player.thirst >= 16 && player.thirst < 36)
                {
                    Console.WriteLine($"Your head is spinning due to how dehydrated you are! You need to drink something soon!");
                    // TODO add stat penalties
                } // TODO add an else that kills the player at 36 hours of not drinking

                if (player.fatigue >= 16 && player.fatigue <= 24)
                {
                    Console.WriteLine("You're feeling pretty tired. You should get some sleep.");
                }
                else if (player.fatigue >= 24)
                {
                    Console.WriteLine($"You're feeling exhausted! You need to sleep soon!");
                    // TODO add stat penalties
                }

                // player input
                // TODO add options
                Console.WriteLine("What would you like to do?");
                string gameInput = Console.ReadLine().ToLower().Trim();

                // eating
                if (gameInput == "eat")
                {
                    Console.WriteLine("What would you like to eat?");
                    foreach (Food tmpFood in myFood)
                    {
                        if (tmpFood.Type == "eat")
                        {
                            if (tmpFood.Quantity > 0)
                            {
                                Console.WriteLine($"{tmpFood.Name}: you have {tmpFood.Quantity} servings left.");
                            }
                        }
                    }
                }
                // TODO let player eat and gain hunger boost

                // drinking
                if (gameInput == "drink")
                {
                    Console.WriteLine("What would you like to drink?");
                    foreach (Food tmpFood in myFood)
                    {
                        if (tmpFood.Type == "drink")
                        {
                            if (tmpFood.Quantity > 0)
                            {
                                Console.WriteLine($"{tmpFood.Name}: you have {tmpFood.Quantity} servings left.");
                            }
                        }
                    }
                }
                // TODO let player drink and gain thirst boost

                if (gameInput == "sleep") // TODO adjust for realism
                {
                    Console.WriteLine($"Sleep for how many hours? Sleeping for {player.fatigue} hours will leave you fully rested.");
                    int sleepInput = int.Parse(Console.ReadLine());
                    player.fatigue -= sleepInput;
                    if(player.fatigue < 0)
                    {
                        player.fatigue = 0;
                    }
                    
                    time += sleepInput;
                    hoursSinceStart += sleepInput;
                    player.hunger += sleepInput;
                    player.thirst += sleepInput;
                    Console.WriteLine($"You sleep for {sleepInput} hours, and awaken feeling more rested.");
                }
                if (gameInput == "travel") // TODO add random encounters
                {
                    hoursSinceStart++;
                    time++;
                    player.hunger++;
                    player.thirst++;
                    player.fatigue++;
                }
            }
        }
    }
}
