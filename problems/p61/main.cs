// 61 Rotate List
// https://leetcode.com/problems/rotate-list/description/
// Difficulty: Medium
// Time Taken: 00:33:26

// Trickier than I thought it was going to be.

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
  public ListNode RotateRight(ListNode head, int k) {
    if (head == null) { return null;}

    ListNode? current = head;
    ListNode? tail = null;
    int n = 0;
    while (current != null) {
      n += 1;
      if (current.next == null) {
        tail = current;
      }
      current = current.next;
    }
    k = k % n;
    if (k == 0) {return head;}
    k = n - k - 1;

    tail.next = head;
    ListNode B = head;
    while (k > 0) {
      B = B.next;
      k -= 1;
    }
    ListNode C = B.next;
    B.next = null;
    return C;
  }
}


public class MainClass {

  public record TC(ListNode a);

  public static void Main(string[] args) {
    Solution solution = new();
    ListNode g = new(7);
    ListNode f = new(6, g);
    ListNode e = new(5, f);
    ListNode d = new(4, e);
    ListNode c = new(3, d);
    ListNode b = new(2, c);
    ListNode a = new(1, b);

    List<TC> testcases = [
      new TC(a),
      // new TC(Parse2D("[[5,1,9,11],[2,4,8,10],[13,3,6,7],[15,14,12,16]]")),
    ];
    foreach (var tc in testcases) {
      var result = solution.RotateRight(tc.a, 9);
      PrintLL(result);
      // Console.WriteLine(result.val);
      // PrintArray2D(tc.a);
      // Console.WriteLine(tc.a);
    }
  }

  public static void PrintLL(ListNode a) {
    HashSet<int> seen = [];
    ListNode current = a;
    while(current != null) {
      Console.Write($"{current.val} ");
      Debug.Assert(!seen.Contains(current.val));
      seen.Add(current.val);
      current = current.next;
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

