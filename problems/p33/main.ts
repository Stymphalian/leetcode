// 33 Search in Rotated Sorted Array
// https://leetcode.com/problems/search-in-rotated-sorted-array/description/
// Difficulty: Medium
// Time Taken: 00:42:16

function binary_search(nums: number[], start: number, end: number, target: number) {
  let left = start;
  let right = end;
  while (left < right) {
    let mid = left + Math.floor((right - left) / 2);
    if (nums[mid] == target) {
      return mid;
    } else if (target < nums[mid]) {
      right = mid;
    } else {
      left = mid + 1;
    }
  }

  if (left >= 0 && left <= right && nums[left] == target) {
    return left;
  }
  return -1;
}

function search(nums: number[], target: number): number {
  let left = 0;
  let right = nums.length -1;
  let end = nums.length - 1;

  while (left < right) {
    let mid = left + Math.floor((right - left) / 2);
    if (nums[mid] < nums[end]) {
      right = mid;
    } else if (nums[mid] > nums[end]) {
      left = mid + 1;
    }
  }
  let pivot = left;

  let left_search = binary_search(nums, 0, pivot, target);
  let right_search = binary_search(nums, pivot, nums.length-1, target);
  if (left_search >= 0) {
    return left_search;
  }
  if (right_search >= 0) {
    return right_search;
  }
  return -1;
};

// Scaffolding
class TC {
  nums: number[];
  // nums2: number[];
  start: number;
  constructor(nums: number[], start: number) {
    this.nums = nums;
    // this.nums2 = nums2;
    this.start = start;
  }
}
const testCases = [
  new TC([4,5,6,7,0,1,2], 0),
  new TC([4,5,6,7,0,1,2], 3),
  new TC([1], 0),
  new TC([1], 1),
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = search(tc.nums, tc.start);
  console.log(result);
}