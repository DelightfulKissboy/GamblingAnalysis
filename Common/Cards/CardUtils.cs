namespace GamblingAnalysis
{
    public static class CardUtils
    {
        public static byte GetCardValue(Card card)
        {
            byte result = (byte)((byte)card.Rank + 1);
            if (result >= 10) return 10;

            return result;
        }
    }
}
