
// 3197 Find the Mininum Area to Cover All Ones II
// https://leetcode.com/problems/find-the-minimum-area-to-cover-all-ones-ii/description/
// Difficulty: Hard
// Time Taken: 02:53:47


public class Solution {

    public record Rect(int left, int right, int top, int bottom) {
        public int Area() {
            int width = (right - left + 1);
            int height = (bottom - top + 1);
            if (width * height < 0) {
                return 0;
            } else {
                return width * height;
            }
        }
        public bool IsZeroArea() {
            return Area() <= 0;
        }
        public void WriteLine() {
            Console.Write("[{0} {1} {2} {3}]", left, right, top, bottom);
        }
    }

    public void PrintArray(int[][] grid) {
        int rows = grid.Length;
        int cols = grid[0].Length;
        for (int row = 0; row < rows; row++) {
            Console.WriteLine(string.Join(", ", grid[row]));
        }
    }

    public void PrintArray(int[,] grid, int rows, int cols) {
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                Console.Write("{0}, ", grid[row, col]);
            }
            Console.WriteLine();
        }
    }

    public IEnumerable<(Rect, Rect)> SplitHorizontal(Rect rect) {
        if (rect.IsZeroArea()) {
            yield break;
        }
        for (int right = rect.left; right <= rect.right; right++) {
            Rect leftRect = new(rect.left, right, rect.top, rect.bottom);
            Rect rightRect = new(right + 1, rect.right, rect.top, rect.bottom);
            if (right + 1 > rect.right) {
                rightRect = new(1, 0, 0, 0);
            }
            yield return (leftRect, rightRect);
        }
    }
   
    public IEnumerable<(Rect, Rect)> SplitVertical(Rect rect) {
        if (rect.IsZeroArea()) {
            yield break;
        }
        for (int bottom = rect.top; bottom <= rect.bottom; bottom++) {
            Rect topRect = new(rect.left, rect.right, rect.top, bottom);
            Rect bottomRect = new(rect.left, rect.right, bottom + 1, rect.bottom);
            if (bottom + 1 > rect.bottom) {
                bottomRect = new(1, 0, 0, 0);
            }
            yield return (topRect, bottomRect);
        }
    }

    public IEnumerable<(Rect, Rect, Rect)> Split(Rect rect) {
        foreach (var (A, B) in SplitHorizontal(rect)) {
            foreach (var (C, D) in SplitVertical(A)) {
                yield return (B, C, D);
            }
            foreach (var (C, D) in SplitVertical(B)) {
                yield return (A, C, D);
            }
            foreach (var (C, D) in SplitHorizontal(A)) {
                yield return (B, C, D);
            }
            foreach (var (C, D) in SplitHorizontal(B)) {
                yield return (A, C, D);
            }
        }
        foreach (var (A, B) in SplitVertical(rect)) {
            foreach (var (C, D) in SplitVertical(A)) {
                yield return (B, C, D);
            }
            foreach (var (C, D) in SplitVertical(B)) {
                yield return (A, C, D);
            }
            foreach (var (C, D) in SplitHorizontal(A)) {
                yield return (B, C, D);
            }
            foreach (var (C, D) in SplitHorizontal(B)) {
                yield return (A, C, D);
            }
        }
    }

    public Rect MinRect(int[][] grid, Rect rect) {
        if (rect.IsZeroArea()) {
            return new(1, 0, 0, 0);
        }
        int left = rect.right;
        int right = rect.left;
        int top = rect.bottom;
        int bottom = rect.top;
        for (int row = rect.top; row <= rect.bottom; row++) {
            for (int col = rect.left; col <= rect.right; col++) {
                if (grid[row][col] == 0) continue;

                top = Math.Min(top, row);
                bottom = Math.Max(bottom, row);
                left = Math.Min(left, col);
                right = Math.Max(right, col);
            }
        }
        return new(left, right, top, bottom);
    }

    public int[,] Precompute(int[][] grid) {
        int rows = grid.Length;
        int cols = grid[0].Length;
        int[,] DP = new int[rows + 1, cols + 1];

        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                int r = row + 1;
                int c = col + 1;

                int top = DP[r - 1, c];
                int left = DP[r, c - 1];
                int top_left = DP[r - 1, c - 1];
                DP[r, c] = top + left - top_left + grid[row][col];
            }
        }
        return DP;
    }

    public int BoxArea(int[,] DP, Rect rect) {
        int full = DP[rect.bottom + 1, rect.right + 1];
        int top = DP[rect.top, rect.right+1];
        int left = DP[rect.bottom+1, rect.left];
        int top_left = DP[rect.top, rect.left];
        return full - (top + left - top_left);
    }
    public int BoxArea(int[,] DP, int x1, int y1, int x2, int y2) {
        return BoxArea(DP, new(x1, x2, y1, y2));
    }

    public int BinarySearch(int low, int high, Func<int, bool> cond) {
        int mid = low + (high - low) / 2;
        while (low <= high) {
            mid = low + (high - low) / 2;
            bool explore_left = cond(mid);
            if (explore_left) {
                high = mid-1;
            } else {
                low = mid+1;
            }
        }
        return mid;
    }

    public int MinArea(int[][] grid, int[,] DP, Rect rect) {
        // if (rect.IsZeroArea()) {
        //     return 0;
        // }
        // int number_ones = BoxArea(DP, rect);
        // if (number_ones == 0) {
        //     return 0;
        // }
        // var (x1, y1, x2, y2) = (rect.left, rect.top, rect.right, rect.bottom);

        // // Find the min_y
        // int min_y = y1;
        // int max_y = y2;
        // int min_x = x1;
        // int max_x = x2;

        // BinarySearch(rect.top, rect.bottom, mid => {
        //     if (BoxArea(DP, x1, y1, x2, mid) > 0) {
        //         min_y = mid;
        //         return true;
        //     }
        //     return false;
        // });
        // // Find the max_y
        // BinarySearch(rect.top, rect.bottom, mid => {
        //     if (BoxArea(DP, x1, mid, x2, y2) <= 0) {
        //         return true;
        //     }
        //     max_y = mid;
        //     return false;
        // });
        // // Find the min_x
        // BinarySearch(rect.left, rect.right, mid => {
        //     if (BoxArea(DP, x1, y1, mid, y2) > 0) {
        //         min_x = mid;
        //         return true;
        //     }
        //     return false;
        // });
        // // Find the max_x
        // BinarySearch(rect.left, rect.right, mid => {
        //     if (BoxArea(DP, mid, y1, x2, y2) <= 0) {
        //         return true;
        //     }
        //     max_x = mid;
        //     return false;
        // });

        // int width = max_x - min_x + 1;
        // int height = max_y - min_y + 1;
        // int area = width * height;
        // return area;

        Rect minRect = MinRect(grid, rect);
        if (area != minRect.Area()) {
            return 0;
        }
        return minRect.Area();
    }

    public int Solve(int[][] grid) {
        int rows = grid.Length;
        int cols = grid[0].Length;
        int[,] DP = Precompute(grid);

        Rect fullRect = MinRect(grid, new(0, cols - 1, 0, rows - 1));
        int minArea = fullRect.Area();
        foreach (var (A, B, C) in Split(fullRect)) {
            int a = MinArea(grid, DP, A);
            int b = MinArea(grid, DP, B);
            int c = MinArea(grid, DP, C);
            if (a + b + c < minArea) {
                minArea = a + b + c;
            }
        }
        return minArea;
    }

    public int MinimumSum(int[][] grid) {
        return Solve(grid);
    }
}

public class MainClass {
    record Case(int[,] nums, int rows, int cols);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(new int[,] { { 1, 0, 1 } , {1, 1, 1}}, 2, 3), // 5
            new Case(new int[,] { { 1, 0, 1, 0 } , { 0, 1, 0, 1 } }, 2, 4), // 5
            new Case(new int[,] { { 1, 1, 0 }, { 1, 0, 0 }, {1,1,0} }, 3, 3), // 5
            new Case(new int[,] { { 0, 0, 0 } , { 0, 0, 0 } , { 0, 0, 1 } , {1,1,0}}, 4, 3), // 3
            new Case(new int[,]{ { 0, 0, 0, 0 } , { 1, 0, 0, 0 } , { 0, 0, 1, 1 }}, 3, 4), // 3
            new Case(new int[,]{ { 0, 0, 0, 0 } , { 1, 0, 1, 1 } , { 0, 0, 0, 0 }}, 3, 4), // 3
            new Case(new int[,]{ { 0, 1, 1, 1 } , {0,0,0,0 } , { 0, 1, 0, 1 }}, 3, 4), // 5
            new Case(new int[,]{ {0,0,0,1,0 }, {0,0,0,0,0 }, {0,1,0,0,1 }, {0,0,0,0,0 }, {0,0,1,0,0 } }, 5, 5), // 6
        };

        foreach (var c in cases) {
            int[][] newNums = new int[c.rows][];
            for (int row = 0; row < c.rows; row++) {
                newNums[row] = new int[c.cols];
                for (int col = 0; col < c.cols; col++) {
                    newNums[row][col] = c.nums[row, col];
                }
            }
            Console.WriteLine(s.MinimumSum(newNums));
        }
    }
}
