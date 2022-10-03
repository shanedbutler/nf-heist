namespace heist
{
    class Rolodex
    {
        public List<IRobber> List { get; set; } = new List<IRobber>();
        public int Cheapest()
        {
            return List.Min(m => m.PercentageCut);
        }
    }
}
