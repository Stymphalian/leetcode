public class Solution {
    const int MOD = 1_000_000_000 + 7;

    public int solve(int n, int x, int last, Dictionary<(int,int), int> memo) {
        if (n < 0) { return 0; }
        if (n == 0) { return 1; }
        ulong power = (ulong) Math.Pow(last, x);
        if (power > (ulong)n) { return 0; }
        if (memo.TryGetValue((n, last), out int value)) {
            return value;
        }

        int count = 0;
        count = (count + solve(n - (int) power, x, last+1, memo)) % MOD;
        count = (count + solve(n, x, last + 1,memo)) % MOD;

        memo[(n, last)] = count;
        return count;
    }


    public int NumberOfWays(int n, int x) {
        return solve(n, x, 1, []);
    }
}