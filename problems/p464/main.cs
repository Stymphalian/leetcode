public class Solution {
    public void printDepth(int depth) {
        for (int i = 0; i < depth; i++) {
            Console.Write(" ");
        }
    }

    public int Sum(int n) {
        return n * (n + 1) / 2;
    }

    public bool solve(int maxChoosableInteger, int desiredTotal, int[] used, Dictionary<int, bool> dp) {
        int key = bitMapToInt(used);
        if (dp.TryGetValue(key, out bool value)) {
            return value;
        }

        int totalSum = 0;
        for (int i = 0; i < used.Length; i++) {
            if (used[i] == 0) {
                totalSum += i;
            }
        }
        if (totalSum < desiredTotal) {
            dp[key] = false;
            return false;
        }

        bool canWin = false;
        for (int choice = 1; choice <= maxChoosableInteger; choice++) {
            if (used[choice] == 1) { continue; }
            if (choice >= desiredTotal) {
                canWin = true;
                break;
            }
            used[choice] = 1;
            bool result = solve(maxChoosableInteger, desiredTotal - choice, used, dp);
            used[choice] = 0;

            if (result == false) {
                canWin = true;
                break;
            }
        }

        dp[key] = canWin;
        return canWin;
    }

    public int bitMapToInt(int[] used) {
        int result = 0;
        for (int i = 0; i < used.Length; i++) {
            result <<= 1;
            if (used[i] == 1) {
                result |= 1;
            }
        }
        return result;
    }

    public bool CanIWin(int maxChoosableInteger, int desiredTotal) {
        return solve(maxChoosableInteger, desiredTotal, new int[maxChoosableInteger + 1], []);
    }
}

public class MainClass {
    record Case(int N, int X);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(10, 11),  // false
            new Case(10, 0),  // true
            new Case(10, 1),  // true
            new Case(4, 6),    // true
            new Case(10, 40),   // false
            new Case(20, 300), // false
            new Case(5, 30),  // false
        };

        foreach (var c in cases) {
            Console.WriteLine(s.CanIWin(c.N, c.X));
        }
    }

}

