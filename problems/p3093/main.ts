// 3093 Longest Common Suffix Queries
// https://leetcode.com/problems/longest-common-suffix-queries/description/
// Difficulty: Hard
// Time Taken: 01:11:18


class Trie {
  children: Map<string, Trie>;
  is_end: boolean;
  end_index: number;
  best_index: number
  constructor() {
    this.children = new Map();
    this.is_end = false;
    this.end_index = -1;
    this.best_index = 0;
  }

  Add(word: string, word_index: number) {
    let current: Trie = this;
    for(let idx = word.length-1; idx >= 0; idx--) {
      const c = word[idx];
      if (!current.children.has(c)) {
        current.children.set(c, new Trie());
      }
      current = current.children.get(c)!;
    }
    if (current.is_end) {
      current.end_index = Math.min(current.end_index, word_index);
    } else {
      current.is_end = true;
      current.end_index = word_index;
    }
  }

  Find(query:string): Trie {
    let current: Trie = this;
    for(let idx = query.length-1; idx >= 0; idx--) {
      const c = query[idx];
      if (!current.children.has(c)) {break;}
      current = current.children.get(c)!;
    }
    return current;
  }
}

function DFS(current: Trie, depth: number) {
  // else we must look at our children and find the nearest complete word
  // amongst our children. Take the lowest index as our value
  let best_depth = Number.MAX_SAFE_INTEGER;
  let best = Number.MAX_SAFE_INTEGER;
  for(const [key, child] of current.children) {
    const [cand, cand_depth] = DFS(child, depth+1);
    if (cand_depth <= best_depth) {
      if (cand_depth == best_depth) {
        best = Math.min(cand, best);
      } else {
        best = cand;
        best_depth = cand_depth;
      }
    }
  }

  if (current.is_end) {
    // If the current node is already an end-point 
    // the best string at this location must be this node.
    best = current.end_index;
    best_depth = depth;
  }
  current.best_index = best;
  return [best, best_depth];
}

function stringIndices(wordsContainer: string[], wordsQuery: string[]): number[] {
  // Create trie
  let tr = new Trie();
  wordsContainer.forEach((x, xi) => tr.Add(x, xi));
  DFS(tr, 0);

  // process each words Query
  let answer = [];
  for(let idx = 0; idx < wordsQuery.length; idx++) {
    // Look up in trie to find the longest common suffix, and then BFS
    // down from that node to find the shortest word/earliest word
    const cand = tr.Find(wordsQuery[idx]);
    answer.push(cand.best_index);
  }

  return answer;
};

// Scaffolding
class TC {
  // start: string;
  // nums: number[];
  // nums2: number[];
  // start: number;
  a: string[];
  b: string[];
  constructor(a: string[], b: string[]) {
    // this.nums = nums;
    // this.nums2 = nums2;
    // this.start = start;
    this.a = a;
    this.b = b;
  }
}
const testCases = [
  new TC(["abcd","bcd","xbcd"], ["cd","bcd","xyz"]),
  new TC(["abcdefgh","poiuygh","ghghgh"], ["gh","acbfgh","acbfegh"]),
  new TC(["abcd","bcda"], ["cdf","afa"]),
]
for (let idx = 0; idx < testCases.length; idx++) {
  let tc = testCases[idx];
  let result = stringIndices(tc.a, tc.b);
  console.log(result);
}