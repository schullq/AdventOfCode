param ($year, $day, $repo)

$day = "{0:D2}" -f $day

$from = $repo+"\Commons\DayXX.cs"
$to = $repo+"\"+$year+"\Day"+$day+".cs"

(Get-Content $from).replace('{0}',$year).replace('{1}',$day) | Out-File $to
