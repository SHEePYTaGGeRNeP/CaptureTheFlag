namespace Assets.Scripts
{
    using System;

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

        public static string RPSToString(Choice choice)
        {
            switch (choice)
            {
                case Choice.Rock:
                    return "Vuur";
                case Choice.Paper:
                    return "Water";
                case Choice.Scissors:
                    return "Boom";
                default:
                    throw new ArgumentOutOfRangeException("choice", choice, null);
            }
        }
    }
}
