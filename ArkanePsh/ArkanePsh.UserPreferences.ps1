# ---------------------------------------------------------------------------
# You can override individual preferences by passing a hashtable with just those
# preference defined as shown below:
#
#     Import-Module Pscx -arg @{ModulesToImport = @{Prompt = $true}}
#
# Any value not specified will be retrieved from the default preferences built
# into the PSCX DLL.
#
# If you have a sufficiently large number of altered preferences, copy this file,
# modify it and pass the path to your preferences file to Import-Module e.g.:
#
#     Import-Module Pscx -arg "$(Split-Path $profile -parent)\Pscx.UserPreferences.ps1"
#
# ---------------------------------------------------------------------------
@{
	CD_GetChildItem = $false          # Display the contents of new provider location after using
									  # cd (Set-LocationEx).  Mutually exclusive with CD_EchoNewLocation.

	CD_EchoNewLocation = $false       # Display new provider location after using cd (Set-LocationEx).
									  # Mutually exclusive with CD_GetChildItem.
}
