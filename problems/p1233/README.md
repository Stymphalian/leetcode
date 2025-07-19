# Remove Sub-Folders from the Filesystem
https://leetcode.com/problems/remove-sub-folders-from-the-filesystem/description \
**Time Taken**: 00:37:38 \
**Difficulty**: Medium \
**Time**: O(n) \
**Space**: O(n)

## Commentary
Tried implementing a brute force approach to begin with which is `O(n^2)`.
For every `sub_folder` check if there exists another `sub_folder` which startsWith
the same prefix. You can't use `string.StartsWith` and have to implement your own 
custom `StartWithByParts` method which compares based off of the chunks after splitting on `/`.
Anyways in the end this gets `Time Limit Exceeded` anyways, but is a pratcial exercise.

The approach I used in the end is a slight variation of a `Trie` data structure.
Instead of `char` for each child we use the `parts` after splitting the folder.
For example: `"/a/b/cd" would be [a, b, cd]`.
In the end the Trie should contain every folder. Now we can do a simple DFS to find the first Node
which is marked as `isWord` and add them to an `answer` list.
To save space you can probably get away with stopping early in the `Insert` if you 
encounter a Node which is already marked `isWord`.





