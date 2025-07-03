#include <cstdio>
#include <cmath>
#include <vector>
#include <climits>
#include <string>
#include <algorithm>
#include <unordered_map>
#include <cassert>

using namespace std;

class Solution {
public:
    char kthCharacter_brute(int k) {
        vector<int> word = {0};
        while (word.size() < k) {
            int word_size = word.size();
            for(int i = 0; i < word_size; i++) {
                word.push_back((word[i] + 1)%26);
            }
        }
        return 'a' + word[k-1];
    }

    char kthCharacter_binary(unsigned k) {
        if (k == 1) {
            return 'a';
        }

        // find the largest power of two which makes k.
        int power_of_two = 1;
        while ((1 << power_of_two) < k) {
            power_of_two += 1;
        }
        power_of_two -= 1;

        // Recursively find the value of k until we reach 1.
        char c = kthCharacter_binary(k - (1 << power_of_two));
        return ((c - 'a') + 1 %26) + 'a';
    }

    char kthCharacter(int k) {
        return kthCharacter_binary(k);
    }
};

int main() {
    Solution s;

    vector<int> cases = {5, 10, 500};
    for (auto c : cases) {
        printf("%c\n", s.kthCharacter(c));
        printf("%c\n", s.kthCharacter_brute(c));
    }
    return 0;
}