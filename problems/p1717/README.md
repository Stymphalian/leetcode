# Maximum Score From Removing Substrings
https://leetcode.com/problems/maximum-score-from-removing-substrings/description \
**Time Taken**: 01:50:33 \
**Difficulty**: Medium \
**Time**: O(n) \
**Space**: O(n)

## Commentary
Used hints. \
I don't understand the intution that we should always process the higher points `x` or `y` pattern first.
I had originally used a DP approach but ran into `TLE` and `memory exceeded` errors.
Even using the `stack` approach was still quite tricky to implement.

The proof of the greedy approach of always using the higher pairing of "ab" or "ba" first can be proven by contradiction.
1) Say `x > y`.
2) We are given some string `s` where say removing a "ba" instead of an "ab" to gives the optimal score.
3) In the simplest case of just a 3-letter string like "aba" it is obviously a contradiction that removing "ba" instead of "ab" would given the optimal score.
4) The more interesting case is when we make a trade-off of taking the "ba" instead of taking two "ab".
4) As a concrete example say we have the string "abab".
5) We could either have `ab ab` or `ba ab`.
6) This would give us scores `x + x` and `y + x`.
7) Our assumption is that taking the "ba" would lead to an optimal solution which implies `y + x > x + x`. This can be reduced to `y > x`
9) Clearly this contradicts our original assumption of `x > y` (1)


\
The editorial has a unique countng only solution which doesn't use any extra space.
It keeps track of the `a_counts` and `b_counts`. When we encounter the higher pairing 
we increment our score. When we reach a non "a" or "b" it acts as a barrier
and we increment as many lower pairings as possible and then reset our counters.
