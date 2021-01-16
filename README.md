# Proto-Germanic

## Background

English is a member of the Germanic language family. The other modern Germanic languages include Dutch, German, Frisian, Afrikaans, Luxembourgish, and the Scandinavian languages. They are all descended from a common ancestral language that was spoken in Northern Europe roughly 2,000 years ago. Linguistics call this language Proto-Germanic (PGmc).

As is the case with any language throughout history, the speakers of Proto-Germanic naturally divided into several dialect groups. The speech of these groups was differentiated by several systematic sound changes. For example, in the ingvaeonic dialect directly ancestral to English, wherever the sounds /n/ and /s/ occured together as the cluser /ns/, the nasal sound was lost. Such is the basis for why in modern English we say "us" while the Dutch say "ons." 

Through time, these small systematic sound changes compounded, often provoking grammatical changes. With enough changes accumulated, dialects that were once fully mutually intelligible became incomprehensible to one another, giving rise to recognisably independent languages. Such is the story of the rise of English and the other modern Germanic languages from a single ancestral tongue.

![Germanic language family](https://cdn.britannica.com/90/1990-050-53E1AB05/Derivation-languages-Germanic-Proto-Germanic.jpg)

## Proto-Indo-European

Just as English descends from Proto-Germanic, so too does Proto-Germanic descend from an even more ancient form of speech. This language was spokehttp://www.linguatics.com/images/indoeuro02c.jpgn by a community that lived five thousand years ago on the steppe between the Dnieper and Ural rivers in what is now Russia and Ukraine. In a series of migrations from that area they spread their language throughout Europe and Western Asia, ultimately giving rise to almost all the languages indigenous to the cresent reaching across Eurasia from Bangladesh to Portgual. Linguistics call this ancestral language Proto-Indo-European (PIE).

![Indo-European Language Family](http://www.linguatics.com/images/indoeuro02c.jpg)

## The Comparative Method

The speakers of PIE were illiterate pastoralists who never recorded their language in any form. Likewise, while Proto-Germanic speakers existed at a time when Classical Civilisation was producing unprecedented volumes of literature, the areas in which they lived lay well beyond the borders of Rome. Only partial fragments exist of Proto-Germanic words and phrases recorded in Elder Futhatk, the oldest known runic alphabet.    

As such, linguistics must reconstruct Proto-Indo-European and Proto-Germanic. They do so using the Comparative Method. The Comparative Method involves analysing related fully atttested languages to determine which of the common elements they share ultimately derive from an ancestral language. In practise, this means identifying which sound changes led to one language differentiated itself from another.

It is important to note that when a sound changes in a language, it does so systemically, almost as if the speakers of the language were doing a find-and-replace. Consider the example above where the ingvaeonic dialect of Proto-Germanic dropped /n/ when followed by /s/. Properly understood, this was a change to the rules that determine which sounds can appear where in the language and was not a contortion of individual words. For that reason, the sound change occurred in all relevant words throughout the language all at once. 

## Grimm's Law

One of the first applications of The Comparative Method was the identification of a key sound change that led to dialects of Indo-European developing into Proto-Germanic. It was discovered by Jacon Grimm, one of the two Germanic brothers famous for recording folk tales in the 19th Centtury. At the time, it was unclear whether the Germanic languages were a part of the Indo-European language family. They appeared unusual, with many strange words and seemingly inexplicable forms.

However, Jacob noticed a systematic correspondence between certain sounds in modern Germanic languages and in Greek, Latin, and other Indo-European languages. For example, he noticed that where a /p/ appeared in Latin or Greek, there was an /f/ in the corresponding Germanic word. While in French one says "**p**ere" and "**p**eche," in English one says "**f**ather" and "**f**ish." 

In identifying this sound change Grimm not only demonstrated that PGmc had developed from PIE, but had also identified a part of the ancestral PIE forms of the words he investigated. Following the above example, many Indo-European languages had terms using /p/ where Germanic languages have /f/. As it is very unlikely that all those languages developed a /f/-/p/ shift indepdently, it is very safe to assume that the original PIE terms contained a /p/ sound.

In identifying dozens and dozens of similar sound changes, linguistics use the comparative method to slowly build a view of the ancestral roots and words that were spoken in PIE, as well as even their grammar. 

# Proto-Germanic Approximator 

## Overview

The Proto-Germanic Approximator (PGA) is a program that generates an approximation of how PIE roots appeared in PGmc. That is to say, it takes PIE roots as its inputs and then applies the systematic sound changes that linguistics have identified . It is effectively an application of the comparative method in reverse. It can be used to test hypotheses regarding how a known PIE root may have appeared in PGmc.

## Inputs

The Proto-Germanic Approximator (PGA) is a program that takes in a .csv file with Proto-Indo-European (PIE) roots and their English meanings. It transforms those roots and outputs .csv and .html files containing a guess at how the PIE form would you have appeared in Proto-Germanic (PGmc).

At the command line, the PGA will request 3 things:

- **Directory:** Enter the full directory where the input .csv is found. PGA will output to this location.
- **Name of .csv:** Enter the filename of the input .csv, including the file extension.
- **Outputs:** Enter the name of the output files. This will be the name of the output .csv and .html.

### IPA

All PIE input roots must be provided in the International Phonetic Alphabet (IPA).

### Input CSV format

The input .csv should be formatted such that each word is separated by a comma, with the odd word being an English meaning, and even words being the PIE roots. See the example_input.csv for reference.

### Outputs

The PGA will output a .csv and .html to the same directory as the input .csv.

## Transformations

As mentioned above, the systematic sound changes that drive language development are much like applying a find-and-replace to the linguistics content within a person's mind. In that spirit, the PGA uses a series of regular expressions to identify and then replace certain sound patterns.

The different transformations that the PGA will apply to a root are:

- **Early pattern**: Replaces any /s/ before or after either /t/ or /d/ with /ss/.
- **Laryngeals**: Replaces laryngeal sounds h₁, h₂, and h₃ with vowels.
- **Resonants:** Inserts /u/ ahead of syllabic resonants /m̥/, /n̥/, /l̥/, and /r̥/, 
- **Grimm 1:** First stage of Grimm's Law, voiceless stops become fricatives.
- **Grimm 2:** Second stage of Grimm's Law, voiced stops become voiceless.
- **Grimm 3:** Third stage of Grimm's Law, aspirated voiced stops become plain stops.
- **Verner's Law:** Voices constants when they occur just before a stressed syllable.
- **Late changes:** Applies a series of miscellaneous final sound changes.

The source for these changes is *Indo-European Languages and Culture*, Second Edition, by Ben Forston.
