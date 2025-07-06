# Finding Pairs With a Certain Sum
https://leetcode.com/problems/finding-pairs-with-a-certain-sum/description \
**Time Taken**: 01:04:43 \
**Difficulty**: Medium \
**Time**: O(n) \
**Space**: O(n)

## Approach
1. Key observation. The `size` of `nums1` is much smaller than `nums2`.
2. For `Count` we can iterate through every `nums1` and find the corresponding
   `target = total - nums1[i]` from `nums2`
3. For finding `target` fast (`O(1)`) we can preprocess the `nums2` into a map which 
counts the number of times the number appears in the array.
4. For `Add` we can just decrement the old value and increment the new value in the map.
