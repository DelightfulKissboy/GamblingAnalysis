namespace GamblingAnalysis
{
    /// <summary>
    /// Represents a shoe of one or more decks shuffled together.
    /// </summary>
    public class Shoe
    {
        private readonly Random _r;
        private LinkedList<Card> _cards;
        public Shoe(byte numDecks, Random r)
        {
            this._r = r;

            _cards = new LinkedList<Card>();
            foreach (var suit in Enum.GetValues<Suit>())
            {
                foreach (var rank in Enum.GetValues<Rank>())
                {
                    for (byte deck = 0; deck < numDecks; deck++)
                    {
                        this._cards.AddLast(new Card(suit, rank));
                    }
                }
            }

            this.Shuffle();
        }

        public void Shuffle()
        {
            int numCards = _cards.Count;
            this._cards = new LinkedList<Card>(_cards.OrderBy((c) => { return (_r.Next() % numCards); }));
        }

        public Card Draw() {
            var card = this._cards.First();
            this._cards.RemoveFirst();
            return card;
        }

        public int Count()
        {
            return this._cards.Count;
        }
    }
}
