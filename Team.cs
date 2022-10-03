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

        public void AddMate(IRobber mate)
        {
            this.Mates.Add(mate);
        }
        public int SkillTotal { get; set;}
        private static Random rnd = new Random();
        public int Luck { get => rnd.Next(-10, 10); }
    }
}
