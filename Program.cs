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
            theBank.printReport();

            // Create a directory of robbers for the user to choose from
            List<IRobber> rolodex = getPremadeRoster();

            Console.WriteLine("Specialist rolodex:\n");
            printRoster(rolodex);


            while (true)
            {
                Console.WriteLine("Add specialist to rolodex?");
                bool keepCreating = ask();

                if (keepCreating == false)
                {
                    break;
                }

                Specialist mate = createSpecialist();
                rolodex.Add(mate);
            }

            Console.WriteLine("\nBuild Your Crew!");
            Console.Write("Crew Name: ");
            string crewName = Console.ReadLine();
            Team crew = new Team(crewName);

            while (true)
            {
                Console.WriteLine("\nAdd specialist to crew?");
                bool keepAdding = ask();
                if (keepAdding == false)
                {
                    break;
                }

                IRobber crewMate = selectSpecialist(rolodex);
                crew.AddMate(crewMate);
            }

            Console.WriteLine($"\nCurrent {crew.Name} line-up");
            Console.WriteLine($"{crew.Mates.Count} crewmates:\n");
            foreach (Specialist mate in crew.Mates)
            {
                mate.PrintInfo();
            }

            crew.SkillTotal = crew.Mates.Sum(m => m.SkillLevel);

            int trialRuns = askIterations();
            int successCount = 0;
            for (int i = 0; i < trialRuns; i++)
            {
                bool wasSuccess = robBank(crew, theBank);
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

        public static List<IRobber> getPremadeRoster()
        {
            // Create a directory of robbers for the user to choose from
            List<IRobber> rolodex = new List<IRobber>();
            Hacker netHacker = new Hacker(".NET Developer", "Hacker", 40, 35);
            rolodex.Add(netHacker);

            Hacker iotHacker = new Hacker("IoT Wizard", "Hacker", 30, 30);
            rolodex.Add(iotHacker);

            Muscle fitnessCoach = new Muscle("Fitness Coach", "Muscle", 25, 30);
            rolodex.Add(fitnessCoach);

            Muscle gymRat = new Muscle("Gym Rat", "Muscle", 45, 35);
            rolodex.Add(gymRat);

            Locksmith lockSmith = new Locksmith("Mobile Locksmith", "Locksmith", 35, 25);
            rolodex.Add(lockSmith);

            Locksmith safeCracker = new Locksmith("Safecracker", "Locksmith", 60, 50);
            rolodex.Add(safeCracker);

            return rolodex;
        }
        public static void printRoster(List<IRobber> robberList)
        {
            foreach (IRobber r in robberList)
            {
                Console.Write($"{robberList.IndexOf(r) + 1}: ");
                r.PrintInfo();
            }
        }
        public static Specialist createSpecialist()
        {
            Console.WriteLine("\nCreate a specialist!");
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
                Console.WriteLine("");
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

            Specialist mate = newSpecialist(specialtyNum, nameInput, skill, take);

            return mate;
        }

        // Method to return user inputted specialty number as it's name string and return specialist
        public static Specialist newSpecialist(int specialtyNum, string name, int skill, int take)
        {
            switch (specialtyNum)
            {
                case 1:
                    return new Hacker(name, "Hacker", skill, take);
                case 2:
                    return new Muscle(name, "Muscle", skill, take);
                case 3:
                    return new Locksmith(name, "Locksmith", skill, take);
                default:
                    return new Specialist(name, "No Specialty", skill, take);
            }
        }
        public static IRobber selectSpecialist(List<IRobber> rolodex)
        {
            printRoster(rolodex);
            int selectionNum;
            while (true)
            {
                Console.WriteLine("Enter number of specialist to add: ");
                string selectionInput = Console.ReadLine();
                bool selectionSuccess = int.TryParse(selectionInput, out int parsedSelection);
                if (selectionSuccess && parsedSelection > 0 && parsedSelection <= rolodex.Count)
                {
                    selectionNum = parsedSelection;
                    break;
                }
                else if (selectionInput == "")
                {
                    Console.WriteLine("You forgot to enter a number!");
                    continue;
                }
                else
                {
                    Console.WriteLine($"\"{selectionInput}\" is not a valid entry");
                    Console.WriteLine($"Enter a number between 1 and {rolodex.Count}");
                }
            }

            IRobber crewMate = rolodex[selectionNum];
            return crewMate;
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

        public static bool robBank(Team crew, Bank theBank)
        {
            Console.WriteLine($"\nTotal skill level for team is {crew.SkillTotal}");
            Console.WriteLine($"Your current luck factor results in {crew.Luck} to the banks difficulty");
            Console.WriteLine($"The resulting difficulty level is {theBank.Difficulty + crew.Luck}");

            bool success;
            if (crew.SkillTotal > (theBank.Difficulty + crew.Luck))
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
    }
}
