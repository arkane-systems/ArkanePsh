# Exported functions

<#
 .Synopsis
 Gets the .NET runtime installation directory.

 .Description
 Returns the directory where the common language runtime is installed.

 .Parameter AsInfo
 Returns a DirectoryInfo object rather than a path string.
 #>

function Get-DotNetInstallDirectory
{
	param
	(
		[switch]$AsInfo
	)

	$runtimePath = [System.Runtime.InteropServices.RuntimeEnvironment]::GetRuntimeDirectory()

	if (-not $AsInfo)
	{
		$runtimePath
	}
	else
	{
		[System.IO.DirectoryInfo] $runtimePath
	}
}

Export-ModuleMember -Function Get-DotNetInstallDirectory

<#
	.Synopsis
	Gets the location of one of the Windows special folders.

	.Description
	Returns the location of one of the Windows special folders, as described in System.Environment.

	.Parameter Alias
	The alias of the special folder to get; one of the System.Environment.SpecialFolders enum values.

	.Parameter AsInfo
	Returns a DirectoryInfo object rather than a path string.

	.Example
	get-specialfolder Desktop
 #>

function Get-SpecialFolder
{
	param
	(
		[System.Environment+SpecialFolder]$Alias,
		[switch]$AsInfo
	)

	$folderPath = [Environment]::GetFolderPath($Alias)

	if (-not $AsInfo)
	{
		$folderPath
	}
	else
	{
		[System.IO.DirectoryInfo] $folderPath
	}
}

Export-ModuleMember -Function Get-SpecialFolder

<#
 .Synopsis
 Does nothing.

 .Description
 Does nothing, either succeeding or failing.

 .Parameter Fail
 Fail to do nothing successfully.

 .Example
	# Do nothing.
	do-nothing
 #>

 function Invoke-Nothing
 {
	[CmdletBinding()]
	param
	(
		[switch]$Fail
	)

	if (-not $Fail)
	{
		write-verbose 'Doing nothing and succeeding.'
	}
	else
	{
		Write-Error -Category NotEnabled 'Doing nothing and failing.'
	}
 }

 Export-ModuleMember -Function Invoke-Nothing

 # System cmdlet aliases

 New-Alias dd Push-Location -Description 'Adds the current location to the top of a location stack.' -Scope Global -Force
 New-Alias du Pop-Location -Description 'Changes the current location to the location most recently pushed onto the stack.' -Scope Global -Force
 New-Alias jobs Get-Job -Description 'Gets Windows PowerShell background jobs that are running in the current session.' -Scope Global -Force
 New-Alias new New-Object -Description 'Creates an instance of a Microsoft .NET Framework or COM object.' -Scope Global -Force

 Export-ModuleMember -Alias dd,du,jobs,new
