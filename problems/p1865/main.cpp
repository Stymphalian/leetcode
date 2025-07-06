#include <cstdio>
#include <cmath>
#include <vector>
#include <climits>
#include <string>
#include <algorithm>
#include <unordered_map>
#include <cassert>

using namespace std;


class FindSumPairs {
public:
    vector<int> _nums1;
    vector<int> _nums2;
    unordered_map<int, int> _nums2_count;
    FindSumPairs(vector<int>& nums1, vector<int>& nums2) {
        _nums1 = nums1;
        _nums2 = nums2;

        for (int i = 0; i < nums2.size(); i++) {
            _nums2_count[nums2[i]] += 1;
        }
    }
    
    void add(int index, int val) {
        _nums2_count[_nums2[index]] -= 1;
        _nums2[index] += val;
        _nums2_count[_nums2[index]] += 1;
    }

    int count(int tot) {
        int answer = 0;
        for(int i = 0; i < _nums1.size(); i++) {
            int target = tot - _nums1[i];
            answer += _nums2_count[target];
        }
        return answer;
    }
};

int brute(vector<int>& nums1, vector<int>& nums2, int target) {
    int count = 0;
    for(int i = 0; i< nums1.size(); i++) {
        for (int j = 0; j < nums2.size(); j++) {
            if (nums1[i] + nums2[j] == target) {
                count += 1;
            }
        }
    }
    return count;
}


int main() {

    // vector<int> n = {1,1,2,2,3,3,3};
    // int a = bisect_left(n, 3);
    // int b = bisect_right(n, 3);
    // printf("%d %d\n", a, b);
    // return 0;


    vector<int> nums1 = {1,1,2,2,2,3};
    vector<int> nums2 = {1,4,5,2,5,4};
    FindSumPairs solution(nums1, nums2);
    vector<string> instructions = {
        "count","add","count","count","add","add","count"
    };
    vector<vector<int>> input = {
        {7},{3,2},{8},{4},{0,1},{1,1},{7}
    };
    for (int i = 0; i < input.size(); i++) {
        if (instructions[i] == "count") {
            int a = solution.count(input[i][0]);
            int b = brute(nums1, nums2, input[i][0]);
            if (a != b) {
                printf("Error: [%d] a = %d, b = %d\n", input[i][0], a, b);
            }
        } else {
            solution.add(input[i][0], input[i][1]);
            nums2[input[i][0]] += input[i][1];
        }
    }
}