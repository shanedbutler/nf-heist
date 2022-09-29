using System;
using System.Collections.Generic;
using System.Linq;

namespace heist
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("\nPlan Your Heist!\n");

            // Instantiate the bank and set difficulty via method
            Bank theBank = createBank();

            Console.Write("\nTeam Name: ");
            string teamName = Console.ReadLine();

            // Create a directory of robbers for the user to choose from
            List<IRobber> rolodex = getRobberRoster();

            Console.WriteLine("\nCurrent Specialists:");
            foreach (IRobber r in rolodex)
            {
                Console.WriteLine($"- {r.Name}");
            }

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
            int successCount = 0;
            for (int i = 0; i < trialRuns; i++)
            {
                bool wasSuccess = robBank(myTeam, theBank);
                if (wasSuccess)
                {
                    successCount++;
                }
            }
            if (successCount > (trialRuns / 2))
            {
                Console.WriteLine("\nCongratulations!");
            }
            else
            {
                Console.WriteLine("\nSorry!");
            }
            Console.WriteLine($"Your heist succeeded {successCount} times.");
            Console.WriteLine($"You failed {trialRuns - successCount} times.");
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
                    continue;
                }
                else
                {
                    Console.WriteLine($"\"{difficultyInput}\" is not a valid entry");
                }
            }
            Bank theBank = new Bank(difficulty);
            return theBank;
        }

        public static List<IRobber> getRobberRoster()
        {
            // Create a directory of robbers for the user to choose from
            List<IRobber> rolodex = new List<IRobber>();
            Hacker netHacker = new Hacker(".NET Hacker", 40, 35);
            rolodex.Add(netHacker);
            
            Hacker iotHacker = new Hacker("IoT Hacker", 30, 30);
            rolodex.Add(iotHacker);

            Muscle fitnessCoach = new Muscle("Fitness Coach", 25, 30);
            rolodex.Add(fitnessCoach);

            Muscle gymRat = new Muscle("Gym Rat", 45, 35);
            rolodex.Add(gymRat);

            Locksmith lockSmith = new Locksmith("Locksmith", 35, 25);
            rolodex.Add(lockSmith);

            Locksmith safeCracker = new Locksmith("Safecracker", 60, 50);
            rolodex.Add(safeCracker);

            return rolodex
        }

        public static Teammate createTeammate()
        {
            Console.WriteLine("\nCreate a teammate!");
            Console.Write("Name: ");
            string nameInput = Console.ReadLine();

            int specialtyNum;
            while (true)
            {
                Console.WriteLine("Specialist Options:");
                Console.WriteLine(" 1. Hacker");
                Console.WriteLine(" 2. Muscle");
                Console.WriteLine(" 3. Locksmith");
                Console.WriteLine("Specialty Number: ");
                string specialtyInput = Console.ReadLine();
                bool specialtySuccess = int.TryParse(specialtyInput, out int parsedSpecialty);
                if (specialtySuccess && parsedSpecialty > 0 && parsedSpecialty <= 3)
                {
                    specialtyNum = parsedSpecialty;
                    break;
                }
                else if (specialtyInput == "")
                {
                    Console.WriteLine("You forgot to enter a number!");
                    continue;
                }
                else
                {
                    Console.WriteLine($"\"{specialtyInput}\" is not a valid entry");
                }
            }

            string specialty = findSpecialty(specialtyNum);

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
                    continue;
                }
                else
                {
                    Console.WriteLine($"\"{skillInput}\" is not a valid entry");
                }
            }

            int take;
            while (true)
            {
                Console.Write("Take percentage: ");
                string takeInput = Console.ReadLine();
                bool takeSuccess = int.TryParse(takeInput, out int parsedTake);
                if (takeSuccess && parsedTake > 0 && parsedTake <= 100)
                {
                    take = parsedTake;
                    break;
                }
                else if (takeInput == "")
                {
                    Console.WriteLine("You forgot to enter a number!");
                    continue;
                }
                else
                {
                    Console.WriteLine($"\"{takeInput}\" is not a valid entry");
                }
            }

            decimal courage;
            while (true)
            {
                Console.Write("Courage factor (.0 - 2.0): ");
                string courageInput = Console.ReadLine();
                bool courageSuccess = decimal.TryParse(courageInput, out decimal parsedCourage);

                if (courageSuccess && parsedCourage >= 0 && parsedCourage <= 2)
                {
                    courage = parsedCourage;
                    break;
                }
                else if (courageInput == "")
                {
                    Console.WriteLine("You forgot to enter a number!");
                    continue;
                }
                else
                {
                    Console.WriteLine($"\"{courageInput}\" is not a valid entry");
                }
            }
            Teammate mate = new Teammate(nameInput, specialty, skill, courage);
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
                    continue;
                }
                else
                {
                    Console.WriteLine($"\"{trialsInput}\" is not a valid entry");
                }
            }
            return trials;
        }

        public static bool robBank(Team myTeam, Bank theBank)
        {
            Console.WriteLine($"\nTotal skill level for team is {myTeam.SkillTotal}");
            Console.WriteLine($"Your current luck factor results in {myTeam.Luck} to the banks difficulty");
            Console.WriteLine($"The resulting difficulty level is {theBank.Difficulty + myTeam.Luck}");

            bool success;
            if (myTeam.SkillTotal > (theBank.Difficulty + myTeam.Luck))
            {
                Console.WriteLine("\nThe heist succeeds! You made money, but now the FBI is after you.");
                success = true;
            }
            else
            {
                Console.WriteLine("\nThe heist has failed. You and your team have been captured.");
                success = false;
            }
            return success;
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

        // Method to return user inputted specialty number as it's name string
        public static string findSpecialty(int num)
        {
            switch (num)
            {
                case 1:
                    return "Hacker";
                case 2:
                    return "Muscle";
                case 3:
                    return "Locksmith";
                default:
                    return "Error";
            }
        }
    }
}
