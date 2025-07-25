# Minimum Score After Removals on a Tree
https://leetcode.com/problems/minimum-score-after-removals-on-a-tree/description \
**Time Taken**: 04:49:01 \
**Difficulty**: Hard \
**Time**: O(n•n) \
**Space**: O(n)


# Commentary
Failed. \
My original solution was also like a double DFS but it kept timing out. 
I believe it was close to a `O(n^2)` solution but I messed up in iterating the second sub-tree which made it `O(n^3)`

Things I learned: \
The editorial has a solution which does a DFS post-order traversal numbering of
the nodes. From here you can do pair-wise edge matching and determine the A,B, and C 
scores by determining which sub-trees are children of another. Very interesting.

I had a very convoluted way of calculating the score between two nodes, 
but the editorial shows a much more elegant way of calculating `C = totalScore XOR A XOR B`.