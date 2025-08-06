public class Solution {

    public class SegmentTree {
        public int[] DP;
        public int N;

        public SegmentTree(int[] nums) {
            N = nums.Length;
            DP = new int[2 * N - 1];
            Build(nums, 0, 0, N - 1);
        }

        public int LeftIndex(int index, int left, int right) {
            return index + 1;
        }
        public int RightIndex(int index, int left, int right) {
            int mid = left + (right - left) / 2;
            int numLeftElems = mid - left + 1;
            int leftTreeSize = 2 * numLeftElems - 1;
            return index + leftTreeSize + 1;
        }

        public void Build(int[] nums, int index, int left, int right) {
            if (left == right) {
                DP[index] = nums[left];
                return;
            }
            int mid = left + (right - left) / 2;
            int leftIndex = LeftIndex(index, left, right);
            int rightIndex = RightIndex(index, left, right);
            Build(nums, leftIndex, left, mid);
            Build(nums, rightIndex, mid + 1, right);
            DP[index] = Math.Max(DP[leftIndex], DP[rightIndex]);
        }

        private void UpdateInternal(int index, int rLeft, int rRight, int pos, int val) {
            if (rLeft == rRight) {
                DP[index] = val;
                return;
            }
            int mid = rLeft + (rRight - rLeft) / 2;
            int leftIndex = LeftIndex(index, rLeft, rRight);
            int rightIndex = RightIndex(index, rLeft, rRight);

            if (pos <= mid) {
                UpdateInternal(leftIndex, rLeft, mid, pos, val);
            } else {
                UpdateInternal(rightIndex, mid + 1, rRight, pos, val);
            }
            DP[index] = Math.Max(DP[leftIndex], DP[rightIndex]);
        }

        public void Update(int pos, int val) {
            UpdateInternal(0, 0, N-1, pos, val);
        }

        private int GetFirstInternal(int index, int rLeft, int rRight, int left, int right, int x) {
            if (rRight < left || rLeft > right) { return -1; }
            if (DP[index] < x) { return -1; }
            if (rLeft == rRight) { return rLeft; }
            
            int mid = rLeft + (rRight - rLeft) / 2;
            int leftIndex = LeftIndex(index, rLeft, rRight);
            int rightIndex = RightIndex(index, rLeft, rRight);

            int gotLeft = GetFirstInternal(leftIndex, rLeft, mid, left, right, x);
            if (gotLeft != -1) { return gotLeft; }
            return GetFirstInternal(rightIndex, mid + 1, rRight, left, right, x);
        }

        public int GetFirst(int left, int right, int x) {
            return GetFirstInternal(0, 0, N-1, left, right, x);
        }
    }

    public int NumOfUnplacedFruits(int[] fruits, int[] baskets) {
        SegmentTree tree = new(baskets);

        int answer = 0;
        for (int fi = 0; fi < fruits.Length; fi++) {

            var found = tree.GetFirst(0, baskets.Length-1, fruits[fi]);
            if (found != -1) {
                tree.Update(found, -1);
                answer += 1;
            }
        }
        return fruits.Length - answer;
    }
}

public class MainClass {
    record Case(int[] Fruits, int[] Baskets);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case([4,2,5], [3,5,4]),
            new Case([3,6,1], [6,4,7]),
        };

        foreach (var c in cases) {
            Console.WriteLine(s.NumOfUnplacedFruits(c.Fruits, c.Baskets));
        }
    }

}
