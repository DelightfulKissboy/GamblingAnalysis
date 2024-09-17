namespace GamblingAnalysis
{
    public class LotteryGamblingStrategy : IGamblingStrategy
    {
        private LotteryGame _game;

        public LotteryGamblingStrategy(LotteryGame game) {
            this._game = game;
        }

        public int Bet(int betAmount)
        {
            return _game.PlayLottery(betAmount);
        }
    }
}
