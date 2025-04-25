using System;
using System.Collections.Generic;
using System.Threading;

namespace ProblematicProblem
{
    class Program
    {
        private static readonly Random _rng = new Random();
        static void Main(string[] args)
        {
            List<string> activities = new List<string>() { "Movies", "Paintball", "Bowling", "Lazer Tag", "LAN Party", "Hiking", "Axe Throwing", "Wine Tasting" };
            Console.WriteLine("Hello, welcome to the random activity generator!");

            bool startGenerator = PromptYesNo("Would you like to generate a random activity?");
            if (startGenerator)
            {
                Console.WriteLine("We are going to need your information first!");
                string userName = GetName();
                int userAge = GetAge();

                bool seeList = PromptYesNo("Would you like to see the current list of activities?");
                if (seeList) { PrintActivities(activities); }

                bool addActivity = PromptYesNo("Would you like to add any activities before we generate one?");
                if (addActivity) { AddActivity(activities); }

                GenerateActivity(userName, userAge, activities);
            }
            else
            {
                Console.WriteLine("OK! Have fun thinking of something on your own!");
            }
        }

        static bool PromptYesNo(string message)
        {
            string input;

            do
            {
                Console.Write($"\n{message} (Y/N) : ");
                input = (Console.ReadLine() ?? "").Trim().ToLower();

                if (input != "y" && input != "n")
                {
                    Console.WriteLine("Invalid response. Please enter 'y' or 'n'.");
                }
            } while (input != "y" && input != "n");

            return input == "y";
        }

        static string GetName()
        {
            string name;

            do
            {
                Console.Write("What is your name? ");
                name = (Console.ReadLine() ?? "").Trim();

                if (name == "")
                {
                    Console.WriteLine("Name requires input, please try again.");
                }
            } while (name == "");

            return name;
        }

        static int GetAge()
        {
            int age;
            var validAge = false;

            do
            {
                Console.Write("What is your age? ");
                validAge = int.TryParse(Console.ReadLine(), out age);

                if (!validAge)
                {
                    Console.WriteLine("Age should be a whole number, please try again.");
                }
            } while (!validAge);

            return age;
        }
        static void PrintActivities(List<string> activities)
        {
            for (int i = 0; i < activities.Count; i++)
            {
                if (i != activities.Count - 1)
                {
                    Console.Write($"{activities[i]} | ");
                    Thread.Sleep(250);
                }
                else
                {
                    Console.Write($"{activities[i]}\n");
                }
            }
        }

        static void AddActivity(List<string> activities)
        {
            bool keepAdding = true;

            while (keepAdding)
            {
                Console.Write("What would you like to add? ");
                string userAddition = (Console.ReadLine() ?? "").Trim();

                if (string.IsNullOrWhiteSpace(userAddition))
                {
                    keepAdding = PromptYesNo("Activity cannot be blank. Enter an activity or 'n' to cancel.");
                }
                else
                {
                    activities.Add(userAddition);
                    PrintActivities(activities);
                    keepAdding = PromptYesNo("Would you like to add more?");
                }
            }
        }

        static void GenerateActivity(string name, int age, List<string> activities)
        {

            bool endActivityPrompt = false;

            Console.Write("\nConnecting to the database");
            for (int i = 0; i < 10; i++)
            {
                Console.Write(". ");
                Thread.Sleep(250);
            }

            while (!endActivityPrompt)
            {
                Console.Write("\nChoosing your random activity");
                for (int i = 0; i < 9; i++)
                {
                    Console.Write(". ");
                    Thread.Sleep(250);
                }

                int randomNumber = _rng.Next(activities.Count);
                string randomActivity = activities[randomNumber];

                if (age < 21 && randomActivity == "Wine Tasting")
                {
                    Console.WriteLine($"\nOh no! Looks like you are too young to do {randomActivity}");
                    Console.WriteLine("Pick something else!");
                    activities.Remove(randomActivity);
                    randomNumber = _rng.Next(activities.Count);
                    randomActivity = activities[randomNumber];
                }

                Console.WriteLine($"\nAh got it! {name}, your random activity is: {randomActivity}!");
                endActivityPrompt = PromptYesNo("Do you want to continue with this activity?");

                if (!endActivityPrompt)
                {
                    activities.Remove(randomActivity);
                }

                if (activities.Count == 0)
                {
                    Console.WriteLine("Oh no! We're out of activities. You'll have to think of something on your own!");
                    endActivityPrompt = true;
                }
            }
        }
    }
}