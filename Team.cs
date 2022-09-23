namespace heist
{
    class Team
    {
        public List<Teammate> Mates = new List<Teammate>();

        public void AddMate(Teammate mate)
        {
            this.Mates.Add(mate);
        }
    }
}