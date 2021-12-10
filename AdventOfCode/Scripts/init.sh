#!/bin/bash 
repo="/mnt/c/Users/Quentin/source/repos/AdventOfCode/AdventOfCode"
fetch="/Scripts/fetch-data.sh"
source /mnt/c/Users/Quentin/source/repos/AdventOfCode/AdventOfCode/Scripts/fetch-data.sh $1 $2 $repo
source /mnt/c/Users/Quentin/source/repos/AdventOfCode/AdventOfCode/Scripts/new.sh $1 $2 $repo
# python3 -m webbrowser https://adventofcode.com/$2/day/$1