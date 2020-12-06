using System;
using System.Collections.Generic;
using System.Text;

namespace Proto_German_Approximator
{
    class Word
    {
        public string EnglishMeaning;
        public string PIERoot;
        public string EarlyChanges;
        public string PostLaryngeal;
        public string PostResonant;
        public string PostGrimm1;
        public string PostGrimm2;
        public string PostGrimm3;
        public string PostVerner;
        public string Late;

        public Word(string aEnglishMeaning, string aPIERoot, string aEarlyChanges, string aPostLaryngeal, string aPostResonant, string aPostGrimm1, string aPostGrimm2, string aPostGrimm3, string aPostVerner, string aLate)
        {
            EnglishMeaning = aEnglishMeaning;
            PIERoot = aPIERoot;
            EarlyChanges = aEarlyChanges;
            PostLaryngeal = aPostLaryngeal;
            PostResonant = aPostResonant;
            PostGrimm1 = aPostGrimm1;
            PostGrimm2 = aPostGrimm2;
            PostGrimm3 = aPostGrimm3;
            PostVerner = aPostVerner;
            Late = aLate;
        }
    }
}
