---
description: Review changed C# and .NET files using the expert .NET software engineer agent.
intent: Identify actionable correctness, security, performance, design, and testing issues in changed C# and .NET code.
on:
  pull_request:
    types: [opened]
    paths:
      - "**/*.cs"
permissions:
  contents: read
  pull-requests: read
  copilot-requests: write
plugins:
  - github/awesome-copilot/plugins/csharp-dotnet-development@marketplace
engine:
  id: copilot
  agent: csharp-dotnet-development:expert-dotnet-software-engineer
safe-outputs:
  noop:
  submit-pull-request-review:
    max: 1
    allowed-events: [COMMENT, REQUEST_CHANGES]
    target: triggering
    footer: if-body
    supersede-older-reviews: true
---

# Evaluate C# Code

Review the C# and .NET files changed by the triggering pull request.

Focus on actionable defects and regressions in correctness, security, performance, API design, async behavior, resource management, and test coverage. Follow the repository's existing conventions and use the installed C#/.NET skills when relevant.

Submit one concise pull request review that lists findings in severity order and cites the affected files and lines. Use `REQUEST_CHANGES` only when concrete correctness, security, or reliability defects must be fixed before merge. Use `COMMENT` for advisory, performance, design, or test-coverage findings that do not need to block the pull request. Do not report purely stylistic preferences. If no actionable issues are found, use `noop` with a short explanation instead of submitting a review.
