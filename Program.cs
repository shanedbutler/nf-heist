﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace heist
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("\nPlan Your Heist!\n");

            Bank theBank = createBank();

            Console.Write("\nTeam Name: ");
            string teamName = Console.ReadLine();

            Team myTeam = new Team(teamName);

            bool keepCreating = true;
            while (keepCreating == true)
            {
                Teammate mate = createTeammate();
                myTeam.AddMate(mate);

                Console.WriteLine("\nAdd another?");
                keepCreating = ask();
            }

            Console.WriteLine($"\nCurrent {myTeam.Name} line-up");
            Console.WriteLine($"{myTeam.Mates.Count} teammates: ");
            foreach (Teammate mate in myTeam.Mates)
            {
                mate.PrintInfo();
            }

            myTeam.SkillTotal = myTeam.Mates.Sum(m => m.Skill);

            int trialRuns = askIterations();

            for (int i = 0; i < trialRuns; i++)
            {
                {
                    robBank(myTeam, theBank);
                }
            }
        }

        public static Bank createBank()
        {
            int difficulty;
            while (true)
            {
                Console.Write("Bank difficulty level: ");
                string difficultyInput = Console.ReadLine();
                bool difficultySuccess = int.TryParse(difficultyInput, out int parsedDifficulty);
                if (difficultySuccess)
                {
                    difficulty = parsedDifficulty;
                    break;
                }
                else if (difficultyInput == "")
                {
                    Console.WriteLine("You forgot to enter a number!");
                }
                else
                {
                    Console.WriteLine($"\"{difficultyInput}\" is not a valid entry");
                }
            }
            Bank theBank = new Bank(difficulty);
            return theBank;
        }

        public static Teammate createTeammate()
        {
            Console.WriteLine("\nCreate a teammate!");
            Console.Write("Name: ");
            string nameInput = Console.ReadLine();

            int skill;
            while (true)
            {
                Console.Write("Skill level: ");
                string skillInput = Console.ReadLine();
                bool skillSuccess = int.TryParse(skillInput, out int parsedSkill);
                if (skillSuccess)
                {
                    skill = parsedSkill;
                    break;
                }
                else if (skillInput == "")
                {
                    Console.WriteLine("You forgot to enter a number!");
                }
                else
                {
                    Console.WriteLine($"\"{skillInput}\" is not a valid entry");
                }
            }

            decimal courage;
            while (true)
            {
                Console.Write("Courage factor: ");
                string courageInput = Console.ReadLine();
                bool courageSuccess = decimal.TryParse(courageInput, out decimal parsedCourage);

                if (courageSuccess)
                {
                    courage = parsedCourage;
                    break;
                }
                else if (courageInput == "")
                {
                    Console.WriteLine("You forgot to enter a number!");
                }
                else
                {
                    Console.WriteLine($"\"{courageInput}\" is not a valid entry");
                }
            }
            Teammate mate = new Teammate(nameInput, skill, courage);
            return mate;
        }

        public static int askIterations()
        {
            int trials;
            while (true)
            {
                Console.Write("How Many Trial Runs?: ");
                string trialsInput = Console.ReadLine();
                bool trialsSuccess = int.TryParse(trialsInput, out int parsedTrials);
                if (trialsSuccess)
                {
                    trials = parsedTrials;
                    break;
                }
                else if (trialsInput == "")
                {
                    Console.WriteLine("You forgot to enter a number!");
                }
                else
                {
                    Console.WriteLine($"\"{trialsInput}\" is not a valid entry");
                }
            }
            return trials;
        }

        public static void robBank(Team myTeam, Bank theBank)
        {
            Console.WriteLine($"\nTotal skill level for team is {myTeam.SkillTotal}");
            Console.WriteLine($"Difficulty level for the bank is {theBank.Difficulty}");
            Console.WriteLine($"Your current luck factor results in {myTeam.Luck} to the banks difficulty");
            if (myTeam.SkillTotal > (theBank.Difficulty + myTeam.Luck))
            {
                Console.WriteLine("The heist succeeds! You made money, but now the FBI is after you.");
            }
            else
            {
                Console.WriteLine("The heist has failed. You and your team have been captured.");
            }
        }

        public static bool ask()
        {
            Console.WriteLine("Y or N?");
            string input = Console.ReadLine().ToLower();

            if (input == "n" || input == "no")
            {
                return false;
            }
            if (input == "y" || input == "yes")
            {
                return true;
            }
            else
            {
                Console.WriteLine("Invalid response");
                return ask();
            }
        }
    }
}
