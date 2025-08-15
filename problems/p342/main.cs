
// Power of Four
// https://leetcode.com/problems/power-of-four/description
public class Solution
{
    public bool IsPowerOfFour(int n2)
    {
        uint n = (uint)Math.Abs((uint)n2);
        if (n == 0) { return false; }

        bool isPower2 = (n & (n - 1)) == 0;
        if (!isPower2) { return false; }

        return n % 3 == 1;
    }
}