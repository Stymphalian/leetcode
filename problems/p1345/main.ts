// 1345 Jump Game IV
// https://leetcode.com/problems/jump-game-iv/description/
// Difficulty: Hard
// Time Taken: 00:36:01

function minJumps(arr: number[]): number {
  let n= arr.length;
  let connected = new Map<number, number[]>();
  for(let idx = 0; idx < arr.length; idx++) {
    const current = arr[idx];
    if (!connected.has(current)) {
      connected.set(current, []);
    }
    connected.get(current)?.push(idx);
  }
  connected.forEach((val: number[], key: number)=>{
    val.sort((a,b)=> (b-a));
  });

  let visited = new Set();
  let queue: [number, number][] = [];
  queue.push([0, 0]);
  while(queue.length > 0) {
    let [current, depth] = queue[0];
    queue.shift();
    visited.add(current);
    if (current == n-1) {
      return depth;
    }

    if (!visited.has(current + 1) && current + 1 < n) {
      visited.add(current+1);
      queue.push([current+1, depth+1]);
    }
    if (!visited.has(current - 1) && current -1 >= 0) {
      visited.add(current-1);
      queue.push([current-1, depth+1]);
    }
    if (connected.has(arr[current])) {
      let others: number[] = connected.get(arr[current])!;
      for(let idx = 0; idx < others.length; idx++) {
        let other = others[idx];
        if (other == current) {continue;}
        if (visited.has(other)) {continue;}
        visited.add(other);
        queue.push([other, depth+1]);
      }
      connected.delete(arr[current]);
    }
  }

  return n-1;
};


// Scaffolding
class TC {
  nums: number[];
  // start: number;
  constructor(nums: number[]) {
    this.nums = nums;
    // this.start = start;
  }
}
const testCases = [
  new TC([100,-23,-23,404,100,23,23,23,3,404]),
  new TC([7]),
  new TC([7,6,9,6,9,6,9,7]),
  new TC([7,7,7,7,7,7,7,7,7,7,7,7,7,11])
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = minJumps(tc.nums);
  console.log(result);
}