$address = "https://adventofcode.com/"+$args[0]+"/day/"+$args[1]+"/input"
$output = $args[2]+"\input.txt"
$cookie = "session="+$env:AOC_SESSION_ID
curl.exe -s --cookie $cookie $address --output $output