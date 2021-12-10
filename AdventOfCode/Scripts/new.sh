#!/bin/bash 
echo 'exec new'
printf -v day "%02d" $1
printf -v year "%02d" $2
printf -v repo "%s" $3
cat $repo/Commons/DayXX.cs > $repo/$year/Day$day.cs
sed -i '' -e "s/{{1}}/$day/" $repo/$year/Day$day.cs
sed -i '' -e "s/{{0}}/$year/" $repo/$year/Day$day.cs
