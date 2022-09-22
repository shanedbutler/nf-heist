namespace heist
{
    public class Teammate
    {
        public string Name {get; set;}
        public int Skill {get; set;}
        public decimal Courage {get; set;}
        public void PrintInfo()
        {
            Console.WriteLine($"-~-~-{Name}-~-~-");
            Console.WriteLine($"Skill Level: {Skill}");
            Console.WriteLine($"Courage Factor: {Courage}");
        }
    }
}