# 1277. Count Square Submatrices with All Ones
# Medium, 01:41:41
# https://leetcode.com/problems/count-square-submatrices-with-all-ones/description/

from typing import *
from pprint import pprint

class Solution:
    def numSubs(self, n):
        if n == 1:
            return 1
        else:
            return n*n + self.numSubs(n-1)

    def isOnes(self, matrix, row, col, size):
        r = row
        c = col
        for r in range(row, row+size):
            for c in range(col, col+size):
                if matrix[r][c] == 0:
                    return False
                
        return True
    
    def brute(self, matrix):
        n = len(matrix)
        m = len(matrix[0])
        window = min(n,m)
        count = 0

        for w in range(1, window+1):
            for row in range(n-w+1):
                for col in range(m-w+1):

                    if self.isOnes(matrix, row, col, w):
                        count += 1
        return count
    
    def can_expand(self, matrix, row, col, size):
        next_size = size+1
        rows = len(matrix)
        cols = len(matrix[0])
        if row + next_size > rows:
            return False
        if col + next_size > cols:
            return False
        for r in range(row, row + next_size):
            if matrix[r][col + next_size-1] == 0:
                return False
        for c in range(col, col + next_size):
            if matrix[row + next_size-1][c] == 0:
                return False
        return True
    
    def mine(self, matrix):
        rows = len(matrix)
        cols = len(matrix[0])
        window = min(rows, cols)
        dp = [[0]*cols for _ in range(rows)]
        count = 0

        for row in range(rows):
            for col in range(cols):
                if matrix[row][col] == 1:
                    dp[row][col] = 1
                    count += 1

        for w in range(2, window+1):
            next_dp = [[0]*cols for _ in range(rows)]

            for row in range(w-1, rows):
                for col in range(w-1, cols):

                    tl = dp[row-1][col-1] if row-1 >= 0 and col-1 >= 0 else 0
                    t = dp[row-1][col] if row-1 >= 0 else 0
                    l = dp[row][col-1] if col-1 >= 0 else 0
                    d = matrix[row][col]
                    if tl == 1 and t == 1 and l == 1 and d == 1:
                        next_dp[row][col] = 1
                        count += 1

            dp = next_dp

        return count
    
    def print_array(self, matrix):
        for row in matrix:
            pprint(row)
            # for col in row:
            #     print("{col} ")
            # print("\n")
    
    def editorial(self, matrix):
        rows = len(matrix)
        cols = len(matrix[0])
        dp = [[0]*(cols+1) for _ in range(rows+1)]
        count = 0

        # self.print_array(dp)

        for row in range(rows):
            for col in range(cols):
                if matrix[row][col]:
                    dp[row+1][col+1] = min(
                        dp[row][col],    # top-left
                        dp[row+1][col],  # top
                        dp[row][col+1]   # left
                    ) + 1
                    count += dp[row+1][col+1]

                # print(row, col)
                # self.print_array(dp)

        return count



    def countSquares(self, matrix: List[List[int]]) -> int:
        return self.editorial(matrix)

def main():
    s = Solution()
    test_cases = [
        [[0,1,1,1],[1,1,1,1],[0,1,1,1]],
        # [[1,0,1],[1,1,0],[1,1,0]]
    ]
    for matrix in test_cases:
        print(s.countSquares(matrix))

if __name__ == "__main__":
    main()
