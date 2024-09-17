namespace GamblingAnalysis
{
    public enum Game
    {
        Baccarat = 0,
        Blackjack = 1,
        Craps = 2,
        Lottery = 3,
        Roulette = 4,
        Slots = 5
    }

    internal class Program
    {
        public const Game GameToPlay = Game.Baccarat;

        static void Main(string[] args)
        {
            Random r = new Random();
                    
            string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(dataPath, $"{GameToPlay}_gambling_data.txt")))
            {
                for (int i = 0; i < 100000; i++)
                {
                    IGamblingStrategy strategy;
                    switch (GameToPlay)
                    {
                        case Game.Baccarat:
                            var baccaratGame = new BaccaratGame(r);
                            strategy = new BaccaratGamblingStrategy(baccaratGame);
                            break;
                        case Game.Blackjack:
                            var blackjackGame = new BlackjackGame(r);
                            strategy = new BlackjackGamblingStrategy(blackjackGame);
                            break;
                        case Game.Craps:
                            var crapsGame = new CrapsGame(r);
                            strategy = new CrapsGamblingStrategy(crapsGame);
                        case Game.Lottery:
                            var lotteryGame = new LotteryGame(r);
                            strategy = new LotteryGamblingStrategy(lotteryGame);
                        case Game.Roulette:
                            var rouletteWheel = new RouletteWheel(r);
                            strategy = new RouletteGamblingStrategy(rouletteWheel);
                        case Game.Slots:
                            LookupTables lookupTables = new LookupTables();
                            SlotMachine slotMachine = new SlotMachine(r, lookupTables);
                            strategy = new SlotMachineGamblingStrategy(slotMachine);
                            break;

                    }
                    outputFile.WriteLine(GambleAllBankroll(1000, 20, strategy) - 1000);
                }
            }
        }

        /// <summary>
        /// Represents a strategy where you make flat bets for your whole bankroll and pocket winnings.
        /// </summary>
        /// <param name="bankroll">Bankroll entering with.</param>
        /// <param name="bet">Flat bet per game.</param>
        /// <param name="gamblingStrategy">Strategy/game played</param>
        /// <returns>Money at the end of the session</returns>
        static int GambleAllBankroll(int bankroll, int bet, IGamblingStrategy gamblingStrategy)
        {
            int winnings = 0;
            while (bankroll >= bet)
            {
                var result = gamblingStrategy.Bet(bet);
                if (result >= 0)
                {
                    winnings += result;
                }
                else
                {
                    bankroll += result;
                }
            }

            return winnings + bankroll;
        }

        static int MakeNBets(int money, int bet, int numBets, IGamblingStrategy gamblingStrategy)
        {
            for (int i = 0; i < numBets; i++)
            {
                var result = gamblingStrategy.Bet(bet);
                // Don't count break-evens as a bet.
                while (result == 0)
                {
                    result = gamblingStrategy.Bet(bet);
                }

                money += result;
            }

            return money;
        }
    }
}