// 2126 Destroying Asteroids
// https://leetcode.com/problems/destroying-asteroids/description/
// Difficulty: Medium
// Time Taken: 00:07:42



function asteroidsDestroyed(mass: number, asteroids: number[]): boolean {
  asteroids.sort((a,b) => (a-b));
  for (let idx = 0; idx < asteroids.length; idx++) {
    if (asteroids[idx] <= mass) {
      mass += asteroids[idx];
    } else {
      return false;
    }
  }
  return true;
};


// Scaffolding
class TC {
  // start: string;
  // a: number[];
  a: number;
  b: number[]
  // nums2: number[];
  // start: number;
  // a: string[];
  // b: string[];
  constructor(a: number, b: number[]) {
    this.a = a;
    this.b = b;
  }
}
const testCases = [
  new TC(10, [3, 9, 19, 5, 21]),
  new TC(5, [4, 9, 23, 4]),
  new TC(86, [156,197,192,14,97,160,14,5]),
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = asteroidsDestroyed(tc.a, tc.b);
  console.log(result);
}