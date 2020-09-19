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
            var description = DescriptionGenerator.GenerateDescriptionFromFile(@"d:\test.java");
            DescriptionGenerator.WriteDescriptionToFile(description, @"d:\test_java.json");
            System.Console.ReadKey();
        }

    }
}