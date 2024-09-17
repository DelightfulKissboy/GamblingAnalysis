using System.Collections;

namespace GamblingAnalysis.Blackjack
{
    public class Hand : IEnumerable<Card>
    {
        private IList<Card> _cards;

        public int Bet { get; set; }

        public Card First { get { return _cards[0]; } }

        public Card Second { get { return _cards[1]; } }

        public Hand(Card card1, Card card2) : this(card1, card2, 0) { }

        public Hand(Card card1, Card card2, int bet)
        {
            this._cards = new List<Card>
            {
                card1,
                card2
            };
            this.Bet = bet;
        }

        public void Add(Card card)
        {
            _cards.Add(card);
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return this._cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
