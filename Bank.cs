namespace heist
{
    class Bank
    {
        public int Difficulty {get; set;}
        public Bank(int userDifficulty)
        {
            this.Difficulty = userDifficulty;
        }
    }
}