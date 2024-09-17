namespace GamblingAnalysis
{
    public class RouletteWheel
    {
        private Random _r;
        public RouletteWheel(Random r)
        {
            this._r = r;
        }

        /// <summary>
        /// Generates a number on the roulette wheel. 37 represents 00, all other numbers
        /// are themselves.
        /// </summary>
        /// <returns>The number spun</returns>
        public byte Spin() { return (byte)_r.Next(38); }

        /// <summary>
        /// Bet on the low numbers.
        /// </summary>
        /// <returns>1 if the spin is low, -1 otherwise.</returns>
        public int BetLows()
        {
            var spin = Spin();
            if (spin != 0 && spin <= 18)
            {
                return 1;
            }

            return -1;
        }
    }
}
