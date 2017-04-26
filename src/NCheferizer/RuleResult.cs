namespace NCheferizer
{
    class RuleResult
    {
        public string Result { get; }
        public int Consumed { get; }

        public RuleResult(string result, int consumed)
        {
            Result = result;
            Consumed = consumed;
        }
    }
}
