#include <cstdio>
#include <cmath>
#include <vector>
#include <climits>
#include <string>
#include <algorithm>
#include <unordered_map>
#include <unordered_set>

using namespace std;

class Solution {
public:
    string getHint(string secret, string guess) {
        int n = secret.size();
        unordered_map<char, int> alphabet;
        for(char c: secret) {
            alphabet[c] += 1;
        }

        int correct_pos = 0;
        int correct_digit = 0;
        for(int i = 0; i < n; i++) {
            char c = guess[i];
            if (guess[i] == secret[i]) {
                correct_pos += 1;
                alphabet[c] -= 1;
            }
        }
        for (int i = 0; i < n; i++) {
            char c = guess[i];
            if (guess[i] == secret[i]) {continue;}
            if (alphabet[c] > 0) {
                alphabet[c] -= 1;
                correct_digit += 1;
            }
        }

        return to_string(correct_pos) + "A" + to_string(correct_digit) + "B";
    }
};

int main() {
    Solution s;

    vector<vector<string>> cases = {
        {"1807", "7810"},
        {"1123", "0111"},
        {"1122", "1222"},
    };
    for (auto c : cases) {
        printf("%s\n", s.getHint(c[0], c[1]).c_str());
    }
    return 0;
}