# Find the Maximum Length of Valid Subsequence I
https://leetcode.com/problems/find-the-maximum-length-of-valid-subsequence-i/description \
**Time Taken**: 00:20:16 \
**Difficulty**: Medium \
**Time**: O(n) \
**Space**: O(1)

## Approach
1. The valid condition is looking at the parity of adjacent numbers.
2. Observe the fact that:
    ```
   even + even == even
   odd + odd == even
   even + odd == odd
   ```
3. Given this fact the longest valid subsequence must be a count of the longest run of even numbers, or longest run of odd numbers, longest sequence of alternating even-odd pairings.
4. Run a loop to make this count and return the Max among the three possibilities.