# 02-project-targeting: Retarget project frameworks to net10.0-compatible settings

## Objective
Apply framework-target updates to all upgradable projects in scope, including SDK-style project files and any supported classic project metadata.

## Scope findings
- `ArkanePsh.Cmdlets/ArkanePsh.Cmdlets.csproj` is SDK-style and directly supports `<TargetFramework>` updates.
- `ArkanePsh/ArkanePsh.pssproj` is a legacy PowerShell project file (`ToolsVersion="4.0"`) with no `TargetFramework` property; it is not a standard SDK-style .NET project retargeting surface.

## Targeting decision
- Retarget `ArkanePsh.Cmdlets.csproj` from `net6.0` to `net10.0`.
- Keep `ArkanePsh.pssproj` unchanged in this task and treat it as non-directly-retargetable metadata project, with any required compatibility handling deferred to follow-up validation/fix tasks.

**Done when**: All in-scope project files declare net10.0-compatible targets or documented equivalent settings when direct net10.0 targeting is unsupported.
