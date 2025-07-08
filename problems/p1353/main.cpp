#include <cstdio>
#include <cmath>
#include <vector>
#include <climits>
#include <string>
#include <algorithm>
#include <unordered_map>
#include <queue>

using namespace std;

class Solution {
public:
    int maxEvents(vector<vector<int>>& events) {
        int n = events.size();
        int max_end_day;
        for(int i = 0; i < n; i++) {
            max_end_day = max(max_end_day, events[i][1]);
        }
        sort(events.begin(), events.end());
        priority_queue<int, vector<int>, greater<int>> pq;

        int index = 0;
        int count = 0;
        for(int day = 0; day <= max_end_day; day++) {
            while (index < n && events[index][0] <= day) {
                pq.push(events[index][1]);
                index += 1;
            }

            while (!pq.empty() && pq.top() < day) {
                pq.pop();
            }

            if (!pq.empty()) {
                pq.pop();
                count += 1;
            }
        }
        return count;
    }
};

int main() {
    Solution s;

    vector<vector<vector<int>>> cases = {
        {{1,2},{1,2},{1,2}},                    // 2
        {{1,2},{2,3},{3,4}},                    // 3
        {{1,2},{2,3},{3,4},{1,2}},              // 4
        {{1,2},{1,2},{3,3},{1,5},{1,5}},        // 5
        {{1,5},{1,5},{1,5},{2,3},{2,3}},         // 5
        {{1,4},{1,4},{1,4},{4,5},{4,5},{4,5}},  // 5
        {{1,2},{1,2},{1,6},{1,2},{1,2}}  // 3
    };
    for (auto c : cases) {
        printf("%d\n", s.maxEvents(c));
    }
    return 0;
}
