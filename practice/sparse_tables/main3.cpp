#include <algorithm>
#include <cassert>
#include <climits>
#include <cmath>
#include <cstdio>
#include <functional>
#include <string>
#include <unordered_map>
#include <vector>

#define min3(a, b, c) min(a, min(b, c))
#define min4(a, b, c, d) min(min(a, b), min(c, d))
#define max3(a, b, c) max(a, max(b, c))
#define max4(a, b, c, d) max(max(a, b), max(c, d))

using namespace std;

using Grid = vector<vector<int>>;

void print_table(vector<int> &table) {
    for (int i = 0; i < table.size(); i++) {
        printf("%2d ", table[i]);
    }
    printf("\n");
}

void print_table(vector<vector<int>> &table) {
    for (int i = 0; i < table.size(); i++) {
        for (int j = 0; j < table[i].size(); j++) {
            printf("%2d ", table[i][j]);
        }
        printf("\n");
    }
}

void print_table(vector<vector<Grid>> &table) {
    for (int y = 0; y < table.size(); y++) {
        for (int x = 0; x < table[y].size(); x++) {
            printf("[%d][%d]\n", y, x);
            print_table(table[y][x]);
        }
        printf("\n");
    }
};

bool all_ones(Grid &grid, int left, int top, int w, int h) {
    for (int y = top; y < top + h; y++) {
        for (int x = left; x < left + w; x++) {
            if (x < 0 || y < 0 || y >= grid.size() || x >= grid[0].size()) {
                return false;
            }
            if (grid[y][x] == 0) {
                return false;
            }
        }
    }
    return true;
}

vector<int> powers_of_two(int n) {
    int power = int(log(n) / log(2)) + 1;
    vector<int> answer;
    while (power >= 0) {
        bool is_set = (n & (1 << power)) > 0;
        if (is_set) {
            answer.push_back(power);
        }
        power -= 1;
    }
    return answer;
}

class SparseTable {
   public:
    vector<vector<Grid>> _dp;  // sparse table, constructed via DP
    int _rows;
    int _cols;
    int _rows_power;
    int _cols_power;

    SparseTable(Grid &array) {
        _rows = array.size();
        _cols = array[0].size();
        _rows_power = int(log(_rows) / log(2)) + 1;
        _cols_power = int(log(_cols) / log(2)) + 1;

        build(array);
    }

    void build(Grid &grid) {
        _dp = vector<vector<Grid>>(
            _rows_power,
            vector<Grid>(
                _cols_power,
                Grid(_rows, vector<int>(_cols, 0))));

        _dp[0][0] = grid;
        for (int py = 1; py < _rows_power; py++) {
            int prev_py = (1 << (py - 1));
            int current_py = (1 << py);
            for (int y = 0; y < _rows - current_py + 1; y++) {
                for (int x = 0; x < _cols; x++) {
                    int a = _dp[py - 1][0][y][x];
                    int b = _dp[py - 1][0][y + prev_py][x];
                    _dp[py][0][y][x] = max(a, b);
                }
            }
        }
        for (int px = 1; px < _cols_power; px++) {
            int prev_px = (1 << (px - 1));
            int current_px = (1 << px);
            for (int y = 0; y < _rows; y++) {
                for (int x = 0; x < _cols - current_px + 1; x++) {
                    int a = _dp[0][px - 1][y][x];
                    int b = _dp[0][px - 1][y][x + prev_px];
                    _dp[0][px][y][x] = max(a, b);
                }
            }
        }

        for (int py = 1; py < _rows_power; py++) {
            for (int px = 1; px < _cols_power; px++) {
                int prev_py = (1 << (py - 1));
                int prev_px = (1 << (px - 1));
                int current_py = (1 << py);
                int current_px = (1 << px);

                for (int y = 0; y < _rows - current_py + 1; y++) {
                    for (int x = 0; x < _cols - current_px + 1; x++) {
                        int a = get(py - 1, px - 1, y + prev_py, x + prev_px);
                        int b = get(py - 1, px - 1, y + prev_py, x);
                        int c = get(py - 1, px - 1, y, x + prev_px);
                        int d = get(py - 1, px - 1, y, x);
                        _dp[py][px][y][x] = max(max(max(a, b), c), d);
                    }
                }
            }
        }

        // print_table(_dp);
    }

    bool inrange(int v, int min, int max) {
        if (v < min || v > max) { return false; }
        return true;
    }

    int get(int py, int px, int y, int x) {
        if (!inrange(py, 0, _rows_power)) { return 0; }
        if (!inrange(px, 0, _cols_power)) { return 0; }
        if (!inrange(y, 0, _rows)) { return 0; }
        if (!inrange(x, 0, _cols)) { return 0; }
        return _dp[py][px][y][x];
    }

    int query(int top, int left, int bottom, int right) {
        int width = (right - left) + 1;
        int height = (bottom - top) + 1;

        vector<int> widths = powers_of_two(width);
        vector<int> heights = powers_of_two(height);

        int count = 0;
        int x = left;
        for (int w_power : widths) {
            int y = top;
            for (int h_power : heights) {
                count = max(count, _dp[h_power][w_power][y][x]);
                y += (1 << h_power);
            }
            x += (1 << w_power);
        }
        return count;
    }

    int query2(int top, int left, int bottom, int right) {
        int width = right - left + 1;
        int height = bottom - top + 1;
        int px = (log(width) / log(2)) + 1;
        int py = (log(height) / log(2)) + 1;
        // x1 top
        // y1 left
        // x2 bottom
        // y2 right
        // l = px
        // k = py

        int a = get(py, px, top, left);
        int b = get(py, px, bottom - (1 << py) + 1, left);
        int c = get(py, px, top, right - (1 << px) + 1);
        int d = get(py, px, bottom - (1 << py) + 1, right - (1 << px) + 1);
        return max(max(max(a, b), c), d);

    //         Sparse[X1][Y1][K][L],
    //         Sparse[X2 - (1 << K) + 1][Y1][K][L],
    //         Sparse[X1][Y2 - (1 << L) + 1][K][L],
    //         Sparse[X2 - (1 << K) + 1][Y2 - (1 << L) + 1][K][L]
    }
};

int brute_min(vector<int> &array, int left, int right) {
    int best = INT_MAX;
    for (int i = left; i <= right; i++) {
        best = min(best, array[i]);
    }
    return best;
}

int brute_mult(vector<int> &array, int left, int right) {
    int best = 1;
    for (int i = left; i <= right; i++) {
        best = best * array[i];
    }
    return best;
}

int brute_sum(vector<int> &array, int left, int right) {
    int best = 0;
    for (int i = left; i <= right; i++) {
        best += array[i];
    }
    return best;
}

int brute_ones(vector<vector<int>> &grid, int top, int left, int bottom, int right) {
    int biggest_square = 0;
    int max_square = max(right - left + 1, bottom - top + 1);

    for (int w = 1; w <= max_square; w++) {
        for (int y = top; y <= bottom - w + 1; y++) {
            for (int x = left; x <= right - w + 1; x++) {
                if (all_ones(grid, x, y, w, w)) {
                    biggest_square = max(biggest_square, w);
                }
            }
        }
    }
    return biggest_square;
}

int brute_sum(vector<vector<int>> &grid, int top, int left, int bottom, int right) {
    int count = 0;
    for (int y = top; y <= bottom; y++) {
        for (int x = left; x <= right; x++) {
            count += grid[y][x];
        }
    }
    return count;
}

void test() {
    srand(0);
    int rows = 30;
    int cols = 28;
    vector<vector<int>> grid(rows, vector<int>(cols, 0));
    for (int y = 0; y < rows; y++) {
        for (int x = 0; x < cols; x++) {
            grid[y][x] = (rand() % 50) > 10 ? 1 : 0;
        }
    }
    SparseTable table(grid);
    // print_table(table._dp);
    print_table(grid);

    int total = 0;
    int correct = 0;
    for (int w = 0; w < cols; w++) {
        for (int h = 0; h < rows; h++) {
            for (int y = 0; y < rows - h; y++) {
                for (int x = 0; x < cols - w; x++) {
                    total += 1;

                    int left = x;
                    int top = y;
                    int right = x + w;
                    int bottom = y + h;

                    int a = table.query(top, left, bottom, right);
                    int b = brute_sum(grid, top, left, bottom, right);
                    if (a != b) {
                        printf("(%d != %d) left = %d, top = %d, right = %d, bottom = %d width = %d, height = %d\n", a, b, left, top, right, bottom, w, h);
                        return;
                    } else {
                        correct += 1;
                    }
                }
            }
        }
    }
    printf("done %d / %d\n", correct, total);
}

vector<vector<int>> get_max_grid(Grid &grid) {
    Grid dp(grid.size(), vector<int>(grid[0].size(), 0));
    dp[0] = grid[0];
    for (int y = 1; y < grid.size(); y++) {
        for (int x = 1; x < grid[0].size(); x++) {
            if (grid[y][x] == 1) {
                int a = dp[y - 1][x - 1];
                int b = dp[y - 1][x];
                int c = dp[y][x - 1];
                dp[y][x] = min(min(a, b), c) + 1;
            }
        }
    }
    return dp;
}

int foo(Grid &Arr, vector<vector<int>>& queries ) {
    int Rows, Cols;
    Rows = Arr.size();
    Cols = Arr[0].size();
    short Log2[1111] = {-1};
    vector<vector<Grid>> Sparse(1001, vector<Grid>(1001, Grid(10, vector<int>(10, 0))));
    // short Sparse[1001][1001][10][10];

    for (int i = 1; i <= 1000; ++i) {
        Log2[i] = Log2[i >> 1] + 1;
    }

    for (int i = 1; i <= Rows; ++i) {
        for (int j = 1; j <= Cols; ++j) {
            if (Arr[i - 1][j - 1] == 1) {
                int a = Sparse[i - 1][j - 1][0][0];
                int b = Sparse[i - 1][j][0][0];
                int c = Sparse[i][j - 1][0][0];
                Sparse[i][j][0][0] = min(min(a, b), c) + 1;
            }
        }
    }

    for (int j = 1; j <= Log2[Cols]; ++j)
        for (int x = 1; x <= Rows; ++x)
            for (int y = 1; y <= Cols - (1 << j) + 1; ++y)
                Sparse[x][y][0][j] = max(
                    Sparse[x][y][0][j - 1],
                    Sparse[x][y + (1 << (j - 1))][0][j - 1]);
    for (int i = 1; i <= Log2[Rows]; ++i)
        for (int j = 0; j <= Log2[Cols]; ++j)
            for (int x = 1; x <= Rows - (1 << i) + 1; ++x)
                for (int y = 1; y <= Cols - (1 << j) + 1; ++y)
                    Sparse[x][y][i][j] = max(
                        Sparse[x][y][i - 1][j],
                        Sparse[x + (1 << (i - 1))][y][i - 1][j]);

    for (int i = 0; i <= Log2[Rows]; ++i) {
        for (int j = 0; j <= Log2[Cols]; ++j) {
            printf("rows = %d, cols = %d\n", i, j);
            for (int y = 0; y < Rows; y++) {
                for (int x = 0; x < Cols; x++) {
                    printf("%2d ", Sparse[y + 1][x + 1][i][j]);
                }
                printf("\n");
            }
            printf("\n");
        }
    }

    auto Query = [&](int top, int left, int bottom, int right)
    {
        printf("top = %d, left = %d, bottom = %d, right = %d\n", top, left, bottom, right);
        int rows = Log2[bottom - top + 1];
        int cols = Log2[right - left + 1];
        int current_py = (1 << rows);
        int current_px = (1 << cols);
        int a = Sparse[top][left][rows][cols];
        int b = Sparse[bottom - current_py + 1][left][rows][cols];
        int c = Sparse[top][right - current_px + 1][rows][cols];
        int d = Sparse[bottom - current_py + 1][right - current_px + 1][rows][cols];

        printf("rows = %d, cols = %d\n", rows, cols);
        printf("%d, %d = %d\n", top, left, a);
        printf("%d, %d = %d\n", bottom - current_py + 1, left, b);
        printf("%d, %d = %d\n", top, right - current_px + 1, c);
        printf("%d, %d = %d\n", bottom - current_py + 1, right - current_px + 1, d);
        
        return max4(a, b, c, d);
    };


    // Query top = 1, left = 1, bottom = 16, right = 16
    // Le, Ri, Mid = 1, 16, 8
    // top = 8, left = 8, bottom = 16, right = 16
    // rows = 3, cols = 3
    // 8, 8 = 11
    // 9, 8 = 0
    // 8, 9 = 0
    // 9, 9 = 0
    // Le, Ri, Mid = 9, 16, 12
    // top = 12, left = 12, bottom = 16, right = 16
    // rows = 2, cols = 2
    // 12, 12 = 11
    // 13, 12 = 0
    // 12, 13 = 0
    // 13, 13 = 0
    // Le, Ri, Mid = 9, 11, 10
    // top = 10, left = 10, bottom = 16, right = 16
    // rows = 2, cols = 2
    // 10, 10 = 9
    // 13, 10 = 0
    // 10, 13 = 0
    // 13, 13 = 0
    // Le, Ri, Mid = 9, 9, 9
    // top = 9, left = 9, bottom = 16, right = 16
    // rows = 3, cols = 3
    // 9, 9 = 0
    // 9, 9 = 0
    // 9, 9 = 0
    // 9, 9 = 0
    // [1, 1, 16, 16] = 8
    for (auto query: queries)
    {
        int top = query[0], left = query[1], bottom = query[2], right = query[3];
        printf("Query top = %d, left = %d, bottom = %d, right = %d\n", top, left, bottom, right);
        int Save = 0;
        int Le = 1;
        int Mid = 0;
        int Ri = min(bottom - top + 1, right - left + 1);
        while (Le <= Ri)
        {
            Mid = (Le + Ri) >> 1;
            printf("Le, Ri, Mid = %d, %d, %d\n", Le, Ri, Mid);
            if (Query(top + Mid - 1, left + Mid - 1, bottom, right) >= Mid)
            {
                Save = Mid;
                Le = Mid + 1;
            }
            else
            {
                Ri = Mid - 1;
            }
        }
        printf("[%d, %d, %d, %d] = %d\n", top, left, bottom, right, Save);
        // cout << Save << '\n';
    }
    return 0;
}

int main() {
    // test();
    // return 0;

    // vector<vector<int>> grid = {
    //     {1, 1, 0, 1},
    //     {0, 1, 1, 0},
    //     {0, 1, 1, 0},
    // };
    // vector<vector<int>> queries = {
    //     {1, 1, 2, 3},
    //     {2, 1, 3, 2},
    //     {3, 2, 3, 4},
    //     {1, 1, 3, 4},
    //     {1, 2, 3, 4}
    // };

    srand(0);
    int rows = 15;
    int cols = 15;
    vector<vector<int>> grid(rows, vector<int>(cols, 0));
    for (int y = 0; y < rows; y++) {
        for (int x = 0; x < cols; x++) {
            grid[y][x] = (rand() % 100) > 2 ? 1 : 0;
            // grid[y][x] = 1;
        }
    }
    print_table(grid);
    vector<vector<int>> queries = {
        {1, 1, 16, 16}
    };
    foo(grid, queries);
    printf("%d\n", brute_ones(grid, queries[0][0] - 1, queries[0][1] - 1, queries[0][2] - 1, queries[0][3] - 1));
    return 0;

    Grid max_grid = get_max_grid(grid);
    SparseTable table(max_grid);
    print_table(table._dp);
    

    return 0;

    for (auto query : queries) {
        int a = table.query(query[0] - 1, query[1] - 1, query[2] - 1, query[3] - 1);
        int b = brute_ones(grid, query[0] - 1, query[1] - 1, query[2] - 1, query[3] - 1);
        if (a != b) {
            printf("Error: [%d,%d,%d,%d] a = %d, b = %d\n", query[0], query[1], query[2], query[3], a, b);
        }
    }
    return 0;
}