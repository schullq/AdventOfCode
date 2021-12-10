$day = $args[1]
$year = $args[0]

$from = $args[2]+"\Commons\DayXX.cs"
$to = $args[2]+"\"+$year+"\Day"+$day+".cs"

(Get-Content $from).replace('{0}',$year).replace('{1}',$day) | Out-File $to
