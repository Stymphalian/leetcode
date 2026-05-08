// 3629 Minimum Jumps to Reach End via Prime Teleportation
// https://leetcode.com/problems/minimum-jumps-to-reach-end-via-prime-teleportation/description/
// Difficulty: Medium
// Time Taken: 02:44:35

// too dumb

public class Solution {

  public int MinJumps(int[] nums) {
    int limit = nums.Max();
    int n = nums.Length;

    List<int>[] primeFactors = new List<int>[limit+1];
    for(int idx = 0; idx <= limit; idx++) {primeFactors[idx] = new();}
    for(int cand = 2; cand <= limit; cand++) {
      if (primeFactors[cand].Count == 0) {
        for (int next = cand; next <= limit; next += cand) {
          primeFactors[next].Add(cand);
        }
      }
    }

    Dictionary<int, List<int>> primeToIdx = [];
    for(int idx = 0; idx < n; idx++) {
      int num = nums[idx];
      foreach(var prime in primeFactors[num]) {
        if (!primeToIdx.ContainsKey(prime)) {
          primeToIdx[prime] = new();
        }
        primeToIdx[prime].Add(idx);
      }
    }

    // BFS
    int best = n-1;
    Queue<(int,int)> q = new();
    bool[] visited = new bool[n];
    Array.Fill(visited, false);
    q.Enqueue((0,0));
    while(q.Count > 0) {
      var (idx, depth) = q.Dequeue();
      // Console.WriteLine($"Current = {idx}, {depth}");
      // Found the last element, return
      if (idx == n-1) {
        return depth;
      }

      // Add all the neighbours
      // check left
      if (idx - 1 >= 0 && !visited[idx-1]) {
        q.Enqueue((idx-1, depth+1));
        visited[idx-1] = true;
      }
      
      // check right
      if (idx + 1 < n && !visited[idx+1]) {
        q.Enqueue((idx+1, depth+1));
        visited[idx+1] = true;
      }
      
      // check prime factors
      if (primeFactors[nums[idx]].Count == 1) {
        int prime = nums[idx];
        if (primeToIdx.ContainsKey(prime)) {
          foreach(var nextIdx in primeToIdx[prime]) {
            if (!visited[nextIdx]) {
              q.Enqueue((nextIdx, depth+1));
              visited[nextIdx] = true;
            }
          }
          primeToIdx[prime].Clear();  
        }
      }
    }

    return best;
  }
}


public class MainClass {

  public record TC(int[] a);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      // new TC([1,2,4,6]),  // 2
      // new TC([2,3,4,7,9]), // 2
      // new TC([4,6,5,8]), // 3
      // new TC([4,5,2]), // 2
      // new TC([7,5,7]), // 1
      // new TC([3,1,5,3]), // 1
      // new TC([893,786,607,137,69,381,790,233,15,42,7,764,890,269,84,262,870,514,514,650,269,485,760,181,489,107,585,428,862,563]), // 21
      new TC([4,3])
    ];
    foreach (var tc in testcases) {
      var result = solution.MinJumps(tc.a);
      // PrintArray(result);
      Console.WriteLine(result);
    }
  }

  public static T[][] P2D<T>(T[,] arr) {
    int rows = arr.GetLength(0);
    int cols = arr.GetLength(1);
    T[][] output = new T[rows][];
    for (int row = 0; row < rows; row++) {
      output[row] = new T[cols];
      for (int col = 0; col < cols; col++) {
        output[row][col] = arr[row, col];
      }
    }
    return output;
  }

  public static int[] Parse1D(string input) {
    var array = Array.ConvertAll(input.Trim('[', ']').Split(','), int.Parse);
    return array;
  }

  // [["#",".","*","."],["#","#","*","."]]
  public static char[][] ParseChar2D(string input) {
    input = input.Trim();
    var inner = input.Substring(1, input.Length - 2).Trim();
    if (inner == "") return new char[0][];
    var rows = inner.Split("],[");
    var array = new char[rows.Length][];
    for (int i = 0; i < rows.Length; i++) {
      var row = rows[i].Trim('[', ']').Trim();
      array[i] = row == "" ? new char[0] : row.Split(',').Select(s => s.Trim('"', ' ')[0]).ToArray();
    }
    return array;
  }

  public static int[][] Parse2D(string input) {
    input = input.Trim();
    var inner = input.Substring(1, input.Length - 2).Trim();
    if (inner == "") return new int[0][];
    var rows = inner.Split("],[");
    var array = new int[rows.Length][];
    for (int i = 0; i < rows.Length; i++) {
      var row = rows[i].Trim('[', ']').Trim();
      array[i] = row == "" ? new int[0] : Array.ConvertAll(row.Split(','), s => int.Parse(s.Trim()));
    }
    return array;
  }

  public static void PrintArray<T>(IEnumerable<T> array, string prefix = "") {
    Console.WriteLine($"{prefix}[{string.Join(", ", array)}]");
  }

  public static void PrintArray2D<T>(T[][] array) {
    Console.WriteLine("[");
    foreach (var row in array) {
      PrintArray(row, " ");
    }
    Console.WriteLine("]");
  }
};

