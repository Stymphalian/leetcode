#include <cstdio>
#include <cmath>
#include <vector>
#include <climits>
#include <string>
#include <algorithm>
#include <unordered_map>
#include <functional>
#include <cassert>

using namespace std;

using Grid = vector<vector<int>>;

void print_table(vector<int>& table) {
    for(int i = 0; i < table.size(); i++) {
        printf("%2d ", table[i]);
    }
    printf("\n");
}

void print_table(vector<vector<int>>& table) {
    for(int i = 0; i < table.size(); i++) {
        for(int j = 0; j < table[i].size(); j++) {
            printf("%2d ", table[i][j]);
        }
        printf("\n");
    }
}

void print_table(vector<vector<Grid>>& table) {
    for(int y = 0; y < table.size(); y++) {
        for (int x = 0; x < table[y].size(); x++) {
            printf("[%d][%d]\n", y, x);
            print_table(table[y][x]);
        }
        printf("\n");
    }
};

bool all_ones(Grid& grid, int left, int top, int w, int h) {
    for(int y = top; y < top + h; y++) {
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
    vector<vector<Grid>> _dp;    // sparse table, constructed via DP
    int _rows;
    int _cols;
    int _rows_power;
    int _cols_power;

    SparseTable(Grid& array) {
        _rows = array.size();
        _cols = array[0].size();
        _rows_power = int(log(_rows) / log(2)) + 1;
        _cols_power = int(log(_cols) / log(2)) + 1;
        
        build(array);
    }

    void build(Grid& grid) {
        _dp = vector<vector<Grid>>(
            _rows_power,
            vector<Grid>(
                _cols_power,
                Grid(_rows, vector<int>(_cols, 0))
            )
        );

        _dp[0][0] = grid;
        for(int py = 1; py < _rows_power; py++) {
            int prev_py = (1 << (py-1));
            int current_py = (1 << py);
            for (int y = 0; y < _rows - current_py + 1; y++) {
                for (int x = 0; x < _cols; x++) {

                    int a = _dp[py-1][0][y][x];
                    int b = _dp[py-1][0][y + prev_py][x];
                    _dp[py][0][y][x] = (a + b);
                }
            }
        }
        for(int px = 1; px < _cols_power; px++) {
            int prev_px = (1 << (px-1));
            int current_px = (1 << px);
            for (int y = 0; y < _rows; y++) {
                for (int x = 0; x < _cols - current_px + 1; x++) {

                    int a = _dp[0][px-1][y][x];
                    int b = _dp[0][px-1][y][x + prev_px];
                    _dp[0][px][y][x] = (a + b);
                }
            }
        }


        for (int py = 1; py < _rows_power; py++) {
            for(int px = 1; px < _cols_power; px++) {

                int prev_py = (1 << (py-1));
                int prev_px = (1 << (px-1));
                int current_py = (1 << py);
                int current_px = (1 << px);

                for (int y = 0; y < _rows - current_py + 1; y++) {
                    for (int x = 0; x < _cols - current_px + 1; x++) {

                        int a = get(py-1, px-1, y + prev_py, x + prev_px);
                        int b = get(py-1, px-1, y + prev_py, x);
                        int c = get(py-1, px-1, y, x + prev_px);
                        int d = get(py-1, px-1, y, x);
                        _dp[py][px][y][x] = (a + b + c+ d);
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
        for (int w_power: widths) {
            int y = top;
            for(int h_power: heights) {
                count += _dp[h_power][w_power][y][x];
                y += (1 << h_power);
            }
            x += (1 << w_power);
        }
        return count;
    }
};

int brute_min(vector<int>& array, int left, int right) {
    int best = INT_MAX;
    for(int i = left; i <= right; i++) {
        best = min(best, array[i]);
    }
    return best;
}

int brute_mult(vector<int>& array, int left, int right) {
    int best = 1;
    for(int i = left; i <= right; i++) {
        best = best * array[i];
    }
    return best;
}

int brute_sum(vector<int>& array, int left, int right) {
    int best = 0;
    for(int i = left; i <= right; i++) {
        best += array[i];
    }
    return best;
}


int brute_ones(vector<vector<int>>& grid, int top, int left, int bottom, int right) {
    int biggest_square = 0;
    int max_square = max(right - left + 1, bottom - top + 1);

    for(int w = 1; w <= max_square; w++) {
        for(int y = top; y <= bottom-w+1; y++) {
            for(int x = left; x <= right-w+1; x++) {
                if (all_ones(grid, x, y, w, w)) {
                    biggest_square = max(biggest_square, w);
                }
            }
        }
    }
    return biggest_square;
}

int brute_sum(vector<vector<int>>& grid, int top, int left, int bottom, int right) {
    int count = 0;
    for(int y = top; y <= bottom; y++) {
        for(int x = left; x <= right; x++) {
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
    for(int w = 0; w < cols; w++) {
        for(int h = 0; h < rows; h++) {
            for(int y = 0; y < rows-h; y++) {
                for(int x = 0; x < cols-w; x++) {
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

int main() {
    // test();
    // return 0;

    vector<vector<int>> grid = {
        {1,1,0,1},
        {0,1,1,0},
        {0,1,1,0},
    };
    vector<vector<int>> queries = {
        {1,1,2,3},
        {2,1,3,2},
        {3,2,3,4},
        {1,1,3,4},
        {1,2,3,4}
    };
    SparseTable table(grid);
    print_table(table._dp);

    for(auto query: queries) {
        int a = table.query(query[0]-1, query[1]-1, query[2]-1, query[3]-1);
        int b = brute_ones(grid, query[0]-1, query[1]-1, query[2]-1, query[3]-1);
        if (a != b) {
            printf("Error: [%d,%d,%d,%d] a = %d, b = %d\n", query[0], query[1], query[2], query[3], a, b);
        }
    }
    return 0;
}