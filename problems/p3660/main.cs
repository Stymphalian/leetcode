// 3660 Jump Game IX
// https://leetcode.com/problems/jump-game-ix/description/
// Difficulty: Medium
// Time Taken: 03:09:48

// fuck me.

public class Solution {

  public class UnionFind {
    int[] parents;
    int[] ranks;
    public UnionFind(int n) {
      parents = new int[n];
      ranks = new int[n];
      for(int idx = 0; idx < n; idx++) {
        parents[idx] = idx;
      }
      Array.Fill(ranks, 0);
    }

    public int Find(int a) {
      int current = a;
      while(parents[current] != current) {
        current = parents[current];
      }
      parents[a] = current;
      return current;
    }

    public void Union(int a, int b) {
      int root_a = Find(a);
      int root_b = Find(b);
      if (root_a == root_b) {
        return;
      }

      int rank_a = ranks[root_a];
      int rank_b = ranks[root_b];
      if(rank_a < rank_b) {
        parents[root_a] = root_b;
        ranks[root_b] += 1;
      } else {
        parents[root_b] = root_a;
        ranks[root_a] += 1;
      }
    }
  }

  public int[] MaxValue(int[] nums) {
    int n = nums.Length;
    int[] answer = new int[n];

    int[] prefixMax = new int[n];
    int[] suffixMin = new int[n];
    for(int idx = 0; idx < n; idx++) {
      prefixMax[idx] = Math.Max(nums[idx], idx-1 >= 0 ? prefixMax[idx-1] : -1);
    }
    for(int idx = n-1; idx >= 0; idx--) {
      suffixMin[idx] = Math.Min(nums[idx], idx+1 < n ? suffixMin[idx+1] : int.MaxValue);
    }

    // when at the end we can always go the global max
    answer[n-1] = prefixMax[n-1]; 

    for(int idx = n-1; idx >= 0; idx--) {
      int left = prefixMax[idx];
      int right = idx+1 < n ? suffixMin[idx+1] : int.MaxValue;

      if (left > right) {
        // stay connected
        answer[idx] = answer[idx+1];
      } else {
        answer[idx] = prefixMax[idx];
      }
    }
    return answer;

    // UnionFind uf = new(n);
    
    // for(int current = 0; current < n; current++) {
    //   // Union all the right
    //   for(int right = current+1; right < n; right++) {
    //     if (nums[right] < nums[current]) {
    //       uf.Union(current, right);
    //     }
    //   }

    //   // Union all the left
    //   for(int left = 0; left < current; left++) {
    //     if (nums[left] > nums[current]) {
    //       uf.Union(current, left);
    //     }
    //   }
    // }

    // Dictionary<int,int> roots = [];
    // for(int idx = 0; idx < n; idx++) {
    //   int root = uf.Find(idx);
    //   if (root == idx) {
    //     roots[root] = 0;
    //   }
    // }
    // for(int idx = 0; idx < n; idx++) {
    //   int root = uf.Find(idx);
    //   roots[root] = Math.Max(nums[idx], roots[root]);
    // }

    // for(int idx = 0; idx < n; idx++) {
    //   int root = uf.Find(idx);
    //   int best = roots[root];
    //   answer[idx] = best;
    // }
  
    // return answer;
  }
}


public class MainClass {

  public record TC(int[] a);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC([2,1,3]),
      new TC([2,3,1]),
      new TC([5,6,3,7,1]),
      new TC([5,6,1,7,3]),
      new TC([30,21,5,35,24]),
    ];
    foreach (var tc in testcases) {
      var result = solution.MaxValue(tc.a);
      PrintArray(result);
      // Console.WriteLine(tc.a);
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

