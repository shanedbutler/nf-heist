namespace heist
{
    public class Teammate
    {
        public string Name { get; }
        public int Skill { get; }
        public decimal Courage { get; }
        public string Specialty { get; }

        public Teammate(string name, string specialty, int skill, decimal courage)
        {
            this.Name = name;
            this.Specialty = specialty;
            this.Skill = skill;
            this.Courage = courage;
            PrintInfo();
        }
        public void PrintInfo()
        {
            Console.WriteLine($"\n-~-~-{Name}-~-~-");
            Console.WriteLine($"Specialty: {Specialty}");
            Console.WriteLine($"Skill Level: {Skill}");
            Console.WriteLine($"Courage Factor: {Courage}");
        }
    }
}
