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
        public static int bigindex = 0;

        static void Main(string[] args)
        {
            //This is for the headings
            string aEnglishMeaning = "English Meaning";
            string aPIERoot = "PIE Root";
            string aEarlyChanges = "Early Changes";
            string aPostLaryngeal = "Post Laryngeals";
            string aPostResonant = "Post Resonant";
            string aPostGrimm1 = "Post Grimm 1";
            string aPostGrimm2 = "Post Grimm 2";
            string aPostGrimm3 = "Post Grimm 3";
            string aPostVerner = "Post Verner";
            string aLate = "Proto-Germanic Approximation";
            ListofWords.Add(new Word(aEnglishMeaning, aPIERoot, aEarlyChanges, aPostLaryngeal, aPostResonant, aPostGrimm1, aPostGrimm2, aPostGrimm3, aPostVerner, aLate));

            //Get inputs and directory for output
            Console.WriteLine("Welcome to the Proto-Germanic Approximator. Please enter the directory of the input .csv:");
            string filelocation = Console.ReadLine();
            Console.WriteLine("Now please enter the name of the input .csv (including file extension):");
            string inputfilename = Console.ReadLine();
            Console.WriteLine("Now please enter the name of the output files:");
            string outputfilename = Console.ReadLine();

            //Read input .csv and get English meanings and Proto-Indo-European roots
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

            //Feed Proto-Indo-European roots into the bigswap method
            foreach (string element in Roots)
            {
                string inputroot = element;
                bigswap(inputroot);
                bigindex++;
            }

            //This single method performs all the transformations that lead to the Proto-Germanic approximations
            void bigswap(string inputroot)
            {
                //Arrays of regex patterns
                string[] earlypattern = { "(([td]s[td]))"};
                string[] earlyreplacement = { "ss" };

                string[] laryngealpattern = { "(h₂e)", "(h₃e)", "\\b(h[₁₂₃](?=[^aeiou]))", "(h[₁₂₃](?=[aeiou]))", "(?<=[aeiou](h[₁₂₃]))", "(h[₁₂₃])", "([aeiou]{2}:)" };
                string[] laryngrealreplacement = { "a" , "o", "", "", ":", "a", "o" };

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
                string[][] RegexUberArray = new string[][] { earlypattern, earlyreplacement, laryngealpattern, laryngrealreplacement, resonantpattern, resonantreplacement, grimm1pattern, grimm1replacement, grimm2pattern, grimm2replacement, grimm3pattern, grimm3replacement, vernerpattern, vernerreplacement, latepattern, laterreplacement };

                //Get the length of each set of regex patterns
                int index = 0;
                int arraylength;
                int[] arraylengths = new int[RegexUberArray.Length];

                foreach (string[] element in RegexUberArray)
                {
                    arraylength = element.Length;
                    arraylengths[index] = arraylength;
                    index++;
                }

                //Iterate through each pair of regex patterns/replacements, outputting the result for each to outputarray
                string input = inputroot;
                string initialroot = input;
                int currentpatternarray = 0;
                int currentreplacementarray = 1;
                int currentitem = 0;
                index = 0;

                string[] outputarray = new string[RegexUberArray.Length / 2];

                while (currentreplacementarray < RegexUberArray.Length)
                {
                    while (currentitem < arraylengths[currentpatternarray])
                    {
                        Regex rgx;
                        rgx = new Regex(RegexUberArray[currentpatternarray][currentitem]); // start at 0,0 - increment the second value to the length of the current array, then add two to the first value
                        input = rgx.Replace(input, RegexUberArray[currentreplacementarray][currentitem]); // start at 1,0
                        currentitem++;
                    }

                    outputarray[index] = input;
                    currentitem = 0;
                    index++;
                    currentpatternarray += 2;
                    currentreplacementarray += 2;
                }

                aEnglishMeaning = EnglishMeanings[bigindex];
                aPIERoot = initialroot;
                aEarlyChanges= outputarray[0];
                aPostLaryngeal = outputarray[1];
                aPostResonant = outputarray[2];
                aPostGrimm1 = outputarray[3];
                aPostGrimm2 = outputarray[4];
                aPostGrimm3 = outputarray[5];
                aPostVerner = outputarray[6];
                aLate = outputarray[7];

                //Create new object whose attributes = the strings resulting from each set of regex replacements - do this for each set of replacements, per PIE root
                ListofWords.Add(new Word(aEnglishMeaning, aPIERoot, aEarlyChanges, aPostLaryngeal, aPostResonant, aPostGrimm1, aPostGrimm2, aPostGrimm3, aPostVerner, aLate));
            }

            //Write output as simple .csv
            int i = 0;
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filelocation + "\\" + outputfilename + ".csv"))
            //new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\output_test.csv"))
            {
                foreach (var element in ListofWords)
                {
                    string outputrow = ListofWords[i].EnglishMeaning + ", " + ListofWords[i].PIERoot + ", " + ListofWords[i].EarlyChanges + ", " + ListofWords[i].PostLaryngeal + ", " + ListofWords[i].PostResonant + ", " + ListofWords[i].PostGrimm1 + ", " + ListofWords[i].PostGrimm2 + ", " + ListofWords[i].PostGrimm3 + ", " + ListofWords[i].PostVerner + ", " + ListofWords[i].Late + "\n";
                    file.Write(outputrow);
                    i++;
                }
            }

            //Get style tags for .html table
            string style;
            using (var reader = new StreamReader(filelocation + "\\style.html"))
            //using (var reader = new StreamReader(@"C:\Users\Public\TestFolder\style.html"))
            style = reader.ReadToEnd();

            //Write output to .html table
            i = 0;
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filelocation + "\\" + outputfilename + ".html"))
            //new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\output_test2.html"))
            {
                file.Write(style + "\n");
                file.Write("<table class=\"t\">");
                string c = "<td class=\"td\">";
                for (int count = ListofWords.Count; i < count; i++)
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

            Console.ReadLine();
        }
    }
}

