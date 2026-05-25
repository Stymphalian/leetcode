// 1871 Jump Game VII
// https://leetcode.com/problems/jump-game-vii/description/
// Difficulty: Medium
// Time Taken: 00:57:43

// Nope


function canReach(s: string, minJump: number, maxJump: number): boolean {
    let n = s.length;
    if (n <= 1) {return true;}
    if (s[n-1]!= '0') { return false;}

    let dp = new Array(n).fill(0);
    let pre = new Array(n+1).fill(0);

    dp[0] = 1;
    for(let idx = 0; idx < minJump; idx++) {
        pre[idx] = 1;
    }

    for(let idx = minJump; idx <= n-1; idx++) {
        let left = idx - maxJump;
        let right = idx - minJump;
        if (s[idx] == '0') {
            const leftPrefix = left-1 >= 0 ? pre[left-1] : 0;
            const number = pre[right] - leftPrefix;
            dp[idx] = number > 0 ? 1 : 0;
        }
        pre[idx] = pre[idx-1] + dp[idx];
    }

    return dp[n-1] == 1;
}


// Scaffolding
class TC {
  // nums: number[];
  // nums2: number[];
  // start: number;
  s: string;
  min: number;
  max: number;
  constructor(s:string, min: number, max:number) {
    this.s = s;
    this.min = min;
    this.max = max;
  }
}
const testCases = [
  new TC("011010", 2, 3), // true
  new TC("01101110", 2, 3), // false
  new TC("011111000111000001011111010", 6, 8), // true
  new TC("00111010", 3, 5), // false
  new TC("00", 1, 1), // true
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = canReach(tc.s, tc.min, tc.max);
  console.log(result);
}