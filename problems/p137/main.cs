

public class Solution {
  public int SingleNumber(int[] nums) {

    uint answer = 0;
    // righ to left
    for (int bit_index = 0; bit_index < 32; bit_index++) {
      
      int bit_count = 0;
      foreach(var num in nums) {
        if (((uint) num & (1 << bit_index)) > 0) {
          bit_count += 1;
        }
      }
      bit_count %= 3;

      if (bit_count != 0) {
        answer |= (uint) 1 << bit_index;
      }
    }

    return (int) answer;
  }
}

public class MainClass {
  public static void Main(string[] args) {
    // testcase1();
    // testcase2();
    testcase3();
  }

  public static void testcase1() {
    Solution solution = new Solution();
    int result = solution.SingleNumber(new int[] { 2, 2, 3, 2});
    Console.WriteLine($"{result}"); // "c"
  }

  public static void testcase2() {
    Solution solution = new Solution();
    int result = solution.SingleNumber(new int[] { 0,1,0,1,0,1,99 });
    Console.WriteLine(result); // ""
  }

  public static void testcase3() {
    Solution solution = new Solution();
    int result = solution.SingleNumber(new int[]{-2,-2,1,1,4,1,4,4,-4,-2});
    Console.WriteLine(result); // ""
  }
};

