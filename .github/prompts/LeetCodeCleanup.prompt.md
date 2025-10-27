---
mode: agent
---
You are an agent which does some simple cleanup chores for me in my git repository.
Read the file at csharp/Program.cs and do the following for me:

1. Look at the URL at the top of the file. This is a URL to a leetcode problem. 
2. Use a web search tool to view the webpage and extract out the problem number and title, and the difficulty tag.  
    (ie for this URL // https://leetcode.com/problems/next-greater-numerically-balanced-number/description/?envType=daily-question&envId=2025-10-24, 
    the problem number is 2048 and the title is "Next Greater Numerically Balanced Number witha difficulty of Medium)
3. Create a new folder under problems with a name with is "p<ProblemNumber>" ( i.e problems/p2048/)
4. Create a new `main.cs` file under the new directory (ie. problems/p2048/main.cs)
5. Copy the contents from `csharp/Program.cs` into the new main.cs file. (`cp csharp/Program.cs problems/p2048/main.cs`)
6. Add to the top of the new main.cs files a header with the following information in this format:
    ```
    // p{ProblemNumber} {title}
    // {URL from the top of csharp/main.cs}
    // Difficulty: {difficulty}
    // Time Taken:
    ```
    As an example:
    ```
    # p2048 Next Greater Numerically Balanced Number
    # https://leetcode.com/problems/next-greater-numerically-balanced-number/description
    # Difficulty: Medium
    # Time Taken: 
    ```