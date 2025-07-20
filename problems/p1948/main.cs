using System.Collections;
using System.ComponentModel;
using System.IO.Compression;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;


public class Trie {
    public Dictionary<string, Trie> children = new();
    public bool isEnd = false;
    public bool isMarked = false;
    public string part = "";

    public Trie(string s = "") {
        part = s;
    }

    public bool IsLeaf() {
        return children.Count == 0;
    }

    public Trie Insert(IList<string> parts) {
        Trie current = this;
        foreach (var part in parts) {
            if (!current.children.ContainsKey(part)) {
                current.children[part] = new Trie(part);
            }
            current = current.children[part];
        }
        current.isEnd = true;
        return current;
    }

    public Trie? Find(IList<string> parts) {
        Trie current = this;
        foreach (var part in parts) {
            if (current.children.ContainsKey(part)) {
                current = current.children[part];
            } else {
                return null;
            }
        }
        return current;
    }

    public bool IsSame(Trie other) {
        if (children.Count != other.children.Count) {
            return false;
        }
        // if (children.Count == 0 && other.children.Count == 0) {
        //     return false;
        // }

        foreach (var child in children) {
            if (!other.children.ContainsKey(child.Key)) {
                return false;
            }
        }
        foreach (var child in other.children) {
            if (!children.ContainsKey(child.Key)) {
                return false;
            }
        }
        foreach (var child in children) {
            if (!child.Value.IsSame(other.children[child.Key])) {
                return false;
            }
        }

        return true;
    }

    public void Mark() {
        isMarked = true;
        foreach (var child in children) {
            child.Value.Mark();
        }
    }
};



public class Solution {

    public void DFS(Trie node, List<string> path, IList<IList<string>> answer) {
        if (node.isEnd) {
            answer.Add(path.ToList());
        }

        foreach (var child in node.children) {
            if (child.Value.isMarked) { continue; }
            path.Add(child.Key);
            DFS(child.Value, path, answer);
            path.RemoveAt(path.Count - 1);
        }
    }

    public IList<IList<string>> DeleteDuplicateFolder(IList<IList<string>> paths) {
        Trie trie = new Trie();
        List<Trie> nodes = new();
        foreach (var parts in paths) {
            nodes.Add(trie.Insert(parts));
        }

        // For each node, compare against every other node
        // If they have equal subfolders, mark them
        for (int i = 0; i < nodes.Count; i++) {
            for (int j = i + 1; j < nodes.Count; j++) {
                Trie n1 = nodes[i];
                Trie n2 = nodes[j];

                if (!n1.IsLeaf() && !n2.IsLeaf() && n1.IsSame(n2)) {
                    n1.Mark();
                    n2.Mark();
                }
            }
        }

        // Regenerate the list of subfolders
        List<string> path = new();
        IList<IList<string>> answer = new List<IList<string>>();
        DFS(trie, path, answer);

        return answer;
    }
}

public class MainClass {

    static void PrintArray(int[] nums) {
        foreach (var num in nums) {
            Console.Write("{0} ", num);
        }
        Console.WriteLine("");
    }

    record Case(string[][] Folders);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case([["a"],["d"],["d","a"]]),  // 5
            new Case([["a"],["c"],["d"],["a","b"],["c","b"],["d","a"]]),  // 5
            new Case([["a"],["c"],["a","b"],["c","b"],["a","b","x"],["a","b","x","y"],["w"],["w","y"]]),
            new Case([["a","b"],["c","d"],["c"],["a"]]),
        };

        foreach (var c in cases) {
            foreach (var f in s.DeleteDuplicateFolder(c.Folders)) {
                foreach (var t in f) {
                    Console.Write(t + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("------");
        }
    }

}
