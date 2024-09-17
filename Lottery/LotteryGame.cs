namespace GamblingAnalysis
{
    public class LotteryGame
    {
        const int Jackpot = 121000000;
        private Random _r;

        public LotteryGame(Random r)
        {
            this._r = r;
        }

        public int PlayLottery(int betAmount)
        {
            int numTickets = betAmount / 2;
            int result = betAmount % 2;

            var lotteryNumbers = new LottoNumbers(this._r);

            for (int i = 0; i < numTickets; i++)
            {
                var playerNumbers = new LottoNumbers(this._r);
                var numMatches = 0;
                foreach (var num in lotteryNumbers.Numbers)
                {
                    if (playerNumbers.Numbers.Contains(num))
                    {
                        numMatches++;
                    }
                }

                var powerballMatch = lotteryNumbers.PowerBall == playerNumbers.PowerBall;
                if (numMatches == 5)
                {
                    result += powerballMatch ? Jackpot : 1000000;
                } else if (numMatches == 4)
                {
                    result += powerballMatch ? 50000 : 100;
                } else if (numMatches == 3)
                {
                    result += powerballMatch ? 100 : 7;
                } else if (numMatches == 2 && powerballMatch)
                {
                    result += 7;
                } else if (powerballMatch)
                {
                    result += 4;
                }

                result -= 2;
            }

            return result;
        }
    }
}
