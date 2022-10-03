namespace heist
{
    class Team
    {
        public string Name { get; }
        public Team(string name)
        {
            this.Name = name;
        }
        public List<IRobber> Mates = new List<IRobber>();

        public void AddMate(IRobber mate, List<IRobber> roster)
        {
            this.Mates.Add(mate);
            roster.Remove(mate);
        }
        public void PrintInfo()
        {
            Console.WriteLine($"\nCurrent crew \"{Name}\" info:");
            Console.WriteLine($"Combined skill of crew: {SkillTotal}");
            Console.WriteLine($"Crew budget remaining: {100 - PercentageTotal}%");
            Console.WriteLine($"{Mates.Count} crewmates: \n");
            foreach (Specialist mate in Mates)
            {
                mate.PrintInfo();
            }
        }
        public int SkillTotal { get => this.Mates.Sum(m => m.SkillLevel); }
        public int PercentageTotal { get => this.Mates.Sum(m => m.PercentageCut); }
        private static Random rnd = new Random();
        public int Luck { get => rnd.Next(-10, 10); }
    }
}
