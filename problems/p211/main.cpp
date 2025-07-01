#include <cstdio>
#include <cmath>
#include <vector>
#include <climits>
#include <string>
#include <algorithm>
#include <unordered_map>

using namespace std;


class TrieNode {
public:
    bool isEndOfWord;
    std::unordered_map<char, TrieNode*> children;

    TrieNode() {
        isEndOfWord = false;
    }

    ~TrieNode() {
        for (auto& pair : children) {
            delete pair.second;
        }
    }
};

class WordDictionary {
private:
    TrieNode* root;

public:
    WordDictionary() {
        root = new TrieNode();
    }

    ~WordDictionary() {
        delete root;
    }

    // Inserts a word into the Trie.
    void addWord(string word) {
        TrieNode* node = root;
        for (char ch : word) {
            if (!node->children.count(ch)) {
                node->children[ch] = new TrieNode();
            }
            node = node->children[ch];
        }
        node->isEndOfWord = true;
    }

    bool searchFromNode(string word, TrieNode* node) {
        for (int ci = 0; ci < word.size(); ci++) {
            char ch = word[ci];
            if (ch == '.') {
                for (auto& pair : node->children) {
                    string substr = word.substr(ci + 1);
                    TrieNode* child = pair.second;
                    if (searchFromNode(substr, child)) {
                        return true;
                    }
                }
                return false;
            }

            if (!node->children.count(ch)) {
                return false;
            }
            node = node->children.at(ch);
        }
        return node->isEndOfWord;
    }

    // Returns true if the word is in the Trie.
    bool search(string word) {
        return searchFromNode(word, root);
    }
};

/**
 * Your WordDictionary object will be instantiated and called as such:
 * WordDictionary* obj = new WordDictionary();
 * obj->addWord(word);
 * bool param_2 = obj->search(word);
 */

int main() {
    vector<string> instructions = {"WordDictionary","addWord","addWord","addWord","search","search","search","search"};
    vector<vector<string>> input = {{},{"bad"},{"dad"},{"mad"},{"pad"},{"bad"},{".ad"},{"b.."}};

    WordDictionary* obj = nullptr;
    for (int i = 0; i < instructions.size(); i++) {
        if (instructions[i] == "WordDictionary") {
            obj = new WordDictionary();
            continue;
        }
        if (instructions[i] == "addWord") {
            obj->addWord(input[i][0]);
            continue;
        }
        if (instructions[i] == "search") {
            bool param_2 = obj->search(input[i][0]);
            printf("Search %s: %d\n", input[i][0].c_str(), param_2);
            continue;
        }
    }
    delete obj;
    return 0;
}