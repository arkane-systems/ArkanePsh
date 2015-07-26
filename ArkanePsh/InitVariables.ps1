#
# InitVariables.ps1
#

# Internal helper functions

function Set-ArkaneVariable ($name, $value)
{
	Set-Variable $name $value -Scope Global -Option AllScope,ReadOnly -Description "ArkanePsh module variable." -Force
}

# Set up relevant variables

Set-ProfileVariable myDocuments          ([System.Environment]::GetFolderPath(5))

Set-ProfileVariable executableExtensions (".exe",".ps1",".cmd",".py")
Set-ProfileVariable archiveExtensions    (".7z",".zip",".gz",".bz2",".rar",".tar",".z")

Set-ProfileVariable windowsIdentity      ([System.Security.Principal.WindowsIdentity]::GetCurrent())
Set-ProfileVariable windowsPrincipal     (new-object System.Security.Principal.WindowsPrincipal $windowsIdentity)
Set-ProfileVariable isAdmin              ($windowsPrincipal.IsInRole([System.Security.Principal.WindowsBuiltInRole]::Administrator))

# Delete internal helper functions

remove-item function:Set-ArkaneVariable
