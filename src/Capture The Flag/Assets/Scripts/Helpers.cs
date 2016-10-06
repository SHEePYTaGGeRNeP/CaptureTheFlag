namespace Assets.Scripts
{
    class Helpers
    {
        public static int LosesOf(int nr)
        {
            switch (nr)
            {
                case 0: return 2;
                case 1: return 0;
                default: return 1;
            }
        }
        
        public static string RPSToString(int nr)
        {
            switch (nr)
            {
                case 0: return "Steen";
                case 1: return "Papier";
                default: return "Schaar";
            }
        }
    }
}
