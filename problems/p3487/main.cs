using System.Globalization;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text;

public class Solution {
    public int MaxSum(int[] nums) {
        var t = nums.ToHashSet();
        t.RemoveWhere(x => x <= 0);
        if (t.Count == 0) {
            return nums.Max();
        }
        return t.Sum();
    }
}

public class MainClass {

    static void PrintArray(int[] nums) {
        foreach (var num in nums) {
            Console.Write("{0} ", num);
        }
        Console.WriteLine("");
    }

    record Case(int[] Nums);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case([1,2,3,4,5]), // 15
            new Case([1,1,0,1,1]),  // 1
            new Case([1,2,-1,-2,1,0,-1]), // 3
            new Case([-1,-2,-3,-4,-5]), // -1
        };

        foreach (var c in cases) {
            Console.WriteLine(s.MaxSum(c.Nums));
        }
    }

}
