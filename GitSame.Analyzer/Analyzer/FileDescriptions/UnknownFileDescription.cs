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
    }
}
