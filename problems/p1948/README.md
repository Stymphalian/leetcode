# Delete Duplicate Folders in System
https://leetcode.com/problems/delete-duplicate-folders-in-system/description \
**Time Taken**: 01:21:38 \
**Difficulty**: Hard \
**Time**: O(n·n) \
**Space**: O(n)


## Commentary
Turns out the brute force approach to this work and completes in time.

The definition of the problem basically asks you to determine for any two folders/nodes
whether their sub-folder structure is exactly the same. 
The brute force approach to this compares every folder against eachother and then
checks to see if their sub-folder structures match. If they do then mark the
two folders.

To make it easier to compare sub-folder structures we encode the list of folders
into a `Trie` tree and compare each `node` against every other `node`.


The editorial solution takes a unique approach of encoding the sub-folder structure
as some `signature`. In one pass they generate the `signature` for every node in the Trie.
In a second pass through all the nodes if the signature already exists then 
we know that "somewhere" in the file system that there exists a duplicate so we
quickly mark the node for deletion.
