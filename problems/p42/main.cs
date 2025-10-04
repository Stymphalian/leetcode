// 42. Trapping Rain Water
// https://leetcode.com/problems/trapping-rain-water/description/
// Time Taken: 04:57:35
// Difficulty: Hard

public class Solution
{
    public int Trap(int[] height)
    {

        int[] rightmostMax = new int[height.Length + 1];
        rightmostMax[height.Length] = 0;
        int best = -1;
        for (int right = height.Length - 1; right >= 0; right -= 1)
        {
            best = Math.Max(height[right], best);
            rightmostMax[right] = best;
        }


        int count = 0;

        int start = 0;
        for (int index = 0; index < height.Length - 1; index++)
        {
            if (height[index + 1] < height[index])
            {
                start = index;
                break;
            }
        }

        int peak1 = start;
        while (peak1 < height.Length)
        {

            // Move to the right-most side of plateaus
            while (peak1 + 1 < height.Length && height[peak1 + 1] == height[peak1])
            {
                peak1 += 1;
            }

            // int peak2Height = findMaxInRange(height, peak1+1, height.Length - 1);
            int peak2Height = rightmostMax[peak1 + 1];
            int maxHeight = Math.Min(height[peak1], peak2Height);

            // Add up all the valleys that will hold the water
            peak1 += 1;
            while (peak1 < height.Length)
            {
                if (height[peak1] >= maxHeight) { break; }
                int diff = Math.Max(maxHeight - height[peak1], 0);
                count += diff;
                peak1 += 1;
            }
        }

        return count;
    }
}