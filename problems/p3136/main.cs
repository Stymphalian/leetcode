public class Solution {
    public bool IsValid(string word) {
        if (word.Length < 3) { return false; }

        bool hasVowel = false;
        bool hasConsonant = false;
        char[] vowels = ['a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U'];
        foreach (var c in word) {
            if (!Char.IsAsciiLetterOrDigit(c)) { return false; }
            if (Char.IsDigit(c)) continue;
            if (vowels.Contains(c)) {
                // Console.WriteLine(String.Format("{0}", c));
                hasVowel = true;
            }
            else {
                hasConsonant = true;
            }
        }

        return hasConsonant && hasVowel;
    }
};