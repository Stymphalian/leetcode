// 2540 Minimum Common Value
// https://leetcode.com/problems/minimum-common-value/description/
// Difficulty: Easy
// Time Taken: 00:04:52

function getCommon(nums1: number[], nums2: number[]): number {
  let n1 = nums1.length;
  let n2 = nums2.length;
  let p1 = 0;
  let p2 = 0;
  while(p1 < n1 && p2 < n2) {
    let cand1 = nums1[p1];
    let cand2 = nums2[p2];
    if (cand1 < cand2) {
      p1++;
    } else if (cand2 < cand1) {
      p2++;
    } else {
      return cand1;
    }
  }
  return -1;
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
  new TC([1, 2, 3], [2, 4]),
  new TC([1, 2, 3, 6], [2, 3, 4, 5]),
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = getCommon(tc.nums, tc.nums2);
  console.log(result);
}