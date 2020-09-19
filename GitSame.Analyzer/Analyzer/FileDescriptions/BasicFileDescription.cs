using System.Runtime.Serialization;

namespace GitSame.Analyzer.FileDescriptions
{
    [DataContract]
    public class BasicFileDescription
    {
        [DataMember(Name = "language")]
        public string Language { get; set; }
    }
}
