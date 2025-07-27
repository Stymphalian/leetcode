public class Solution
{
    public int CountHillValley(int[] nums) {
        int index = 0;
        int count = 0;
        while (index < nums.Length) {
            int left = index - 1;
            int right = index + 1;
            while (right < nums.Length && nums[right] == nums[index]) {
                right++;
            }

            if (left < 0 || right >= nums.Length) {
                index = right;
                continue;
            }

            if (nums[left] < nums[index] && nums[right] < nums[index]) {
                count++;
            } else if (nums[left] > nums[index] && nums[right] > nums[index]) {
                count++;
            }
            index = right;
        }
        return count;
    }
}