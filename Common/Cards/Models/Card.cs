namespace GamblingAnalysis
{
    /// <summary>
    /// Represents a standard French playing card.
    /// </summary>
    public class Card
    {
        public Suit Suit { get; }
        public Rank Rank { get; }

        public Card(Suit suit, Rank rank) {
            this.Suit = suit;
            this.Rank = rank;
        }
    }
}
