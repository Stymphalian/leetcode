class SegmentTree {
    int[] DP;
    int N;
    public SegmentTree(ref int[] nums) {
        N = nums.Length;
        DP = new int[2 * N + 1];
        Build(ref nums, 0, 0, nums.Length - 1);
    }

    public void Build(ref int[] nums, int index, int left, int right) {
        if (left == right) {
            DP[index] = nums[left];
            return;
        }

        int leftIndex = 2 * index + 1;
        int rightIndex = 2 * index + 2;
        int mid = left + (right - left) / 2;
        Build(ref nums, leftIndex, left, mid);
        Build(ref nums, rightIndex, mid + 1, right);
        DP[index] = DP[leftIndex] + DP[rightIndex];
    }

    public int InternalSum(int index, int tl, int tr, int l, int r) {
        if (l > r)
            return 0;
        if (l == tl && r == tr) {
            return DP[index];
        }
        int mid = (tl + tr) / 2;
        return InternalSum(index * 2 + 1, tl, mid, l, Math.Min(r, mid))
            + InternalSum(index * 2 + 2, mid + 1, tr, Math.Max(l, mid + 1), r);
    }

    public int Sum(int left, int right) {
        return InternalSum(0, 0, N - 1, left, right);
    }

    public void InternalUpdate(int index, int tl, int tr, int pos, int new_val) {
        if (tl == tr) {
            DP[index] = new_val;
        } else {
            int mid = (tl + tr) / 2;
            if (pos <= mid) {
                InternalUpdate(index * 2 + 1, tl, mid, pos, new_val);
            } else {
                InternalUpdate(index * 2 + 2, mid + 1, tr, pos, new_val);
            }

            DP[index] = DP[index * 2 + 1] + DP[index * 2 + 2];
        }
    }

    public void Update(int pos, int value) {
        InternalUpdate(0, 0, N - 1, pos, value);
    }
};