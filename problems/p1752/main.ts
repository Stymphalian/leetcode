// 1752 Check if Array Is Sorted and Rotated
// https://leetcode.com/problems/check-if-array-is-sorted-and-rotated/description/
// Difficulty: Easy
// Time Taken: 00:24:43

function check(nums: number[]): boolean {
  let n = nums.length;
  let breaks = 1;
  for(let c = 0; c < n; c++) {
    let current = c % n;
    let next = (c + 1) % n;
    if (nums[current] > nums[next]) {
      breaks -= 1;
    }
    if (breaks < 0) {
      return false;
    }
  }
  return true;    
};


// Scaffolding
class TC {
  nums: number[];
  // nums2: number[];
  // start: number;
  constructor(nums: number[]) {
    this.nums = nums;
    // this.nums2 = nums2;
    // this.start = start;
  }
}
const testCases = [
  new TC([3,4,5,1,2]),
  new TC([2,1,3,4]),
  new TC([1,2,3]),
  new TC([1,3,2]),
  new TC([2,1]),
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = check(tc.nums);
  console.log(result);
}