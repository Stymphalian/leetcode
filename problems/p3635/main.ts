// 3635 Earliest Finish Time for Land and Water Rides II
// https://leetcode.com/problems/earliest-finish-time-for-land-and-water-rides-ii/description/
// Difficulty: Medium
// Time Taken: 00:44:16

function earliestFinishTime(landStartTime: number[], landDuration: number[], waterStartTime: number[], waterDuration: number[]): number {
  // try land first, then water
  let n = landStartTime.length;
  let m = waterStartTime.length;

  let minLandEnd = Number.MAX_SAFE_INTEGER;
  for(let idx = 0; idx < n; idx++) {
    minLandEnd = Math.min(minLandEnd, landStartTime[idx] + landDuration[idx]);
  }
  let landFirstBest = Number.MAX_SAFE_INTEGER;
  for(let idx = 0; idx < m; idx++) {
    let cand = 0;
    if (waterStartTime[idx] <= minLandEnd) {
      cand = minLandEnd + waterDuration[idx];
    } else {
      cand = waterStartTime[idx] + waterDuration[idx];
    }
    landFirstBest = Math.min(landFirstBest, cand);
  }

  // try water first, then land
  let minWaterEnd = Number.MAX_SAFE_INTEGER;
  for(let idx = 0; idx < m; idx++) {
    minWaterEnd = Math.min(minWaterEnd, waterStartTime[idx] + waterDuration[idx]);
  }
  let waterFirstBest = Number.MAX_SAFE_INTEGER;
  for(let idx = 0; idx < n; idx++) {
    let cand = 0;
    if (landStartTime[idx] <= minWaterEnd) {
      cand = minWaterEnd + landDuration[idx];
    } else {
      cand = landStartTime[idx] + landDuration[idx];
    }
    waterFirstBest = Math.min(waterFirstBest, cand);
  }

  return Math.min(landFirstBest, waterFirstBest);
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
  a: number[];
  b: number[];
  c: number[];
  d: number[];
  constructor(a:number[], b: number[], c: number[], d: number[]) {
    // this.a = a;
    // this.b = b;
    this.a = a;
    this.b = b;
    this.c = c;
    this.d = d;
  }
}
const testCases = [
  new TC([2,8], [4,1], [6], [3]), // 9
  new TC([5], [3], [1], [10]), // 14
  new TC([99], [59], [99,54], [85,20]),  // 158
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = earliestFinishTime(tc.a, tc.b, tc.c, tc.d);
  console.log(result);
}