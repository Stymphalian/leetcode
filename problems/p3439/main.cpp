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

    int editorial(int eventTime, int k, vector<int>& startTime, vector<int>& endTime) {
        // O(1) space, O(n) time
        int n = startTime.size();
        int right = 0;
        int best_gap = 0;
        int meeting_sum = 0;
        for(right = 0; right < n; right++) {
            meeting_sum += (endTime[right] - startTime[right]);
            int left_start = (right <= k-1) ? 0 : endTime[right - k];
            int right_end = (right == n-1) ? eventTime : startTime[right + 1];
            int space = (right_end - left_start);
            int gap = space - meeting_sum;
            best_gap = max(best_gap, gap);

            if (right >= k-1) {
                int left_index = right - k + 1;
                meeting_sum -= (endTime[left_index] - startTime[left_index]);
            }
        }
        return best_gap;
    }

    int maxFreeTime(int eventTime, int k, vector<int>& startTime, vector<int>& endTime) {
        int window_size = k + 1;
        int n = startTime.size();

        vector<long long> gaps;
        gaps.push_back(0);
        long long prev_end = 0;
        long long running_sum = 0;
        long long biggest_sum = 0;
        for(int ei = 0; ei < n; ei++) {
            long long gap = static_cast<long long>(startTime[ei]) - prev_end;
            prev_end = static_cast<long long>(endTime[ei]);
            running_sum += gap;
            gaps.push_back(running_sum);
            if (gaps.size()-1 >= window_size) {
                int right = gaps.size()-1;
                int left = gaps.size()-1-window_size;
                biggest_sum = max(biggest_sum, gaps[right] - gaps[left]);
            }
        }
        gaps.push_back(running_sum + eventTime - static_cast<long long>(endTime[n-1]));
        if (gaps.size()-1 >= window_size) {
            int right = gaps.size()-1;
            int left = gaps.size()-1-window_size;
            biggest_sum = max(biggest_sum, gaps[right] - gaps[left]);
        }
        return biggest_sum;
    }
};

struct Case {
    int eventTime;
    int k;
    vector<int> startTime;
    vector<int> endTime;
    Case(int eventTime, int k, vector<int> startTime, vector<int> endTime) : eventTime(eventTime), k(k), startTime(startTime), endTime(endTime) {}
};

int main() {
    Solution s;

    vector<Case> cases = {
        Case(5, 1, {1,3}, {2,5}),              // 2 
        Case(10, 1, {0,2,9}, {1,4,10}),        // 6
        Case(5, 2, {0,1,2,3,4}, {1,2,3,4,5}),  // 0 
        Case(34, 2, {0,17}, {14,19}),          // 18
        Case(21, 1, {7,10,16}, {10,14,18}),    // 7
    };
    for (auto c : cases) {
        printf("%d\n", s.maxFreeTime(c.eventTime, c.k, c.startTime, c.endTime));
    }
    return 0;
}