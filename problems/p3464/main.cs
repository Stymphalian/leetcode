// 3464 Maximize the Distance Between Points on a Square
// https://leetcode.com/problems/maximize-the-distance-between-points-on-a-square/description/
// Difficulty: Hard
// Time Taken: 03:06:12

// fuck


public class Solution {
  public int MaxDistance(int side, int[][] points, int k) {
    List<int[]> bot = [];
    List<int[]> left = [];
    List<int[]> right = [];
    List<int[]> top = [];
    foreach (var point in points) {
      if (point[1] == 0) {
        bot.Add(point);
      } else if (point[1] == side) {
        top.Add(point);
      } else if (point[0] == 0) {
        left.Add(point);
      } else if (point[0] == side) {
        right.Add(point);
      }
    }
    bot = bot.OrderBy(a => a[0]).ToList();  // sort left to right
    right = right.OrderBy(a => a[1]).ToList(); // Sort Vertically (bottom to top)
    top = top.OrderBy(a => a[0]).Reverse().ToList(); // sort right to left
    left = left.OrderBy(a => a[1]).Reverse().ToList(); // Sort vertically (top to bot)
    List<long> orderedPoints = [];
    foreach (var point in bot) { orderedPoints.Add(point[0]); }
    foreach (var point in right) { orderedPoints.Add((long)side + point[1]); }
    foreach (var point in top) { orderedPoints.Add(2L * side + (side - point[0])); }
    foreach (var point in left) { orderedPoints.Add(3L * side + (side - point[1])); }

    // Binary search for the answer;
    long lower = 0;
    long upper = side;
    long answer = 0;
    while (lower <= upper) {
      long mid = lower + (upper - lower) / 2;
      bool got = Check(orderedPoints, k, side, mid);
      if (got) {
        lower = mid + 1;
        answer = mid;
      } else {
        upper = mid - 1;
      }
    }
    return (int)answer;
  }

  public bool Check(List<long> points, int k, int side, long dist) {
    // long perimeter = side * 4L;

    // foreach (long start in points) {
    //   long end = start + perimeter - dist;
    //   long cur = start;

    //   for (int i = 0; i < k - 1; i++) {
    //     int idx = LowerBinarySearch(points, cur + dist);
    //     if (idx == points.Count || points[idx] > end) {
    //       cur = -1;
    //       break;
    //     }
    //     cur = points[idx];
    //   }

    //   if (cur >= 0) {
    //     return true;
    //   }
    // }
    // return false;

    // iterate for every starting position
    for (int start = 0; start < points.Count; start++) {
      long end = points[start] + 4L*side - dist;

      // try to find k points which are >= dist apart
      int kCount = 1;
      long current = points[start];
      while (kCount < k) {
        // Find the next nearest element whose dist >= dist
        int nextIndex = LowerBinarySearch(points, current + dist);
        if (nextIndex >= points.Count || points[nextIndex] > end) {
          // Can't find a next point
          break;
        }
        current = points[nextIndex];
        kCount += 1;
      }

      if (kCount >= k) {
        return true;
      }
    }
    return false;
  }

  private int LowerBinarySearch(List<long> arr, long target) {
    int left = 0, right = arr.Count;
    while (left < right) {
      int mid = left + (right - left) / 2;
      if (arr[mid] < target) {
        left = mid + 1;
      } else {
        right = mid;
      }
    }
    return left;
  }
}

public class MainClass {

  public record TC(int a, int[][] b, int c);

  public static void Main(string[] args) {
    Solution solution = new();
    List<TC> testcases = [
      // new TC(2, Parse2D("[[0,2],[2,0],[2,2],[0,0]]"), 4), // 2
      // new TC(2, Parse2D("[[0,0],[1,2],[2,0],[2,2],[2,1]]"), 4), // 1
      // new TC(2, Parse2D("[[0,0],[0,1],[0,2],[1,2],[2,0],[2,2],[2,1]]"), 5), // 1
      // new TC(4, Parse2D("[[4,4],[3,4],[2,0],[4,3],[4,0]]"), 4), // 2
      // new TC(6, Parse2D("[[2,0],[5,0],[0,0],[2,6]]"), 4), // 2
      // new TC(15, Parse2D("[[15,6],[0,13],[13,0],[10,0]]"), 4), // 3
      new TC(5, Parse2D("[[1,5],[5,0],[5,5],[0,2],[5,1]]"), 4) // 3
    ];
    foreach (var tc in testcases) {
      var result = solution.MaxDistance(tc.a, tc.b, tc.c);
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

