# Reschedule Meetings for Maximum Free Time II
https://leetcode.com/problems/reschedule-meetings-for-maximum-free-time-ii/description \
**Time Taken**:  02:18:36\
**Difficulty**: Medium \
**Time**: O(n) \
**Space**: O(n)

## Approach
A good problem.

My initial solution was a `O(n · log(n))` solution which used a binary-search. \
The editorial shows a way to do `O(n)` by doing a two-pass algorithm. \
First let's make an observation on HOW we are supposed to find the optimal solution.

1. For each meeting there is always a `leftGap` and a `rightGap` (the gap could be of size `0`)
1. For each meeting there are two ways of maximizing the gap size:

    1. Case 1: We shift the meeting into the leftGap or rightGap which gives us a `gap` of size `leftGap + rightGap`
    1. Case 2: We are able to find a slot/gap somewhere to the left or right where we can entirely
       move the meeting into. This makes a free gap of size `leftGap + rightGap + meeting_length`

1. The biggest gap can be found my taking the `maximum` after iterating through all the meetings.
1. The crutch of the algorithm is how to efficiently find `case(2)`. How can we find an existing `gap` which can fit the current `meeting_length`.
1. My initial solution was to make a list of all the gaps `availableGaps`, sort them, and then as I iterate through each meeting use a `bisect_left` to find if a candidate gap can fit the meeting.
1. The editorial uses a two-pass algorithm.
    
    1. Do two passes through the meetings. Once going `left-to-right` and another going `right-to-left`.
    1. As you iterate `left-to-right` keep track of the biggest `freeLeftGap` you have seen.
    1. Keeping track of this `freeLeftGap` variable allows us to efficiently determine if the current meeting fits into `case(2)`
    1. Do the same for `right-to-left` but keep track of the biggest `freeRightGap`
    1. Keep track of the `biggestGap` you have seen through both iterations and return that as your answer.
