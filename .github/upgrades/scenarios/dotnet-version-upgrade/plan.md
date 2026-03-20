# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade the ArkanePsh solution to .NET 10 where project types and tooling are compatible.
**Scope**: 2 projects, low API/package risk, mixed project formats (SDK-style cmdlets project + classic PowerShell project).

## Tasks

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: 2 projects, low complexity signals, no package/API blockers, and no inter-project dependency tiers.

### 01-prerequisites: Validate SDK and upgrade preconditions

Confirm local SDK/tooling compatibility for net10.0 and identify any global.json constraints before project retargeting starts.

**Done when**: The required .NET SDK baseline is validated and any global.json constraints are documented/resolved for the solution.

---

### 02-project-targeting: Retarget project frameworks to net10.0-compatible settings

Apply framework-target updates to all upgradable projects in scope, including SDK-style project files and any supported classic project metadata.

**Done when**: All in-scope project files declare net10.0-compatible targets or documented equivalent settings when direct net10.0 targeting is unsupported.

---

### 03-compatibility-fixes: Resolve upgrade-related build and configuration issues

Address compilation, configuration, and compatibility issues introduced by retargeting so the upgraded solution can be built reliably.

**Done when**: Upgrade-introduced errors are resolved and solution build succeeds without framework-targeting errors.

---

### 04-validation: Validate build and tests on upgraded targets

Run solution-level validation after retargeting and fixes to verify the upgrade is stable and warnings/errors are understood.

**Done when**: Build completes successfully and test execution status is captured (or explicitly documented if no tests are present).
