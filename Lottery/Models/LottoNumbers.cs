namespace GamblingAnalysis
{
    public class LottoNumbers
    {
        public ISet<int> Numbers { get; }

        public int PowerBall { get; }


        public LottoNumbers(Random r)
        {
            this.PowerBall = r.Next(26) + 1;
            this.Numbers = new HashSet<int>();
            while (this.Numbers.Count < 5)
            {
                Numbers.Add(r.Next(69) + 1);
            }
        }
    }
}
