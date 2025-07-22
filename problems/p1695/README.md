# Maximum Erasure Value
https://leetcode.com/problems/maximum-erasure-value/description \
**Time Taken**: 00:25:37 \
**Difficulty**: Medium \
**Time**: O(n) \
**Space**: O(n)

## Commentary
Sub-array and maximum score. It screamed to me sum-array and a sliding window.
General idea is to iterate left-to-right and keep a `left` and `right` indices
of the current active sub-array.

1. As we advanced the `right` pointer keep a running `sum` of the current sub-array. 
Also keep a `dictionary`of the last index we have seen this `value`, as well as a sum-array.
2. If we have seen this `value` before, find the last `index` we found this `value`
and recalculate the subarray sum between the `last_index` and the current `index` using
the sum-array. By keeping the `dictionary` we can easily "jump" the `left` pointer 
to the correct position instead of advancing slowly.