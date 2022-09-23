namespace heist
{
    public class Teammate
    {
        public string Name {get;}
        public int Skill {get;}
        public decimal Courage {get;}

        public Teammate(string name, int skill, decimal courage)
        {
            this.Name = name;
            this.Skill = skill;
            this.Courage = courage;
            PrintInfo();
        }
        public void PrintInfo()
        {
            Console.WriteLine($"\n-~-~-{Name}-~-~-");
            Console.WriteLine($"Skill Level: {Skill}");
            Console.WriteLine($"Courage Factor: {Courage}\n");
        }
    }
}