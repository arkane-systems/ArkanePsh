#---------------------------------------------------------
# Desc: This script generates the ArkanePsh.dll-Help.xml file
#---------------------------------------------------------
param([string]$localizedHelpPath = $(throw "You must specify the path to the localized help dir"),
      [string]$configuration = $(throw "You must specify the build configuration"))
      
$ScriptDir        = Split-Path $MyInvocation.MyCommand.Path -Parent
$ModuleDir        = "$ScriptDir\..\bin\$configuration\netstandard2.0"      
$ModulePath         = Join-Path $ModuleDir "ArkanePsh"            
$ModuleManifest     = "$ModulePath.psd1"            
$ModuleModule       = "$ModulePath.dll"             
$MergedHelpPath   = Join-Path $ModuleDir MergedHelp.xml
$ModuleHelpPath     = Join-Path $ModuleDir ArkanePsh.dll-Help.xml

Import-Module $ModuleManifest

# Test the XML help files
gci $localizedHelpPath\*.xml  | Foreach {
    if (!(Test-Xml $_)) {
        Test-Xml $_ -verbose
        Write-Error "$_ is not a valid XML file"
        exit 1
    }
}

Get-PSSnapinHelp $ModuleModule -LocalizedHelpPath $localizedHelpPath -OutputPath $MergedHelpPath

$contents = Get-Content $MergedHelpPath
$contents | foreach {$_ -replace 'ModulePathInfo','String'} | Out-File $MergedHelpPath -Encoding Utf8

Convert-Xml $MergedHelpPath -xslt $ScriptDir\Maml.xslt -OutputPath $ModuleHelpPath

$helpXml = [xml](Get-Content $ModuleHelpPath)
$attrs = $helpXml.helpItems.Attributes | Where Name -ne schema
$attrs | Foreach {[void]$helpXml.helpItems.Attributes.Remove($_)}

$msHelpAttr = $helpXml.CreateAttribute('xmlns', 'MSHelp', 'http://www.w3.org/2000/xmlns/')
$msHelpAttr.Value = 'http://msdn.microsoft.com/mshelp'
$attrs += $msHelpAttr

$helpXml.helpItems.command | %{$cmd = $_; $attrs | % {[void]$cmd.SetAttributeNode($_.Clone())}}
$helpXml.Save($ModuleHelpPath)

# Low tech approach to merging in the provider help
$helpfile = Get-Content $ModuleHelpPath | ? {$_ -notmatch '</helpItems>'}
#$providerHelp = @()
#gci $providerHelpPath\Provider*.xml | ? {$_.Name -notmatch 'Provider_template'} | Foreach {
#    Write-Host "Processing $_"
#    $providerHelp += Get-Content $_
#}

#$helpfile += $providerHelp
$helpfile += '</helpItems>'
$helpfile | Out-File $ModuleHelpPath -Encoding Utf8