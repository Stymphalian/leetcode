// 2785 Sort Vowels in a String
// https://leetcode.com/problems/sort-vowels-in-a-string/description/
// Time Taken: 00:07:49
// Difficulty: Medium

public class Solution
{
    public bool IsVowel(char c)
    {
        return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' || c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U';
    }

    public string Editorial(string s)
    {
        int[] counts = new int[10];
        char[] vowelsMap = ['A', 'E', 'I', 'O', 'U', 'a', 'e', 'i', 'o', 'u'];
        foreach (char c in s)
        {
            switch (c)
            {
                case 'A': counts[0]++; break;
                case 'E': counts[1]++; break;
                case 'I': counts[2]++; break;
                case 'O': counts[3]++; break;
                case 'U': counts[4]++; break;
                case 'a': counts[5]++; break;
                case 'e': counts[6]++; break;
                case 'i': counts[7]++; break;
                case 'o': counts[8]++; break;
                case 'u': counts[9]++; break;
            }
        }

        char[] sb = new char[s.Length];
        int countsIndex = 0;

        for (int index = 0; index < s.Length; index++)
        {
            char c = s[index];
            while (countsIndex < counts.Length && counts[countsIndex] <= 0)
            {
                countsIndex++;
            }

            if (IsVowel(c) && countsIndex < counts.Length)
            {
                sb[index] = vowelsMap[countsIndex];
                counts[countsIndex] -= 1;
            }
            else
            {
                sb[index] = c;
            }
        }

        return new string(sb);
    }

    public string Mine(string s)
    {
        // Get all the vowels
        var sortedVowels = s.ToList().Where(IsVowel).OrderBy(x => x - 'a').ToList();
        if (sortedVowels.Count == 0)
        {
            return s;
        }

        var sortedIndex = 0;
        char[] sb = new char[s.Length];

        for (int i = 0; i < s.Length; i++)
        {
            if (IsVowel(s[i]))
            {
                sb[i] = sortedVowels[sortedIndex++];
            }
            else
            {
                sb[i] = s[i];
            }
        }
        return new string(sb);
    }

    public string SortVowels(string s)
    {
        return Editorial(s);
    }
}

public class MainClass {
    record Case(string c);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case("lEetcOde"), 
            new Case("lYmpH"),
        };

        foreach (var c in cases) {
            Console.WriteLine(s.SortVowels(c.c));
        }
    }
}

