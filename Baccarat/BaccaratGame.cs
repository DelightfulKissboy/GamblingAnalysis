using GamblingAnalysis.Baccarat;

namespace GamblingAnalysis
{
    public class BaccaratGame
    {
        private readonly Random _r;
        private Shoe _shoe;

        public BaccaratGame(Random r) {
            this._r = r;
            CreateNewShoe();
        }

        public int BetDealer(int bet)
        {
            var result = PlayCoup();
            switch (result)
            {
                case BaccaratResult.DealerWin:
                    return (int)(bet * 0.95);
                case BaccaratResult.Tie:
                    return 0;
                default:
                    return -bet;
            }
        }

        public int BetPlayer(int bet)
        {
            var result = PlayCoup();
            switch (result)
            {
                case BaccaratResult.PlayerWin:
                    return bet;
                case BaccaratResult.Tie:
                    return 0;
                default:
                    return -bet;
            }
        }

        private BaccaratResult PlayCoup()
        {
            ResetShoeIfNecessary();
            var playerTotal = AddCardValues(CardUtils.GetCardValue(this._shoe.Draw()), CardUtils.GetCardValue(this._shoe.Draw()));
            var dealerTotal = AddCardValues(CardUtils.GetCardValue(this._shoe.Draw()), CardUtils.GetCardValue(this._shoe.Draw()));

            if (playerTotal >= 8 || dealerTotal >= 8) return GetResultFromTotals(playerTotal, dealerTotal);

            bool playerDrew;
            Card playerThirdCard = null;
            if (playerDrew = playerTotal <= 5)
            {
                playerThirdCard = this._shoe.Draw();

                playerTotal = AddCardValues(playerTotal, CardUtils.GetCardValue(playerThirdCard));
            }

            if ((playerDrew && DealerDrawsGivenPlayerDrew(dealerTotal, CardUtils.GetCardValue(playerThirdCard))) || (!playerDrew && dealerTotal <= 5))
            {
                dealerTotal = AddCardValues(dealerTotal, CardUtils.GetCardValue(this._shoe.Draw()));
            }

            return GetResultFromTotals(playerTotal, dealerTotal);
        }

        private void CreateNewShoe()
        {
            this._shoe = new Shoe(Settings.NumDecks, _r);
            var numToBurn = CardUtils.GetCardValue(this._shoe.Draw());
            for (int i = 0; i < numToBurn; i++)
            {
                this._shoe.Draw();
            }
        }

        private void ResetShoeIfNecessary()
        {
            if (_shoe.Count() > 7) return;

            CreateNewShoe();
        }

        private byte AddCardValues(byte value1, byte value2)
        {
            return (byte)((value1 + value2) % 10);
        }

        private BaccaratResult GetResultFromTotals(byte playerTotal, byte dealerTotal)
        {
            if (playerTotal == dealerTotal) return BaccaratResult.Tie;
            return playerTotal > dealerTotal ? BaccaratResult.PlayerWin : BaccaratResult.DealerWin;
        }

        private bool DealerDrawsGivenPlayerDrew(byte dealerTotal, byte playerThirdCardValue)
        {
            bool result = dealerTotal <= 2;
            result |= dealerTotal == 3 && playerThirdCardValue != 8;
            result |= dealerTotal == 4 && playerThirdCardValue >= 2 && playerThirdCardValue <= 7;
            result |= dealerTotal == 5 && playerThirdCardValue >= 4 && playerThirdCardValue <= 7;
            result |= dealerTotal == 6 && (playerThirdCardValue == 6 || playerThirdCardValue == 7);

            return result;
        }
    }
}
