namespace GamblingAnalysis
{
    public class CrapsGame
    {
        private Random _r;

        public CrapsGame(Random r) {
            this._r = r;
        }

        public int DontPassBetWithOdds(int bet)
        {
            byte comeOut = Roll();
            if (comeOut == 12)
            {
                return 0;
            }

            if (comeOut == 2 || comeOut == 3)
            {
                return bet;
            }

            if (comeOut == 7 || comeOut == 11)
            {
                return -bet;
            }

            int oddsBet;
            // Make odds bet as close to original bet as possible, but divisible
            // by the payout
            if (comeOut == 4 || comeOut == 10)
            {
                oddsBet = bet - bet % 2;
            } else if (comeOut == 5 || comeOut == 9)
            {
                oddsBet = bet - bet % 3;
            } else
            {
                oddsBet = bet - bet % 6;
            }

            while (true)
            {
                var roll = Roll();
                if (roll == 7)
                {
                    int oddsReward = 0;
                    if (comeOut == 4 || comeOut == 10)
                    {
                        oddsReward = oddsBet / 2;
                    } else if (comeOut == 5 || comeOut == 9)
                    {
                        oddsReward += oddsBet * 2 / 3;
                    } else if (comeOut == 6 || comeOut == 8)
                    {
                        oddsReward += oddsBet * 5 / 6;
                    }

                    return bet + oddsReward;
                }

                if (roll == comeOut)
                {
                    return -bet - oddsBet;
                }
            }
        }

        private byte Roll()
        {
            return (byte)(this._r.Next(6) + this._r.Next(6) + 2);
        }
    }
}
