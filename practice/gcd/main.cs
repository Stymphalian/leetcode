public class Solution {
  public int gcd(int a, int b) {
    if (b > a) {
      (a, b) = (b, a);
    }
    while (b != 0) {
      (a, b) = (b, a % b);
    }
    return a;
  }

  public (int, int, int) egcd(int a, int b) {
    if (b > a) {
      (a, b) = (b, a);
    }
    int x = 1;
    int y = 0;
    int x1 = 0;
    int y1 = 1;
    while (b != 0) {
      int q = a / b;
      (a, b) = (b, a % b);

      (x, x1) = (x1, x - q * x1);
      (y, y1) = (y1, y - q * y1);
    }
    return (a, x, y);
  }


  public void test() {
    Console.WriteLine(egcd(1914, 899)); // 29, 8, -17
    // var rng = new Random();
    // int test_count = 10000;
    // while (test_count >= 0) {
    //   int a = rng.Next() % 100;
    //   int b = rng.Next() % 100;

    //   var true_result = gcd_raw(a, b);
    //   var my_result = gcd(a, b);
    //   if (true_result != my_result) {
    //     Console.WriteLine($"{a}, {b} => {true_result}, {my_result}");
    //     break;
    //   }
    //   test_count -= 1;
    // }
  }
}

public class MainClass {

  public record TestCase(int[] e, int[][] c);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TestCase> testcases = [
      // new TestCase([1,1,1], Parse2D("[[0,2,1,4]]")),
      // new TestCase([2,3,1,5,4], Parse2D("[[1,4,2,3],[0,2,1,2]]")),
      new TestCase([780], Parse2D("[[0,0,1,13],[0,0,1,17],[0,0,1,9],[0,0,1,18],[0,0,1,16],[0,0,1,6],[0,0,1,4],[0,0,1,11],[0,0,1,7],[0,0,1,18],[0,0,1,8],[0,0,1,15],[0,0,1,12]]")),
    ];
    foreach (var tc in testcases) {
      solution.test();
      // Console.WriteLine(result);
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

