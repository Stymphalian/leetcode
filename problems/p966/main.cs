// 966 Vowel Spellchecker
// https://leetcode.com/problems/vowel-spellchecker/description/
// Time Taken: 02:45:15
// Difficulty: Medium
// I used tries, and got super confused with the requirements. Especially the "first such match"
// Editorial is very elegant.. if not a little space inefficient. Makes sense
// To just have an unique key with vowels replaced with '*'

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class Solution {
    static char[] VOWELS = ['a', 'e', 'i', 'o', 'u'];
    static char[] CAPTIAL_VOWELS = ['A', 'E', 'I', 'O', 'U'];

    public static bool IsVowel(char c) {
        return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' ||
                c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U';
    }

    class Trie {
        Dictionary<char, Trie> _children;
        bool _isWord;
        int _index;

        public Trie() {
            _children = [];
            _isWord = false;
            _index = int.MaxValue;
        }

        public Trie? Get(char c) {
            if (_children.ContainsKey(c)) {
                return _children[c];
            } else {
                return null;
            }
        }

        public void Add(string word, int index) {
            var current = this;
            foreach (char c in word) {
                if (!current._children.ContainsKey(c)) {
                    current._children[c] = new Trie();
                }
                current = current._children[c];
            }
            current._isWord = true;
            current._index = Math.Min(index, current._index);
        }


        public int DFS(Trie? current, string word, int index, bool checkDirect, bool checkCapitals, bool checkVowels) {
            if (current == null) {
                return int.MaxValue;
            }
            if (index >= word.Length) {
                if (current._isWord) {
                    return current._index;
                } else {
                    return int.MaxValue;
                }
            }
            char target = word[index];
            char lower = Char.ToLower(target);
            char upper = Char.ToUpper(target);
            char other = lower == target ? upper : lower;

            int found = int.MaxValue;
            int best = int.MaxValue;

            if (checkDirect) {
                if (current.Get(target) != null) {
                    found = DFS(current.Get(target), word, index + 1, checkDirect, checkCapitals, checkVowels);
                    best = Math.Min(best, found);
                }
            }

            if (checkCapitals) {
                if (current.Get(other) != null) {
                    found = DFS(current.Get(other), word, index + 1, checkDirect, checkCapitals, checkVowels);
                    best = Math.Min(best, found);
                }
            }


            if (IsVowel(target) && checkVowels) {
                if (IsVowel(target)) {
                    foreach (var vowel in VOWELS) {
                        if (current.Get(vowel) != null) {
                            found = DFS(current.Get(vowel), word, index + 1, checkDirect, checkCapitals, checkVowels);
                            best = Math.Min(best, found);
                        }
                    }
                    foreach (var vowel in CAPTIAL_VOWELS) {
                        if (current.Get(vowel) != null) {
                            found = DFS(current.Get(vowel), word, index + 1, checkDirect, checkCapitals, checkVowels);
                            best = Math.Min(best, found);
                        }
                    }
                }
            }

            return best;
        }

        public int FindDirectMatch(string word) {
            return DFS(this, word, 0, true, false, false);
        }
        public int FindCapitals(string word) {
            return DFS(this, word, 0, true, true, false);
        }
        public int FindAny(string word) {
            return DFS(this, word, 0, true, true, true);
        }

        public int Find(string word) {
            int found = FindDirectMatch(word);
            if (found != int.MaxValue) {
                return found;
            }

            found = FindCapitals(word);
            if (found != int.MaxValue) {
                return found;
            }

            found = FindAny(word);
            if (found != int.MaxValue) {
                return found;
            }

            return int.MaxValue;
        }
    };

    public string[] Mine(string[] wordlist, string[] queries) {
        Trie root = new();
        // HashSet<string> directs = [];
        for (int index = 0; index < wordlist.Length; index++) {
            root.Add(wordlist[index], index);
            // directs.Add(wordlist[index]);
        }

        List<string> answers = [];
        foreach (var query in queries) {
            // if (directs.Contains(query)) {
            //     answers.Add(query);
            //     continue;
            // }
            int found = root.Find(query);
            if (found == int.MaxValue) {
                answers.Add("");
            } else {
                answers.Add(wordlist[found]);
            }
        }

        return answers.ToArray();
    }

    public string DeVowel(string word) {
        string lower = word.ToLower();
        StringBuilder sb = new();

        foreach (var c in lower) {
            if (IsVowel(c)) {
                sb.Append('*');
            } else {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }


    public string[] Spellchecker(string[] wordlist, string[] queries) {
        HashSet<string> directs = new();
        Dictionary<string, string> capitals = new();
        Dictionary<string, string> vowels = new();

        foreach (var word in wordlist) {
            directs.Add(word);
            capitals.TryAdd(word.ToLower(), word);
            vowels.TryAdd(DeVowel(word), word);
        }

        List<string> answers = [];
        foreach (var query in queries) {
            if (directs.Contains(query)) {
                answers.Add(query);
                continue;
            }

            string lower = query.ToLower();
            if (capitals.ContainsKey(lower)) {
                answers.Add(capitals[lower]);
                continue;
            }

            string noVowels = DeVowel(query);
            if (vowels.ContainsKey(noVowels)) {
                answers.Add(vowels[noVowels]);
                continue;
            }

            answers.Add("");
        }

        return answers.ToArray();
    }
}

public class MainClass {
    record Case(string[] wordList, string[] queries);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(["KiTe","kite","hare","Hare"], ["kite","Kite","KiTe","Hare","HARE","Hear","hear","keti","keet","keto"]),
            new Case(["yellow"],["YellOw"]),
            new Case(["zeo", "Zuo"], ["zuo"]),
        };

        foreach (var c in cases) {
            var arr = s.Spellchecker(c.wordList, c.queries);
            Console.WriteLine(string.Join(", ", arr));
        }
    }
}

