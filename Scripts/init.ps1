param ($year, $day)

if (!$day)
{
    $day = Get-Date -UFormat "%d"
}

$repo = "Z:\Users\Quentin\Documents\Projets\AdventOfCode"
$url = "https://adventofcode.com/"+$year+"/day/"+$day
powershell $repo\Scripts\fetch-data.ps1 -year $year -day $day -repo $repo
powershell $repo\Scripts\new.ps1 -year $year -day $day -repo $repo
Start-Process $url