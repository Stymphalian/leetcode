// 2144 Minimum Cost of Buying Candies With Discount
// https://leetcode.com/problems/minimum-cost-of-buying-candies-with-discount/description/
// Difficulty: Easy
// Time Taken: 00:06:31

function minimumCost(cost: number[]): number {
  cost.sort((a,b) => (a-b));
  let idx = cost.length -1;
  let candy_count = 0;
  let total = 0;
  while (idx >= 0) {
    total += cost[idx];
    candy_count += 1;
    if (candy_count % 2 == 0) {
      idx--;
    }
    idx--;
  }
  return total;
};

// Scaffolding
class TC {
  // start: string;
  // a: number[];
  // a: number;
  b: number[]
  // nums2: number[];
  // start: number;
  // a: string[];
  // b: string[];
  constructor(b: number[]) {
    // this.a = a;
    this.b = b;
  }
}
const testCases = [
  new TC([1,2,3]),
  new TC([6,5,7,9,2,2]),
  new TC([5,5])
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = minimumCost(tc.b);
  console.log(result);
}