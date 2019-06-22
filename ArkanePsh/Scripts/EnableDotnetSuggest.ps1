# enable 'dotnet suggest' if it is available

if (Get-Command "dotnet-suggest" -ErrorAction SilentlyContinue) 
{
	$availableToComplete = (dotnet-suggest list) | Out-String
	$availableToCompleteArray = $availableToComplete.Split([Environment]::NewLine, [System.StringSplitOptions]::RemoveEmptyEntries) 


	Register-ArgumentCompleter -Native -CommandName $availableToCompleteArray -ScriptBlock {
		param($commandName, $wordToComplete, $cursorPosition)
		$fullpath = (Get-Command $wordToComplete.CommandElements[0]).Source

		$arguments = $wordToComplete.Extent.ToString().Replace('"', '\"')
		dotnet-suggest get -e $fullpath --position $cursorPosition -- "$arguments" | ForEach-Object {
			[System.Management.Automation.CompletionResult]::new($_, $_, 'ParameterValue', $_)
		}
	}
	$env:DOTNET_SUGGEST_SCRIPT_VERSION = "1.0.0"
}
