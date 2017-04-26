namespace NCheferizer
{
    class RuleHolder
    {
        public string Name { get; }
        public Rule Rule { get; }

        public RuleHolder(string name, Rule rule)
        {
            Name = name;
            Rule = rule;
        }
    }
}
