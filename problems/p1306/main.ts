// 1306 Jump Game III
// https://leetcode.com/problems/jump-game-iii/description/
// Difficulty: Medium
// Time Taken: 00:41:40

function canReach(arr: number[], start: number): boolean {
  let n = arr.length;
  let visited = new Set<number>();
  let stack: number[] = [];
  stack.push(start);
  let good = false;

  while(stack.length > 0) {
    let current = stack[stack.length-1];
    stack.pop();
    if (visited.has(current)) {
      continue;
    }
    visited.add(current);
    let jump = arr[current];
    if (jump == 0) {good = true;}
  
    let pos = current + jump;
    let neg = current - jump;
    if (pos < n) {
      stack.push(pos);
    }
    if (neg >= 0) {
      stack.push(neg);
    }
  }

  return good;
};


// Scaffolding
class TC {
  nums: number[];
  start: number;
  constructor(nums: number[], start: number) {
    this.nums = nums;
    this.start = start;
  }
}
const testCases = [
  new TC([4, 2, 3, 0, 3, 1, 2], 5),
  new TC([4, 2, 3, 0, 3, 1, 2], 0),
  new TC([3, 0, 2, 1, 2], 2),
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = canReach(tc.nums, tc.start);
  console.log(result);
}