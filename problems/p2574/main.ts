// 2574 Left and Right Sum Differences
// https://leetcode.com/problems/left-and-right-sum-differences/description/
// Difficulty: Easy
// Time Taken: 00:06:26

function leftRightDifference(nums: number[]): number[] {
  let n = nums.length;
  let leftSum = new Array(n);
  let rightSum = new Array(n);
  for(let idx = 0; idx < n; idx++) {
    leftSum[idx] = ((idx -1 >= 0) ? leftSum[idx-1] : 0) + nums[idx];
  }
  for(let idx = n-1; idx >= 0; idx--) {
    rightSum[idx] = ((idx + 1 < n) ? rightSum[idx+1] : 0) + nums[idx];
  }

  let answer = [];
  for(let idx = 0; idx < n; idx++) {
    let left = leftSum[idx];
    let right = rightSum[idx];
    let cand = Math.abs(left - right);
    answer.push(cand);
  }
  return answer;
};

// Scaffolding
class TC {
  // start: string;
  // a: number[];
  // a: number;
  // b: number[]
  // nums2: number[];
  // start: number;
  // a: string[];
  // b: string[];
  // a: number[];
  // b: number[];
  // c: number[];
  // d: number[];
  a: number[];
  // b: number;
  // constructor(a:number[], b: number[], c: number[], d: number[]) {
  constructor(a: number[]) {
    this.a = a;
    // this.b = b;
    // this.a = a;
    // this.b = b;
    // this.c = c;
    // this.d = d;
  }
}
const testCases = [
  new TC([10, 4, 8, 3]),
  new TC([1]),
  // new TC(120, 130), // 3
  // new TC(198, 202), // 3
  // new TC(4848, 4848), // 1
  // new TC(5872, 5921), // 58
  // new TC(56203, 61133),
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = leftRightDifference(tc.a);
  console.log(result);
}