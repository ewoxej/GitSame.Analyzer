using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitSame.Analyzer.Grammars
{
    class Java : GrammarBase
    {
        public Java() : base()
        {
            Name = "java";
            Extensions = new List<String> { "java" };
            Keywords = new List<String> { "abstract", "continue","for","new","switch",
                                          "assert", "default", "goto", "package", "synchronized",
                                           "do","if","private","this",
                                           "break","implements","protected","throw",
                                           "else","import","public","throws",
                                           "case","enum","instanceof","return","transient",
                                           "catch","extends","try",
                                           "final","interface","static",
                                           "class","finally","strictfp","volatile",
                                           "const","native","super","while"};
            BlockStartRule = "{";
            BlockEndRule = "}";
            IdentifierRules = @"^(^\D[_a-zA-Z0-9$]+)$";
            PrimitiveTypes = new List<string> { "boolean", "int", "short", "double", "byte", "char", "void", "float", "long" };
            AssignmentStatements = "=";
            MathOperators = @"[\+-\/*%]";
            ComparsionOperators = ">=|<=|==|<|>|!=";
            SplitRule = @"(->|\/\/|\/\*|\*\/|[ {}.()=:;,\n\r<>])";

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
