// 1722 Minimize Hamming Distance After Swap Operations
// https://leetcode.com/problems/minimize-hamming-distance-after-swap-operations/description/
// Difficulty: Medium
// Time Taken: 00:30:35

using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public class Solution {

  public class UnionSet {

    int[] parents = [];
    int[] ranks = [];
    public UnionSet(int size) {
      parents = new int[size];
      ranks = new int[size];
      Array.Fill(ranks, 1);
      for(int index = 0; index < size; index++) {
        parents[index] = index;
      }
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
      // merge smaller set into larger set
      if (rank_a <= rank_b) {
        parents[root_a] = root_b;
        ranks[root_b] += rank_a;
      } else {
        parents[root_b] = root_a;
        ranks[root_a] += rank_b;
      }
    }


    public Dictionary<int, List<int>> GetGroups() {
      Dictionary<int, List<int>> groups = [];
      for(int index = 0; index < parents.Length; index++) {
        int root = Find(index);
        if (!groups.ContainsKey(root)) {
          groups[root] = [];
        }
        groups[root].Add(index);
      }

      foreach(var (root, group) in groups) {
        group.Sort();
      }
      return groups;
    }
  }

  public int MinimumHammingDistance(int[] source, int[] target, int[][] allowedSwaps) {
    // Create a union set of from the allowedSwaps
    // For each distinc set, calculate the hamming distance of the set
    // we do not need to care about order as we can swap freely between elements
    // make a sorted array and count distinct from there.

    UnionSet us = new UnionSet(source.Length);
    foreach(var swap in allowedSwaps) {
      us.Union(swap[0], swap[1]);
    }

    // Get groups from the union set
    var groups = us.GetGroups();


    int hamming = 0;
    foreach(var (root, group) in groups) {

      // Get the source and target values from each group
      Dictionary<int, int> srcCounts = [];
      foreach(var index in group) {
        int s = source[index];
        if (!srcCounts.ContainsKey(s)) {
          srcCounts[s] = 0;
        }
        srcCounts[s] += 1;
      }

      // calculate the diffs between the groups
      int diffs = 0;
      foreach(var index in group) {
        int t = target[index];
        if (srcCounts.ContainsKey(t)) {
          if (srcCounts[t] == 0) {
            diffs += 1;
          } else {
            srcCounts[t] -= 1;
          }
        } else {
          diffs += 1;
        }
      }
      hamming += diffs;
    }

    return hamming;
  }
}


public class MainClass {

  public record TC(int[] n, int[] t, int[][] a);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TC> testcases = [
      new TC([1,2,3,4], [2,1,4,5], Parse2D("[[0,1],[2,3]]")),
      new TC([1,2,3,4], [1,3,2,4], Parse2D("[]")),
      new TC([5,1,2,4,3], [1,5,4,2,3], Parse2D("[[0,4],[4,2],[1,3],[1,4]]")),
    ];
    foreach (var tc in testcases) {
      var result = solution.MinimumHammingDistance(tc.n, tc.t, tc.a);
      // PrintArray(result);
      Console.WriteLine(result);
    }
  }

  public static int[] Parse1D(string input) {
    var array = Array.ConvertAll(input.Trim('[', ']').Split(','), int.Parse);
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

