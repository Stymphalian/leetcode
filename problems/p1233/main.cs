using System.Collections;
using System.Reflection.Metadata;

public class Trie {
    public Dictionary<string, Trie> children = new();
    public bool isEnd = false;

    public void Insert(string[] parts) {
        Trie current = this;
        foreach (var part in parts) {
            if (!current.children.ContainsKey(part)) {
                current.children[part] = new Trie();
            }
            current = current.children[part];
        }
        current.isEnd = true;
    }
};

public class Solution {


    public void DFS(Trie trie, ref List<string> answer, string prefix = "") {
        foreach (var child in trie.children) {
            if (child.Value.isEnd) {
                answer.Add(prefix + child.Key);
            } else {
                DFS(child.Value, ref answer, prefix + child.Key + "/");
            }
        }
    }

    public IList<string> RemoveSubfolders(string[] folder) {
        Trie trie = new();
        foreach (var root in folder) {
            string[] parts = root.Split("/");
            trie.Insert(parts);
        }

        List<string> answer = new();
        DFS(trie, ref answer);
        return answer;
    }
}

public class MainClass {

    record Case(string[] Folders);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(["/a","/a/b","/c/d","/c/d/e","/c/f"]),  // 5
            new Case(["/a","/a/b/c","/a/b/d"]),
            new Case(["/a/b/c","/a/b/ca","/a/b/d"]),
        };

        foreach (var c in cases) {
            foreach (var f in s.RemoveSubfolders(c.Folders)) {
                Console.WriteLine(f);
            }
            Console.WriteLine("");
        }
    }

}