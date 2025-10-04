// 2785 Sort Vowels in a String
// https://leetcode.com/problems/sort-vowels-in-a-string/description/
// Time Taken: 00:07:49
// Difficulty: Medium


using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Security.Cryptography;

public class Solution {

    static public List<(int, int)> getCardinalDirections(int x, int y) {
        return [  (x, y - 1),(x + 1, y),(x, y + 1), (x - 1, y)];
    }

    static public bool IsInBounds(int x, int y, int rows, int cols) {
        if (x < 0 || x >= cols) { return false; }
        if (y < 0 || y >= rows) { return false; }
        return true;
    }

    static public int getMax(int[][] heightMap) {
        int rows = heightMap.Length;
        int cols = heightMap[0].Length;
        int best = -1;
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                best = Math.Max(best, heightMap[row][col]);
            }
        }
        return best;
    }

    void CanReachInside(int x, int y, int[][] heightMap, HashSet<(int, int)> visited) {
        if (visited.Contains((x,y))) { return; }

        int rows = heightMap.Length;
        int cols = heightMap[0].Length;
        visited.Add((x, y));
        foreach (var (cx, cy) in getCardinalDirections(x, y)) {
            if (!IsInBounds(cx, cy, rows, cols)) { continue; }
            if (heightMap[cy][cx] < heightMap[y][x]) { continue; } // only flow uphill
            if (visited.Contains((cx, cy))) { continue; }
            CanReachInside(cx, cy, heightMap, visited);
        }
    }

    public void GetConnectedPool(int x, int y, int[][] heightMap, HashSet<(int,int)> visited) {
        int rows = heightMap.Length;
        int cols = heightMap[0].Length;
        if (visited.Contains((x, y))) { return;}

        visited.Add((x, y));
        foreach (var (cx, cy) in getCardinalDirections(x, y)) {
            if (!IsInBounds(cx, cy, rows, cols)) { continue; }
            if (heightMap[cy][cx] > heightMap[y][x]) { continue; } // only flow downhill
            if (visited.Contains((cx, cy))) { continue; }

            GetConnectedPool(cx, cy, heightMap, visited);
        }
    }

    public (bool, int) getPoolCount(int z, HashSet<(int,int)> pool, int[][] heightMap, HashSet<(int,int)> outside) {
        int rows = heightMap.Length;
        int cols = heightMap[0].Length;

        int poolCount = 0;
        foreach (var (x1, y1) in pool) {
            foreach (var (cx, cy) in getCardinalDirections(x1, y1)) {
                if (!IsInBounds(cx, cy, rows, cols)) { continue; }
                if (pool.Contains((cx, cy))) { continue; }
                if (heightMap[cy][cx] > z) { continue; }
                if (outside.Contains((cx, cy))) {
                    poolCount = 0;
                    return (false, 0);
                }
            }

            int diff = Math.Max(0, z - heightMap[y1][x1] + 1);
            poolCount += diff;
        }
        return (true, poolCount);
    }

    public int BinarySearch(int x, int y, int[][] heightMap, int maxHeight, HashSet<(int, int)> outside, HashSet<(int, int)> processed) {
        // Get connected pool 
        HashSet<(int, int)> pool = [];
        GetConnectedPool(x, y, heightMap, pool);

        // Increase water-level until we start leaking
        int bestPoolCount = -1;
        // int left = heightMap[y][x];
        // int right = maxHeight;
        for (int z = heightMap[y][x]; z <= maxHeight; z++) {
            var (ok, poolCount) = getPoolCount(z, pool, heightMap, outside);
            if (!ok) { break; }
            bestPoolCount = Math.Max(bestPoolCount, poolCount);
        }

        foreach (var elem in pool) {
            processed.Add(elem);
        }
        return bestPoolCount;
    }
    public int Dumb(int[][] heightMap) {
        int rows = heightMap.Length;
        int cols = heightMap[0].Length;

        // Create a hashset to easily tell if a node is "connected" to the perimeter
        // If it is then the water can escape to the outside.
        HashSet<(int, int)> outside = [];
        for (int row = 0; row < rows; row++) {
            CanReachInside(0, row, heightMap, outside);
            CanReachInside(cols - 1, row, heightMap, outside);
        }
        for (int col = 0; col < cols; col++) {
            CanReachInside(col, 0, heightMap, outside);
            CanReachInside(col, rows - 1, heightMap, outside);
        }

        // Process from top to bottom, this way we can ensure we do minimal processing
        PriorityQueue<(int, int), int> pq = new();
        int maxHeight = -1;
        for (int row = 0; row < rows; row++) {
            for (int col = 0; col < cols; col++) {
                maxHeight = Math.Max(heightMap[row][col], maxHeight);
                if (outside.Contains((col, row))) { continue; }
                pq.Enqueue((row, col), -heightMap[row][col]);
            }
        }

        int count = 0;
        HashSet<(int, int)> processed = [];
        while (pq.Count > 0) {
            var (row, col) = pq.Dequeue();
            if (processed.Contains((col, row))) { continue; }
            count += BinarySearch(col, row, heightMap, maxHeight, outside, processed);
        }

        return count;
    }
    

    public int TrapRainWater(int[][] heightMap) {
        int rows = heightMap.Length;
        int cols = heightMap[0].Length;

        // Create the boundary
        HashSet<(int, int)> visited = [];
        PriorityQueue<(int, int, int), int> boundary = new();
        for (int col = 0; col < cols; col++) {
            boundary.Enqueue((col, 0, heightMap[0][col]), heightMap[0][col]);
            boundary.Enqueue((col, rows - 1, heightMap[rows-1][col]), heightMap[rows - 1][col]);
            visited.Add((col, 0));
            visited.Add((col, rows-1));
        }
        for (int row = 1; row < rows - 1; row++) {
            boundary.Enqueue((0, row,heightMap[row][0]), heightMap[row][0]);
            boundary.Enqueue((cols - 1, row, heightMap[row][cols - 1]), heightMap[row][cols - 1]);
            visited.Add((0, row));
            visited.Add((cols - 1, row));
        }

        int count = 0;
        while (boundary.Count > 0) {
            var (x, y, boundaryHeight) = boundary.Dequeue();

            foreach (var (cx, cy) in getCardinalDirections(x, y)) {
                if (!IsInBounds(cx, cy, rows, cols)) { continue; }
                if (visited.Contains((cx, cy))) { continue; }

                int cellHeight = heightMap[cy][cx];
                if (cellHeight < boundaryHeight) {
                    count += (boundaryHeight - cellHeight);
                }

                var h = Math.Max(cellHeight, boundaryHeight);
                boundary.Enqueue((cx, cy, h), h);
                visited.Add((cx, cy));
            }
        }

        return count;
    }
}

public class MainClass {
    record Case(int[,] nums);

    public static int[][] ConvertToJaggedArray(int[,] array2D) {
        int rows = array2D.GetLength(0);
        int cols = array2D.GetLength(1);

        int[][] jaggedArray = new int[rows][];

        for (int i = 0; i < rows; i++) {
            jaggedArray[i] = new int[cols];
            for (int j = 0; j < cols; j++) {
                jaggedArray[i][j] = array2D[i, j];
            }
        }

        return jaggedArray;
    }

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(new int[,] { { 1, 4, 3, 1, 3, 2 }, { 3, 2, 1, 3, 2, 4 }, { 2, 3, 3, 2, 3, 1 } }),  //4 
            new Case(new int[,] {{3,3,3,3,3},{3,2,2,2,3},{3,2,1,2,3},{3,2,2,2,3},{3,3,3,3,3} }), // 10
            new Case(new int[,] {{12,13,1,12},{13,4,13,12},{13,8,10,12},{12,13,12,12},{13,13,13,13} }), // 14
            new Case(new int[,] {{9,9,9,9,9,9,8,9,9,9,9},{9,0,0,0,0,0,1,0,0,0,9},{9,0,0,0,0,0,0,0,0,0,9},{9,0,0,0,0,0,0,0,0,0,9},{9,9,9,9,9,9,9,9,9,9,9} }), // 215
            new Case(new int[,] {{14,17,18,16,14,16},{17,3,10,2,3,8},{11,10,4,7,1,7},{13,7,2,9,8,10},{13,1,3,4,8,6},{20,3,3,9,10,8} }), // 25
        };

        foreach (var c in cases) {
            var jaggedArray = ConvertToJaggedArray(c.nums);
            var result = s.TrapRainWater(jaggedArray);
            Console.WriteLine(result);
        }
    }
}

