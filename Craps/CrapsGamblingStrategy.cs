namespace GamblingAnalysis
{
    public class CrapsGamblingStrategy : IGamblingStrategy
    {
        private CrapsGame _crapsGame;

        public CrapsGamblingStrategy(CrapsGame crapsGame) {
            this._crapsGame = crapsGame;
        }

        public int Bet(int betAmount)
        {
            return this._crapsGame.DontPassBetWithOdds(betAmount);
        }
    }
}
