using GamblingAnalysis.Blackjack;

namespace GamblingAnalysis
{
    public class BlackjackGame
    {
        private Random _r;
        private Shoe _shoe;

        public BlackjackGame(Random r) {
            this._r = r;
            this._shoe = new Shoe(Settings.NumDecks, r);
        }
        public int PlayGame(int bet)
        {
            if (this._shoe.Count() <= 75) this._shoe = new Shoe(Settings.NumDecks, this._r);

            var result = 0;
            var playerHand = new Hand(this._shoe.Draw(), this._shoe.Draw(), bet);
            var otherPlayerHands = new List<Hand>();
            // Simulate other players to have realistic chew through of deck.
            for (int i = 0; i < Settings.NumOtherPlayers; i++)
            {
                otherPlayerHands.Add(new Hand(this._shoe.Draw(), this._shoe.Draw()));
            }

            var dealerHand = new Hand(this._shoe.Draw(), this._shoe.Draw());

            foreach (var hand in otherPlayerHands)
            {
                PlayPlayerHand(hand, dealerHand.First);
            }

            var playerBlackjack = GetHandValue(playerHand) == 21;
            var dealerBlackjack = GetHandValue(dealerHand) == 21;
            if (playerBlackjack && dealerBlackjack) return 0;

            if (playerBlackjack) return bet * 3 / 2;

            var playerHands = PlayPlayerHand(playerHand, dealerHand.First);

            if (dealerBlackjack && Settings.LoseOnlyOGBetDealerBJ) return -bet;
            PlayDealerHand(dealerHand);
            var dealerHandValue = GetHandValue(dealerHand);
            foreach (var hand in playerHands)
            {
                var handValue = GetHandValue(hand);
                if (handValue > 21)
                {
                    result -= hand.Bet;
                    continue;
                }

                if (dealerHandValue > 21 || handValue > dealerHandValue)
                {
                    result += hand.Bet;
                } else if (handValue < dealerHandValue) {
                    result -= hand.Bet;
                }
            }

            return result;
        }

        public IList<Hand> PlayPlayerHand(Hand playerHand, Card dealerRevealedCard, byte numHands = 1)
        {
            var result = new List<Hand>();

            var canSplit = CardUtils.GetCardValue(playerHand.First) == CardUtils.GetCardValue(playerHand.Second) && numHands < Settings.MaxNumHands;
            var playerAction = GetInitialPlayerAction(playerHand, dealerRevealedCard, canSplit);
            switch (playerAction)
            {
                case Action.Stand:
                    result.Add(playerHand);
                    return result;
                case Action.Hit:
                    playerHand.Add(this._shoe.Draw());
                    break;
                case Action.Split:
                    var firstHand = new Hand(playerHand.First, this._shoe.Draw(), playerHand.Bet);
                    var secondHand = new Hand(playerHand.Second, this._shoe.Draw(), playerHand.Bet);
                    if (firstHand.First.Rank == Rank.Ace)
                    {
                        // Split aces; just return hands.
                        result.Add(firstHand);
                        result.Add(secondHand);
                        return result;
                    }

                    var firstHands = PlayPlayerHand(firstHand, dealerRevealedCard, (byte)(numHands + 1));
                    var secondHands = PlayPlayerHand(secondHand, dealerRevealedCard, (byte)(firstHands.Count + 1));

                    foreach (var hand in firstHands)
                    {
                        result.Add(hand);
                    }

                    foreach (var hand in secondHands)
                    {
                        result.Add(hand);
                    }

                    return result;
                case Action.DoubleDown:
                    playerHand.Bet *= 2;
                    playerHand.Add(this._shoe.Draw());
                    result.Add(playerHand);
                    return result;
            }

            while (GetHandValue(playerHand) < 21)
            {
                playerAction = GetPlayerAction(playerHand, dealerRevealedCard);
                switch (playerAction)
                {
                    case Action.Hit:
                        playerHand.Add(this._shoe.Draw());
                        break;
                    default:
                        result.Add(playerHand);
                        return result;
                }
            }

            result.Add(playerHand);
            return result;
        }

        public void PlayDealerHand(Hand dealerHand)
        {
            bool soft;
            var handValue = GetHandValue(dealerHand, out soft);
            while (handValue < 17 || (Settings.DealerHitsSoft17 && handValue == 17 && soft))
            {
                dealerHand.Add(this._shoe.Draw());
                handValue = GetHandValue(dealerHand, out soft);
            }
        }

        private Action GetInitialPlayerAction(Hand playerHand, Card dealerRevealedCard, bool canSplit)
        {
            var dealerCardValue = CardUtils.GetCardValue(dealerRevealedCard);

            if (canSplit)
            {
                var shouldSplit = playerHand.First.Rank == Rank.Ace;
                shouldSplit |= playerHand.First.Rank == Rank.Nine && dealerCardValue != 1 && dealerCardValue != 7 && dealerCardValue != 10;
                shouldSplit |= playerHand.First.Rank == Rank.Eight;
                shouldSplit |= playerHand.First.Rank == Rank.Seven && dealerCardValue != 1 && dealerCardValue <= 7;
                shouldSplit |= playerHand.First.Rank == Rank.Six && dealerCardValue >= 3 && dealerCardValue <= 6;
                shouldSplit |= playerHand.First.Rank == Rank.Six && Settings.DoubleAfterSplitAllowed && dealerCardValue == 2;
                shouldSplit |= playerHand.First.Rank == Rank.Four && Settings.DoubleAfterSplitAllowed && (dealerCardValue == 5 || dealerCardValue == 6);
                shouldSplit |= (playerHand.First.Rank == Rank.Three || playerHand.First.Rank == Rank.Two) && dealerCardValue >= 4 && dealerCardValue <= 7;
                shouldSplit |= (playerHand.First.Rank == Rank.Three || playerHand.First.Rank == Rank.Two) && Settings.DoubleAfterSplitAllowed && (dealerCardValue == 2 || dealerCardValue == 3);

                if (shouldSplit) return Action.Split;
            }

            var playerHandValue = GetHandValue(playerHand, out bool soft);
            if (soft && dealerCardValue <= 6)
            {
                var softShouldDoubleDown = playerHandValue == 19 && dealerCardValue == 6;
                softShouldDoubleDown |= playerHandValue == 18 && dealerCardValue >= 2;
                softShouldDoubleDown |= playerHandValue == 17 && dealerCardValue >= 3;
                softShouldDoubleDown |= (playerHandValue == 15 || playerHandValue == 16) && dealerCardValue >= 4;
                softShouldDoubleDown |= (playerHandValue == 13 || playerHandValue == 14) && dealerCardValue >= 5;

                if (softShouldDoubleDown) return Action.DoubleDown;
            }

            var shouldDoubleDown = playerHandValue == 11;
            shouldDoubleDown |= playerHandValue == 10 && dealerCardValue != 1 && dealerCardValue != 10;
            shouldDoubleDown |= playerHandValue == 9 && dealerCardValue >= 3 && dealerCardValue <= 6;

            if (shouldDoubleDown) return Action.DoubleDown;

            return GetPlayerAction(playerHand, dealerRevealedCard);
        }

        private Action GetPlayerAction(Hand playerHand, Card dealerRevealedCard)
        {
            var handValue = GetHandValue(playerHand, out bool soft);
            var dealerCardValue = CardUtils.GetCardValue(dealerRevealedCard);
            if (soft)
            {
                if (handValue >= 19)
                {
                    return Action.Stand;
                }
                
                if (handValue == 18)
                {
                    if (dealerCardValue == 1 || dealerCardValue >= 9) return Action.Hit;

                    return Action.Stand;
                }

                return Action.Hit;
            }

            if (handValue >= 17)
            {
                return Action.Stand;
            }

            if (handValue >= 13)
            {
                if (dealerCardValue == 1 || dealerCardValue >= 7) return Action.Hit;

                return Action.Stand;
            }

            if (handValue == 12)
            {
                if (dealerCardValue <= 3 || dealerCardValue >= 7) return Action.Hit;

                return Action.Stand;
            }

            return Action.Hit;
        }

        private byte GetHandValue(Hand hand)
        {
            return GetHandValue(hand, out bool soft);
        }

        private byte GetHandValue(Hand hand, out bool soft)
        {
            byte total = 0;
            byte numAces = 0;

            soft = false;

            // Total non-ace values first.
            foreach (var card in hand) {
                if (card.Rank == Rank.Ace)
                {
                    numAces++;
                } else
                {
                    total += CardUtils.GetCardValue(card);
                }
            }

            if (numAces == 0) return (byte)total;

            var highAceTotal = total + 11 + numAces - 1;
            var lowAceTotal = total + numAces;

            if (highAceTotal > 21) return (byte)lowAceTotal;

            soft = true;
            return (byte)highAceTotal;
        }
    }
}
