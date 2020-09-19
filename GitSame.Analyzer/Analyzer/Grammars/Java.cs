using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace GitSame.Analyzer.Grammars
{
    [DataContract]
    class Java : GrammarBase
    {
        public Java()
        {
            pathToGrammar = "../../GrammarFiles/java.json";
        }
        public override List<String> PostTokenizer(string[] tokens)
        {
            List<String> listToReturn = new List<String>();
            bool isOneLineComment = false;
            bool isMultiLineComment = false;
            foreach (var i in tokens)
            {
                if (i == "//" && !isMultiLineComment)
                    isOneLineComment = true;
                else if (i == "/*")
                    isMultiLineComment = true;
                else if (i == "\n")
                    isOneLineComment = false;
                else if (i == @"*\")
                    isMultiLineComment = false;
                else if (!String.IsNullOrEmpty(i) && i != "\n" && i != "\r" && i != " " && !isMultiLineComment && !isOneLineComment)
                    listToReturn.Add(i);
            }
            return listToReturn;
        }
    }
}
