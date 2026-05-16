// 154 Find Minimum in Rotated Sorted Array II
// https://leetcode.com/problems/find-minimum-in-rotated-sorted-array-ii/description/
// Difficulty: Hard
// Time Taken: 01:12:21

// Your a failure harry

function findMin(nums: number[]): number {
  let n = nums.length;
  let left = 0;
  let right = n - 1;

  while (left < n-1 && nums[left] == nums[right]) {
    left += 1;
  }

  while (left < right) {
    let mid = left + Math.floor((right - left) / 2);
    if (nums[mid] > nums[n-1]) {
      left = mid + 1;
    } else {
      right = mid;
    }
  }
  return nums[left];
};

// Scaffolding
class TC {
  nums: number[];
  constructor(nums: number[]) {
    this.nums = nums;
  }
}
const testCases = [
  new TC([3,3,3,3,3,3,3,3,1,3]),
  new TC([3,3,1,3]),
  new TC([3,1,3,3]),
  new TC([3,1]),
  new TC([1, 3, 5]),
  new TC([3, 4, 5, 1, 2]),
  new TC([4, 5, 6, 7, 0, 1, 2]),
  new TC([11, 13, 15, 17]),
  new TC([1]),
  new TC([2, 2, 2, 0, 1]),
  new TC([2, 2, 2, 0, 1, 2, 2, 2, 2, 2]),
  new TC([2, 2, 2, 0, 1, 2, 2, 2, 2, 2, 2, 2]),
  new TC([5,1,3]),
]
for (let idx = 0; idx < testCases.length; idx++) {
  let result = findMin(testCases[idx].nums);
  console.log(result);
}