namespace GamblingAnalysis
{
    public class BlackjackGamblingStrategy : IGamblingStrategy
    {
        private BlackjackGame _game;

        public BlackjackGamblingStrategy(BlackjackGame game) {
            this._game = game;
        }

        public int Bet(int betAmount)
        {
            return this._game.PlayGame(betAmount);
        }
    }
}
