
public class Trie {
    public Dictionary<string, Trie> Children = new();
    public bool IsEnd = false;
    public bool IsMarked = false;
    public string Signature = "";

    public bool IsLeaf() {
        return Children.Count == 0;
    }

    public Trie Insert(IList<string> parts) {
        Trie current = this;
        foreach (var part in parts) {
            if (!current.Children.ContainsKey(part)) {
                current.Children[part] = new Trie();
            }
            current = current.Children[part];
        }
        current.IsEnd = true;
        return current;
    }
};


public class Solution {

    public void DFS(Trie node, List<string> path, IList<IList<string>> answer) {
        if (node.IsEnd) {
            answer.Add(path.ToList());
        }

        foreach (var child in node.Children) {
            if (child.Value.IsMarked) { continue; }
            path.Add(child.Key);
            DFS(child.Value, path, answer);
            path.RemoveAt(path.Count - 1);
        }
    }

    public string GetSignatures(Trie node, Dictionary<string, int> sigs) {
        var keys = node.Children.Keys.ToList();
        keys.Sort();

        // TODO: Find a better way to generate the signature.
        // A integer hash based approach should be more appropriate (merkle tree?)
        string sig = "";
        foreach (var key in keys) {
            sig += key + "(" + GetSignatures(node.Children[key], sigs) + ")";
        }

        if (!sigs.ContainsKey(sig)) {
            sigs[sig] = 0;
        }
        sigs[sig] += 1;
        node.Signature = sig;
        return sig;
    }

    public void MarkNodes(Trie node, Dictionary<string, int> sigs) {
        if (node.Signature != "" && sigs[node.Signature] >= 2) {
            node.IsMarked = true;
        }
        foreach (var child in node.Children) {
            MarkNodes(child.Value, sigs);
        }
    }

    public IList<IList<string>> DeleteDuplicateFolder(IList<IList<string>> paths) {
        Trie trie = new Trie();
        foreach (var parts in paths) {
            trie.Insert(parts);
        }

        // Calculate the signatures for every node
        Dictionary<string, int> sigs = new();
        GetSignatures(trie, sigs);

        // Mark nodes which have dupcliate signatures
        MarkNodes(trie, sigs);

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
