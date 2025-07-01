#  Design Add and Search Words Data Structure
https://leetcode.com/problems/design-add-and-search-words-data-structure/description/ \
**Time Taken**: 01:31:01 \
**Difficulty**: Medium \
**Complexity**:
```
Time: 
AddWord: O(m)
Search: O(m * k^d)
                   
Space:
O(m * k)           

where `m` the average word length.
where `k` is the length of the alphabet (26 in this case)
and `d` is the number of wildcards.
```

## Approach
Any word search type of problem usually involves something around hashing/maps
and or Tries. In this case, this is a slight variation of a classic Trie problem. 
This problem uses a normal Trie to hold the words. To handle the wild-card characters
during the `Search()` you run a simple DFS over the node's children.

A Trie is tree data structure used for quckly searching for strings. Each
node in the tree holds a set of children which are the 'letters' which appear 
after itself. The `level/height` of the node determines its position in the string.

For example:
```
Say the Trie holds the words "bad" and "bays"
The tree would look something like this:

        _root_
          |
          |
          b
          |
          a
         /  \
        /    \
       d [x]  y
              |
              |
              s [x]

The [x] are markers on the Node to tell us that a string which terminates
with that letter exists in the Trie. 
To search for a word you start by traversing from the root and follow the 
children nodes as it matches your target string.

For example say we want to look for "bad"
1. We would start at the _root_ looking for a 'b' in its children. We find it
2. We are at the node 'b' we want to look for 'a' in its children. We find it.
3. We are at the node 'a' we want to look for 'd' in its children. We find it.
4. We are at the node 'd'. We are at the end of our input string and this node is
   marked as containing the string so we return true.

For example say we want to look for "bay"
1. We would start at the _root_ looking for a 'b' in its children. We find it
2. We are at the node 'b' we want to look for 'a' in its children. We find it.
3. We are at the node 'a' we want to look for 'y' in its children. we find it.
4. We are at the node 'y'. We are at the end of our input string but this node
   is not marked as containing the string so we know it does not exist in the trie
   and return false.

For example say we want to looky for "baby"
1. We would start at the _root_ looking for a 'b' in its children. We find it
2. We are at the node 'b' we want to look for 'a' in its children. We find it.
3. We are at the node 'a' we want to look for 'b' in its children. We do not find it.
4. Because we do not find a child with the letter 'b' we know it does not exist
   in the trie and return False.
```


