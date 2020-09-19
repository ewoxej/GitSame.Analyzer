using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GitSame.Analyzer.Grammars
{
    [DataContract]
    class Common : GrammarBase
    {
        public Common()
        {
            pathToGrammar = "";
        }
        public override List<string> PostTokenizer(string[] tokens)
        {
            throw new NotImplementedException();
        }
    }
}