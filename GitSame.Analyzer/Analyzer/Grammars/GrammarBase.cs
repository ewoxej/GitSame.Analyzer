using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GitSame.Analyzer.Grammars
{
    [DataContract]
    public abstract class GrammarBase
    {
        [DataMember]
        public string Name { get; protected set; }
        [DataMember]
        public List<String> Extensions { get; protected set; }
        [DataMember]
        public List<String> Keywords { get; protected set; }
        [DataMember]
        public string BlockStartRule { get; protected set; }
        [DataMember]
        public string BlockEndRule { get; protected set; }
        [DataMember]
        public string IdentifierRules { get; protected set; }
        [DataMember]
        public List<String> PrimitiveTypes { get; protected set; }
        [DataMember]
        public string AssignmentStatements { get; protected set; }
        [DataMember]
        public string ComparsionOperators { get; protected set; }
        [DataMember]
        public string MathOperators { get; protected set; }
        [DataMember]
        public string SplitRule { get; protected set; }
        public abstract List<String> PostTokenizer( string[] tokens);
    }
}
