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

            Rolodex rolodex = new Rolodex();
            // Return a list of robbers for the user to choose from
            rolodex.List = getPremadeRoster();

            Console.WriteLine("Specialist rolodex:\n");
            printRoster(rolodex.List);

            while (true)
            {
                Console.WriteLine("Add specialist to rolodex?");
                bool keepCreating = ask();

                if (keepCreating == false)
                {
                    break;
                }

                Specialist mate = createSpecialist();
                rolodex.List.Add(mate);
            }

            // Instantiate the bank
            Bank theBank = new Bank();
            theBank.printReport();

            Console.WriteLine("Build Your Crew!");
            Console.Write("Crew Name: ");
            string crewName = Console.ReadLine();
            Team crew = new Team(crewName);

            while (true)
            {
                crew.PrintInfo();
                Console.WriteLine("Add specialist to crew?");
                bool keepAdding = ask();
                if (keepAdding == false)
                {
                    break;
                }

                IRobber crewMate = selectRobber(rolodex.List, crew);
                crew.AddMate(crewMate, rolodex.List);

                if (crew.PercentageTotal + rolodex.Cheapest() > 100)
                {
                    Console.WriteLine("\nYou've reached your budget for specialists!");
                    break;
                }
            }

            int trialRuns = askIterations();
            int successCount = 0;
            int personalEarnings = 0;
            for (int i = 0; i < trialRuns; i++)
            {
                bool wasSuccess = robBank(crew, theBank);
                if (wasSuccess)
                {
                    successCount++;
                    personalEarnings += calculateEarnings(crew, theBank);
                    Console.WriteLine($"You made {personalEarnings}!");
                }

                //Reset bank and print report for next iteration on all but last iteration
                if (i + 1 != trialRuns)
                {
                theBank = new Bank();
                Console.WriteLine("\nNext Bank!");
                theBank.printReport();
                }
            }
            if (successCount > (trialRuns / 2))
            {
                Console.WriteLine("Congratulations!");
            }
            else
            {
                Console.WriteLine("Sorry!");
            }
            Console.WriteLine($"Your heist succeeded {successCount} times.");
            Console.WriteLine($"You failed {trialRuns - successCount} times.");
        }

        // Create a directory of robbers for the user to choose from
        public static List<IRobber> getPremadeRoster()
        {
            List<IRobber> preList = new List<IRobber>();
            Hacker netHacker = new Hacker(".NET Developer", "Hacker", 70, 25);
            preList.Add(netHacker);

            Hacker iotHacker = new Hacker("IoT Wizard", "Hacker", 50, 20);
            preList.Add(iotHacker);

            Muscle fitnessCoach = new Muscle("Fitness Coach", "Muscle", 45, 20);
            preList.Add(fitnessCoach);

            Muscle gymRat = new Muscle("Gym Rat", "Muscle", 70, 25);
            preList.Add(gymRat);

            Locksmith lockSmith = new Locksmith("Mobile Locksmith", "Locksmith", 50, 15);
            preList.Add(lockSmith);

            Locksmith safeCracker = new Locksmith("Safecracker", "Locksmith", 70, 40);
            preList.Add(safeCracker);

            return preList;
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

        // Method to return a robber by user selection
        public static IRobber selectRobber(List<IRobber> rolodex, Team crew)
        {
            // Display only the robbers that the user has budget for.
            List<IRobber> availableRobbers = (from r in rolodex
                                              where r.PercentageCut + crew.PercentageTotal < 100
                                              select r).ToList();
            printRoster(availableRobbers);
            int selectionNum;
            while (true)
            {
                Console.WriteLine("Enter ID number of specialist to add: ");
                string selectionInput = Console.ReadLine();
                bool selectionSuccess = int.TryParse(selectionInput, out int parsedSelection);
                if (selectionSuccess && parsedSelection > 0 && parsedSelection <= availableRobbers.Count)
                {
                    selectionNum = parsedSelection - 1;
                    if(crew.Mates.Any(m => m.Specialty == rolodex[selectionNum].Specialty))
                    {
                        Console.WriteLine($"You already have a {rolodex[selectionNum].Specialty}!");
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (selectionInput == "")
                {
                    Console.WriteLine("You forgot to enter a number!");
                    continue;
                }
                else
                {
                    Console.WriteLine($"\"{selectionInput}\" is not a valid entry");
                    Console.WriteLine($"Enter a number between 1 and {availableRobbers.Count}");
                }
            }
            return rolodex[selectionNum];
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
            Console.WriteLine($"Your current luck factor is {crew.Luck}");

            List<bool> systemSuccessList = new List<bool>();

            foreach (IRobber mate in crew.Mates)
            {
                systemSuccessList.Add(mate.PerformSkill(theBank, crew.Luck));
            }

            bool success;
            if (systemSuccessList.Any(success => success == false))
            {
                Console.WriteLine("The heist has failed. You and your team have been captured.");
                success = false;
            }
            else
            {
                Console.WriteLine("The heist succeeds! You made money, but now the FBI is after you.");
                success = true;
            }
            return success;
        }
        public static int calculateEarnings(Team crew, Bank theBank)
        {
            int earningsTotal = theBank.CashOnHand;
            foreach (var mate in crew.Mates)
            {
                earningsTotal =- theBank.CashOnHand * (mate.PercentageCut / 100);
            }
            return earningsTotal;
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
