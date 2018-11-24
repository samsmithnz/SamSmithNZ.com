##############################################
#Deploy windows service 
##############################################

#Example parameters:
#'D:\CompassADService\Powershell\DeployWindowsService.ps1' -serviceName CompassADService -displayName CompassADService -sourceLocation  D:\CompassADService\* -destinationLocation "C:\Program Files (x86)\CompassADService\" -SqlServerName WTRENTSQLDEV1V

Param ([string] $serviceName, 
	   [string] $displayName,
	   [string] $sourceLocation,
       [string] $destinationLocation,
       [string] $SqlServerName)
      
$binaryName = $destinationLocation + "Compass.ADService.exe"
$serviceDef = Get-Service -Name $serviceName -ErrorAction SilentlyContinue
$ServiceConfig = $destinationLocation + "Compass.ADService.exe.config"
$DefaultSqlServerName = 'WTRENTSQLDEV1V'

Write-Output "Entering script DeployWindowsService.ps1"
Write-Output "Version 1.00" #Initial Release with Compass variables

If ($serviceDef -eq $null)
{
    Write-Output "First install of service..."
    If(-Not (Test-Path -Path $destinationLocation ))
    {
		 New-Item $destinationLocation -ItemType directory
    }
    Copy-Item $sourceLocation $destinationLocation -Force

	#Edit Configuration file to update server Name
	Write-Output "Editing config..."
	(Get-Content $ServiceConfig -Raw).replace($DefaultSqlServerName, $SqlServerName) | Set-Content $ServiceConfig

    New-Service -Name $serviceName -StartupType Automatic -DisplayName $displayName -BinaryPathName $binaryName
}
else
{
    Write-Output "Service has already been installed, upgrading..."
    if($serviceDef.Status -eq "Running")
    {
		Write-Output "Stopping Service..."
        Stop-Service -Name $serviceName
    }
    Copy-Item $sourceLocation $destinationLocation -Force
	#Edit Configuration file to update server Name	
	Write-Output "Editing config..."
	(Get-Content $ServiceConfig -Raw).replace($DefaultSqlServerName, $SqlServerName) | Set-Content $ServiceConfig
}
Write-Output "Starting Service..."           
Start-Service -Name $serviceName

Write-Output "Exiting script CopyFiles.ps1"

