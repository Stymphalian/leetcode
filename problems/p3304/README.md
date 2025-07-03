# Find the K-th Character in String Game I
https://leetcode.com/problems/find-the-k-th-character-in-string-game-i/description \
**Time Taken**: 00:09:36 \
**Difficulty**: Easy \
**Time**: O(log(k)) \
**Space**: O(log(k))

## Approach
Brute force of just generating the strings and looking at the `k` element finishes within the time-limit.

Looking at the editorial you can see that there is a more optimal recursive solution which runs in `O(logk)` time.
I will try to explain that solution in my own words.

1. Let's make an observation. Say we are looking for `k=5` and we had already generated the string `abbc`.
We can see that upon the next doubling of the string `abbc -> abbcbccd` we know the `5th` element is `b`.
2. We know that the `b/5th` element from `abbc(b)ccd` was generated from the `(a)bbc` in the previous string
by applying the operation `('a' + 1)%26`.
3. So, if we somehow knew which `k'` index of the previous string (`1` in this case) is used to get the current `k` element we would able to easily 
get the `kth` element character without needing to fully generate the next sequence of characters.
4. This lends itself to a recursive definition of the problem.
    ```
    kthCharacter(k) = (kthCharacter(k') + 1)%26
    ``` 
    using some `k'` which we need to compute.
5. Now what is `k'`? Let's look back at our example: We had our two string `(a)bbc` and `abbc(b)ccd`.
The first half of `abbcbccd` is just the first string `abbc`. If we removed `abbc` the string would now be `bccd`.
Notice that how the index of the the `b` in `(b)ccd` is in the position of the `(a)bbc` from the first string. \
That is by definition the `k'` index that we are looking for.
6. In more general terms to generate `k'` from `k` we want to substract from `k` the length of the previously generated string. 
    ```
    k' = k - len(previous_string)
    ```
7. We know that the strings are always doubling in length `1 -> 2 -> 4 -> 8...`. So by definition
what we are looking for is the largest power of 2 which is strictly less than `k`
    ```
    k' = k - (2^i)       where 2^i < k
    ```
8. Now that we know how to compute `k'` we can use the recursive definition in `step (4)` to quickly
generate our solution.

