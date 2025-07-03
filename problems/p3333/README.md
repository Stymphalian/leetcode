# Find the Original Typed String II
https://leetcode.com/problems/find-the-original-typed-string-ii/description \
**Time Taken**: 09:56:13 \
**Difficulty**: Hard \
**Time**: O(n*k) \
**Space**: O(n*k)

## Approach
https://www.youtube.com/watch?v=CymJVoz7XSY \
https://medium.com/@florian_algo/unveiling-the-bounded-knapsack-problem-737d71c4146b

I was not able to solve this by myself.
The eventual solution is a DP + prefix sum + counting type of problem.
It is infeasible to directly count the number of possible typed strings due to the 
time complexity being `O(2**n)`. You must notice a few crucial observations to solve the problem.

1. You can break up the `word` into different segments/groups and determine their size.
For example `AAABBC` has 3 groups with sizes `[3,2,1]`.
2. From this it is possible to calculate the **total** number of possible strings.
You can do this by multiplying together the size of each segment (i.e `3*2*1 = 6`)
3. If we could somehow determine the number of possible strings which have length `atmost k` 
we could then easily compute what we want (`atleast k`) by doing `total - (atmost k)`.
4. Again computing `atmost-k` directly is `O(2**n)` so we need to be smarter.
5. Figuring out how to compute `atmost-k` is the hardest part of this problem. One possible 
solution is to use `Dynamic Programming` to count the number of possible strings.
From my internet browsing I have seen that you can also think of the `atmost-k`
counting as a variation of the `unbounded knapsack` problem.
6. Using `DP` directly is also too slow. In the end you will find that it will be will around `O(n * k * k)`
7. By using a `prefix sum` we can reduce this to `O(n*k)`.

\
Now let's try to explain each of these parts:

### Explaining 2.
We notice that if a letter appears in the word it MUST be included in the final string.
This means that for each segment `AAABBC` the final string must atleast look 
something like this `A__ B_ C`. We can think of calculating the total number of possible
strings something like a permuation. We have 3 spaces `_ _ _` and in each space
we need to put in a subset of each segment. 
So we have 3 segments with different possible lengths:
```
A,AA,AAA
B,BB
C
```
So if we want to count the number of possible combinations it is simply just a 
straight multiplication of the sizes of each segment. (`3 x 2 x 1`)

### Explaining 5.
The DP approach to the counting is not at all straight-forward nor intuitive.
I will attempt to explain the logic but I can give no insight on how one would 
come up with this approach by themselves.
In the end the values in the last row of the DP gives us the counts for the `atmost-k` number of strings
(`SUM(DP[segment][str_len])`)

It is easiest to work through an example. Let's use the string `AAABBB`, `k=5`
Here is the DP table:
```
           string length
            0   1   2   3   4
          +---+---+---+---+---+
segment  0| 1 | 0 | 0 | 0 | 0 |
          +---+---+---+---+---+
         1| 0 |   |   |   |   |
          +---+---+---+---+---+
         2| 0 |   |   |   |   |
          +---+---+---+---+---+
```

The row is the possible segments that we can use to construct the string.
The column determines the length of the string we want to construct.
Given our example we have 2 segments `(AAA, BBB)`. The max length is `4` which 
is `k-1` which is the defintion of `atmost k` length which is what we want.

The initial state of the DP table is `1` at position 0,0.
This means that without using any letters from the segments how many ways to make the empty string.
Obviously there is only `1` way to do that. There are `0` ways to make any other length
string (obviously because we have no letters to use).
Also the number of ways to make an empty string while using atleast 1 segment is `0`.
Clearly if we *have* to use atleast 1 letter it is impossible to make an empty string.


Here is the recurrence of the dp table:
```
DP[segment_index][str_len] = (
    DP[segment_index-1][str_len-1] + 
    DP[segment_index-1][str_len-2] + 
    DP[segment_index-1][str_len-3] + 
    ...
    DP[segment_index-1][str_len-v]
)
where `v` is number of letters in the current segment (v == segments[segment_index])
```

Let me try to explain the intuition behind what is happening.
First lets start with a example without using the DP table.

```
Say we have our two segments (AAA, BBB).
If we keep track of the number of possible ways of making a string of length X
but only using letters from 'AAA'
We would have something like this:
0 | {}
1 | {A}
2 | {AA}
3 | {AAA}

Next we want to construct how many ways we can make strings of length X, but 
now we can include the letters from segment 'BBB'

If we were to do this constructively/intuitively we would try every possible 
variation of segment B against every set from the previous segment.
After that we bucket them into their appropriate string lengths.

0 | {}       -> {}
1 | {A}      -> {AB  , ABB  , ABBB         }
2 | {AA}     -> {AAB , AABB , AABBB        }
3 | {AAA}    -> {AAAB, AAABB, AAABBB       }

0 | {}
1 | {}                     --- Notice that this is empty.
2 | {AB}
3 | {ABB, AAB}
4 | {ABBB, AABB, AAAB}

Note how there are no strings of length 1 from the computation. By definition
the typed string must contain atleast 1 letter from each segment. Because we have
two segments it is impossible to make a typed string which includes 2 segments 
but is only of length 1.

Let's look carefully about what we did to construct the strings of length 3 {ABB, AAB}
    "AA" + "B"
    "A"  + "BB"
    ""   + "BBB" --- invalid because no "A"
    ==>
    {ABB, AAB}

We looked at each string from the previous segment and then "added" as many B's
as we could fit into the string but still have it less-than-equal to length 3.
We also needed always include atleast 1 letter from the previous segment.

This is the recurrence described above.
  DP["BBB"][3] = (                     DP[segment_index][str_len] = (
      DP["AAA][2] +         ==              DP[segment_index-1][str_len-1] +
      DP["AAA][1] +         ==              DP[segment_index-1][str_len-2] +
      DP["AAA][0]                           DP[segment_index-1][str_len-3] 
  )                                    )

As you can probably see the `str_len-x` is just ensuring we only add the 
appropriate number of letters in the current segment (B's) to keep the 
length of the string the same. 
The DP[segment_index-1] is the length from the previous segment
```


Now let's walk through a direct example by filling in numbers in the DP table.
```
Say we are at the start of the DP table:
We have segments (AAA, BBB):

           string length
            0   1   2   3   4
          +---+---+---+---+---+
segment  0| 1 | 0 | 0 | 0 | 0 |
          +---+---+---+---+---+
         1| 0 |   |   |   |   |
          +---+---+---+---+---+
         2| 0 |   |   |   |   |
          +---+---+---+---+---+


Lets add the the first segment (AAA) 
Note how at each step we are summing up a window of size 3 to compute the next DP value.

           string length
            0   1   2   3   4
          +---+---+---+---+---+
segment  0|_1_| 0 | 0 | 0 | 0 |    We add numbers at (0,0) to get the
          +---+---+---+---+---+    count at position (1,1)     
         1| 0 | 1 |   |   |   |
          +---+---+---+---+---+
         2| 0 |   |   |   |   |
          +---+---+---+---+---+

           string length
            0   1   2   3   4
          +---+---+---+---+---+
segment  0|_1_|_0_| 0 | 0 | 0 |    We add numbers at (0,0),(0,1) to get
          +---+---+---+---+---+    the count at position (1,2)
         1| 0 | 1 | 1 |   |   |
          +---+---+---+---+---+
         2| 0 |   |   |   |   |
          +---+---+---+---+---+

           string length
            0   1   2   3   4
          +---+---+---+---+---+
segment  0|_1_|_0_|_0_| 0 | 0 |    We add numbers at (0,0),(0,1),(0,2) to get
          +---+---+---+---+---+    the count at position (1,3)
         1| 0 | 1 | 1 | 1 | 0 |
          +---+---+---+---+---+    
         2| 0 |   |   |   |   |    
          +---+---+---+---+---+

           string length
            0   1   2   3   4
          +---+---+---+---+---+
segment  0| 1 |_0_|_0_|_0_| 0 |    We add numbers at (0,1),(0,2),(0,3)
          +---+---+---+---+---+    the count at position (1, 4)
         1| 0 | 1 | 1 | 1 | 0 |
          +---+---+---+---+---+    
         2| 0 |   |   |   |   |    
          +---+---+---+---+---+

Now lets add the next segment (BBB)

           string length
            0   1   2   3   4
          +---+---+---+---+---+
segment  0| 1 | 0 | 0 | 0 | 0 |    We add numbers at (1,0) to get
          +---+---+---+---+---+    the count at position (2,1)
         1|_0_| 1 | 1 | 1 | 0 |
          +---+---+---+---+---+    
         2| 0 | 0 |   |   |   |    
          +---+---+---+---+---+

           string length
            0   1   2   3   4
          +---+---+---+---+---+
segment  0| 1 | 0 | 0 | 0 | 0 |    We add numbers at (1,0),(1,1) to 
          +---+---+---+---+---+    the count at position (2,2)
         1|_0_|_1_| 1 | 1 | 0 |
          +---+---+---+---+---+    
         2| 0 | 0 | 1 |   |   |    
          +---+---+---+---+---+

           string length
            0   1   2   3   4
          +---+---+---+---+---+
segment  0| 1 | 0 | 0 | 0 | 0 |    We add numbers at (1,0),(1,1), (1,2) to get
          +---+---+---+---+---+    the count at position (2,3)
         1|_0_|_1_|_1_| 1 | 0 |
          +---+---+---+---+---+    
         2| 0 | 0 | 1 | 2 |   |    
          +---+---+---+---+---+

           string length
            0   1   2   3   4
          +---+---+---+---+---+
segment  0| 1 | 0 | 0 | 0 | 0 |    We add numbers at (1,1),(1,2),(1,3) to get
          +---+---+---+---+---+    the count at position (2,4)
         1| 0 |_1_|_1_|_1_| 0 |
          +---+---+---+---+---+    
         2| 0 | 0 | 1 | 2 | 3 |    
          +---+---+---+---+---+

The last row tells us the counts using all the segments (AAA, BBB)
making a string of length 0, 1, 2, 3 and 4.
To find the value we want (atmost-k) we sum up the last row to our final number (6).

```

### Explaining 6 and 7.

The unoptimized DP loop should look something like this in code:
```
vector<int> segments;  // [AAA,BBb] ==> [3,3]
int dp[segments.size()][k];
dp[0][0] = 1;

for (int seg_index = 0; seg_index < segments.size(); seg_index++) {
    for (int str_pos = 1; str_pos < k; str_pos++) {
        for (int seg_size = 1; seg_size <= segments[seg_index]; seg_size++) {
            if (str_pos - seg_size < 0) break;
            dp[seg_index] += dp[seg_index-1][str_pos - seg_size];
        }
    }
}

```

If you evaluate the Time complexity of that function you will see that it is `O(n * k * k)`
Where `n` is the length of the word and `k` is k from the problem statement.
This is essentially a cubic time complexity and will be too slow for the given input size `O(2000^3)`.

To speed this up we try to remove the inner-most loop `for(int seg_size...)`.
The observation that can be seen is that the inner-most loop is trying to compute 
a sum between two indices and adding that value to the next DP position. 
This is a perfect use-case for a `prefix-sum` array which changes the inner-loop
time complexity from `O(k)` to `O(1)`.

Making that change we can make the time complexity into `O(n*(k + k))` or effectively `O(n*k)`

```
vector<int> segments;  // [AAA,BBb] ==> [3,3]
int dp[segments.size()][k];
dp[0][0] = 1;

// O(n) operations to go through every segment.
for (int seg_index = 0; seg_index < segments.size(); seg_index++) {

    // O(k) operation to create the prefix_sum array
    vector<int> prefix_sum(k+1, 0);
    prefix_sum[0] = 0;
    for (int i = 1; i < k; i++) {
        prefix_sum[i] = (prefix_sum[i-1] + dp[seg_index-1][i-1]);
    }

    // O(k) to go through the k positions
    for (int str_pos = 1; str_pos < k; str_pos++) {        
        int seg_size = segments[seg_index];
        int left = str_pos - seg_size >= 0 ? prefix_sum[str_pos - seg_size] : 0;
        int right = prefix_sum[str_pos];
        dp[seg_index][str_pos] = (right - left);
    }
}

```

The last optimization that can be seen is that the during each iteration 
the next DP value only uses the immediately previous row `segment_index-1`. 
We can save lots of memory by only keeping a single row of the DP. During 
each iteration we can just swap the `DP` with the latest one we calculated.
The final code will be:

```
vector<int> dp(k, 0);
vector<int> prefix_sum(k+1, 0);
dp[0] = 1;

for(int seg_index = 0; seg_index < segments.size(); seg_index++) {
    vector<int> next_dp(k, 0);
    
    prefix_sum[0] = 0;
    for (int i = 1; i < k; i++) {
        prefix_sum[i] = (prefix_sum[i-1] + dp[i-1]) % MOD;
    }

    for(int str_pos = 1; str_pos < k; str_pos++) {
        int v = segments[seg_index];
        int left = str_pos - v >= 0 ? prefix_sum[str_pos - v] : 0;
        int right = prefix_sum[str_pos];

        next_dp[str_pos] = (right - left) % MOD;
    }

    dp = next_dp;
}
```