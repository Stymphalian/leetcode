// 3043 Find the Length of the Longest Common Prefix
// https://leetcode.com/problems/find-the-length-of-the-longest-common-prefix/description/
// Difficulty: Medium
// Time Taken: 00:24:36

// Alternative is to create a hash map of all the prefixes of one of the array
// Iterate through all the numbers of the other array and try to find the 
// longest prefix by looking it up in the hashmap
// You can create the hashmap by iteratively cutting off the rightmost digit 
// from a number.

class Trie {
  children: Map<string, Trie>;
  is_end: boolean;

  constructor() {
    this.children = new Map();
    this.is_end = false;
  }

  Add(target: string) {
    let current: Trie = this;
    for(let idx = 0; idx < target.length; idx++) {
      let char = target[idx];
      if(!current.children.has(char)) {
        current.children.set(char, new Trie());
      }
      current = current.children.get(char)!;
    }
    current.is_end = true;
  }
}

function longestCommonPrefix(arr1: number[], arr2: number[]): number {
  let getPrefix = (a: string, t: Trie): number => {
    let current = t;
    let count = 0;
    for(let idx = 0; idx < a.length; idx++) {
      let char = a[idx];
      if (current.children.has(char)) {
        count += 1;
        current = current.children.get(char)!;
      } else {
        break;
      }
    }
    return count;
  }

  let arr1_str = arr1.map(x => x.toString());
  let arr2_str = arr2.map(x => x.toString());
  let longer = (arr1.length > arr2.length) ? arr1_str : arr2_str;
  let shorter = (arr1.length > arr2.length) ? arr2_str : arr1_str;
  let trie = new Trie();
  longer.forEach(x => trie.Add(x));

  let longest = 0;
  shorter.forEach(x => {
    let cand = getPrefix(x, trie);
    longest = Math.max(cand, longest);
  });

  return longest;
};

// Scaffolding
class TC {
  nums: number[];
  nums2: number[];
  // start: number;
  constructor(nums: number[], nums2: number[]) {
    this.nums = nums;
    this.nums2 = nums2;
    // this.start = start;
  }
}
const testCases = [
  new TC([1,10,100], [1000]), // 3
  new TC([1,2,3], [4,4,4]), // 0
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = longestCommonPrefix(tc.nums, tc.nums2);
  console.log(result);
}