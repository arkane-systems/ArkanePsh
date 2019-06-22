#
# InitVariables.ps1
#

# Internal helper functions

function Set-ArkaneVariable ($name, $value)
{
	Set-Variable $name $value -Scope Global -Option AllScope,ReadOnly -Description "ArkanePsh module variable." -Force
}

# Set up relevant variables

Set-ArkaneVariable myDocuments          ([System.Environment]::GetFolderPath(5))

Set-ArkaneVariable executableExtensions (".exe",".ps1",".cmd",".py")
Set-ArkaneVariable archiveExtensions    (".7z",".zip",".gz",".bz2",".rar",".tar",".z")

Set-ArkaneVariable windowsIdentity      ([System.Security.Principal.WindowsIdentity]::GetCurrent())
Set-ArkaneVariable windowsPrincipal     (new-object System.Security.Principal.WindowsPrincipal $windowsIdentity)
Set-ArkaneVariable isAdmin              ($windowsPrincipal.IsInRole([System.Security.Principal.WindowsBuiltInRole]::Administrator))

# This next is intended to be overriden in profile.
Set-Variable workingFolder "C:\Working" -Scope Global -Option AllScope -Description "(Overridable) ArkanePsh module variable." -Force

# Delete internal helper functions

remove-item function:Set-ArkaneVariable
