# Proto-Germanic Approximator 

## Overview

The Proto-Germanic Approximator (PGA) is a program that takes in a .csv file with Proto-Indo-European (PIE) roots and their English meanings. It transforms those roots and outputs .csv and .html files containing a guess at how the PIE form would you have appeared in Proto-Germanic (PGmc).

## Inputs

The PGA will request 3 things:

- **Directory:** Enter the full directory where the input .csv is found. PGA will output to this location.
- **Name of .csv:** Enter the filename of the input .csv, including the file extension.
- **Outputs:** Enter the name of the output files. This will be the name of the output .csv and .html.

### Input CSV format

The input csv should be formatted such that each word is separated by a comma, with the odd word being an English meaning, and even words being the PIE roots. See the example_input.csv for reference.

### Outputs

The PGA will output a .csv and .html to the same directory as the input .csv.
