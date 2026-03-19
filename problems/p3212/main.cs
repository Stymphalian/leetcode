// 3212. Count Submatrices With Equal Frequency of X and Y
// https://leetcode.com/problems/count-submatrices-with-equal-frequency-of-x-and-y/description/?envType=daily-question&envId=2026-03-19
// Difficulty: Medium
// Time: 00:28:17

public class Solution {


    public int NumberOfSubmatrices(char[][] grid) {
        int height = grid.Length;
        int width = grid[0].Length;
        (int, int)[][] dp = new (int, int)[grid.Length][];
        for (int y = 0; y < height; y++) {
            dp[y] = new (int, int)[width];
        }

        (int, int) get(int y, int x) {
            return (y >= 0 && x >=0) ? dp[y][x] : (0,0);
        }


        // Base case (1st row)
        int count = 0;
        for(int x = 0; x < width; x++) {
            var (xcnt, ycnt) = get(0,x-1);
            if (grid[0][x] == 'X') {
                xcnt += 1;
            } else if (grid[0][x] == 'Y') {
                ycnt += 1; 
            }
            dp[0][x] = (xcnt, ycnt);
            if (xcnt == ycnt && xcnt > 0) {
                count += 1;
            }
        }

        // Fill in the DP
        for (int y = 1; y < height; y++) {
            for (int x = 0; x < width; x++) {
                var topleft = get(y-1, x-1);
                var top = get(y-1, x);
                var left = get(y, x-1);
                int xcnt = top.Item1 + left.Item1 - topleft.Item1;
                int ycnt = top.Item2 + left.Item2 - topleft.Item2;
                if (grid[y][x] == 'X') { xcnt += 1;}
                else if (grid[y][x] == 'Y') { ycnt += 1;}
                dp[y][x] = (xcnt, ycnt);

                if (xcnt == ycnt && xcnt > 0) {
                    count += 1;
                }
            }
        }

        return count;
    }
}

public class MainClass {

    record Case(char[][] N);

    public static void Main(string[] args) {
        Solution s = new Solution();

        // Original MaxFrequency tests
        Console.WriteLine("=== Testing MaxFrequency ===");
        Case[] cases = {
            // [["X","Y","."],["Y",".","."]]
            new Case(new char[][] {
                new char[] { 'X', 'Y', '.' },
                new char[] { 'Y', '.', '.' }
            }),
            // [["X","X"],["X","Y"]]
            new Case(new char[][] {
                new char[] { 'X', 'X' },
                new char[] { 'X', 'Y' }
            }),
            // [[".","."],[".","."]]
            new Case(new char[][] {
                new char[] { '.', '.' },
                new char[] { '.', '.' }
            }),
        };
        for (int ci = 0; ci < cases.Length; ci++) {
            Case c = cases[ci];
            var answer = s.NumberOfSubmatrices(c.N);
            Console.WriteLine(string.Format("Case {0}: {1}", ci + 1, answer));
        }
    }

};
