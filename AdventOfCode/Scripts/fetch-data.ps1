Write-Output $args[0]
Write-Output $args[1]
Write-Output $args[2]
$address = "https://adventofcode.com/"+$args[0]+"/day/"+$args[1]+"/input"
$output = $args[2]+"\input.txt"
$cookie = "session="+$env:AOC_SESSION_ID
Write-Output $cookie
curl.exe --cookie $cookie $address --output $output