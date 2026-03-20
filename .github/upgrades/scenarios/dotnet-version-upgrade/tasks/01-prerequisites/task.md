# 01-prerequisites: Validate SDK and upgrade preconditions

## Objective
Confirm local SDK/tooling compatibility for net10.0 and identify any global.json constraints before project retargeting starts.

## Findings
- `validate_dotnet_sdk_installation(net10.0)` returned: **Compatible SDK found**.
- `validate_dotnet_sdk_in_globaljson(net10.0)` returned: **No global.json found** (no constraints to resolve).

## Conclusion
Prerequisites are satisfied. The workflow can proceed to project retargeting.

**Done when**: The required .NET SDK baseline is validated and any global.json constraints are documented/resolved for the solution.
