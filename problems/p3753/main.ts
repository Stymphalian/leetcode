// 3753 Total Waviness of Numbers in Range II
// https://leetcode.com/problems/total-waviness-of-numbers-in-range-ii/description/
// Difficulty: Hard
// Time Taken: HH:MM:SS

// Fail

function isPoint(a: string, b: string, c: string): boolean {
  let [ai, bi, ci] = [parseInt(a), parseInt(b), parseInt(c)];
  return (ai < bi && bi > ci) || (ai > bi && bi < ci);
}

function waviness2(digits: string, memo: Map<string, number>): number {
  let n = digits.length;
  if (n <= 2) { return 0; }
  if (memo.has(digits)) { return memo.get(digits)!; }
  if (n == 3) {
    return isPoint(digits[0], digits[1], digits[2]) ? 1 : 0;
  }

  let newNum = digits.substring(1, n-1);
  let count = waviness2(newNum, memo);
  count += isPoint(digits[0], digits[1], digits[2]) ? 1 : 0;
  count += isPoint(digits[n - 3], digits[n - 2], digits[n - 1]) ? 1 : 0;
  memo.set(digits, count);
  return count
}

function totalWaviness2(num1: number, num2: number): number {
  let memo = new Map<string, number>();
  let answer = 0;
  for (let cand = num1; cand <= num2; cand++) {
    const w = waviness2(cand.toString(), memo);
    answer += w;
  }
  return answer;
};

function keyify(t: [boolean, string]) {
  return t.join(",");
}


function DP(dp: Map<string, number>) {
  let count = 0;
  for(let idx = 0; idx <= 999; idx++) {
    let s = idx.toString();
    if (s.length >= 3 && isPoint(s[0], s[1], s[2])) {
      count += 1;
    }
    dp.set(keyify([false, s]), count);
  }

  count = 0;
  for(let idx = 0; idx <= 999; idx++) {
    let s = idx.toString();
    if (isPoint(s[0], s[1], s[2])) {
      count += 1;
    }
    dp.set(keyify([true, s]), count);
  }
}

function nine(len: number) {
  return "".padStart(len, "9");
}


function waviness(digits: string, has_prefix: boolean,  memo: Map<string, number>): number {
  let n = digits.length;
  let key = keyify([has_prefix, digits]);
  if (memo.has(key)) { return memo.get(key)!; }
  // if n == 3; memo should already have the answer

  let lead = parseInt(digits[0]);
  let count = 0;
  for(let idx = 0; idx < lead; idx++) {
    if (idx == 0) {
      let right = ``.padEnd(n-1, '9');
      count += waviness(right, false, memo);
    } else {
      let left = idx-1 > 0 ? `${idx-1}`.padEnd(n, '9') : ``.padEnd(n-1, '9');
      let right = `${idx}`.padEnd(n, '9');
      let left_count = waviness(left, false, memo);
      let right_count = waviness(right, false, memo);
      count += right_count - left_count;
    }
  }

  let start = parseInt(`${lead}`.padEnd(3, '0'));
  let end = parseInt(`${lead}` + digits.substring(1, 3));
  let prefix_count = 0;
  for(let idx = start; idx <= end; idx++) {
    let s = idx.toString();
    if (isPoint(s[0], s[1], s[2])) {
      prefix_count += 1;
    }
  }

  // let prefix = `${lead}` + digits.substring(1, 3);
  // let prefix_count = memo.get([false, prefix])!;
  let suffix = digits.substring(1);
  let suffix_count = waviness(suffix, true, memo);
  count += (prefix_count + suffix_count);

  memo.set(key, count);
  return count;
}

function totalWaviness(num1: number, num2: number): number {
  let memo = new Map<string, number>();
  DP(memo);
  waviness((num1-1).toString(), false, memo);
  waviness(num2.toString(), false, memo);

  // console.log(totalWaviness2(1000, 1999));
  let n1 = memo.get(keyify([false, (num1-1).toString()]))!;
  let n2 = memo.get(keyify([false, num2.toString()]))!;
  return n2 - n1;
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
  constructor(a: number, b: number) {
    this.a = a;
    this.b = b;
    // this.a = a;
    // this.b = b;
    // this.c = c;
    // this.d = d;
  }
}
const testCases = [
  // new TC(120, 130), // 3
  // new TC(198, 202), // 3
  // new TC(4848, 4848), // 1
  new TC(5872, 5921), // 58
  // new TC(56203, 61133),
]
// totalWaviness2(0, 1);
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  totalWaviness2(0, 1);
  let result = totalWaviness(tc.a, tc.b);
  console.log(result);
}