# Minimum Difference in Sums After Removal of Elements
https://leetcode.com/problems/minimum-difference-in-sums-after-removal-of-elements/description \
**Time Taken**: 08:41:16 \
**Difficulty**: Hard \
**Time**: O(n * log(n)) \
**Space**: O(n)

Didn't solve. \
Lessons: \
1. Straightforward to make the observation that the optimal diff in sums is between a Min - Max subsequence.
2. I had an original idea of using two PQs to keep track of a left and right side. I was thinking about how to subtractively reduce the left and right side into the proper subsequences. This lead to lots of problems when the sub-sequence got less than length `n`.
3. I don't know how to make the leap to keep track of the min/max at index i arrays.
4. I think for counting problems you should always try to think about not actually computing the answer directly. Think about how to purely do the counting without needing to construct the underlying subsequence itself.
4. I had a lot of trouble with creating the arrays. I got stuck with the indexing, and lots of off-by-one errors.