using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GitSame.Analyzer.FileDescriptions
{
    public class UnknownFileDescription : BasicFileDescription
    {
        [DataMember]
        public List<string> tokens;
        public void HashTokens( List<string> t )
        {
            tokens = new List<string>();
            foreach (var i in t)
                tokens.Add(Helper.sha256_hash(i));
        }
    }
}
