# Find the Original Typed String I
https://leetcode.com/problems/find-the-original-typed-string-i/description/ \
**Time Taken**: 00:08:10 \
**Difficulty**: Easy \
**Time**: O(n) \
**Space**: O(1)

## Approach
Walk through the string. For any section of the string with `n` consecutive characters
we want to count as `n-1` number of possible variations. We can do this in one 
pass by just going through the array and checking the previous character to see
if it matches the current character.