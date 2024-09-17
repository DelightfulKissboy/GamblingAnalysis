namespace GamblingAnalysis
{
    public class SlotMachine
    {
        private readonly Random _r;
        private readonly LookupTables _lookupTables;

        public SlotMachine(Random r, LookupTables lookupTables) {
            this._r = r;
            this._lookupTables = lookupTables;
        }

        public Outcome GenerateOutcome()
        {
            var reel1Result = this._lookupTables.Reel1Lookup[(byte)this._r.Next(64)];
            var reel2Result = this._lookupTables.Reel2Lookup[(byte)this._r.Next(64)];
            var reel3Result = this._lookupTables.Reel3Lookup[(byte)this._r.Next(64)];

            return new Outcome(reel1Result, reel2Result, reel3Result);
        }

        public int ScoreOutcome(Outcome outcome)
        {
            if (outcome == null) return 0;

            if (outcome == new Outcome(Symbol.RedSeven, Symbol.WhiteSeven, Symbol.BlueSeven)) return 2400;
            if (outcome == new Outcome(Symbol.RedSeven, Symbol.RedSeven, Symbol.RedSeven)) return 1199;
            if (outcome == new Outcome(Symbol.WhiteSeven, Symbol.WhiteSeven, Symbol.WhiteSeven)) return 200;
            if (outcome == new Outcome(Symbol.BlueSeven, Symbol.BlueSeven, Symbol.BlueSeven)) return 150;
            if (outcome == new Outcome(Symbol.OneBar, Symbol.TwoBar, Symbol.ThreeBar)) return 50;
            if (outcome == new Outcome(Symbol.ThreeBar, Symbol.ThreeBar, Symbol.ThreeBar)) return 40;
            if (outcome == new Outcome(Symbol.TwoBar, Symbol.TwoBar, Symbol.TwoBar)) return 25;
            if (outcome == new Outcome(Symbol.OneBar, Symbol.OneBar, Symbol.OneBar)) return 10;
            if (outcome == new Outcome(Symbol.Blank, Symbol.Blank, Symbol.Blank)) return 1;


            if (IsSeven(outcome.Reels[0]) && IsSeven(outcome.Reels[1]) && IsSeven(outcome.Reels[2])) return 80;
            if (IsBar(outcome.Reels[0]) && IsBar(outcome.Reels[1]) && IsBar(outcome.Reels[2])) return 5;

            Color reel1Color = GetColor(outcome.Reels[0]);
            Color reel2Color = GetColor(outcome.Reels[1]);
            Color reel3Color = GetColor(outcome.Reels[2]);
            if (reel1Color == Color.Red && reel2Color == Color.White && reel3Color == Color.Blue) return 20;
            if (reel1Color == reel2Color && reel2Color == reel3Color) return 2;

            return 0;
        }

        private bool IsSeven(Symbol symbol)
        {
            return symbol == Symbol.RedSeven || symbol == Symbol.BlueSeven || symbol == Symbol.WhiteSeven;
        }

        private bool IsBar(Symbol symbol)
        {
            return symbol == Symbol.OneBar || symbol == Symbol.TwoBar || symbol == Symbol.ThreeBar;
        }

        private Color GetColor(Symbol symbol)
        {
            switch (symbol)
            {
                case Symbol.RedSeven:
                    return Color.Red;
                case Symbol.BlueSeven:
                    return Color.Blue;
                case Symbol.WhiteSeven:
                    return Color.White;
                case Symbol.OneBar:
                    return Color.Red;
                case Symbol.TwoBar:
                    return Color.White;
                case Symbol.ThreeBar:
                    return Color.Blue;
                default:
                    return Color.None;
            }
        }
    }
}
