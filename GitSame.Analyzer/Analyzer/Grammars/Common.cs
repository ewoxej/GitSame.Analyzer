using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GitSame.Analyzer.Grammars
{
    [DataContract]
    class Common : GrammarBase
    {
        public Common()
        {
            pathToGrammar = "../../GrammarFiles/common.json";
        }
        public override List<string> PostTokenizer(string[] tokens)
        {
            List<String> listToReturn = new List<String>();
            string tempStr = "";
            foreach (var i in tokens)
            {
                if (i == "\n")
                {
                    if( tempStr != "" )
                    listToReturn.Add(tempStr);
                    tempStr = "";
                }
                else if( !Regex.Match(i,@"(\W)").Success )
                {
                    tempStr += i;
                }
            }
            List<String> newList = new List<String>();
            for ( int i =0;i<listToReturn.Count;++i)
            {
                listToReturn[i] = listToReturn[i].ToLower();
                if (listToReturn[i].Length > 20)
                {
                    newList.Add( listToReturn[i].Substring(0, (listToReturn[i].Length / 2)));
                    newList.Add(listToReturn[i].Substring(listToReturn[i].Length/2));
                }
            }
            return newList;
        }
    }
}