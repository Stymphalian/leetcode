// 3300 Minimum Element After Replacement With Digit Sum
// https://leetcode.com/problems/minimum-element-after-replacement-with-digit-sum/description/
// Difficulty: Easy
// Time Taken: 00:06:06

function minElement(nums: number[]): number {
  let best = Number.MAX_SAFE_INTEGER;
  for(let idx = 0; idx < nums.length; idx++) {

    let cand = 0;
    let current = nums[idx];
    while (current > 0) {
      let digit = current % 10;
      cand += digit;
      current = Math.floor(current / 10);
    }
    if (current > 0) {
      cand += current;
    }

    best = Math.min(best, cand);
  }

  return best;
};

// Scaffolding
class TC {
  // start: string;
  a: number[];
  // nums2: number[];
  // start: number;
  // a: string[];
  // b: string[];
  constructor(a: number[]) {
    this.a = a;
  }
}
const testCases = [
  new TC([10,12,13,14]),
  new TC([1,2,3,4]),
  new TC([999, 19, 199]),
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = minElement(tc.a);
  console.log(result);
}