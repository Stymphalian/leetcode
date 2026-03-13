#!/usr/bin/env python3

import argparse
import json
import re
import sys
import urllib.error
import urllib.request
from pathlib import Path


LEETCODE_GRAPHQL = "https://leetcode.com/graphql"


def post_graphql(query: str, variables: dict | None = None) -> dict:
    payload = json.dumps(
        {
            "query": query,
            "variables": variables or {},
        }
    ).encode("utf-8")
    request = urllib.request.Request(
        LEETCODE_GRAPHQL,
        data=payload,
        headers={
            "Content-Type": "application/json",
            "Referer": "https://leetcode.com/",
            "User-Agent": "leetcode-header-skill/1.0",
        },
        method="POST",
    )
    with urllib.request.urlopen(request, timeout=20) as response:
        return json.loads(response.read().decode("utf-8"))


def extract_slug(problem_url: str) -> str:
    match = re.search(r"/problems/([^/]+)/", problem_url)
    if not match:
        raise ValueError(f"Could not parse slug from URL: {problem_url}")
    return match.group(1)


def fetch_daily_problem() -> dict:
    query = """
    query questionOfToday {
      activeDailyCodingChallengeQuestion {
        question {
          questionFrontendId
          title
          titleSlug
          difficulty
        }
      }
    }
    """
    payload = post_graphql(query)
    question = (
        payload.get("data", {})
        .get("activeDailyCodingChallengeQuestion", {})
        .get("question", {})
    )

    required = ["questionFrontendId", "title", "titleSlug", "difficulty"]
    missing = [field for field in required if not question.get(field)]
    if missing:
        raise RuntimeError(f"Missing daily problem fields: {', '.join(missing)}")

    return {
        "number": str(question["questionFrontendId"]),
        "title": question["title"],
        "slug": question["titleSlug"],
        "difficulty": question["difficulty"],
    }


def fetch_problem_by_slug(slug: str) -> dict:
    query = """
    query getQuestionDetail($titleSlug: String!) {
      question(titleSlug: $titleSlug) {
        questionFrontendId
        title
        titleSlug
        difficulty
      }
    }
    """
    payload = post_graphql(query, {"titleSlug": slug})
    question = payload.get("data", {}).get("question", {})

    required = ["questionFrontendId", "title", "titleSlug", "difficulty"]
    missing = [field for field in required if not question.get(field)]
    if missing:
        raise RuntimeError(
            f"Missing problem fields for slug '{slug}': {', '.join(missing)}"
        )

    return {
        "number": str(question["questionFrontendId"]),
        "title": question["title"],
        "slug": question["titleSlug"],
        "difficulty": question["difficulty"],
    }


def canonical_problem_url(slug: str) -> str:
    return f"https://leetcode.com/problems/{slug}/description/"


def build_header(number: str, title: str, url: str, difficulty: str, time_taken: str) -> str:
    return "\n".join(
        [
            f"// {number} {title}",
            f"// {url}",
            f"// Difficulty: {difficulty}",
            f"// Time Taken: {time_taken}",
            "",
            "",
        ]
    )


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(
        description="Create problems/p{number}/main.cs with a LeetCode header."
    )
    parser.add_argument(
        "--problem-url",
        help="Optional LeetCode problem URL. If omitted, use current daily problem.",
    )
    parser.add_argument(
        "--time-taken",
        default=None,
        help="Header value for 'Time Taken' (default: placeholder HH:MM:SS).",
    )
    parser.add_argument(
        "--force",
        action="store_true",
        help="Overwrite main.cs if it already exists.",
    )
    return parser.parse_args()


def main() -> int:
    args = parse_args()

    try:
        if args.problem_url:
            slug = extract_slug(args.problem_url)
            problem = fetch_problem_by_slug(slug)
        else:
            problem = fetch_daily_problem()
    except (urllib.error.URLError, TimeoutError, RuntimeError, ValueError) as error:
        print(f"Error fetching LeetCode problem metadata: {error}", file=sys.stderr)
        return 1

    number = problem["number"]
    title = problem["title"]
    slug = problem["slug"]
    difficulty = problem["difficulty"]
    url = canonical_problem_url(slug)

    workspace_root = Path(__file__).resolve().parents[4]
    problem_dir = workspace_root / "problems" / f"p{number}"
    target_file = problem_dir / "main.cs"

    problem_dir.mkdir(parents=True, exist_ok=True)

    if target_file.exists() and not args.force:
        print(f"Skipped: {target_file} already exists. Use --force to overwrite.")
        return 0

    time_taken = args.time_taken or "HH:MM:SS"
    content = build_header(number, title, url, difficulty, time_taken)
    target_file.write_text(content, encoding="utf-8")

    print(f"Created: {target_file}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
