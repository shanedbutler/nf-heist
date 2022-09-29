namespace heist
{
    interface IRobber
    {
        string Name { get; set; }
        int SkillLevel { get; set; }
        int PercentageCut { get; set; }
        void PerformSkill(Bank bank);
    }

    public class Specialist : IRobber
    {
        public string Name { get; set; }
        public int SkillLevel { get; set; }
        public int PercentageCut { get; set; }
        public virtual void PerformSkill(Bank bank)
        {
            Console.WriteLine($"{Name} does their thing! It was cool, but had no target effect!");
        }
        public Specialist(string name, int skill, int percentage)
        {
            this.Name = name;
            this.SkillLevel = skill;
            this.PercentageCut = percentage;
        }
    }
    public class Hacker : Specialist
    {
/*         public string Name { get; set; }
        public int SkillLevel { get; set; }
        public int PercentageCut { get; set; } */
        public override void PerformSkill(Bank bank)
        {
            Console.WriteLine($"{Name} is infiltrating the mainframe");
            bank.AlarmScore -= SkillLevel;
            if (bank.SecurityGuardScore < 0)
            {
                Console.WriteLine($"{Name} has convinced the guards to join their workout. They are completely neutralized!");
            }
        }
        public Hacker(string name, int skill, int percentage) : base(name, skill, percentage)
        {
        }
    }
    public class Muscle : Specialist
    {
/*         public string Name { get; set; }
        public int SkillLevel { get; set; }
        public int PercentageCut { get; set; } */
        public override void PerformSkill(Bank bank)
        {
            Console.WriteLine($"{Name} is distracting the guards by doing cross-fit in the lobby");
            bank.SecurityGuardScore -= SkillLevel;
            if (bank.SecurityGuardScore < 0)
            {
                Console.WriteLine($"{Name} has convinced the guards to join their workout. They are completely neutralized!");
            }
        }
        public Muscle(string name, int skill, int percentage) : base(name, skill, percentage)
        {
        }
    }
    public class Locksmith : Specialist
    {
/*         public string Name { get; set; }
        public int SkillLevel { get; set; }
        public int PercentageCut { get; set; } */
        public override void PerformSkill(Bank bank)
        {
            Console.WriteLine($"{Name} has going to town on the bank vault");
            bank.VaultScore -= SkillLevel;
            if (bank.VaultScore < 0)
            {
                Console.WriteLine($"{Name} has cracked the vault. The gold bullion ready to go!");
            }
        }
        public Locksmith(string name, int skill, int percentage) : base(name, skill, percentage)
        {
        }
    }
}
