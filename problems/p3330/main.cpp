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
    int possibleStringCount(string word) {
        int answer = 1;
        for(int i = 0; i < word.size(); i++) {
            char c = word[i];
            if (i > 0 && word[i-1] == c) {
                answer += 1;
            }
        }
        return answer;
    }
};

int main() {
    Solution s;

    vector<string> cases = {
        "abbcccc",
        "abcd",
        "aaaa",
        "ere"
    };
    for (auto c : cases) {
        printf("%d\n", s.possibleStringCount(c));
    }
    return 0;
}