using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

public class Solution {

    public (int left, int right) run(int[] fruits, int index) {
        int left = index;
        int right = index + 1;
        while (right < fruits.Length && fruits[left] == fruits[right]) {
            right += 1;
        }
        return (left, right - 1);
    }

    public int TotalFruit(int[] fruits) {
        if (fruits.Length <= 2) {
            return fruits.Length;
        }


        var fruit1 = run(fruits, 0);
        var fruit2 = run(fruits, fruit1.right + 1);
        if (fruit2.right >= fruits.Length) {
            return fruits.Length;
        }

        var left = fruit1.left;
        var right = fruit2.right + 1;
        var best = right - left;
        while (right < fruits.Length) {
            var current = run(fruits, right);
            var fi = fruits[current.right];
            var f1 = fruits[fruit1.right];
            var f2 = fruits[fruit2.right];

            if (fi != f1 && fi != f2) {
                if (fruit1.left > fruit2.left) {
                    fruit2 = current;
                } else {
                    fruit1 = fruit2;
                    fruit2 = current;
                }
                left = fruit1.left;
                right = current.right + 1;
            } else if (fi == f1) {
                fruit1 = current;
                right = current.right + 1;
            } else if (fi == f2) {
                fruit2 = current;
                right = current.right + 1;
            }

            best = Math.Max(right - left, best);
        }
        return best;
    }
}

public class MainClass {
    record Case(int[] Nums);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case([1,2,1]), // 3
            new Case([0,1,2,2]), // 3
            new Case([1,2,3,2,2]), // 4
            new Case([0,0,1,1]), // 4
            new Case([0,1,1,1,1,1,1]), // 7
            new Case([0,0,0,0,0,0,1,2]), // 7
            new Case([3,3,3,3,3,3]), // 6
            new Case([0,1,6,6,4,4,6]) // 5
        };

        foreach (var c in cases) {
            Console.WriteLine(s.TotalFruit(c.Nums));
        }
    }

}
