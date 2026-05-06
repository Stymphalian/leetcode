// 1861 Rotating the Box
// https://leetcode.com/problems/rotating-the-box/description/
// Difficulty: Medium
// Time Taken: 00:28:51


public class Solution {

  public char[][] Rotate(char[][] matrix) {
    int rows = matrix.Length;
    int cols = matrix[0].Length;

    char[][] newMatrix = new char[cols][];

    for(int col = 0; col < cols; col++) {
      newMatrix[col] = new char[rows];
      for(int row = 0; row < rows; row++) {
        newMatrix[col][rows-row-1] = matrix[row][col];
      }
    }
    return newMatrix;
  }



  public void ApplyGravity(char[][] boxGrid) {
    bool isObs(int row, int col) {
      return boxGrid[row][col] == '*';
    }
    bool isStone(int row, int col) {
      return boxGrid[row][col] == '#';
    }

    int rows = boxGrid.Length;
    int cols = boxGrid[0].Length;
    for(int col = 0; col < cols; col++) {

      int row = rows-1;
      int lastFloor = rows;
      while(row >= 0) {
        if (isObs(row, col)) {
          lastFloor = row;
        } else if (isStone(row, col)) {
          // shift stone down to the lastFloor, and make it the next floor
          boxGrid[row][col] = '.';
          boxGrid[lastFloor-1][col] = '#';
          lastFloor = lastFloor-1;
        } else  { // isSpace
          // pass
        }
        row--;
      }
    }
    
  }

  public char[][] RotateTheBox(char[][] boxGrid) {
    char[][] newBox = Rotate(boxGrid);
    ApplyGravity(newBox);
    return newBox;
  } 
}

public class MainClass {

  public record TC(char[][] a);

  public static void Main(string[] args) {
    Solution solution = new();
    
    List<TC> testcases = [
      new TC(ParseChar2D("""[["#",".","#"]]""")),
      new TC(ParseChar2D("""[["#",".","*","."],["#","#","*","."]]""")),
      new TC(ParseChar2D("""[["#","#","*",".","*","."],["#","#","#","*",".","."],["#","#","#",".","#","."]]""")),
    ];
    foreach (var tc in testcases) {
      var result = solution.RotateTheBox(tc.a);
      PrintArray2D(result);
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

