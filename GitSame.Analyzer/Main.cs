using System;
using System.IO;
using GitSame;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using GitSame.Analyzer;
namespace tsql1
{
   
    class Program
    {
        static void Main(string[] args)
        {
            //var d1 = DescriptionGenerator.GenerateDescriptionFromFile(@"d:\test.java");
            var d1 = DescriptionGenerator.GenerateDescriptionFromFile(@"d:\test\vidos.mp4");
            //DescriptionGenerator.WriteDescriptionToFile(d1, @"d:\test\t.json");
            var d2 = DescriptionGenerator.GenerateDescriptionFromFile(@"d:\test\bin1.docx");
            System.Console.WriteLine(Comparator.CompareDescriptions(d1, d2));
            System.Console.ReadKey();
        }

    }
}