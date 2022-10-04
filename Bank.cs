namespace heist
{
    public class Bank
    {
        public int CashOnHand { get; set; } = new Random().Next(50000, 1000000);
        public int AlarmScore { get; set; } = new Random().Next(0, 100);
        public int VaultScore { get; set; } = new Random().Next(0, 100);
        public int SecurityGuardScore { get; set; } = new Random().Next(0, 100);
        public bool IsSecure
        {
            get
            {
                return AlarmScore <= 0 && VaultScore <= 0 && SecurityGuardScore <= 0 ? false : true;
            }
        }
        public void printReport()
        {
            Dictionary<string, int> scores = new Dictionary<string, int>()
            {
                {"Alarm", AlarmScore},
                {"Vault", VaultScore},
                {"Security", SecurityGuardScore}
            };

            var sorted = from score in scores
                        orderby score.Value
                        select score;

            string mostSecure = (from score in sorted
                                 select score.Key).First();

            string leastSecure = (from score in sorted
                                  select score.Key).Last();

            Console.WriteLine($"Most Secure: {mostSecure}");
            Console.WriteLine($"Least Secure: {leastSecure}\n");
        }
    }
}
