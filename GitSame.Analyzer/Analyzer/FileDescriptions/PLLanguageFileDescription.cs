using System.Runtime.Serialization;


namespace GitSame.Analyzer.FileDescriptions
{
    public class PLLanguageFileDescription : BasicFileDescription
    {
        [DataMember]
        public BlockDescription Scope { get; set; }
        public PLLanguageFileDescription()
        {
            Scope = new BlockDescription();
        }
    }
}
