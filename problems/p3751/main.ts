// 3751 Total Waviness of Numbers in Range I
// https://leetcode.com/problems/total-waviness-of-numbers-in-range-i/description/
// Difficulty: Medium
// Time Taken: 00:07:15


function isWavy(num: number) {
  let digits = num.toString();
  if (digits.length <= 2) {
    return 0;
  }

  let counts = 0;
  for(let idx = 1; idx < digits.length-1; idx++) {
    let prev = digits[idx-1];
    let current = digits[idx];
    let next = digits[idx+1];
    if (prev < current && current > next) {
      counts += 1;
    } else if (prev > current && current < next) {
      counts += 1;
    }
  }
  return counts;
}
function totalWaviness(num1: number, num2: number): number {
  let answer = 0;
  for(let cand = num1; cand <= num2; cand++) {
    const waviness = isWavy(cand);
    answer += waviness;
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
  a: number;
  b: number;
  // constructor(a:number[], b: number[], c: number[], d: number[]) {
  constructor(a:number, b: number) {
    this.a = a;
    this.b = b;
    // this.a = a;
    // this.b = b;
    // this.c = c;
    // this.d = d;
  }
}
const testCases = [
  new TC(120, 130),
  new TC(198, 202), 
  new TC(4848, 4848),
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = totalWaviness(tc.a, tc.b);
  console.log(result);
}
