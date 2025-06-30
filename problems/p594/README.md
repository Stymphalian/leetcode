# Longest Harmonious Subsequence
https://leetcode.com/problems/longest-harmonious-subsequence/description \
**Time Taken**: 00:14:54 \
**Difficulty**: Easy \
**Time**: O(n*log_n) \
**Space**: O(1)

## Approach
Another subsequence type question. In general most subsequence problems either
use some form of sorting (n*logn), search (logn) or some form of DP. Given that
this problem is an EASY most likely sorting/searching was the way to go.

The `harmonious` condition means that the two numbers must differ by only `1`. 
This lends itself very well to a binary search as I can explicitly try to find 
"matching" number given a starting number. 

So the algorithm is:
1. Sort the array O(n*log_n)
2. Iterate through every element `x` in the array O(n)
3. Find the corresponding `x-1` value in the array with binary search O(n*log_n)
4. The distance between indices of `x` and `x-1` is the length of the subsequence
5. Keep track of the longest distance


---

It might be confusing to why we are allowed to sort the input array.
You might think that sorting the array would destroy the ordering of the array 
and so we would break the subsequence condition.

1. Say we have an unsorted array like this:
```[x...,   A,   y...,   B,  z...]```
2. Lets say the numbers `A` and `B` are the two numbers which give the longest harmonious subsequence and the subsequence indices are `left == index(A)` and `right == index(B)`
3. There are some numbers in sections `x...`, `y...`, and `z...` which chould be less than, greater than or even equal to the numbers `A` and `B`.
4. If we sort the array it will look something like this. ```[x,y,z...   A,   x,y,z...,   B,   x,y,z...]```.
 Elements from the `x,y and z` sections have been mixed into the different intervals.
5. Now the section between `A` and `B` could no longer be the same subsequence from the unsorted array.
6. BUT this is okay! If we take a look at the section `A, xyz..., B` we now have the subsequence
   with indices `left == min_index([A, xyz..., B])` and `right == max_index([A, xyz..., B])`
7. This might not have been the original subsequence but it still IS a subsequence from the original array and its still meets all our conditions.

In general I have found that if the specific ordering of the subsequence elements does not
matter to the problem I like to think of the subsequences more like a subset (but with duplicate elements allowed).
```