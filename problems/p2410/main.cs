// using Answer = (int Min, int Max);
// using Key = (int n, int p1, int p2);

using System.Collections;

public class Solution {
    public int MatchPlayersAndTrainers(int[] players, int[] trainers) {
        int answer = 0;
        var sortedPlayers = players.ToList();
        sortedPlayers.Sort();
        var sortedTrainers = trainers.ToList();
        sortedTrainers.Sort();

        int player = 0;
        int trainer = 0;
        while (true) {
            if (player >= players.Length) { break; }
            while (trainer < trainers.Length && sortedTrainers[trainer] < sortedPlayers[player]) {
                trainer += 1;
            }
            if (trainer >= trainers.Length) { break; }


            if (sortedPlayers[player] <= sortedTrainers[trainer]) {
                player += 1;
                trainer += 1;
                answer += 1;
            }


        }
        return answer;
    }
};


public class MainClass {

    record Case(int[] Players, int[] Trainers);

    public static void Main(string[] args) {
        Solution s = new Solution();
        Case[] cases = {
            new Case([4,7,9], [8,2,5,8]),
            new Case([1,1,1], [10]),
        };
        for (int ci = 0; ci < cases.Length; ci++) {
            Case c = cases[ci];
            var answer = s.MatchPlayersAndTrainers(c.Players, c.Trainers);
            Console.WriteLine(string.Format("{0}", answer));
        }
    }

};
