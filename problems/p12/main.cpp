#include <cstdio>
#include <cmath>
#include <vector>
#include <climits>
#include <string>
#include <algorithm>
#include <unordered_map>

using namespace std;

class Solution {
public:
    string intToRoman(int num) {
        vector<int> values = {1, 5, 10, 50, 100, 500, 1000};
        vector<string> romans = {"I", "V", "X", "L", "C", "D", "M"};
        string answer = "";

        while (num > 0) {
            int tens = romans.size() - 1;
            for (tens; tens >= 0; tens -= 2) {
                if (num / values[tens] > 0) {
                    break;
                }
            }

            int digit = num / values[tens];
            if (digit == 4) {
                answer += romans[tens];
                answer += romans[tens+1];
                num -= digit * values[tens];
            } else if (digit == 9) {
                answer += romans[tens];
                answer += romans[tens+2];
                num -= digit * values[tens];
            } else {
                if (digit < 4) {
                    for (int i = 0; i < digit; i++) {
                        answer += romans[tens];
                    }
                    num -= digit * values[tens];
                } else {
                    int fives = tens + 1;
                    answer += romans[fives];
                    for (int i = 0; i < digit - 5; i++) {
                        answer += romans[tens];
                    }
                    num -= values[fives];
                    num -= (digit - 5) * values[tens];
                }
            }
        }

        return answer;
    }
};


int main() {
    Solution s;

    printf("%s\n", s.intToRoman(3749).c_str());
    printf("%s\n", s.intToRoman(58).c_str());
    printf("%s\n", s.intToRoman(1994).c_str());
    return 0;
}