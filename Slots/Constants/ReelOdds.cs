namespace GamblingAnalysis
{
    public static class ReelOdds
    {
        public static readonly IDictionary<Symbol, byte> Reel1OddsMapping = new Dictionary<Symbol, byte>()
        {
            { Symbol.Blank, 32 },
            { Symbol.OneBar, 6 },
            { Symbol.TwoBar, 7 },
            { Symbol.ThreeBar, 6 },
            { Symbol.BlueSeven, 6 },
            { Symbol.WhiteSeven, 6 },
            { Symbol.RedSeven, 1 },
        };

        public static readonly IDictionary<Symbol, byte> Reel2OddsMapping = new Dictionary<Symbol, byte>(){
            { Symbol.Blank, 32 },
            { Symbol.OneBar, 8 },
            { Symbol.TwoBar, 6 },
            { Symbol.ThreeBar, 7 },
            { Symbol.BlueSeven, 7 },
            { Symbol.WhiteSeven, 1 },
            { Symbol.RedSeven, 3 },
        };

        public static readonly IDictionary<Symbol, byte> Reel3OddsMapping = new Dictionary<Symbol, byte>(){
            { Symbol.Blank, 32 },
            { Symbol.OneBar, 9 },
            { Symbol.TwoBar, 9 },
            { Symbol.ThreeBar, 5 },
            { Symbol.BlueSeven, 1 },
            { Symbol.WhiteSeven, 7 },
            { Symbol.RedSeven, 1 },
        };
    }
}
