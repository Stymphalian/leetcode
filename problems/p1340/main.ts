// 1340 Jump Game V
// https://leetcode.com/problems/jump-game-v/description/
// Difficulty: Hard
// Time Taken: 00:30:00

function goodGradient(arr: number[], start: number, target: number): boolean {
    if (target >= start) {
        for(let idx = start+1; idx <= target; idx++) {
            if (arr[idx] >= arr[start]) { return false;}
        }
    } else {
        for(let idx = start-1; idx >= target; idx--) {
            if (arr[idx] >= arr[start]) { return false;}
        }
    }
    return true;
}

function maxJ(arr: number[], d: number, start: number, memo: Map<number, number>): number {
    if (memo.has(start)) {
        return memo.get(start)!;
    }
    let max = 0;
    for(let offset = -d; offset <= d; offset++) {
        let target = start + offset;
        if (target == start) {continue;}
        if (target < 0 || target >= arr.length) {continue;}
        if (arr[target] >= arr[start]) {continue;}
        if (!goodGradient(arr, start, target)) {continue;}
        
        let cand = maxJ(arr, d, target, memo);
        max = Math.max(cand, max);
    }
    memo.set(start, max+1);
    return max + 1;
}

function maxJumps(arr: number[], d: number): number {
    let memo = new Map<number, number>();
    let max = 0;
    for(let idx = 0; idx < arr.length; idx++) {
        let cand = maxJ(arr, d, idx, memo)
        max = Math.max(cand, max);
    }
    return max;
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
  new TC([6,4,14,6,8,13,9,7,10,6,12], 2),
  new TC([3,3,3,3,3], 3), 
  new TC([7,6,5,4,3,2,1], 1),
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = maxJumps(tc.nums, tc.start);
  console.log(result);
}