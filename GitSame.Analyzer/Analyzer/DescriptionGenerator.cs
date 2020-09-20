using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using GitSame.Analyzer.FileDescriptions;

namespace GitSame.Analyzer
{
    public class DescriptionGenerator
    {
        private static Grammars.GrammarBase[] availibleGrammars;
        private static Grammars.GrammarBase commonGrammar;
        private static T initGrammar<T>() where T : Grammars.GrammarBase, new()
        {
            return Grammars.GrammarBase.initInstance<T>();
        }
        static DescriptionGenerator()
        {
            availibleGrammars = new Grammars.GrammarBase[] { initGrammar<Grammars.Java>() };
            commonGrammar = initGrammar<Grammars.Common>();
        }
        public static List<String> Tokenize(Grammars.GrammarBase grammar, string input)
        {
            string[] substrings = Regex.Split(input, grammar.SplitRule);
            return grammar.PostTokenizer(substrings);
        }
        public static BasicFileDescription GenerateDescriptionFromContent(Grammars.GrammarBase grammar, string content)
        {
            var tokens = Tokenize(grammar, content);
            if (grammar == commonGrammar)
            {
                var fileDescription = new UnknownFileDescription();
                fileDescription.Language = grammar.Name;
                fileDescription.HashTokens(tokens);
                return fileDescription;
            }
            else
            {
                var fileDescription = new PLLanguageFileDescription();
                fileDescription.Language = grammar.Name;
                var rootBlock = fileDescription.Scope;
                rootBlock.Analyze( 0, grammar, tokens);
                fileDescription.Scope = rootBlock;
                return fileDescription;
            }
        }

        public static void WriteDescriptionToFile( BasicFileDescription description, string filepath )
        {
            using (FileStream file = new FileStream(filepath, FileMode.Create, System.IO.FileAccess.Write))
            {
                if ((PLLanguageFileDescription)description != null)
                {
                    var ser = new DataContractJsonSerializer(typeof(PLLanguageFileDescription));
                    ser.WriteObject(file, (PLLanguageFileDescription)description);
                }
                else
                {
                    var ser = new DataContractJsonSerializer(typeof(UnknownFileDescription));
                    ser.WriteObject(file, (UnknownFileDescription)description);
                }
            }
        }

        public static BasicFileDescription GenerateDescriptionFromFile(string pathToFile)
        {
            string fileExt = Path.GetExtension(pathToFile).Substring(1);//get rid of dot in the beginning
            Grammars.GrammarBase grammar = MatchGrammar(fileExt);
            string input = File.ReadAllText(pathToFile);
            grammar.Name = fileExt;
            return GenerateDescriptionFromContent(grammar, input);
        }
        public static Grammars.GrammarBase MatchGrammar(string fileExtension)
        {
            foreach (var i in availibleGrammars)
                if (i.Extensions.Contains(fileExtension))
                    return i;
            return commonGrammar;
        }

    }
}
