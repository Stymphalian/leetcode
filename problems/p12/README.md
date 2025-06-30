# Roman to Integer
https://leetcode.com/problems/integer-to-roman/description/ \
Time Taken: 01:39

The problem itself is not complicated, but I found that the rules that they 
outline are not particularly straight forward to translate into code.
I found it easier in my brain to use the closest power of 10 less than or equal
to the number and then find the corresponding roman numeral. 
From there you can find the left-most digit and then apply the rules.

1. Any digit `9` should be a closest `10` numeral followed by the next bigger `10` numeral.
2. Any digit `4` should be a closest `10` numeral followed by the closest `5` numeral
3. Any digit `<=3` can just be directly concatenated `digit` number of times with the closest `10` roman numeral.
4. Any digit `>=5` must have a leading `5` followed by the concatenating the remaining (digits - 5) with the `10` roman numerals


For the examples given:
```
I V
X L
C D
M

3749 == MMM DCC XV IX
3 - closest power of 10 is M. Apply rule 3. (MMM)
7 - closest power of 10 is C. Apply rule 4. (DCC)
4 - closest power of 10 is X. Apply rule 2. (XV)
9 - closest power of 10 is I. Apply rule 1. (IX)

58 == L VIII
5 - closest power of 10 is X. Apply rule 4. (L)
8 - closest power of 10 is I. Apply rule 4. (VIII)

1994 == M CM XC IV
1 - closest power of 10 is M. Apply rule 3. (M)
9 - closest power of 10 is C. Apply rule 1. (CM)
9 - closest power of 10 is X. Apply rule 1. (XC)
4 - closest power of 10 is I. Apply rule 2. (IV)
```

