# Maximum Number of Events That Can Be Attended II
https://leetcode.com/problems/maximum-number-of-events-that-can-be-attended-ii/description \
**Time Taken**: 01:54:55\
**Difficulty**: Hard \
**Time**: O(n•k•log(n)) \
**Space**: O(n•k)

Unsolved. Needed to look at the editorial solution.
Takeaways:
1. I was on the right track with using a DP/recursive approach. I initially abandoned
it because I couldn't figure out how to memoize the results. My key was (index, k, value)
but it looks like `value` is not needed in the top-down DP approach.
2. I couldn't quite figure out how to efficiently find the next set of events
to process, but the binary_search makes perfect sense.
