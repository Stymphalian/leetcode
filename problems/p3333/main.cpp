#include <cstdio>
#include <cmath>
#include <vector>
#include <climits>
#include <string>
#include <algorithm>
#include <unordered_map>
#include <cassert>

using namespace std;

static const int MOD = pow(10,9) + 7;

void print_vector(vector<pair<char, int>>& v) {
    for (auto s : v) {
        printf("%c: %d ", s.first, s.second);
    }
    printf("\n");
}

void print_vector(vector<int>& v) {
    for (auto s : v) {
        printf("%d ", s);
    }
    printf("\n");
}

class Solution {
public:

    vector<pair<char, int>> get_segments(string word) {
        vector<pair<char, int>> segments;
        int segment_count = 1;
        for(int i = 1; i < word.size(); i++) {
            if (word[i] == word[i-1]) {
                segment_count += 1;
            } else {
                segments.push_back(make_pair(word[i-1], segment_count));
                segment_count = 1;
            }
        }
        if(segment_count > 0) {
            segments.push_back(make_pair(word[word.size()-1], segment_count));
        }
        return segments;
    }

    int get_lower_count(vector<pair<char, int>>& segments, int k) {
        vector<int> dp(k, 0);
        vector<int> prefix_sum(k+1, 0);
        dp[0] = 1;
        // print_vector(dp);
        
        for(int seg_index = 0; seg_index < segments.size(); seg_index++) {
            vector<int> next_dp(k, 0);
            
            prefix_sum[0] = 0;
            for (int i = 1; i < k; i++) {
                prefix_sum[i] = (prefix_sum[i-1] + dp[i-1]) % MOD;
            }

            for(int str_pos = 1; str_pos < k; str_pos++) {

                int v = segments[seg_index].second;
                int left = str_pos - v >= 0 ? prefix_sum[str_pos - v] : 0;
                int right = prefix_sum[str_pos];

                next_dp[str_pos] = (right - left) % MOD;

                // int count = 0;
                // for(int v = 1; v <= segments[seg_index].second; v++) {
                //     if (str_pos - v < 0) { break; }
                //     count = (count + dp[str_pos - v]) % MOD;
                // }
                // next_dp[str_pos] = count;
            }

            dp = next_dp;
            // print_vector(dp);
        }

        // print_vector(dp);

        int answer = 0;
        for (int i = 0; i < dp.size(); i++) {
            answer = (answer + dp[i]) % MOD;
        }
        return answer;
    }

    int possibleStringCount(string word, int k) {
        if (word.size() < k) {
            return 0;
        }
        if (word.size() == k) {
            return 1;
        }

        auto segments = get_segments(word);
        int n = word.size();
        int m = segments.size();

        long long total_count = 1;
        for (auto s : segments) {
            total_count = (total_count * s.second) % MOD;
        }
        assert(total_count > 0);

        if (segments.size() >= k) {
            return total_count;
        }

        int lower_count = get_lower_count(segments, k);
        int answer = (total_count - lower_count) % MOD;
        if (answer < 0) {
            answer = MOD + answer;
        }
        return answer;
    }
};

int main() {
    Solution s;

    vector<pair<string,int>> cases = {
        make_pair("aabbccdd", 7),  // 5
        make_pair("aabbccdd", 8),  // 1
        make_pair("aaabbb", 3),    // 8
        make_pair("aan", 2),       // 2
        make_pair("kkkkk", 4),     // 2
        make_pair("aaabbb", 4),    // 6
        make_pair("ccccttt", 6),   // 3
        make_pair("bbbbbyyyyyyyyyyccccccccyyyqqqqhffffhhhhhhhhsswwwwvvvvvlllldddddddddnnnnnnvr", 69), // 23761
        make_pair("ggggggggaaaaallsssssaaaaaaaaaiiqqqqqqqqqqbbbbbbbvvfffffjjjjeeeeeefffmmiiiix", 34), // 834168507
        make_pair("wwwwwwwbbbbssssssssvvoooooooqqqqqqqqqvvvvvooooooocccccppppkkkkkkjnddddddbbb", 50), // 497563975
        make_pair("aaabbb", 5), //3
    };
    for (auto c : cases) {
        printf("%d\n", s.possibleStringCount(c.first, c.second));
    }
    return 0;
}