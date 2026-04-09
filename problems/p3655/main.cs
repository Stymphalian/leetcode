// 3655 XOR After Range Multiplication Queries II
// https://leetcode.com/problems/xor-after-range-multiplication-queries-ii/description/
// Difficulty: Hard
// Time Taken: 02:30:00

// Didn't solve. 
// Learned:
// Fermat's little theorem can apply in surprising places
// big and little algorithm pattern
// relearned difference arrays

public class Solution {
  public const long MOD = 1000000000 + 7;


  public long powMod(long a, long exp, long mod) {
    // binary exponentiation algorithm 
    if (exp == 0) { return 1; }
    long b = powMod(a, exp / 2, mod);
    long b2 = b * b % mod;
    if (exp % 2 == 0) {
      return b2;
    } else {
      return (b2 * a) % mod;
    }
  }

  public long modInverse(long a, long mod) {
    // fermat's little theorem.
    // a is any number, MOD is prime, therefore a and MOD are co-prime
    return powMod(a, mod - 2, mod);
  }

  public void ProcessBigK(int[] nums, int[] query) {
    var (_, ri, ki, vi) = (query[0], query[1], query[2], query[3]);
    for (int li = query[0]; li <= ri; li += ki) {
      nums[li] = (int)((long)nums[li] * vi % MOD);
    }
  }
  public int XorAfterQueries(int[] nums, int[][] queries) {
    int n = nums.Length;
    int sqrt_n = (int)Math.Sqrt(n);

    // big k
    Dictionary<int, List<int[]>> small_ks = [];
    foreach (var query in queries) {
      var (li, ri, ki, vi) = (query[0], query[1], query[2], query[3]);
      if (ki >= sqrt_n) {
        ProcessBigK(nums, query);
      } else {
        if (!small_ks.ContainsKey(ki)) {
          small_ks[ki] = [];
        }
        small_ks[ki].Add(query);
      }
    }

    // small ks
    foreach (var (ki, ranges) in small_ks) {
      long[] diffs = new long[n + 2];
      Array.Fill(diffs, 1);

      foreach (var query in ranges) {
        var (li, ri, _, vi) = (query[0], query[1], query[2], query[3]);

        int start = li;
        int steps = ((ri - li) / ki) + 1;
        int end = li + steps * ki;
        diffs[start] = diffs[start] * vi % MOD;
        if (end < n) {
          diffs[end] = diffs[end] * modInverse(vi, MOD) % MOD;  
        }
      }

      // Product-wise prefix sum of the difference array to retrieve the 
      // correct `num` sequence with the range query multiplications applied.
      for (int index = 0; index < n; index++) {
        if (index >= ki) {
          diffs[index] = diffs[index] * diffs[index - ki] % MOD;
        }
        nums[index] = (int)(nums[index] * diffs[index] % MOD);
      }
    }


    int bitwise = 0;
    foreach (var num in nums) {
      bitwise ^= num;
    }
    return bitwise;
  }
}

public class MainClass {

  public record TestCase(int[] e, int[][] c);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TestCase> testcases = [
      // new TestCase([1,1,1], Parse2D("[[0,2,1,4]]")), // 4
      // new TestCase([2,3,1,5,4], Parse2D("[[1,4,2,3],[0,2,1,2]]")), // 31
      // new TestCase([38,18,415,150,371,456,563,184,308], Parse2D("[[7,8,8,20],[8,8,6,2],[2,3,3,1],[7,8,6,19],[0,4,6,3],[4,7,8,15],[7,7,2,16],[1,5,1,5],[6,7,4,6],[7,8,4,9],[2,8,2,13],[4,6,7,15],[7,7,7,11],[6,6,7,11],[8,8,7,10],[1,3,8,2],[0,0,3,19],[8,8,7,2],[2,3,2,5]]")),
      new TestCase([403,883,1000,866,986,355,614,383,660,450,426,943,242,579,58,101], Parse2D("[[11,12,6,10],[7,11,15,8],[13,14,16,20],[5,15,7,11],[7,9,3,17],[3,8,13,6],[0,9,12,20],[7,11,4,3],[7,10,12,17],[15,15,3,11],[12,13,16,6]]"))
    ];
    foreach (var tc in testcases) {
      var result = solution.XorAfterQueries(tc.e, tc.c);
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

