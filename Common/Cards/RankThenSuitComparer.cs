namespace GamblingAnalysis
{
    public class RankThenSuitComparer : IComparer<Card>
    {
        public int Compare(Card? x, Card? y)
        {
            if (x == null || y == null) return 0;
            return x.Rank == y.Rank ? x.Suit - y.Suit : x.Rank - y.Rank;
        }
    }
}
