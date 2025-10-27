// p2048 Next Greater Numerically Balanced Number
// https://leetcode.com/problems/next-greater-numerically-balanced-number/description/?envType=daily-question&envId=2025-10-24
// Difficulty: Medium
// Time Taken: 05:45:59

public class Solution {

    public static void Print2DArray(List<List<int>> array) {
        foreach (var arr in array) {
            PrintArray(arr);
            Console.WriteLine();
        }
    }

    public static void PrintArray(List<int> array) {
        foreach (var n in array) {
            Console.Write("{0} ", n);
        }
        Console.WriteLine();
    }

    public List<int> NumberToIntList(int n) {
        List<int> digits = [];
        var nStr = n.ToString();
        for (int i = 0; i < nStr.Length; i++) {
            digits.Add(nStr[i] - '0');
        }
        return digits;
    }
    public int IntListToNumber(List<int> ns) {
        int answer = 0;
        int multiplier = 1;
        for (int index = ns.Count - 1; index >= 0; index--) {
            answer += ns[index] * multiplier;
            multiplier *= 10;
        }
        return answer;
    }

    public IEnumerable<List<int>> GetNumberSets(int numDigits, List<int> elements, int index, List<int> answer) {
        if (numDigits == 0) {
            yield return answer;
        }
        if (numDigits < 0) { yield break; }
        if (index >= elements.Count) { yield break; }
        if (elements[index] > numDigits) { yield break; }

        answer.Add(elements[index]);
        foreach (var numberSet in GetNumberSets(numDigits - elements[index], elements, index + 1, answer)) {
            yield return numberSet;
        }
        answer.RemoveAt(answer.Count - 1);

        foreach (var numberSet in GetNumberSets(numDigits, elements, index + 1, answer)) {
            yield return numberSet;
        }
    }

    public IEnumerable<List<int>> BuildBeautifulNumber(Dictionary<int, int> digits, int digitCount, List<int> newNumber) {
        if (digitCount == 0) {
            yield return newNumber;
        }

        for (int digit = 1; digit <= 9; digit++) {
            if (!digits.ContainsKey(digit)) { continue; }
            if (digits[digit] == 0) { continue; }

            digits[digit] -= 1;
            newNumber.Add(digit);
            foreach (var newNum in BuildBeautifulNumber(digits, digitCount - 1, newNumber)) {
                yield return newNum;
            }
            newNumber.RemoveAt(newNumber.Count - 1);
            digits[digit] += 1;
        }
    }

    public (Dictionary<int, int>, int) BuildBagFromNumberList(List<int> numberList) {
        Dictionary<int, int> bag = [];
        int count = 0;
        foreach (var number in numberList) {
            if (!bag.ContainsKey(number)) {
                bag[number] = 0;
            }
            bag[number] += 1;
            count += 1;
        }
        return (bag, count);
    }

    public int maxFromNumDigits(int numDigits) {
        int number = 0;
        int multiplier = 1;
        for (int index = 0; index < numDigits; index++) {
            number += numDigits * multiplier;
            multiplier *= 10;
        }
        return number;
    }

    public int maxFromNumberList(List<int> numberList) {
        int number = 0;
        int multiplier = 1;
        for (int index = 0; index < numberList.Count; index++) {
            number += numberList[index] * multiplier;
            multiplier *= 10;
        }
        return number;
    }

    public int minFromNumberList(List<int> numberList) {
        int number = 0;
        int multiplier = 1;
        for (int index = numberList.Count - 1; index >= 0; index--) {
            number += numberList[index] * multiplier;
            multiplier *= 10;
        }
        return number;
    }

    public List<int> numberSetToNumberList(List<int> numberSet) {
        List<int> numberList = [];
        foreach (var digit in numberSet) {
            for (int i = 0; i < digit; i++) {
                numberList.Add(digit);
            }
        }
        return numberList;
    }

    public int NextBeautifulNumber(int n) {
        var digits = NumberToIntList(n);
        var numDigits = digits.Count;
        // var n2 = IntListToNumber(digits);
        // Debug.Assert(n2 == n);

        if (n + 1 > maxFromNumDigits(numDigits)) {
            numDigits += 1;
        }

        int best = int.MaxValue;
        foreach (var numberSet in GetNumberSets(numDigits, [1, 2, 3, 4, 5, 6, 7, 8, 9], 0, [])) {
            List<int> numberList = numberSetToNumberList(numberSet);
            int maxNumber = maxFromNumberList(numberList);
            // PrintArray(numberSet);
            // Console.WriteLine(maxNumber);
            if (n > maxNumber) { continue; }

            var (bag, bagCount) = BuildBagFromNumberList(numberList);
            foreach (var cand in BuildBeautifulNumber(bag, bagCount, [])) {
                int candNum = IntListToNumber(cand);

                if (candNum > n) {
                    best = Math.Min(best, candNum);
                    break;
                }
            }
        }

        return best;
    }
}

public class MainClass {
    record Case(int N);

    public static int[][] ConvertToJaggedArray(int[,] array2D) {
        int rows = array2D.GetLength(0);
        int cols = array2D.GetLength(1);

        int[][] jaggedArray = new int[rows][];

        for (int i = 0; i < rows; i++) {
            jaggedArray[i] = new int[cols];
            for (int j = 0; j < cols; j++) {
                jaggedArray[i][j] = array2D[i, j];
            }
        }

        return jaggedArray;
    }

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case(1),
            new Case(1000),
            new Case(3000),
            new Case(16407),
        };

        foreach (var c in cases) {
            // var jaggedArray = ConvertToJaggedArray(c.nums);
            var result = s.NextBeautifulNumber(c.N);
            Console.WriteLine(result);
        }
    }
}

