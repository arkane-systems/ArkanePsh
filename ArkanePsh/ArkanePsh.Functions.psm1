# Exported functions

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
    Save the shell history to a history file.

    .Description
    Saves the shell history to a CLIXML history file, which can be used by Import-History.

    .Parameter Path
    The history file to save, defaulting to .history.tmp in the current user's documents folder.
 #>

 function Export-History
 {
     param
     (
         [string] $path = "${myDocuments}\.history.tmp"
     )

     Get-History -count $MaximumHistoryCount | Group-Object CommandLine | foreach {$_.Group[0]} | Export-Clixml $path
 }

 New-Alias shy Import-History -Description "Save the shell history to a history file." -Scope Global -Force

 Export-ModuleMember -Function Export-History -Alias shy

 <#
    .Synopsis
    Gets the current system time.

    .Description
    Returns the current system time as a System.DateTime.
#>

function Get-CurrentTime
{
    [DateTime]::Now
}

New-Alias now Get-CurrentTime -Description "Gets the current system time." -Scope Global -Force

Export-ModuleMember -Function Get-CurrentTime -Alias now

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
    Gets a list of all assemblies loaded into the current PowerShell session.

    .Description
    Returns a list of all assemblies loaded into the current PowerShell session.
#>

function Get-LoadedAssemblies
{
    [AppDomain]::CurrentDomain.GetAssemblies()
}

Export-ModuleMember -Function Get-LoadedAssemblies

<#
    .Synopsis
    Gets a list of all accessible types of the current loaded assemblies.

    .Description
    Returns a list of all accessible types of the current loaded assemblies.
#>

function Get-LoadedTypes
{
    Get-LoadedAssemblies | foreach `
    {
        if (-not $_.IsDynamic)
        {
            $_.GetExportedTypes()
        }
    }
}

Export-ModuleMember -Function Get-LoadedTypes

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
    Load the shell history from a history file.

    .Description
    Loads the shell history from a CLIXML history file, such as that produced by export-history.

    .Parameter Path
    The history file to load, defaulting to .history.tmp in the current user's documents folder.
 #>

 function Import-History
 {
     param
     (
         [string] $path = "${myDocuments}\.history.tmp"
     )

     Import-Clixml $path | Add-History
 }

 New-Alias lhy Import-History -Description "Load the shell history from a history file." -Scope Global -Force

 Export-ModuleMember -Function Import-History -Alias lhy

<#
    .Synopsis
    Invoke the assembly installation utility.

    .Description
    Invokes the assembly installation utility with arbitrary parameters.
#>

function Install-Assembly
{
    . "$(Get-DotNetInstallDirectory)installutil.exe" $args $input
}

Export-ModuleMember -Function Install-Assembly

<#
    .Synopsis
    Invoke the Microsoft Build Engine.

    .Description
    Invokes the Microsoft Build Engine with arbitrary parameters.
#>

function Invoke-BuildEngine
{
    . "$(Get-DotNetInstallDirectory)msbuild.exe" $args $input
}

Export-ModuleMember -Function Invoke-BuildEngine

<#
    .Synopsis
    Invoke the C# compiler.

    .Description
    Invokes the C# compiler with arbitrary parameters.
#>
function Invoke-CSharpCompiler
{
    # needs updating to find Roslyn compiler

    . "$(Get-DotNetInstallDirectory)csc.exe" $args $input
}

Export-ModuleMember -Function Invoke-CSharpCompiler

<#
    .Synopsis
    Invoke the IL assembler.

    .Description
    Invokes the IL assembler with arbitrary parameters.
#>
function Invoke-IlAssembler
{
    . "$(Get-DotNetInstallDirectory)ilasm.exe" $args $input
}

Export-ModuleMember -Function Invoke-IlAssembler

<# 
    .Synopsis 
    Displays pipelined text in a temporary file. The temp file 
    is displayed in the default text editor. When the editor is closed, the temp 
    file is deleted. If you wish to save the information that  
    is displayed, use the saveas feature from the editor. 

    .Example 
    "string" | Out-TempFile 
    Displays the word string in a temporary file via the default text editor. 
    When the default text editor is closed, the temporary file that contained 
    the word "string" is deleted. 

    .Example 
    Get-Process | Out-TempFile 
    Displays process info in a temporary file via the default text editor. 
    When the default text editor is closed, the temporary file that contained 
    the process information is deleted. 

    .Example 
    Out-TempFile -in (get-service) 
    Displays service info in a temporary file via the default text editor. 
    When the default text editor is closed, the temporary file that contained 
    the process information is deleted. 

    .Parameter in 
    Data to display in a temporary file 

    .Inputs 
    [psobject] 

    .Outputs 
    [psobject] 
#>

function Out-TempFile 
{ 
    [CmdletBinding()] 
    param
    ( 
        [Parameter(Mandatory = $True,valueFromPipeline=$true,Position=0)] 
        [psobject] 
        $in 
    )

    BEGIN 
    {
        $tempFile = [io.path]::GetTempFileName()
    } 

    PROCESS 
    {
        out-file -filePath $tempFile -InputObject $in -Append -Width 300 -Encoding unicode
    } 

    END 
    {
        . (Get-ApplicationPath txt) $tempFile | Out-Null 
        Remove-Item $tempFile
    } 
}

Export-ModuleMember Out-TempFile

 <#
    .Synopsis
    Set the location to a peer directory of the current directory.

    .Parameters Path
    The filename of the peer directory to change to.
 #>

 function Set-LocationOver
 {
     param
     (
         [string]$path = $(throw "USAGE: Set-LocationOver -Path path")
     )

     Set-Location ('..\' + $path)
 }

 New-Alias over Set-LocationOver -Description "Set the location to a peer directory of the current directory." -Scope Global -Force

 Export-ModuleMember -Function Set-LocationOver -Alias over

 <#
    .Synopsis
    Set the location to the root of the current PSDrive.
 #>

 function Set-LocationRoot
 {
     Set-Location /
 }

 New-Alias root Set-LocationRoot -Description "Set the location to the root of the current PSDrive." -Scope Global -Force

 Export-ModuleMember -Function Set-LocationRoot -Alias root

 <#
    .Synopsis
    Set the location to the parent folder of the current directory.
 #>

 function Set-LocationUp
 {
     Set-Location ..
 }

 New-Alias up Set-LocationUp -Description "Set the location to the parent folder of the current directory." -Scope Global -Force

 Export-ModuleMember -Function Set-LocationUp -Alias up

 <#
    .Synopsis
    Determines if you are running in a 64 bit process.
 #>

 function Test-Is64Bit
 {
     [System.Environment]::Is64BitProcess
 }

 Export-ModuleMember -Function Test-Is64Bit

<#
    .Synopsis
    Determines if you are running in the Windows PowerShell ISE
 
    .Description
    This function determines if you are running in the Windows Powershell
    ISE by querying the $ExecutionContext automatic variable.
 
    .Example
    Test-IsISEHost
  
    .Example
    if(Test-IsISEHost) { "Using the ISE" }
    # Prints out Using the ISE when run inside the ISE, otherwise nothing

#>

function Test-IsIseHost
{
    $ExecutionContext.Host.name -match "ISE Host$"
}

Export-ModuleMember -Function Test-IsISEHost

 <#
    .Synopsis
    Test whether the target computer is a laptop/tablet.

    .Parameter Computer
    The computer name of the computer to test.
 #>

 function Test-IsLaptop
 {
     param
     (
         [string]$computer = "localhost"
     )

     $isLaptop = $false

     if (Get-WmiObject -Class win32_systemenclosure -ComputerName $computer | 
         Where-Object { $_.chassistypes -eq 9 -or $_.chassistypes -eq 10 `
         -or $_.chassistypes -eq 14})
     {
         $isLaptop = $true
     }

     if (Get-WmiObject -Class win32_battery -ComputerName $computer) 
     { 
         $isLaptop = $true
     }

     $isLaptop
 }

 Export-ModuleMember -Function Test-IsLaptop

 <#
    .Synopsis
    Refreshes the current user's host profile.

    .Description
    Reloads the PowerShell profile for the current user on the current host.

    .Example
    update-hostprofile
  #>

function Update-HostProfile
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

 function Update-Profile
 {
     . $profile.CurrentUserAllHosts
 }

 Export-ModuleMember -Function Update-Profile

<#
    .Synopsis
    Execute a ScriptBlock as another culture.

    .Parameter Culture
    The culture (a CultureInfo) in which to execute the ScriptBlock.

    .Parameter Script
    The ScriptBlock to execute.
#>

function Use-Culture
{
    param
    (
        [System.Globalization.CultureInfo]$culture = $(throw "USAGE: Using-Culture -Culture culture -Script {scriptblock}"),
        $script= $(throw "USAGE: Using-Culture -Culture culture -Script {scriptblock}")
    )

    $OldCulture = [System.Threading.Thread]::CurrentThread.CurrentCulture
    trap 
    {
        [System.Threading.Thread]::CurrentThread.CurrentCulture = $OldCulture
    }
    [System.Threading.Thread]::CurrentThread.CurrentCulture = $culture
    Invoke-expression $script
    [System.Threading.Thread]::CurrentThread.CurrentCulture = $OldCulture
}

Export-ModuleMember -Function Use-Culture

<#
    .Synopsis
    Execute a ScriptBlock in another location.

    .Parameter Location
    The location in which to execute the ScriptBlock.

    .Parameter Script
    The ScriptBlock to execute.
#>

function Use-Location
{
    param
    (
        [string]$location = $(throw "USAGE: using-location -Location location -Script {scriptblock}"),
        $script = $(throw "USAGE: using-location -Location location -Script {scriptblock}")
    )

    push-location $location -stackname usingLocationStack
    invoke-expression $script
    pop-location -stackname usingLocationStack
}

New-Alias within Use-location -Description "Execute a ScriptBlock in another location." -Scope Global -Force

Export-ModuleMember -Function Use-Location -Alias within
