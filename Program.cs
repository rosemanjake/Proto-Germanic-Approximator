using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Data;

namespace Proto_German_Approximator
{
    class Program
    {
        public static List<Word> ListofWords = new List<Word>();
        
        public static List<string> Roots = new List<string>();
        public static List<string> EnglishMeanings = new List<string>();
        
        //This is for the headings
        public static string aEnglishMeaning = "English Meaning";
        public static string aPIERoot = "PIE Root";
        public static string aEarlyChanges = "Early Changes";
        public static string aPostLaryngeal = "Post Laryngeals";
        public static string aPostResonant = "Post Resonant";
        public static string aPostGrimm1 = "Post Grimm 1";
        public static string aPostGrimm2 = "Post Grimm 2";
        public static string aPostGrimm3 = "Post Grimm 3";
        public static string aPostVerner = "Post Verner";
        public static string aLate = "Proto-Germanic Approximation";

        static void Main(string[] args)
        {
            //Add headers
            ListofWords.Add(new Word(aEnglishMeaning, aPIERoot, aEarlyChanges, aPostLaryngeal, aPostResonant, aPostGrimm1, aPostGrimm2, aPostGrimm3, aPostVerner, aLate));

            //Get inputs and directory for output
            Console.WriteLine("Welcome to the Proto-Germanic Approximator. Please enter the directory of the input .csv:");
            string filelocation = Console.ReadLine();
            Console.WriteLine("Now please enter the name of the input .csv (including file extension):");
            string inputfilename = Console.ReadLine();
            Console.WriteLine("Now please enter the name of the output files:");
            string outputfilename = Console.ReadLine();

            //Get files
            Inputter(filelocation, inputfilename);
            //Get array of regex patterns
            string[][] UberArray = UberArrayer();
            //Get array of pattern lengths
            int[] LengthArray = Lengther(UberArray);
            //Call main methods
            MainLoop(UberArray, LengthArray);
            //Write outputs
            CSVOutputter(filelocation, outputfilename);
            HTMLOutputter(filelocation, outputfilename);

            Console.ReadLine();
        }

        //Read input .csv and get English meanings and Proto-Indo-European roots
        static void Inputter(string filelocation, string inputfilename)
        {
            using (var reader = new StreamReader(filelocation + "\\" + inputfilename))
            //using (var reader = new StreamReader(@"C:\Users\Public\TestFolder\example_input.csv"))
            {
                while (!reader.EndOfStream)
                {
                    var splits = reader.ReadLine().Split(',');
                    EnglishMeanings.Add(splits[0]);
                    Roots.Add(splits[1]);
                }
            }
        }

        //Feed Proto-Indo-European roots into the main loop of methods
        static void MainLoop(string [][]UberArray, int[] LengthArray)
        {
            for (int rootindex = 0; rootindex < Roots.Count; rootindex++)
            {
                Replacer(rootindex, Roots[rootindex], UberArray, LengthArray);
            }
        }

        //Build the array of regex patterns we'll use
        static string[][] UberArrayer()
        {
            //Arrays of regex patterns
            string[] earlypattern = { "(([td]s[td]))" };
            string[] earlyreplacement = { "ss" };

            string[] laryngealpattern = { "(h₂e)", "(h₃e)", "\\b(h[₁₂₃](?=[^aeiou]))", "(h[₁₂₃](?=[aeiou]))", "(?<=[aeiou](h[₁₂₃]))", "(h[₁₂₃])", "([aeiou]{2}:)" };
            string[] laryngrealreplacement = { "a", "o", "", "", ":", "a", "o" };

            string[] resonantpattern = { "(m̥)", "(n̥)", "(l̥)", "(r̥)" };
            string[] resonantreplacement = { "um", "ur", "ul", "ur" };

            string[] grimm1pattern = { "(?<![s])(p(?!ʰ)(?!ʷ̜))", "(?<![s])(t(?!ʰ)(?!ʷ̜))", "(?<![s])(k(?!ʰ)(?!ʷ̜))", "(?<![s])(kʷ̜)" };
            string[] grimm1replacement = { "f", "θ", "x", "xʷ̜", };

            string[] grimm2pattern = { "(b(?!ʰ)(?!ʷ̜))", "(d(?!ʰ)(?!ʷ̜))", "(k(?!ʰ)(?!ʷ̜))", "(gʷ̜(?!ʰ))" };
            string[] grimm2replacement = { "p", "t", "k", "kʷ̜", };

            string[] grimm3pattern = { "(bʰ)", "(dʰ)", "(gʰ)", "(gʷ̜ʰ)" };
            string[] grimm3replacement = { "b", "d", "g", "gʷ̜", };

            string[] vernerpattern = { "(fˈ)", "(θˈ)", "(xˈ)", "(xʰˈ)", "(sˈ)", "(\\bgʷ)" }; // Must be \b rather that $ for end of word patterns. //(ˈ[^aeiou]?ˈ?[aieou](t)\b) is a hacky way of finding word final t follows a non-stressed syllable, to prevent switching t in single-syllable words 
            string[] vernerreplacement = { "v", "ð", "ɣ", "ɣʰ", "z", "b" };

            string[] latepattern = { "(ˈ[^aeiou]?ˈ?[aieou](t)\b)", "(m\\b)", "(m[td])", "(no\\b)", "(oa\\b)", "aa\\b" }; //(ˈ[^aeiou]?ˈ?[aieou](t)\b) is a hacky way of finding word final t following a non-stressed syllable, to prevent switching t in single-syllable words 
            string[] laterreplacement = { "", "n", "n", "a", "ana", "a" };

            //An array of the regex pattern arrays
            return new string[][] { earlypattern, earlyreplacement, laryngealpattern, laryngrealreplacement, resonantpattern, resonantreplacement, grimm1pattern, grimm1replacement, grimm2pattern, grimm2replacement, grimm3pattern, grimm3replacement, vernerpattern, vernerreplacement, latepattern, laterreplacement };
        }

        //Get the length of each set of regex patterns as an int[]
        static int[] Lengther(string[][] UberArray)
        {
            int[] LengthArray = new int[UberArray.Length];

            for (int i = 0; i < UberArray.Length; i++)
            {
                LengthArray[i] = UberArray[i].Length;
            }

            return LengthArray;
        }

        //Iterate through each pair of regex patterns/replacements, outputting the result of each to outputarray
        static void Replacer(int rootindex, string root, string[][] UberArray, int[] LengthArray)
        {       
            int currentpatternarray = 0;
            int currentreplacementarray = 1;

            string[] outputarray = new string[UberArray.Length / 2];

            for (int i = 0; currentreplacementarray < UberArray.Length; i++,  currentreplacementarray += 2, currentpatternarray += 2)
            {
                if (i == 0)
                {
                    outputarray[i] = Regexer(root, UberArray, LengthArray, currentpatternarray, currentreplacementarray);
                }
                else
                {
                    outputarray[i] = Regexer(outputarray[i - 1], UberArray, LengthArray, currentpatternarray, currentreplacementarray); //if we don't do this then the original root will get passed in each time we loop through
                }
            }

            //Create new object whose attributes = the strings resulting from each set of regex replacements - do this for each set of replacements, per PIE root
            ListofWords.Add(new Word(EnglishMeanings[rootindex], root, outputarray[0], outputarray[1], outputarray[2], outputarray[3], outputarray[4], outputarray[5], outputarray[6], outputarray[7]));
        }

        //Apply the actual replacements via regex
        static string Regexer (string root, string[][]UberArray, int[]LengthArray, int currentpatternarray, int currentreplacementarray)
        {
            for (int currentitem = 0; currentitem < LengthArray[currentpatternarray]; currentitem++)
            {
                Regex rgx;
                rgx = new Regex(UberArray[currentpatternarray][currentitem]); // start at 0,0 - increment the second value to the length of the current array, then add two to the first value
                root = rgx.Replace(root, UberArray[currentreplacementarray][currentitem]); // start at 1,0
            }
            return root;
        }

        //Write output as simple .csv
        static void CSVOutputter(string filelocation, string outputfilename)
        {            
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filelocation + "\\" + outputfilename + ".csv"))
            //new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\output_test.csv"))
            {
                for (int i = 0; i < ListofWords.Count; i++)
                {
                    string outputrow = ListofWords[i].EnglishMeaning + ", " + ListofWords[i].PIERoot + ", " + ListofWords[i].EarlyChanges + ", " + ListofWords[i].PostLaryngeal + ", " + ListofWords[i].PostResonant + ", " + ListofWords[i].PostGrimm1 + ", " + ListofWords[i].PostGrimm2 + ", " + ListofWords[i].PostGrimm3 + ", " + ListofWords[i].PostVerner + ", " + ListofWords[i].Late + "\n";
                    file.Write(outputrow);
                }
            }
        }

        //Write output as a more presentable HTML table
        static void HTMLOutputter(string filelocation, string outputfilename)
        {
            //Get style tags for .html table
            string style;
            using (var reader = new StreamReader(filelocation + "\\style.html"))
            {
                style = reader.ReadToEnd();
            }


            //Write output to .html table
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filelocation + "\\" + outputfilename + ".html"))
            //new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\output_test2.html"))
            {
                file.Write(style + "\n");
                file.Write("<table class=\"t\">");
                string c = "<td class=\"td\">";
                for (int i = 0; i < ListofWords.Count; i++)
                {
                    if (i == 0)
                    {
                        string headerrow = "<tr class=\"h\";>" + c + ListofWords[i].EnglishMeaning + "</td>" + c + ListofWords[i].PIERoot + "</td>" + c + ListofWords[i].EarlyChanges + "</td>" + c + ListofWords[i].PostLaryngeal + "</td>" + c + ListofWords[i].PostResonant + "</td>" + c + ListofWords[i].PostGrimm1 + "</td>" + c + ListofWords[i].PostGrimm2 + "</td>" + c + ListofWords[i].PostGrimm3 + "</td>" + c + ListofWords[i].PostVerner + "</td>" + c + ListofWords[i].Late + "</td>" + "</tr>" + "\n";
                        file.Write(headerrow);
                        i++;
                    }
                    string outputrow = "<tr>" + c + ListofWords[i].EnglishMeaning + "</td>" + c + ListofWords[i].PIERoot + "</td>" + c + ListofWords[i].EarlyChanges + "</td>" + c + ListofWords[i].PostLaryngeal + "</td>" + c + ListofWords[i].PostResonant + "</td>" + c + ListofWords[i].PostGrimm1 + "</td>" + c + ListofWords[i].PostGrimm2 + "</td>" + c + ListofWords[i].PostGrimm3 + "</td>" + c + ListofWords[i].PostVerner + "</td>" + c + ListofWords[i].Late + "</td>" + "</tr>" + "\n";
                    file.Write(outputrow);
                }
                file.Write("</table>");
            }
        }        
    }
}

