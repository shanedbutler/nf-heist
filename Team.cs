namespace heist
{
    class Team
    {
        public string Name { get; }
        public Team(string name)
        {
            this.Name = name;
        }
        public List<Teammate> Mates = new List<Teammate>();

        public void AddMate(Teammate mate)
        {
            this.Mates.Add(mate);
        }
    }
}