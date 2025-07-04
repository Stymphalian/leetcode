# Find the K-th Character in String Game II
https://leetcode.com/problems/find-the-k-th-character-in-string-game-ii/description
**Time Taken**: 00:41:54 \
**Difficulty**: Hard \
**Time**: O(log(k)) \
**Space**: O(log(k))

## Approach
It is best to understand the optimal approach to the version `I` of this problem [p3304](https://leetcode.com/problems/find-the-k-th-character-in-string-game-i/description).
The approach is essentially the same. We frame the problem as a recursive function
but now we need to handle the two different operations.
```
kthCharacter(k, operations) = kthCharacter(k', operations[:-1])
```

1. `k'` is generated in the same-kind of way as in p3304. We substract the length
of the previous string from the current index. The only difference is that `k` 
could already be `less-than-equal` to the previous length and in this case we
don't need to subtract as we are already in the range.
2. One thing to note either operation `0` or `1` will always double the length
of the string. It is just that `1` will also increment the letters.
3. Now we just handle the operations. If the operation is `1` we need to increment the character
otherwise just return the returned character. We also need some additional.
    ```
    c = kthCharacter(k', operations[:-1])
    if operations[-1] == 0:
      return c
    else:
      if k < prev_length:
        return c
      else:
        return (c+1)%26
    ```

