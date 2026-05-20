// 2657 Find the Prefix Common Array of Two Arrays
// https://leetcode.com/problems/find-the-prefix-common-array-of-two-arrays/description/
// Difficulty: Medium
// Time Taken: 00:17:12

function findThePrefixCommonArray(A: number[], B: number[]): number[] {
  let n = A.length;
  let answer = [];
  let count = 0;
  let seenA = new Array(n).fill(0);
  let seenB = new Array(n).fill(0);
  for(let idx = 0; idx < n; idx++){
    let a = A[idx];
    let b = B[idx];

    seenA[a] = 1;
    seenB[b] = 1;
    if (seenA[b] == 1) {
      count += 1;
    }
    if (a != b && seenB[a] == 1) {
      count += 1;
    }
    answer.push(count);
  }
  return answer;
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
  new TC([1,3,2,4], [3,1,2,4]),
  new TC([2,3,1], [3,1,2]),
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = findThePrefixCommonArray(tc.nums, tc.nums2);
  console.log(result);
}