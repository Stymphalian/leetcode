
// 3195 Find the Mininum Area to Cover All Ones I
// https://leetcode.com/problems/find-the-minimum-area-to-cover-all-ones-i/description
// Difficulty: Medium??
// Time Taken: 00:20:15

using System.Globalization;
using System.Numerics;
using System.Runtime.InteropServices;

public class Solution {

    int[,] usedRows = new int[9, 10];
    int[,] usedCols = new int[9, 10];
    int[,] usedSquares = new int[9, 10];

    public bool checkRowForNum(char[][] chars, int targetRow, int targetNum) {
        for (int col = 0; col < chars[targetRow].Length; col++) {
            if (chars[targetRow][col] == '.') continue;
            if (chars[targetRow][col] - '0' == targetNum) return true;
        }
        return false;
    }

    public bool checkColForNum(char[][] chars, int targetCol, int targetNum) {
        for (int row = 0; row < chars.Length; row++) {
            if (chars[row][targetCol] == '.') continue;
            if (chars[row][targetCol] - '0' == targetNum) return true;
        }
        return false;
    }

    public bool checkSquareForNum(char[][] chars, int targetRowOrig, int targetColOrig, int targetNum) {
        var (targetRow, targetCol) = getSquareIndexFromCoords(targetRowOrig, targetColOrig);
        for (int row = targetRow * 3; row < targetRow * 3 + 3; row++) {
            for (int col = targetCol * 3; col < targetCol * 3 + 3; col++) {
                if (chars[row][col] == '.') continue;
                if (chars[row][col] - '0' == targetNum) return true;
            }
        }
        return false;
    }

    public (int, int) getSquareIndexFromCoords(int row, int col) {
        int squareRow = row / 3;
        int squareCol = col / 3;
        return (squareRow, squareCol);
    }

    public bool checkConditions(char[][] board) {
        int[] used = new int[9];

        // check rows
        for (int row = 0; row < board.Length; row++) {
            for (int i = 0; i < used.Length; i++) {
                used[i] = 0;
            }

            for (int col = 0; col < board[row].Length; col++) {
                if (board[row][col] == '.') return false;
                int num = board[row][col] - '0';
                if (used[num - 1] > 0) return false;
                used[num - 1]++;
            }
        }

        // check cols
        for (int col = 0; col < board[0].Length; col++) {
            for (int i = 0; i < used.Length; i++) {
                used[i] = 0;
            }

            for (int row = 0; row < board.Length; row++) {
                if (board[row][col] == '.') return false;
                int num = board[row][col] - '0';
                if (used[num - 1] > 0) return false;
                used[num - 1]++;
            }
        }

        // check squares
        for (int row = 0; row < board.Length; row += 3) {
            for (int col = 0; col < board[row].Length; col += 3) {
                for (int i = 0; i < used.Length; i++) {
                    used[i] = 0;
                }

                // Square
                for (int r = row; r < row + 3; r++) {
                    for (int c = col; c < col + 3; c++) {
                        if (board[r][c] == '.') return false;
                        int num = board[r][c] - '0';
                        if (used[num - 1] > 0) return false;
                        used[num - 1]++;
                    }
                }

            }
        }

        return true;
    }


    public bool canPlace(char[][] board, int row, int col, int targetNum) {
        // if (checkRowForNum(board, row, targetNum)) return false;
        // if (checkColForNum(board, col, targetNum)) return false;
        // if (checkSquareForNum(board, row, col, targetNum)) return false;
        // return true;
        int idx = (row / 3) * 3 + (col / 3);
        return usedRows[row, targetNum] + usedCols[col, targetNum] + usedSquares[idx, targetNum] == 0;
    }

    public void placeNum(char[][] board, int row, int col, int targetNum) {
        usedRows[row, targetNum] += 1;
        usedCols[col, targetNum] += 1;
        usedSquares[(row / 3)*3 +  (col / 3), targetNum] += 1;
        board[row][col] = (char)(targetNum + '0');
    }
    public void removeNum(char[][] board, int row, int col, int targetNum) {
        usedRows[row, targetNum] -= 1;
        usedCols[col, targetNum] -= 1;
        usedSquares[(row / 3)*3 +  (col / 3), targetNum] -= 1;
        board[row][col] = '.';
    }

    public bool CanSolve(char[][] board, List<(int, int)> spaces, int spaceIndex) {
        if (spaceIndex >= spaces.Count) {
            return checkConditions(board);
            // if (!checkConditions(board)) {
            //     throw new Exception("Invalid board");
            // }
            // return true;
        }

        int row = spaces[spaceIndex].Item1;
        int col = spaces[spaceIndex].Item2;
        for (int cand = 1; cand <= 9; cand++) {
            if (canPlace(board, row, col, cand)) {
                placeNum(board, row, col, cand);
                if (CanSolve(board, spaces, spaceIndex + 1)) return true;
                removeNum(board, row, col, cand);
            }
        }

        return false;
    }

    public void SolveSudoku(char[][] board) {
        List<(int, int)> spaces = new();
        for (int row = 0; row < board.Length; row++) {
            for (int col = 0; col < board[row].Length; col++) {
                if (board[row][col] == '.') {
                    spaces.Add((row, col));
                } else {
                    int targetNum = board[row][col] - '0';
                    placeNum(board, row, col, targetNum);
                }

            }
        }

        CanSolve(board, spaces, 0);
    }

    public void PrintBoard(char[][] board) {
        for (int row = 0; row < board.Length; row++) {
            for (int col = 0; col < board[row].Length; col++) {
                Console.Write("{0}, ", board[row][col]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}

public class MainClass {
    record Case(char[,] nums);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(new char[,] {
                {'5','3','.','.','7','.','.','.','.'},
                {'6','.','.','1','9','5','.','.','.'},
                {'.','9','8','.','.','.','.','6','.'},
                {'8','.','.','.','6','.','.','.','3'},
                {'4','.','.','8','.','3','.','.','1'},
                {'7','.','.','.','2','.','.','.','6'},
                {'.','6','.','.','.','.','2','8','.'},
                {'.','.','.','4','1','9','.','.','5'},
                {'.','.','.','.','8','.','.','7','9'},
            }
            )
        };

        foreach (var c in cases) {

            char[][] caseDoubleArray = new char[9][];
            for (int i = 0; i < 9; i++) {
                caseDoubleArray[i] = new char[9];
                for (int j = 0; j < 9; j++) {
                    caseDoubleArray[i][j] = c.nums[i, j];
                }
            }

            // Console.WriteLine(s.SolveSudoku(caseDoubleArray));
            s.SolveSudoku(caseDoubleArray);
            s.PrintBoard(caseDoubleArray);
        }
    }
}

