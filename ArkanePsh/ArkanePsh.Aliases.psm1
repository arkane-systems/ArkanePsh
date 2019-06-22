 # System cmdlet aliases

 New-Alias dd Push-Location -Description 'Adds the current location to the top of a location stack.' -Scope Global -Force
 New-Alias du Pop-Location -Description 'Changes the current location to the location most recently pushed onto the stack.' -Scope Global -Force
 New-Alias jobs Get-Job -Description 'Gets Windows PowerShell background jobs that are running in the current session.' -Scope Global -Force
 New-Alias new New-Object -Description 'Creates an instance of a Microsoft .NET Framework or COM object.' -Scope Global -Force

 Export-ModuleMember -Alias dd,du,jobs,new

 # Alias functions

 <#
    .Synopsis
    Delete files (with confirmation).
 #>

function del?
{
    remove-item -Confirm $args
}

Export-ModuleMember -Function del?

 <#
    .Synopsis
    Delete files (with force).
 #>

function del!
{
    remove-item -force $args
}

Export-ModuleMember -Function del!

<#
    .Synopsis
    Retrieve a list of external PowerShell scripts in the path.
#>

function Get-Scripts
{
    Get-Command -Type ExternalScript
}

Export-ModuleMember -Function Get-Scripts

<#
    .Synopsis
    Create and enter a directory.
#>

function nd
{
    mkdir $args ;
    Set-Location $args[0]
}

Export-ModuleMember -Function nd

<#
    .Synopsis
    Obliterate a directory and all its contents.

    .Description
    Obliterate a directory and all its contents (requires confirmation).
#>

function zap
{
    Remove-Item -Confirm -Recurse -Force $args
}

Export-ModuleMember -Function zap

# Go functions

<#
    .Synopsis
    Set current location to the current user's desktop folder.
#>

function Set-LocationDesktop
{
    Get-SpecialFolder Desktop | Set-Location
}

New-Alias go-desktop Set-LocationDesktop -Description "Set current location to the current user's desktop folder." -Scope Global -Force

Export-ModuleMember -Function Set-LocationDesktop -Alias go-desktop

<#
    .Synopsis
    Set current location to the current user's home directory.
#>

function Set-LocationHome
{
    Set-Location ~
}

New-Alias go-home Set-LocationHome -Description "Set current location to the current user's home directory." -Scope Global -Force

Export-ModuleMember -Function Set-LocationHome -Alias go-home

<#
    .Synopsis
    Set current location to the current user's user profile directory.
#>

function Set-LocationProfile
{
    Get-SpecialFolder UserProfile | Set-Location
}

New-Alias go-profile Set-LocationProfile -Description "Set current location to the current user's user profile directory." -Scope Global -Force

Export-ModuleMember -Function Set-LocationProfile -Alias go-profile

<#
    .Synopsis
    Set current location to the local temporary files directory.
#>

function Set-LocationTemp
{
    Set-Location "$(Get-SpecialFolder LocalApplicationData)\Temp"
}

New-Alias go-temp Set-LocationTemp -Description "Set current location to the local temporary files directory." -Scope Global -Force

Export-ModuleMember -Function Set-LocationTemp -Alias go-temp

<#
    .Synopsis
    Set current location to the local working folder.

    .Description
    Set current location to the local working folder (defined in the $workingFolder variable).
#>

function Set-LocationWorking
{
    Set-Location $workingFolder
}

New-Alias go-working Set-LocationWorking -Description "Set current location to the local working folder." -Scope Global -Force

Export-ModuleMember -Function Set-LocationWorking -Alias go-working
