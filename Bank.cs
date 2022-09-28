namespace heist
{
    public class Bank
    {
        public int Difficulty { get; set; }
        public int CashOnHand { get; set; }
        public int AlarmScore { get; set; }
        public int VaultScore { get; set; }
        public int SecurityGuardScore { get; set; }
        public bool IsSecure
        {
            get
            {
                return AlarmScore <= 0 && VaultScore <= 0 && SecurityGuardScore <= 0 ? false : true;
            }
        }
        public Bank(int userDifficulty)
        {
            this.Difficulty = userDifficulty;
        }
    }
}
