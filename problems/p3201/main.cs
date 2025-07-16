// using Answer = (int Min, int Max);
// using Key = (int n, int p1, int p2);

using System.Collections;

public class Solution
{
    public int MaximumLength(int[] nums)
    {
        int evenCount = 0;
        int oddCount = 0;
        int mixCount = 0;
        int prevParity = -1;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] % 2 == 0)
            {
                evenCount += 1;
            }
            else
            {
                oddCount += 1;
            }
            if (prevParity != nums[i] % 2)
            {
                mixCount += 1;
            }
            prevParity = nums[i] % 2;
        }
        return Math.Max(Math.Max(evenCount, oddCount), mixCount);
    }
}


public class MainClass
{

    record Case(int[] Nums);

    public static void Main(string[] args)
    {
        Solution s = new Solution();
        Case[] cases = {
            new Case([1,2,3,4]),
            new Case([1,2,1,1,2,1,2]),
            new Case([1,3]),
            new Case([1,3,5,6,7,9,11,2,4,8]),
        };
        for (int ci = 0; ci < cases.Length; ci++)
        {
            Case c = cases[ci];
            var answer = s.MaximumLength(c.Nums);
            Console.WriteLine(string.Format("{0}", answer));
        }
    }

};
