---
name: leetcode-header
description: 'Create today\'s LeetCode C# starter file with a standardized header comment. Use when you want to fetch the current daily problem from https://leetcode.com/problemset/ and scaffold problems/p{number}/main.cs automatically.'
argument-hint: 'Optional: "force" to overwrite existing main.cs, and "time=HH:MM:SS" to set Time Taken explicitly (default placeholder: HH:MM:SS).'
user-invocable: true
---

# LeetCode Header Scaffolder

Creates a new folder under `problems/` for the current daily LeetCode problem and writes `main.cs` with the required header format.

## Output

- Folder: `problems/p{problemNumber}/`
- File: `problems/p{problemNumber}/main.cs`
- Header format:

```csharp
// 3197 Find the Mininum Area to Cover All Ones II
// https://leetcode.com/problems/find-the-minimum-area-to-cover-all-ones-ii/description/
// Difficulty: Hard
// Time Taken: 02:53:47
```

## Procedure

1. Fetch today\'s problem from LeetCode:
   - Browser-first path:
     - Open `https://leetcode.com/problemset/` in the integrated browser.
     - Extract the daily challenge problem URL and (if visible) title/number/difficulty.
   - Reliable fallback path:
     - If browser extraction is blocked or unclear, use the helper script directly, which fetches daily metadata from LeetCode GraphQL.

2. Run the scaffold script from workspace root:

```bash
python3 ./.github/skills/leetcode-header/scripts/create_leetcode_header.py
```

3. Optional flags:
   - Overwrite existing file:

```bash
python3 ./.github/skills/leetcode-header/scripts/create_leetcode_header.py --force
```

   - Set explicit time value (otherwise the header uses placeholder `HH:MM:SS`):

```bash
python3 ./.github/skills/leetcode-header/scripts/create_leetcode_header.py --time-taken 00:10:25
```

   - Build from a specific problem URL instead of daily:

```bash
python3 ./.github/skills/leetcode-header/scripts/create_leetcode_header.py --problem-url https://leetcode.com/problems/two-sum/description/
```

## Completion checks

- `problems/p{number}` exists.
- `main.cs` exists under that folder.
- The first four lines match the required header fields and order.

## Decision points

- If browser extraction and GraphQL daily metadata disagree, trust GraphQL metadata for `number/title/difficulty` and use canonical `https://leetcode.com/problems/{slug}/description/` URL.
- If `main.cs` already exists and `--force` is not set, stop without overwriting.

## Script

- `scripts/create_leetcode_header.py`
