#!/bin/bash 
echo 'exec fetch'
printf -v day "%d" $1
printf -v year "%04d" $2
printf -v repo "%s" $3
echo "$repo/input.txt"
curl -s --cookie "session=$AOC_SESSION_ID" https://adventofcode.com/$year/day/$day/input --output $repo/input.txt