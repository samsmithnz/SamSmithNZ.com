#
# SetWebConfigSettings.ps1
#

param(
    [string]$parametersFilePath,
	$appVariables
)

Write-Output "Entering script SetWebConfiguration.ps1"
Write-Output "Version 1.00"
Write-Output ("Path to Parameters File: {0}" -f $parametersFilePath)
 
# read in the setParameters file
$contents = gc -path $parametersFilePath
 
# perform a regex replacement
$newContents = "";
$i = 0
$contents | % {
    #loop through the contents, getting each line
    $line = $_

    #the result of this match is stored in the $matches variable
    if ($_ -match "__(\w+)__") {
        $i = $i + 1
        
        #Show the $matches value to debug
        Write-Output ("Found parameter name/value data on line #{0}: {1}='{2}'" -f $i, $matches[1], $matches[0])
        #Show the entire line to debug
        Write-Output ("Line #{0}: {1}" -f $i, $_)
		
        #The the matching value for this name from the varaibles hash/keyvaluepair passed into the script
        $setting = $appVariables.Item($matches[1])

        #We found an item, replace it in the file        
        if ($setting) {
            Write-Output ("Replacing key '{0}' with value '{1}'" -f $matches[1], $setting)
            $line = $_ -replace "__(\w+)__", $setting
        }
    }
    $newContents += $line + [Environment]::NewLine
}

#"Overwriting SetParameters file with new content"
sc $parametersFilePath -Value $newContents
 
Write-Output "Exiting script SetWebConfigSettings.ps1"