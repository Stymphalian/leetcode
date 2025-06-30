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
    int bisect_left(vector<int>& nums, int target) {
        int left = 0;
        int right = nums.size() - 1;
        while (left < right) {
            int mid = left + (right - left) / 2;
            if (nums[mid] < target) {
                left = mid + 1;
            } else {
                right = mid;
            }
        }
        return left;
    }

    int findLHS(vector<int>& nums) {
        int best_left = 0;
        int best_right = 0;
        bool found = false;
        sort(nums.begin(), nums.end());

        for(int right = 0; right < nums.size(); right++) {
            int left = bisect_left(nums, nums[right]-1);
            if (abs(nums[right] - nums[left]) == 1) {
                found = true;
                if (abs(right - left) > abs(best_right - best_left)) {
                    best_left = left;
                    best_right = right;
                }
            }
        }
        if (found) {
            return abs(best_right - best_left) + 1;
        } else {
            return 0;
        }
    }
};

int main() {
    Solution s;

    vector<vector<int>> cases = {
        {1,3,2,2,5,2,3,7},
        {1,2,3,4},
        {1,1,1,1}
    };
    for (auto c : cases) {
        printf("%d\n", s.findLHS(c));
    }
    return 0;
}