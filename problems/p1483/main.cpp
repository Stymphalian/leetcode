#include <cstdio>
#include <cmath>
#include <vector>
#include <climits>
#include <string>
#include <algorithm>
#include <unordered_map>

using namespace std;


const int LIMIT = log(50000) / log(2) + 1;

class TreeAncestor {
public:
    vector<vector<int>> _ups;

    TreeAncestor(int n, vector<int>& parents) {
        _ups = vector<vector<int>>(n, vector<int>(LIMIT, -1));
        for (int node = 0; node < n; node++) {
            _ups[node][0] = parents[node];
        }
        for (int level = 1; level < LIMIT; level++) {
            for (int node = 0; node < n; node++) {
                int parent = _ups[node][level-1];
                if (parent != -1) {
                    _ups[node][level] = _ups[parent][level-1];
                }
            }
        }
    }

    int brute(int node, int k) {
        int count = 0;
        int current = node;
        while (count < k && current != -1) {
            count += 1;
            current = _ups[current][0];
        }
        return current;
    }
    
    int getKthAncestor(int node, int k) {

        // int current = node;
        // int limit = LIMIT;
        // int current_index = (1 << limit);
        // while (current_index != k && current_index >= 0 && current != -1) {
        //     if (current_index <= k) {
        //         current = _ups[current][limit];
        //         current_index += (1 << limit);
        //     } else {
        //         limit -= 1;
        //         current_index -= (1 << limit);
        //     }
        // }
        // if (limit < 0 || current == -1) {
        //     return current;
        // } else {
        //     return _ups[current][limit];
        // }

        for(int i = LIMIT; i >= 0; i--){ // remember to check the bit from TOP!!!!!!!!
            // int num = pow(2, i); // we don't think bit, just see if we can jump to [num th] ancestor
            int num = (1 << i);
            if(k >= num){        // if we can
                node = _ups[node][i];
                k -= num;         // we jump back, so the rest step is [k - num]
                if(node == -1) return -1;				
            }
        }
        return node;
    }
};

int main() {
    // vector<int> parents = {-1, 0, 0, 1, 1, 2, 2};
    vector<int> parents = {
        -1, 
        0, 0, 
        1, 1, 2, 2,
        3, 3, 4, 4, 5, 5, 6, 6, 
        7, 7, 8, 8, 9, 9, 10, 10, 11,11, 12,12, 13,13, 14,14,
        15,15, 16,16, 17,17, 18,18, 19,19, 20,20, 21,21, 22,22, 23,23, 24,24, 25,25, 26,26, 27,27, 28,28, 29,29, 30,30
    };
    TreeAncestor tree(parents.size(), parents);
    vector<pair<int,int>> queries = {
        {3,1}, {5,2}, {6,3}, 
        {30, 3}
    };

    int limit = log(parents.size()) / log(2);
    for (int n = 0; n < parents.size(); n++) {
        for (int depth = 0; depth < limit; depth++) {
            int a = tree.getKthAncestor(n, depth);
            int b = tree.brute(n, depth);
            if (a != b) {
                printf("%d,%d\n", n, depth);
            }
        }
    }
    // for (auto q : queries) {
    //     int a = tree.getKthAncestor(q.first, q.second);
    //     int b = tree.brute(q.first, q.second);
    //     printf("%d:%d\n", a, b);
    // }
    return 0;
}