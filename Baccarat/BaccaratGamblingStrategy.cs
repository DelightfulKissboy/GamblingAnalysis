namespace GamblingAnalysis
{
    public class BaccaratGamblingStrategy : IGamblingStrategy
    {
        private readonly BaccaratGame _game;
        public BaccaratGamblingStrategy(BaccaratGame game)
        {
            this._game = game;
        }

        public int Bet(int betAmount)
        {
            return this._game.BetDealer(betAmount);
        }
    }
}
