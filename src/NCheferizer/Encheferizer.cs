using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCheferizer
{
    delegate RuleResult Rule(string subject, Position pos, Context context);

    class Encheferizer
    {
        static readonly Lazy<Encheferizer> instance = new Lazy<Encheferizer>();
        public static Encheferizer Instance => instance.Value;

        readonly Random r = new Random();
        readonly LinkedList<RuleHolder> rules = new LinkedList<RuleHolder>();

        public Encheferizer() => SetupRules();

        void SetupRules()
        {
            DefineRule(
                "Pass the Bork",
                (subject, position, context) => {
                    if (position == Position.First && subject.Length >= 4 && subject.Substring(0,4).ToLower() == "bork")
                        return new RuleResult(subject, 4);
                    return null;
                }
            );

            DefineRule(
                "Randomly borkify after . or !",
                (subject, position, context) => {
                    if (!context.ShouldBork)
                        return null;
                    if (position == Position.Last && (subject[0] == '.' || subject[0] == '!') && r.Next(0, 3) == 0)
                        return new RuleResult(", bork bork bork!", 1);
                    return null;
                }
            );

            DefineRule(
                "Initial e becomes i",
                (subject, position, context) => {
                    if (position == Position.First && char.ToLower(subject[0]) == 'e')
                        return new RuleResult(ChangeCase('i', subject[0]), 1);
                    return null;
                }
            );

            DefineRule(
                "Initial o becomes oo",
                (subject, position, context) => {
                    if (position == Position.First && char.ToLower(subject[0]) == 'o')
                        return new RuleResult(ChangeCase('o', subject[0]) + "o", 1);
                    return null;
                }
            );

            DefineRule(
                "Interior ew becomes oo",
                (subject, position, context) => {
                    if (position != Position.First && subject.Length >= 2 && subject.Substring(0, 2) == "ew")
                        return new RuleResult("oo", 2);
                    return null;
                }
            );

            DefineRule(
                "Final e becomes e-a",
                (subject, position, context) => {
                    if (position == Position.Last && subject[0] == 'e')
                        return new RuleResult("e-a", 1);
                    return null;
                }
            );

            DefineRule(
                "Interior f becomes ff",
                (subject, position, context) => {
                    if (position != Position.First && subject[0] == 'f')
                        return new RuleResult("ff", 1);
                    return null;
                }
            );

            DefineRule(
                "Interior ir becomes ur, or first-occurring interior i becomes ee",
                (subject, position, context) => {
                    if (position != Position.First) {
                        if (subject.Length >= 2 && subject.Substring(0, 2) == "ir")
                            return new RuleResult("ur", 2);
                        else if (subject[0] == 'i' && !context.ChefSawAnI) {
                            context.ChefSawAnI = true;
                            return new RuleResult("ee", 1);
                        }
                    }
                    return null;
                }
            );

            DefineRule(
                "ow becomes oo, or o becomes u",
                (subject, position, context) => {
                    if (position != Position.First) {
                        if (subject.Length >= 2 && subject.Substring(0, 2) == "ow")
                            return new RuleResult("oo", 2);
                        else if (subject[0] == 'o')
                            return new RuleResult("u", 1);
                    }
                    return null;
                }
            );

            DefineRule(
                "tion becomes shun",
                (subject, position, context) => {
                    if (position != Position.First && subject.Length >= 4 && subject.Substring(0, 4) == "tion")
                        return new RuleResult("shun", 4);
                    return null;
                }
            );

            DefineRule(
                "Interior u becomes oo",
                (subject, position, context) => {
                    if (position != Position.First && subject[0] == 'u')
                        return new RuleResult("oo", 1);
                    return null;
                }
            );

            DefineRule(
                "An becomes un",
                (subject, position, context) => {
                    if (subject.Length >= 2 && subject.Substring(0, 2).ToLower() == "an")
                        return new RuleResult(ChangeCase('u', subject[0]) + "n", 2);
                    return null;
                }
            );

            DefineRule(
                "Au becomes oo",
                (subject, position, context) => {
                    if (subject.Length >= 2 && subject.Substring(0, 2).ToLower() == "au")
                        return new RuleResult(ChangeCase('o', subject[0]) + "o", 2);
                    return null;
                }
            );

            DefineRule(
                "Non-final a becomes e",
                (subject, position, context) => {
                    // If the subject is only 1 character long, don't change it.
                    if (position != Position.Last && subject.Length != 1 && char.ToLower(subject[0]) == 'a')
                        return new RuleResult(ChangeCase('e', subject[0]), 1);
                    return null;
                }
            );

            DefineRule(
                "Final en becomes ee",
                (subject, position, context) => {
                    if (position != Position.First && subject == "en")
                        return new RuleResult("ee", 2);
                    return null;
                }
            );

            DefineRule(
                "The becomes zee",
                (subject, position, context) => {
                    if (subject.Length >= 3 && subject.Substring(0, 3).ToLower() == "the")
                        return new RuleResult(ChangeCase('z', subject[0]) + "ee", 3);
                    return null;
                }
            );

            DefineRule(
                "Th becomes t",
                (subject, position, context) => {
                    if (subject.Length >= 2 && subject.Substring(0, 2).ToLower() == "th")
                        return new RuleResult(ChangeCase('t', subject[0]), 2);
                    return null;
                }
            );

            DefineRule(
                "V becomes F",
                (subject, position, context) => {
                    if (char.ToLower(subject[0]) == 'v')
                        return new RuleResult(ChangeCase('f', subject[0]), 1);
                    return null;
                }
            );

            DefineRule(
                "W becomes V",
                (subject, position, context) => {
                    if (char.ToLower(subject[0]) == 'w')
                        return new RuleResult(ChangeCase('v', subject[0]), 1);
                    return null;
                }
            );

            DefineRule(
                "Pass on anything else",
                (subject, position, context) => new RuleResult(new string(subject[0], 1), 1)
            );
        }

        string ChangeCase(char @char, char input)
            => new string(char.IsUpper(input) ? char.ToUpper(@char) : @char, 1);

        void DefineRule(string name, Rule rule)
            => rules.AddLast(new RuleHolder(name, rule));

        /// <summary>
        /// Encheferizes an input string.
        /// </summary>
        /// <param name="input">The input text.</param>
        /// <param name="shouldBork">Should I bork?</param>
        /// <returns>An encheferized string.</returns>
        /// <remarks>This method is thread-safe.</remarks>
        public string Encheferize(string input, bool shouldBork = true)
            => string.Join(" ", input.Split(' ').Select(w => EncheferizeWord(w, shouldBork)));

        string EncheferizeWord(string word, bool shouldBork)
        {
            var inPos = 0;
            var output = new StringBuilder();
            var context = new Context {
                ShouldBork = shouldBork,
                ChefSawAnI = false
            };

            while (inPos < word.Length) {
                var position = (inPos == 0) ? Position.First : (
                    inPos == (word.Length - 1) ? Position.Last : Position.Internal
                );

                var subject = word.Substring(inPos);

                foreach (var ruleH in rules) {
                    var result = ruleH.Rule(subject, position, context);
                    if (result != null) {
                        output.Append(result.Result);
                        inPos += result.Consumed;
                        break;
                    }
                }
            }

            return output.ToString();
        }
    }
}
