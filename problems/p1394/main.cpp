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
    int findLucky(vector<int>& arr) {
        unordered_map<int, int> m;
        for (int value: arr) {
            m[value] += 1;
        }

        int biggest = -1;
        for(auto i : m) {
            if (i.first == i.second) {
                biggest = max(biggest, i.first);
            }
        }
        return biggest;
    }
};

int main() {
    vector<vector<int>> cases = {
        {2,2,3,4},
        {1,2,2,3,3,3},
        {2,2,2,3,3}
    };
    Solution s;
    for(auto c : cases) {
        printf("%d\n", s.findLucky(c));
    }
    
    return 0;
}