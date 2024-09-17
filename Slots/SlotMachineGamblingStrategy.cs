namespace GamblingAnalysis
{
    public class SlotMachineGamblingStrategy : IGamblingStrategy
    {
        private readonly SlotMachine _slotMachine;
        public SlotMachineGamblingStrategy(SlotMachine slotMachine) {
            _slotMachine = slotMachine;
        }

        /// <summary>
        /// Bet using the slot machine.
        /// </summary>
        /// <param name="bet">The amount bet.</param>
        /// <returns>Negative bet if a loss, winnings otherwise.</returns>
        public int Bet(int bet)
        {
            return _slotMachine.ScoreOutcome(_slotMachine.GenerateOutcome()) * bet - bet;
        }
    }
}
