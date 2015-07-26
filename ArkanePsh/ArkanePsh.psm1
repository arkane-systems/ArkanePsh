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
