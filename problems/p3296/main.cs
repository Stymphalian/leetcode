
// https://leetcode.com/problems/minimum-number-of-seconds-to-make-mountain-height-zero/description
// Time Taken: 00:55:42

public class Solution {
  public long MinSeconds(int mountainHeight, int[] workerTimes) {

    long[] nextWorkerCost = new long[workerTimes.Length];
    long[] timesWorkerIsChosen = new long[workerTimes.Length];
    long[] totalTimeForWorker = new long[workerTimes.Length];
    var pq = new PriorityQueue<int, long>();
    for(int index = 0; index < workerTimes.Length; index++) {
      nextWorkerCost[index] = workerTimes[index];
      timesWorkerIsChosen[index] = 1;
      totalTimeForWorker[index] = 0;
      pq.Enqueue(index, workerTimes[index]);
    }

    while (mountainHeight > 0) {
      // Find the best candidate with the least workerTime to reduce by 1
      int bestIndex = pq.Dequeue();
      mountainHeight -= 1;
      timesWorkerIsChosen[bestIndex] += 1;
      totalTimeForWorker[bestIndex] += nextWorkerCost[bestIndex];
      nextWorkerCost[bestIndex] = workerTimes[bestIndex] * timesWorkerIsChosen[bestIndex];
      pq.Enqueue(bestIndex, totalTimeForWorker[bestIndex] + nextWorkerCost[bestIndex]);
    }

    long maxTime = 0;
    foreach(var workerTime in totalTimeForWorker) {
      maxTime = Math.Max(workerTime, maxTime);
    }
    return maxTime;
  }

  public long MinNumberOfSeconds(int mountainHeight, int[] workerTimes) {
    return MinSeconds(mountainHeight, workerTimes);
  }
}


public class MainClass {
  public static void Main(string[] args) {
    testcase1();
    testcase2();
    testcase3();
  }

  public static void testcase1() {
    Solution solution = new Solution();
    int mountainHeight = 4;
    int[] workerTimes = { 2, 1, 1};
    long result = solution.MinNumberOfSeconds(mountainHeight, workerTimes);
    Console.WriteLine(result); // 3
  }

  public static void testcase2() {
    Solution solution = new Solution();
    int mountainHeight = 10;
    int[] workerTimes = { 3, 2, 2, 4};
    long result = solution.MinNumberOfSeconds(mountainHeight, workerTimes);
    Console.WriteLine(result); // 12
  }

  public static void testcase3() {
    Solution solution = new Solution();
    int mountainHeight = 5;
    int[] workerTimes = {1};
    long result = solution.MinNumberOfSeconds(mountainHeight, workerTimes);
    Console.WriteLine(result); // 15
  }
};