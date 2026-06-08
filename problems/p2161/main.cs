// 2161 Partition Array According to Given Pivot
// https://leetcode.com/problems/partition-array-according-to-given-pivot/description/
// Difficulty: Medium
// Time Taken: 00:17:40

public class Solution {
  public int[] PivotArray(int[] nums, int pivot) {
    List<int> leftStack = new List<int>();
    int equalCount = 0;
    List<int> rightStack = new List<int>();
    foreach(var num in nums) {
      if(num < pivot) {
        leftStack.Add(num);
      } else if ( num == pivot) {
        equalCount += 1;
      } else {
        rightStack.Add(num);
      }
    }


    List<int> answer = [];
    foreach(var n in leftStack) {
      answer.Add(n);
    }
    for(int idx = 0; idx < equalCount; idx++) {
      answer.Add(pivot);      
    }
    foreach(var n in rightStack) {
      answer.Add(n);
    }
    return answer.ToArray();
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

