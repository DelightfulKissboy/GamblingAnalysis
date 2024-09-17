namespace GamblingAnalysis
{
    public class RouletteGamblingStrategy : IGamblingStrategy
    {
        private RouletteWheel _rouletteWheel;
        public RouletteGamblingStrategy(RouletteWheel rouletteWheel)
        {
            this._rouletteWheel = rouletteWheel;
        }

        public int Bet(int betAmount)
        {
            return this._rouletteWheel.BetLows() * betAmount;
        }
    }
}
