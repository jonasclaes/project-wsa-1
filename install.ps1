$url = "https://github.com/jonasclaes/project-wsa-1/releases/download/v0.0.1/win-x64.zip"
$output = "$PSScriptRoot\project-wsa-1.zip"
$start_time = Get-Date

Invoke-WebRequest -Uri $url -OutFile $output
Write-Output "Time taken: $((Get-Date).Subtract($start_time).Seconds) second(s)."

Write-Output "Extracting now.."
Expand-Archive -Path $output -DestinationPath "C:\project-wsa-1"

Remove-Item -Path $output