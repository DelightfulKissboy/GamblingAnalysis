namespace GamblingAnalysis
{
    /// <summary>
    /// Represents the outcome of a spin
    /// </summary>
    public class Outcome
    {
        /// <summary>
        /// Internal representation for the reels
        /// </summary>
        public readonly Symbol[] Reels;

        /// <summary>
        /// Constructs a new instance of the <see cref="Outcome"/> class
        /// </summary>
        /// <param name="reel1">The symbol on the first reel</param>
        /// <param name="reel2">The symbol on the second reel</param>
        /// <param name="reel3">The symbol on the third reel</param>
        public Outcome(Symbol reel1, Symbol reel2, Symbol reel3) {
            this.Reels = new Symbol[3];
            this.Reels[0] = reel1;
            this.Reels[1] = reel2;
            this.Reels[2] = reel3;
        }

        /// <summary>
        /// Determines if two outcomes are equal. Outcomes are equal iff they contain the exact same symbols
        /// as each other.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if the objects is equal to this outcome, false otherwise</returns>
        public override bool Equals(object? obj)
        {
            var item = obj as Outcome;
            if (item == null) return false;

            return Enumerable.SequenceEqual(this.Reels, item.Reels);
        }

        public override int GetHashCode()
        {
            return Reels.GetHashCode();
        }

        public override string? ToString()
        {
            var result = "[";
            foreach (var symbol in Reels)
            {
                result += symbol.ToString();
                result += ", ";
            }

            result += "]";

            return result;
        }
    }
}
