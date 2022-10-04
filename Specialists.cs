namespace heist
{
    interface IRobber
    {
        string Name { get; set; }
        string Specialty { get; set; }
        int SkillLevel { get; set; }
        int PercentageCut { get; set; }
        void PrintInfo();
        bool PerformSkill(Bank bank, int luck);
    }
    public class Specialist : IRobber
    {
        public string Name { get; set; }
        public string Specialty { get; set; }
        public int SkillLevel { get; set; }
        public int PercentageCut { get; set; }
        public virtual bool PerformSkill(Bank bank, int luck)
        {
            Console.WriteLine($"{Name} does their thing! It was cool, but had really no effect. A failure!");
            return false;
        }
        public void PrintInfo()
        {
            Console.WriteLine($"{Name}");
            Console.WriteLine($"Specialty: {Specialty}");
            Console.WriteLine($"Skill Level: {SkillLevel}");
            Console.WriteLine($"Take Percentage: {PercentageCut}\n");
        }
        public Specialist(string name, string specialty, int skill, int percentage)
        {
            this.Name = name;
            this.Specialty = specialty;
            this.SkillLevel = skill;
            this.PercentageCut = percentage;
        }
    }
    public class Hacker : Specialist
    {
        public override bool PerformSkill(Bank bank, int luck)
        {
            Console.WriteLine($"{Name} is infiltrating the mainframe");
            bank.AlarmScore -= SkillLevel + luck;
            if (bank.AlarmScore < 0)
            {
                Console.WriteLine($"{Name} has hacked their system wide open!");
                return true;
            }
            else
            {
                Console.WriteLine($"{Name} has been foiled by Azure security services!");
                return false;
            }
        }
        public Hacker(string name, string specialty, int skill, int percentage) : base(name, specialty, skill, percentage)
        {
        }
    }
    public class Muscle : Specialist
    {
        public override bool PerformSkill(Bank bank, int luck)
        {
            Console.WriteLine($"{Name} is distracting the guards by doing cross-fit in the lobby");
            bank.SecurityGuardScore -= SkillLevel + luck;
            if (bank.SecurityGuardScore < 0)
            {
                Console.WriteLine($"{Name} has convinced the guards to join their workout. They are completely neutralized!");
                return true;
            }
            else
            {
                Console.WriteLine($"{Name} has miscalculated their appetite for fitness and has been taken out!");
                return false;
            }
        }
        public Muscle(string name, string specialty, int skill, int percentage) : base(name, specialty, skill, percentage)
        {
        }
    }
    public class Locksmith : Specialist
    {
        public override bool PerformSkill(Bank bank, int luck)
        {
            Console.WriteLine($"{Name} has going to town on the bank vault");
            bank.VaultScore -= SkillLevel + luck;
            if (bank.VaultScore < 0)
            {
                Console.WriteLine($"{Name} has cracked the vault. The gold bullion ready to go!");
                return true;
            }
            else
            {
                Console.WriteLine($"{Name} has tripped the security mechanism and is locked within the vault!");
                return false;
            }
        }
        public Locksmith(string name, string specialty, int skill, int percentage) : base(name, specialty, skill, percentage)
        {
        }
    }
}
