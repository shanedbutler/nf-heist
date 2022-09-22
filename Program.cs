using System;
using System.Collections.Generic;
using System.Linq;

namespace heist
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Plan Your Heist!/n");
            createTeammate();
        }
        public static void createTeammate()
        {
            Teammate MateOne = new Teammate();
            Console.WriteLine("Create a teammate!");
            Console.Write("Name: ");
            MateOne.Name = Console.ReadLine();
            Console.Write("Skill level: ");
            string skillInput = Console.ReadLine();
            try
            {
                MateOne.Skill = int.Parse(skillInput);
            }
            catch (System.Exception)
            {
                if (skillInput == "")
                {
                    Console.WriteLine("You forgot to enter a number!");
                }
                else
                {
                    Console.WriteLine($"\"{skillInput}\" is not a valid entry");
                }
            }
            Console.Write("Courage factor: ");
            string courageInput = Console.ReadLine();
            try
            {
                decimal parsed = decimal.Parse(courageInput);
                MateOne.Courage = parsed / 10;
            }
            catch (System.Exception)
            {
                if (courageInput == "")
                {
                    Console.WriteLine("You forgot to enter a number!");
                }
                else
                {
                    Console.WriteLine($"\"{courageInput}\" is not a valid entry");
                }
            }
            MateOne.PrintInfo();
        }

    }
}
