namespace GamblingAnalysis
{
    public class LookupTables
    {
        public readonly IDictionary<byte, Symbol> Reel1Lookup;
        public readonly IDictionary<byte, Symbol> Reel2Lookup;
        public readonly IDictionary<byte, Symbol> Reel3Lookup;

        public LookupTables()
        {
            Reel1Lookup = new Dictionary<byte, Symbol>();
            Reel2Lookup = new Dictionary<byte, Symbol>();
            Reel3Lookup = new Dictionary<byte, Symbol>();

            CreateLookupFromOdds(Reel1Lookup, ReelOdds.Reel1OddsMapping);
            CreateLookupFromOdds(Reel2Lookup, ReelOdds.Reel2OddsMapping);
            CreateLookupFromOdds(Reel3Lookup, ReelOdds.Reel3OddsMapping);
        }

        private void CreateLookupFromOdds(IDictionary<byte, Symbol> lookup, IDictionary<Symbol, byte> odds) {
            byte i = 0;
            foreach (var Symbol in odds.Keys)
            {
                for (int j = 0; j < odds[Symbol]; j++)
                {
                    lookup[i] = Symbol;
                    i++;
                }
            }
        }
    }
}
