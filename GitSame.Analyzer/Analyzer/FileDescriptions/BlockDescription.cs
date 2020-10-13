using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace GitSame.Analyzer.FileDescriptions
{
    [DataContract]
    public class BlockDescription
    {
        public Dictionary<string, int> IdentifierUsages { get; set; }
        [DataMember]
        private List<int> IdentifierUsagesPattern { get; set; }
        [DataMember]
        public Dictionary<string, int> PrimitiveTypeUsages { get; set; }
        [DataMember]
        public Dictionary<string, int> KeywordUsages { get; set; }
        [DataMember]
        public int AssignmentStatements { get; set; }
        [DataMember]
        public int MathExpressions { get; set; }
        [DataMember]
        public int CompareExpressions { get; set; }
        [DataMember]
        public List<BlockDescription> NestedBlocks { get; set; }
        [DataMember]
        public int TokensCount { get; set; }

        public BlockDescription()
        {
            IdentifierUsages = new Dictionary<string, int>();
            PrimitiveTypeUsages = new Dictionary<string, int>();
            KeywordUsages = new Dictionary<string, int>();
            NestedBlocks = new List<BlockDescription>();
        }
        public void IncrementKeywordUsages(string keyword, int increment = 1)
        {
            var tmp = KeywordUsages;
            IncrementKeyUsageInDict(ref tmp, keyword,increment);
            KeywordUsages = tmp;
        }
        public void IncrementTypeUsages(string type, int increment = 1)
        {
            var tmp = PrimitiveTypeUsages;
            IncrementKeyUsageInDict(ref tmp, type, increment);
            PrimitiveTypeUsages = tmp;
        }
        public void IncrementIdentifierUsages(string identifier,int increment = 1)
        {
            var tmp = IdentifierUsages;
            IncrementKeyUsageInDict(ref tmp, identifier,increment);
            IdentifierUsages = tmp;
        }

        public int Analyze(int loopStartPosition, Grammars.GrammarBase grammar, List<string> tokens, int internalTokensLimit )
        {
            int blockTokensCount = 0;
            for (int i = loopStartPosition; i < tokens.Count; ++i)
            {
                blockTokensCount++;
                if (grammar.Keywords.Contains(tokens[i]))
                    IncrementKeywordUsages(tokens[i]);
                else if (grammar.PrimitiveTypes.Contains(tokens[i]))
                    IncrementTypeUsages(tokens[i]);
                else if (Regex.Match(tokens[i], grammar.IdentifierRules).Success)
                    IncrementIdentifierUsages(tokens[i]);
                else if (Regex.Match(tokens[i], grammar.AssignmentStatements).Success)
                    AssignmentStatements++;
                else if (Regex.Match(tokens[i], grammar.ComparsionOperators).Success)
                    CompareExpressions++;
                else if (Regex.Match(tokens[i], grammar.MathOperators).Success)
                    MathExpressions++;
                else if (grammar.BlockStartRule == tokens[i])
                {
                    BlockDescription nestedBlock = new BlockDescription();
                    i = nestedBlock.Analyze( i + 1, grammar, tokens, internalTokensLimit);
                    if (nestedBlock.TokensCount < internalTokensLimit )
                    {
                        IncrementInternalBlockData(nestedBlock);
                    }
                    else
                    {
                        NestedBlocks.Add(nestedBlock);
                    }
                }
                else if (grammar.BlockEndRule == tokens[i])
                {
                    TokensCount = blockTokensCount;
                    PreSerializeStep();
                    return i;
                }
            }
            return tokens.Count;
        }

        private void PreSerializeStep()
        {
            IdentifierUsagesPattern = new List<int>();
            foreach (var i in IdentifierUsages.Values)
                IdentifierUsagesPattern.Add(i);

            var keys = KeywordUsages.Keys;
            foreach ( var i in keys.ToArray())
            {
                int val = 0;
                KeywordUsages.TryGetValue(i, out val);
                if (val < 2)
                    KeywordUsages.Remove(i);
            }
            keys = PrimitiveTypeUsages.Keys;
            foreach (var i in keys.ToArray())
            {
                int val = 0;
                PrimitiveTypeUsages.TryGetValue(i, out val);
                if (val < 2)
                    PrimitiveTypeUsages.Remove(i);
            }
        }
        private static void IncrementKeyUsageInDict(ref Dictionary<string, int> dict, string key, int incrementValue)
        {
            int val;
            if (dict.TryGetValue(key, out val))
            {
                dict.Remove(key);
                dict.Add(key, val + incrementValue);
            }
            else
            {
                dict.Add(key, incrementValue);
            }
        }

        private void IncrementInternalBlockData( BlockDescription data )
        {
            foreach( var i in data.KeywordUsages )
                IncrementKeywordUsages(i.Key,i.Value);
            foreach (var i in data.IdentifierUsages)
                IncrementIdentifierUsages(i.Key, i.Value);
            foreach (var i in data.PrimitiveTypeUsages)
                IncrementTypeUsages(i.Key, i.Value);

            AssignmentStatements += data.AssignmentStatements;
            MathExpressions += data.MathExpressions;
            CompareExpressions += data.CompareExpressions;
            TokensCount += data.TokensCount;
            foreach (var i in data.NestedBlocks)
                NestedBlocks.Add(i);
        }
    }
}
