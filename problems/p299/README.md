# Bulls and Cows
https://leetcode.com/problems/bulls-and-cows/description/ \
**Time Taken**: 00:16:04 \
**Difficulty**: Medium \
**Time**: O(n) \
**Space**: O(n)

## Approach
1. First generate a map `alphabet` which counts the number of times a digit appears in the secret string.
2. Do two separate iterations through the guess string. First count the number of `bulls`
   For each bull decrement the counter in `alphabet` map
3. In the second interation count the number of `cows` but only if `alphabet[c] > 0`



