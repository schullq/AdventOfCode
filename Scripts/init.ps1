param ($year, $day)

if (!$year)
{
    $year = Get-Date -UFormat "%yyyy"
}

if (!$day)
{
    $day = Get-Date -UFormat "%d"
}

$currentPath = Split-Path $MyInvocation.MyCommand.Path -Parent
$repo = Split-Path $currentPath -Parent
$url = "https://adventofcode.com/"+$year+"/day/"+$day
powershell $currentPath\fetch-data.ps1 -year $year -day $day -repo $repo
powershell $currentPath\new.ps1 -year $year -day $day -repo $repo
Start-Process $url