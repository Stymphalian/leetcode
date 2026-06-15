// 2095 Delete the Middle Node of a Linked List
// https://leetcode.com/problems/delete-the-middle-node-of-a-linked-list/description/
// Difficulty: Medium
// Time Taken: 00:29:22


public class ListNode {
  public int val;
  public ListNode next;
  public ListNode(int val = 0, ListNode next = null) {
    this.val = val;
    this.next = next;
  }
}
public class Solution {
  public ListNode DeleteMiddle(ListNode head) {
    int n  = 0;
    ListNode slow = head;
    ListNode fast = head;
    ListNode prev = null;
    ListNode prevPrev = null;
    while(fast != null) {
      fast = fast.next;
      n += 1;
      if (fast != null) {
        n += 1;
        fast = fast.next;
      }
      prevPrev = prev;
      prev = slow;
      slow = slow.next;
    }

    if (n == 1) {return null;}
    if (n == 2) {
      head.next = null;
      return head;
    }
    if (n % 2 == 0) {
      prev.next = slow.next;
    } else {
      prevPrev.next = slow;
    }
    return head;
  }
  
}

public class MainClass {

  public record TC(int[] a);

  public static void Main(string[] args) {
    Solution solution = new();

    List<TC> testcases = [
      new TC([1,3,4,7,1,2,6]),
      new TC([1,2,3,4]),
      new TC([2,1]),
    ];
    foreach (var tc in testcases) {
      var result = solution.DeleteMiddle(CreateListFromArray(tc.a)!);
      PrintListNode(result);
      // PrintArray(result);
      // Console.WriteLine(result);
    }
  }

  public static void PrintListNode(ListNode p) {
    ListNode current = p;
    while (current != null) {
      Console.Write($"{current.val} ");
      current = current.next;
    }
    Console.WriteLine();
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

