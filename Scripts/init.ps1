param ($year, $day)

if (!$year)
{
    $year = Get-Date -format "yyyy"
}

if (!$day)
{
    $day = Get-Date -format "dd"
    $dayUrl = $day.TrimStart('0')
}
else
{
    if ($day / 10 -eq 0)
    {
        $dayUrl = $day
        $day = '0' + $day
    }
    else
    {
        $dayUrl = $day.ToString().TrimStart('0')
    }
}

$currentPath = Split-Path $MyInvocation.MyCommand.Path -Parent
$repo = Split-Path $currentPath -Parent
$url = "https://adventofcode.com/"+$year+"/day/"+$dayUrl
& $currentPath\fetch-data.ps1 -year $year -day $dayUrl -repo $repo
& $currentPath\new.ps1 -year $year -day $day -repo $repo
Start-Process $url