namespace GamblingAnalysis
{
    public interface IGamblingStrategy
    {
        /// <summary>
        /// Runs a bet using the gambling strategy.
        /// </summary>
        /// <param name="betAmount">Amount that was bet</param>
        /// <returns>0 if a loss or bet plus winnings if a win</returns>
        int Bet(int betAmount);
    }
}
