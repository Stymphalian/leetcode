#include <cstdio>
#include <cmath>
#include <vector>
#include <climits>
#include <string>
#include <algorithm>
#include <unordered_map>
#include <functional>

using namespace std;


void print_table(vector<vector<int>>& table) {
    for(int i = 0; i < table.size(); i++) {
        for(int j = 0; j < table[i].size(); j++) {
            printf("%2d ", table[i][j]);
        }
        printf("\n");
    }
}

class SparseTable {
public:
    vector<vector<int>> _dp;    // sparse table, constructed via DP
    int _n;                     // length of the array
    int _p;                     // largest 2**p <= n

    SparseTable(vector<int>& array) {
        _n = array.size();
        _p = (log(_n) / log(2)) + 1;
        build(array);
    }

    void build(vector<int>& array) {
        int num_cols = array.size();
        int num_rows = (log(num_cols) / log(2)) + 1;
        _dp = vector<vector<int>>(num_rows, vector<int>(num_cols));
        _dp[0] = array;

        for (int row = 1; row < num_rows; row++) {
            int current_len = 1 << row;
            int prev_len = (1 << (row-1));
            for (int col = 0; col < num_cols - current_len + 1; col++) {

                _dp[row][col] = func(
                    _dp[row-1][col], 
                    _dp[row-1][col + prev_len]
                );
            }
        }
        // print_table(_dp);
    }

    int query(int left, int right, int* best) {
        int m = (right - left) + 1;
        int power_two = (log(m) / log(2));

        int index = left;
        while (power_two >= 0) {
            bool is_bit_set = (m & (1 << power_two)) > 0;
            if (is_bit_set) {
                accumulate(_dp[power_two][index], best);
                index += (1 << power_two);
            }
            power_two -= 1;
        }
        return *best;
    }

    int func(int left, int right) {
        // return min(left, right);
        // return left * right;
        return (left + right);
    }
    void accumulate(int value, int* acc) {
        // *acc = min(*acc, value);
        // *acc = *acc * value;
        *acc += value;
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

int main() {
    srand(0);

    int n = 32;
    vector<int> array;
    for(int i = 0; i < n; i++) {
        array.push_back(rand() % 100);
    }
    // array = {1,2,-3,2,4,-1,5};
    // n = array.size();

    SparseTable sparse(array);
    // sparse.query(2, 8);
    // int best = 1;
    for(int left = 0; left < n; left++) {
        for(int right = left; right < n; right++) {
            int best = 0;
            int a = brute_sum(array, left, right);
            int b = sparse.query(left, right, &best);
            if (a != b) {
                printf("Error: [%d,%d] a = %d, b = %d\n", left, right, a, b);
            }
        }
    }

    return 0;
}