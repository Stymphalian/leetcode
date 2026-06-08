// 2196 Create Binary Tree From Descriptions
// https://leetcode.com/problems/create-binary-tree-from-descriptions/description
// Difficulty: Medium
// Time Taken: 00::30:00

public class TreeNode {
  public int val;
  public TreeNode left;
  public TreeNode right;
  public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null) {
    this.val = val;
    this.left = left;
    this.right = right;
  }
}
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution {
  public TreeNode CreateBinaryTree(int[][] descriptions) {
    Dictionary<int, TreeNode> refs = [];

    HashSet<int> possibleRoots = [];
    HashSet<int> seenChildren = [];
    foreach(var desc in descriptions) {
      var (parent, child, isLeft) = (desc[0], desc[1], desc[2]);
      if (!refs.ContainsKey(parent)) {
        refs[parent] = new TreeNode(parent, null, null);
        if (!seenChildren.Contains(parent)) {
          possibleRoots.Add(parent);  
        }
      }
      if (!refs.ContainsKey(child)) {
        refs[child] = new TreeNode(child, null, null);
      }
      var parentNode = refs[parent];
      var childNode = refs[child];
      if (isLeft == 1) {
        parentNode.left = childNode;
      } else {
        parentNode.right = childNode;
      }

      seenChildren.Add(child);
      if (possibleRoots.Contains(child)) {
        possibleRoots.Remove(child);
      }
    }

    if (possibleRoots.Count != 1) {
      return null;
    } else {
      var rootNum = possibleRoots.First();
      return refs[rootNum];  
    }
  }
}

public class MainClass {

  public record TC(int[] a, int b);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC([9,12,5,10,14,3,10], 10),
      new TC([-3,4,3,2], 2),
    ];
    foreach (var tc in testcases) {
      var result = solution.PivotArray(tc.a, tc.b);
      PrintArray(result);
      // Console.WriteLine(result);
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

