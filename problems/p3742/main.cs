// 3742 Maximum Path Score in a Grid
// https://leetcode.com/problems/maximum-path-score-in-a-grid/description/
// Difficulty: Medium
// Time Taken: 00:56:33



// I'm too dumb to exist.

public class Solution {
  public int MaxPathScore(int[][] grid, int k) {
    int height = grid.Length;
    int width = grid[0].Length;
    int[,,] DP = new int[height+1,width+1,k+1];
    for (int i = 0; i <= height; i++) {
      for (int j = 0; j <= width; j++) {
        for (int c = 0; c <= k; c++) {
          DP[i, j, c] = -1;
        }
      }
    }
    DP[0,0,0] = 0;

    int Get(int row, int col, int cost) {
      if (row < 0 || col < 0 || row >= height || col >= width) {return -1;}
      if (cost > k) { return -1; }
      return DP[row, col, cost];
    }

    for (int row = 0; row < height; row++) {
      for (int col = 0; col < width; col++) {
        for(int cost = 0; cost <= k; cost++) {
          var currentScore = Get(row, col, cost);
          if (currentScore == -1) {
            continue;
          }

          // right
          if (col + 1 < width) {
            int rightVal = grid[row][col+1];
            int rightCost = rightVal == 2 ? 1 : rightVal;
            int nextRightCost = cost + rightCost;
            int currentRightScore = Get(row, col+1, nextRightCost);
            int candRightScore = currentScore + rightVal;
            if (nextRightCost <= k) {
              DP[row, col+1, nextRightCost] = Math.Max(
                currentRightScore, candRightScore
              );    
            }
          }
          

          // down
          if (row + 1 < height) {
            int downVal = grid[row+1][col];
            int downCost = downVal == 2 ? 1 : downVal;
            int nextDownCost = cost + downCost;
            int currentDownScore = Get(row+1, col, nextDownCost);
            int candDownScore = currentScore + downVal;
            if (nextDownCost <= k) {
              DP[row+1, col, nextDownCost] = Math.Max(
                currentDownScore, candDownScore
              );    
            }
          }
          
        }
      }
    }

    int bestScore = -1;
    for(int cost = 0; cost <= k; cost++) {
      bestScore = Math.Max(DP[height-1, width-1,cost], bestScore);
    }
    return bestScore;
  }
}