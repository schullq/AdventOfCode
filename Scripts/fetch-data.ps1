param ($year, $day, $repo)

$address = "https://adventofcode.com/"+$year+"/day/"+$day+"/input"
$output = $repo+"\input.txt"
$cookie = "session="+$env:AOC_SESSION_ID
curl.exe -s --cookie $cookie $address --output $output