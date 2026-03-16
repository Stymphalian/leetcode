// 1878 Get Biggest Three Rhombus Sums in a Grid
// https://leetcode.com/problems/get-biggest-three-rhombus-sums-in-a-grid/description/
// Difficulty: Medium
// Time Taken: 00:53:04

public class Solution {

  public int GetRhombusSize(int x, int y, int size, int[][] grid) {
    if (size == 0) {
      return grid[y][x];
    }

    int bottom = y + 2*size;
    int sum = grid[y][x] + grid[bottom][x];
    int count = 2;

    // Move top to left
    int index = 1;
    while (index <= size) {
      sum += grid[y + index][x - index];
      index++;
      count += 1;

    }
    // Move top to right
    index = 1;
    while (index <= size) {
      sum += grid[y + index][x + index];
      index++;
      count += 1;
    }
    // move bottom to right
    index = 1;
    while (index <= size - 1) {
      sum += grid[bottom - index][x + index];
      index++;
      count += 1;
    }
    // move bottom to left
    index = 1;
    while (index <= size - 1) {
      sum += grid[bottom - index][x - index];
      index++;
      count += 1;
    }

    // Debug.Assert(count == (size+1)*4 - 4);
    // Console.WriteLine(sum);
    return sum;
  }

  public int[] GetBiggestThree(int[][] grid) {
    int height = grid.Length;
    int width = grid[0].Length;
    int maxSize = Math.Max(height, width);

    // Brute force
    HashSet<int> sizes = new HashSet<int>();
    for (int size = maxSize; size >= 0; size--) {
      for (int y = 0; y < height; y++) {
        for (int x = 0; x < width; x++) {
          if (x - size < 0 || x + size >= width || y + 2*size >= height) {
            continue;
          }
          sizes.Add(GetRhombusSize(x, y, size, grid));
        }
      }
    }
    int[] answer = [.. sizes.ToList().OrderDescending().Take(3)];
    return answer;
  }
}

public class MainClass {
  public static void Main(string[] args) {
    testcase1();
    testcase2();
    testcase3();
    testcase4();
  }

  public static void PrintArray<T> ( T[] array ) {
    Console.WriteLine($"[{string.Join(", ", array)}]");
  }

  public static void testcase1() {
    Solution solution = new Solution();
    // [[3,4,5,1,3],[3,3,4,2,3],[20,30,200,40,10],[1,5,5,4,1],[4,3,2,2,5]]
    int[] result = solution.GetBiggestThree(
      new int[][] {
        new int[] {3,4,5,1,3},
        new int[] {3,3,4,2,3},
        new int[] {20,30,200,40,10},
        new int[] {1,5,5,4,1},
        new int[] {4,3,2,2,5}
      }
    );
    PrintArray(result);
  }

  public static void testcase2() {
    Solution solution = new Solution();
    // [[1,2,3],[4,5,6],[7,8,9]]
    int[] result = solution.GetBiggestThree(
      new int[][] {
        new int[] {1,2,3},
        new int[] {4,5,6},
        new int[] {7,8,9}
      }
    );
    PrintArray(result);
  }

  public static void testcase3() {
    Solution solution = new Solution();
    // [[7,7,7]]
    int[] result = solution.GetBiggestThree(
      new int[][] {
        new int[] {7,7,7}
      }
    );
    // Console.WriteLine($"{result}"); // "c"
    PrintArray(result);
  }

  public static void testcase4() {
    Solution solution = new Solution();
    // [[20,17,9,13,5,2,9,1,5],[14,9,9,9,16,18,3,4,12],[18,15,10,20,19,20,15,12,11],[19,16,19,18,8,13,15,14,11],[4,19,5,2,19,17,7,2,2]]
    int[] result = solution.GetBiggestThree(
      new int[][] {
        new int[] {20,17,9,13,5,2,9,1,5},
        new int[] {14,9,9,9,16,18,3,4,12},
        new int[] {18,15,10,20,19,20,15,12,11},
        new int[] {19,16,19,18,8,13,15,14,11},
        new int[] {4,19,5,2,19,17,7,2,2}
      }
    );
    // Console.WriteLine($"{result}"); // "c"
    PrintArray(result);
  }
};

