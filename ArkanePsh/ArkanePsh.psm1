# Exported functions

<#
 .Synopsis
 Edits the current user's host profile.

 .Description
 Launches the default editor on the PowerShell profile for the current user on the current host.

 .Example
 edit-hostprofile
 #>

function Edit-HostProfile
{
	Start-Process -FilePath (Get-ApplicationPath ps1) -ArgumentList $profile
}

Export-ModuleMember -Function Edit-HostProfile

<#
 .Synopsis
 Edits the current user's profile.

 .Description
 Launches the default editor on the PowerShell profile for the current user on any host.

 .Example
 edit-profile
 #>

function Edit-Profile
{
	Start-Process -FilePath (Get-ApplicationPath ps1) -ArgumentList $profile.CurrentUserAllHosts
}

Export-ModuleMember -Function Edit-Profile

<#
 .Synopsis
 Get the application path for the given extension.

 .Description
 Returns the application path (default executable) for the given extension.

 .Parameter Extension
 The extension for which to return the application path (i.e., 'exe').

 .Example
 get-applicationpath docx
 #>

function Get-ApplicationPath
{
	[CmdletBinding()]
	param
	(
		[string]$Extension
	)

	try
	{
		$default = (Get-ItemProperty -Path "HKLM:\Software\Classes\.$Extension" -Name '(Default)' -ErrorAction Stop).'(Default)'

		(Get-ItemProperty "HKLM:\Software\Classes\$default\shell\open\command" -Name '(Default)' -ErrorAction Stop).'(Default)' -match '([^"^\s]+)\s*|"([^"]+)"\s*' | Out-Null
		$path = $matches[0].ToString()

		$path.Trim('"',' ')
	}
	catch
	{
		Write-Error "An application path was not found for the filetype '.$Extension'."
	}
}

Export-ModuleMember -Function Get-ApplicationPath

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
	invoke-nothing
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

 <#
	.Synopsis
	Refreshes the current user's host profile.

	.Description
	Reloads the PowerShell profile for the current user on the current host.

	.Example
	refresh-hostprofile
  #>

function Refresh-HostProfile
{
	. $profile
}

 Export-ModuleMember -Function Refresh-HostProfile

 <#
	.Synopsis
	Refreshes the current user's profile.

	.Description
	Reloads the PowerShell profile for the current user on any host.

	.Example
	refresh-profile
  #>

 function Refresh-Profile
 {
	 . $profile.CurrentUserAllHosts
 }

 Export-ModuleMember -Function Refresh-Profile

 # System cmdlet aliases

 New-Alias dd Push-Location -Description 'Adds the current location to the top of a location stack.' -Scope Global -Force
 New-Alias du Pop-Location -Description 'Changes the current location to the location most recently pushed onto the stack.' -Scope Global -Force
 New-Alias jobs Get-Job -Description 'Gets Windows PowerShell background jobs that are running in the current session.' -Scope Global -Force
 New-Alias new New-Object -Description 'Creates an instance of a Microsoft .NET Framework or COM object.' -Scope Global -Force

 Export-ModuleMember -Alias dd,du,jobs,new
