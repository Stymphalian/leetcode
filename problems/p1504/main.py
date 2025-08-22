# 1504: Count Submatrcies with All Ones
# Medium, 04:24:57
# https://leetcode.com/problems/count-submatrices-with-all-ones/description/

from typing import *
from pprint import pprint
import functools
import math
from collections import namedtuple

Entry = namedtuple('Entry', ['index', 'count', 'height'])

class Solution:
    def print_array(self, dp):
        for row in dp:
            pprint(row)
        print()

    def editorial(self, matrix):
        rows = len(matrix)
        cols = len(matrix[0])

        dp = [[0]*(cols) for _ in range(rows)]
        for row in range(rows):
            for col in range(cols):
                if col == 0:
                    dp[row][col] = matrix[row][col]
                elif matrix[row][col] == 1:
                    dp[row][col] = dp[row][col-1] + 1

        answer = 0
        for row in range(rows):
            for col in range(cols):

                current = dp[row][col]
                for k in range(row,-1,-1):
                    current = min(current, dp[k][col])
                    if current == 0:
                        break
                    answer += current
            
        # self.print_array(dp)
        return answer
        
    def editorial2(self, matrix):
        rows, cols = len(matrix), len(matrix[0])

        heights = [0]*cols
        answer = 0

        for row in range(rows):
            # create the new heights histogram
            for col in range(cols):
                if matrix[row][col]:
                    heights[col] = heights[col] + 1
                else:
                    heights[col] = 0
                    
            # for each right boundary, find the left boundary
            # and increment the counts. The left boundary is the index in 
            # which the heights[left] < heights[right].
            # We efficiently find the left by using a monotonic stack data structure.
            # Each stack entry is (index, count, height)
            stack = [Entry(-1,0,-1)]
            for right, height in enumerate(heights):
                while stack[-1].height >= height:
                    stack.pop()
                
                left, prev, _ = stack[-1]
                width = (right - left)
                current_count = (width*height) + prev
                stack.append(Entry(right, current_count, height))

                answer += current_count
    
        return answer

    def numSubmat(self, mat: List[List[int]]) -> int:
        return self.editorial2(mat)
    

def main():
    s = Solution()
    test_cases = [
        [[1,0,1],[1,1,0],[1,1,0]],  # 13
        [[0,1,1,0],[0,1,1,1],[1,1,1,0]] # 24
    ]
    for matrix in test_cases:
        print(s.numSubmat(matrix))

if __name__ == "__main__":
    main()
