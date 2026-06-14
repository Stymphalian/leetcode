// 2130 Maximum Twin Sum of a Linked List
// https://leetcode.com/problems/maximum-twin-sum-of-a-linked-list/description/
// Difficulty: Medium
// Time Taken: 00:20:20

/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class ListNode {
  public int val;
  public ListNode next;
  public ListNode(int val = 0, ListNode next = null) {
    this.val = val;
    this.next = next;
  }
}
public class Solution {
  public int PairSum(ListNode head) {
    int n = 0;
    // Find the number of elements
    ListNode current = head;
    while (current != null) {
      current = current.next;
      n += 1;
    }

    // Find the middle
    int m = 0;
    ListNode prev = null;
    current = head;
    while (m != n / 2) {
      ListNode next = current.next;
      current.next = prev;
      prev = current;
      current = next;
      m += 1;
    }

    // Walk backwards
    ListNode left = prev;
    ListNode right = current;
    prev = right;
    int best = 0;
    while (left != null && right != null) {
      int cand = left.val + right.val;
      best = Math.Max(cand, best);

      // Advance both pointers, but also fix left's pointers
      ListNode leftNext = left.next;
      left.next = prev;
      prev = left;
      left = leftNext;

      right = right.next;
    }

    return best;
  }
}


public class MainClass {

  public record TC(int[] a);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC([5,4,2,1]),
      new TC([4,2,2,3]),
      new TC([1,100000]),
    ];
    foreach (var tc in testcases) {
      var result = solution.PairSum(CreateListFromArray(tc.a)!);
      // PrintArray(result);
      Console.WriteLine(result);
    }
  }


  public static ListNode? CreateListFromArray(int[] vals) {
    ListNode? head = null;
    ListNode? current = null;
    foreach (var val in vals) {
      if (head == null) {
        head = new ListNode(val);
        current = head;
      } else {
        current.next = new ListNode(val);
        current = current.next;
      }
    }
    return head;
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

