// 3120 Count the Number of Special Characters I
// https://leetcode.com/problems/count-the-number-of-special-characters-i/description/
// Difficulty: Easy
// Time Taken: 00:10:28

function numberOfSpecialChars(word: string): number {
  let lowers = new Array(26).fill(0);
  let uppers = new Array(26).fill(0);
  let a = 'a'.charCodeAt(0);
  let z = 'z'.charCodeAt(0);
  let A = 'A'.charCodeAt(0);

  for(let idx = 0; idx < word.length; idx++) {
    const c = word[idx].charCodeAt(0);
    if (a <= c && c <= z) {
      const ci = c - a;
      lowers[ci] += 1;
    } else {
      const ci = c - A;
      uppers[ci] += 1;
    }
  }

  let count = 0;
  for(let idx = 0; idx < lowers.length; idx++) {
    if (lowers[idx] > 0 && uppers[idx] > 0) {
      count += 1;
    }
  }
  return count;
};

// Scaffolding
class TC {
  start: string;
  // nums: number[];
  // nums2: number[];
  // start: number;
  constructor(start: string) {
    // this.nums = nums;
    // this.nums2 = nums2;
    this.start = start;
  }
}
const testCases = [
  new TC("aaAbcBC"),
  new TC("abc"),
  new TC("abBCab"),
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = numberOfSpecialChars(tc.start);
  console.log(result);
}