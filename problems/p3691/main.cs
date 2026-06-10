// 3691 Maximum Total Subarray Value II
// https://leetcode.com/problems/maximum-total-subarray-value-ii/description/
// Difficulty: Hard
// Time Taken: HH:MM:SS

// range query + maxheap
// Need to make observation that l -> r range, as r increases the
// value can only monotonically increase. f.

public abstract class SegmentTree<T> {
  public T[] DP;
  public int N;

  public SegmentTree(int[] nums) {
    N = nums.Length;
    DP = new T[2 * N - 1];
    Build(nums, 0, 0, nums.Length - 1);
  }

  public abstract T Combine(T leftValue, T rightValue);
  public abstract T Construct(int value);
  public virtual T DefaultValue() { return default; }

  public int LeftIndex(int index, int left, int right) {
    return index + 1;
  }
  public int RightIndex(int index, int left, int right) {
    // Using pre-order traversal indexing of the vertices
    // int mid = left + (right - left) / 2;
    // int leftSize = mid - left + 1;
    // int leftRoom = 2 * leftSize - 1;
    // return index + 1 + leftRoom;
    return index + 2 * ((right - left) / 2 + 1);
  }

  public void Build(int[] nums, int index, int left, int right) {
    if (left == right) {
      DP[index] = Construct(nums[left]);
      return;
    }

    int mid = left + (right - left) / 2;
    int leftIndex = LeftIndex(index, left, right);
    int rightIndex = RightIndex(index, left, right);
    Build(nums, leftIndex, left, mid);
    Build(nums, rightIndex, mid + 1, right);
    DP[index] = Combine(DP[leftIndex], DP[rightIndex]);
  }

  public T InternalSearch(int index, int rangeLeft, int rangeRight, int targetLeft, int targetRight) {
    if (targetLeft > targetRight) {
      return DefaultValue();
    }
    if (targetLeft == rangeLeft && targetRight == rangeRight) {
      return DP[index];
    }

    int mid = rangeLeft + (rangeRight - rangeLeft) / 2;
    int leftIndex = LeftIndex(index, rangeLeft, rangeRight);
    int rightIndex = RightIndex(index, rangeLeft, rangeRight);
    T leftValue = InternalSearch(leftIndex, rangeLeft, mid, targetLeft, Math.Min(mid, targetRight));
    T rightValue = InternalSearch(rightIndex, mid + 1, rangeRight, Math.Max(mid + 1, targetLeft), targetRight);

    return Combine(leftValue, rightValue);
  }

  public T Search(int left, int right) {
    return InternalSearch(0, 0, N - 1, left, right);
  }

  public void InternalUpdate(int index, int tl, int tr, int pos, int new_val) {
    if (tl == tr) {
      DP[index] = Construct(new_val);
    } else {
      int mid = tl + (tr - tl) / 2;
      int leftIndex = LeftIndex(index, tl, tr);
      int rightIndex = RightIndex(index, tl, tr);
      if (pos <= mid) {
        InternalUpdate(leftIndex, tl, mid, pos, new_val);
      } else {
        InternalUpdate(rightIndex, mid + 1, tr, pos, new_val);
      }

      DP[index] = Combine(DP[leftIndex], DP[rightIndex]);
    }
  }

  public void Update(int pos, int value) {
    InternalUpdate(0, 0, N - 1, pos, value);
  }
};

public class SegmentTreeMax : SegmentTree<int> {
  public SegmentTreeMax(int[] nums) : base(nums) { }
  public override int Combine(int leftValue, int rightValue) {
    return Math.Max(leftValue, rightValue);
  }
  public override int DefaultValue() { return int.MinValue; }
  public override int Construct(int value) { return value; }
}

public class SegmentTreeMin : SegmentTree<int> {
  public SegmentTreeMin(int[] nums) : base(nums) { }
  public override int Combine(int leftValue, int rightValue) {
    return Math.Min(leftValue, rightValue);
  }
  public override int DefaultValue() { return int.MaxValue; }
  public override int Construct(int value) { return value; }
}

public class Solution {
  public long MaxTotalValue(int[] nums, int k) {
    SegmentTreeMin minTree = new(nums);
    SegmentTreeMax maxTree = new(nums);

    long Value(int left, int right) {
      int min = minTree.Search(left, right);
      int max = maxTree.Search(left, right);
      return Math.Abs(max - min);
    }

    PriorityQueue<(int,int), long> pq = new();
    for(int left = 0; left < nums.Length; left++) {
      long val = Value(left, nums.Length-1);
      pq.Enqueue((left, nums.Length-1), -val);
    }

    long answer = 0;
    for(int idx = 0; idx < k; idx++) {
      if (pq.TryPeek(out (int, int) elem, out long value)) {
        var (left, right) = elem;
        answer -= value;  
        if (right > left) {
          long val = Value(left, right-1);
          pq.Enqueue((left, right-1), -val);
        }
        pq.Dequeue();
      }
    }
    return answer;
  }
}


public class MainClass {

  public record TC(int[] a, int b);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC([1,3,2], 2),
      new TC([4,2,5,1], 3),
    ];
    foreach (var tc in testcases) {
      var result = solution.MaxTotalValue(tc.a, tc.b);
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

