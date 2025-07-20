using System.Collections;
using System.ComponentModel;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

namespace Practice {
    public class FenwickTree {
        public int[] Tree;

        public FenwickTree(int[] nums) {
            Tree = new int[nums.Length];
            Construct(nums);
        }

        void Construct(int[] nums) {
            nums.CopyTo(Tree, 0);
            for (int index = 1; index <= nums.Length; index++) {
                int lsb = BitOperations.TrailingZeroCount(index);
                int parent = index + (1 << lsb);
                if (parent <= Tree.Length) {
                    Tree[parent - 1] += Tree[index - 1];
                }
            }
        }

        public int Sum(int index) {
            index += 1;
            int count = 0;
            while (index > 0) {
                count += Tree[index - 1];
                int lsb = BitOperations.TrailingZeroCount(index);
                index -= (1 << lsb);
            }
            return count;
        }

        public int SumRange(int left, int right) {
            int leftSum = Sum(left - 1);
            int rightSum = Sum(right);
            return rightSum - leftSum;
        }

        public void Add(int index, int value) {
            index += 1;
            while (index <= Tree.Length) {
                Tree[index - 1] += value;
                int lsb = BitOperations.TrailingZeroCount(index);
                index += (1 << lsb);
            }
        }

        public int Get(int index) {
            return SumRange(index, index);
        }

        public void Set(int index, int value) {
            Add(index, -Get(index) + value);
        }
    }

    public class MainClass {

        static int bruteRangeSum(int[] nums, int left, int right) {
            int sum = 0;
            for (int i = left; i <= right; i++) {
                sum += nums[i];
            }
            return sum;
        }
        
        static void PrintArray(int[] nums) {
            foreach (var num in nums) {
                Console.Write("{0} ", num);
            }
            Console.WriteLine("");
        }

        public static void Main(String[] args) {
            var rng = new Random(0);

            for (int N = 10; N < 50; N++) {
                int[] nums = new int[N];
                for (int i = 0; i < N; i++) {
                    nums[i] = rng.Next(-100, 100);
                }
                PrintArray(nums);

                FenwickTree tree = new FenwickTree(nums);

                for (int change = 0; change < 1000; change++) {
                    int pos = rng.Next(0, N);
                    int val = rng.Next(-10, 10);

                    tree.Add(pos, val);
                    nums[pos] += val;
                    // tree.Set(pos, val);
                    // nums[pos] = val;

                    for (int left = 0; left < N; left++) {

                        if (tree.Get(left) != nums[left]) {
                            Console.WriteLine("Error {0} [{1}] got={2} want={3}", N, left, tree.Get(left), nums[left]);
                            return;
                        }

                        for (int right = left + 1; right < N; right++) {
                            var got = tree.SumRange(left, right);
                            int want = bruteRangeSum(nums, left, right);
                            if (got != want) {
                                Console.WriteLine("Error {0} [{1},{2}] got={3} want={4}", N, left, right, got, want);
                                return;
                            }
                        }
                    }

                }
            }
        }

        // record Case(string[] Folders);

        // public static void Main(String[] args) {
        //     Solution s = new Solution();

        //     List<Case> cases = new List<Case> {
        //         new Case(["/a","/a/b","/c/d","/c/d/e","/c/f"]),  // 5
        //         new Case(["/a","/a/b/c","/a/b/d"]),
        //         new Case(["/a/b/c","/a/b/ca","/a/b/d"]),
        //     };

        //     foreach (var c in cases) {
        //         foreach (var f in s.RemoveSubfolders(c.Folders)) {
        //             Console.WriteLine(f);
        //         }
        //         Console.WriteLine("");
        //     }
        // }

    }
}