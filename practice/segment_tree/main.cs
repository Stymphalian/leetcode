using System.Collections;
using System.Reflection.Metadata;

public abstract class SegmentTree<T> {
    public T[] DP;
    public int N;

    public SegmentTree(T[] nums) {
        N = nums.Length;
        DP = new T[2 * N - 1];
        Build(nums, 0, 0, nums.Length - 1);
    }

    public abstract T Combine(T leftValue, T rightValue);
    public virtual T StartValue(T value) { return value; }
    public virtual T DefaultValue() { return default; }

    public int LeftIndex(int index, int left, int right) {
        return index + 1;
    }
    public int RightIndex(int index, int left, int right) {
        // Using pre-order traversal indexing of the vertices
        // int mid = left + (right - left) / 2;
        // int leftSize = mid - left + 1;
        // int leftRoom = 2 * leftSize - 1;
        // return index + 1 + leftRoom;
        return index + 2 * ((right - left) / 2 + 1);
    }

    public void Build(T[] nums, int index, int left, int right) {
        if (left == right) {
            DP[index] = StartValue(nums[left]);
            return;
        }

        int mid = left + (right - left) / 2;
        int leftIndex = LeftIndex(index, left, right);
        int rightIndex = RightIndex(index, left, right);
        Build(nums, leftIndex, left, mid);
        Build(nums, rightIndex, mid + 1, right);
        DP[index] = Combine(DP[leftIndex], DP[rightIndex]);
    }

    public T InternalSearch(int index, int rangeLeft, int rangeRight, int targetLeft, int targetRight) {
        if (targetLeft > targetRight) {
            return DefaultValue();
        }
        if (targetLeft == rangeLeft && targetRight == rangeRight) {
            return DP[index];
        }

        int mid = rangeLeft + (rangeRight - rangeLeft) / 2;
        int leftIndex = LeftIndex(index, rangeLeft, rangeRight);
        int rightIndex = RightIndex(index, rangeLeft, rangeRight);
        T leftValue = InternalSearch(leftIndex, rangeLeft, mid, targetLeft, Math.Min(mid, targetRight));
        T rightValue = InternalSearch(rightIndex, mid + 1, rangeRight, Math.Max(mid + 1, targetLeft), targetRight);

        return Combine(leftValue, rightValue);
    }

    public T Search(int left, int right) {
        return InternalSearch(0, 0, N - 1, left, right);
    }

    public void InternalUpdate(int index, int tl, int tr, int pos, T new_val) {
        if (tl == tr) {
            DP[index] = new_val;
        } else {
            int mid = tl + (tr - tl) / 2;
            int leftIndex = LeftIndex(index, tl, tr);
            int rightIndex = RightIndex(index, tl, tr);
            if (pos <= mid) {
                InternalUpdate(leftIndex, tl, mid, pos, new_val);
            } else {
                InternalUpdate(rightIndex, mid + 1, tr, pos, new_val);
            }

            DP[index] = Combine(DP[leftIndex], DP[rightIndex]);
        }
    }

    public void Update(int pos, T value) {
        InternalUpdate(0, 0, N - 1, pos, value);
    }
};

public class SegmentTreeMax : SegmentTree<int> {
    public SegmentTreeMax(int[] nums) : base(nums) { }
    public override int Combine(int leftValue, int rightValue) {
        return Math.Max(leftValue, rightValue);
    }

    public override int DefaultValue() { return int.MinValue; }

    public int InternalGetFirst(int index, int rLeft, int rRight, int left, int right, int x) {
        if (rLeft > right || rRight < left) { return -1; }
        if (DP[index] < x) {return -1;}
        if (rLeft == rRight) { return rLeft; }

        int mid = rLeft + (rRight - rLeft) / 2;
        int leftIndex = LeftIndex(index, rLeft, rRight);
        int rightIndex = RightIndex(index, rLeft, rRight);

        int leftValue = InternalGetFirst(leftIndex, rLeft, mid, left, right, x);
        if (leftValue != -1) { return leftValue; }
        return InternalGetFirst(rightIndex, mid + 1, rRight, left, right, x);
    }

    public int GetFirst(int left, int right, int x) {
        return InternalGetFirst(0, 0, N - 1, left, right, x);
    }
}

public class SegmentTreeSum : SegmentTree<int> {
    public SegmentTreeSum(int[] nums) : base(nums) { }
    public override int Combine(int leftValue, int rightValue) {
        return leftValue + rightValue;
    }
}

public class SegmentTreeMaxCount : SegmentTree<(int max, int count)> {
    public SegmentTreeMaxCount(int[] nums)
        : base(nums.ToList().Select(x => (x, 1)).ToArray()) { }
    public override (int max, int count) Combine((int max, int count) leftValue, (int max, int count) rightValue) {
        if (leftValue.max == rightValue.max) {
            return (leftValue.max, leftValue.count + rightValue.count);
        } else {
            return leftValue.max > rightValue.max ? leftValue : rightValue;
        }
    }

    public override (int max, int count) DefaultValue() {
        return (int.MinValue, 1);
    }
}

public class ZeroCount : SegmentTree<int> {
    public ZeroCount(int[] nums) : base(nums) { }
    public override int Combine(int leftValue, int rightValue) {
        return leftValue + rightValue;
        // return (leftValue == 0 ? 1 : 0) + (rightValue == 0 ? 1 : 0);
    }
    public override int StartValue(int value) {
        return (value == 0 ? 1 : 0);
    }

    public int SearchKth(int index, int rLeft, int rRight, int k) {
        if (k > DP[index]) {
            return -1;
        }
        if (rLeft == rRight) {
            return rLeft;
        }

        int mid = rLeft + (rRight - rLeft) / 2;
        int leftIndex = LeftIndex(index, rLeft, rRight);
        int rightIndex = RightIndex(index, rLeft, rRight);
        int leftKCount = DP[leftIndex];
        if (leftKCount >= k) {
            return SearchKth(leftIndex, rLeft, mid, k);
        } else {
            return SearchKth(rightIndex, mid + 1, rRight, k - leftKCount);
        }
    }

    public int KthZeroCount(int k) {
        if (k == 0){ return -1; }
        return SearchKth(0, 0, N - 1, k);
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

    static int bruteRangeMax(int[] nums, int left, int right) {
        int sum = nums[left];
        for (int i = left; i <= right; i++) {
            sum = Math.Max(sum, nums[i]);
        }
        return sum;
    }

    static int bruteRangeMaxCount(int[] nums, int left, int right) {
        int max = bruteRangeMax(nums, left, right);
        int count = 0;
        for (int i = left; i <= right; i++) {
            if (nums[i] == max) {
                count++;
            }
        }
        return count;
    }

    static int bruteRangeZeroCount(int[] nums, int left, int right) {
        int count = 0;
        for (int i = left; i <= right; i++) {
            if (nums[i] == 0) {
                count++;
            }
        }
        return count;
    }

    static int bruteKthZeroCount(int[] nums, int k) {
        int count = 0;
        if (k <= 0) { return -1; }
        for (int i = 0; i < nums.Length; i++) {
            if (nums[i] == 0) {
                count += 1;
            }
            if (count == k) {
                return i;
            }
        }
        return -1;
    }
    
    static int bruteFirstElementBiggerThanX(int[] nums, int left, int right, int x) {
        for (int i = left; i <= right; i++) {
            if (nums[i] >= x) {
                return i;
            }
        }
        return -1;
    }

    public static void Main(String[] args) {
        var rng = new Random(0);

        for (int N = 10; N < 50; N++) {
            int[] nums = new int[N];
            for (int i = 0; i < N; i++) {
                nums[i] = rng.Next(50, 100);
            }
            foreach (var num in nums) {
                Console.Write("{0} ", num);
            }
            Console.WriteLine("");

            SegmentTreeMax tree = new SegmentTreeMax(nums);

            for (int j = 0; j < 10; j++) {
                int pos = Random.Shared.Next(0, N);
                int val = Random.Shared.Next(50, 100);
                tree.Update(pos, val);
                nums[pos] = val;

                for (int left = 0; left < N; left++) {
                    for (int right = left + 1; right < N; right++) {

                        int x = rng.Next(0, 200);
                        var got = tree.GetFirst(left, right, x);
                        int want = bruteFirstElementBiggerThanX(nums, left, right, x);
                        if (got != want) {
                            Console.WriteLine("Error {0} [{1},{2}] got={3} want={4} x = {5}", N, left, right, got, want, x);
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