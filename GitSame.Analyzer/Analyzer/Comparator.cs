using GitSame.Analyzer.FileDescriptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitSame.Analyzer
{
    class Comparator
    {
        static public bool CompareFiles( string file1, string file2 )
        {
            string content1 = File.ReadAllText(file1);
            string content2 = File.ReadAllText(file2);
            return CompareContents(content1, content2);
        }
        static public bool CompareContents( string content1, string content2 )
        {
            string hash1 = Helper.sha256_hash(content1);
            string hash2 = Helper.sha256_hash(content2);
            return (hash1 == hash2);
        }

        static public int CompareDescriptions( BasicFileDescription file1, BasicFileDescription file2 )
        {
            try
            {
                var plFile1 = (PLLanguageFileDescription)file1;
                var plFile2 = (PLLanguageFileDescription)file2;
                return ComparePLLanguageDescriptions(plFile1, plFile2);
            }
            catch (InvalidCastException)
            {
                try
                {
                    var unFile1 = (UnknownFileDescription)file1;
                    var unFile2 = (UnknownFileDescription)file2;
                    return CompareUnknownDescriptions(unFile1, unFile2);
                }
                catch(InvalidCastException)
                {
                    return 0;
                }
            }
        }

        static private int ComparePLLanguageDescriptions( PLLanguageFileDescription file1, PLLanguageFileDescription file2 )
        {
            return file1.Scope.Compare(file2.Scope);
        }

        static private int CompareUnknownDescriptions(UnknownFileDescription file1, UnknownFileDescription file2)
        {
            if (file2.tokens.Count == 0 && file1.tokens.Count == 0)
                return 100;
            else if (file1.tokens.Count == 0 || file2.tokens.Count == 0 )
                return 0;

            int minCount = (file1.tokens.Count < file2.tokens.Count) ? file1.tokens.Count : file2.tokens.Count;
            int similarCount = 0;
            for( int i =0;i<minCount;++i)
            {
                if (file1.tokens[i] == file2.tokens[i])
                    similarCount++;
            }
            return (int)(similarCount / (double)minCount*100);
        }
    }
}
