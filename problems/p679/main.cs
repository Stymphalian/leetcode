
// 24 Game
// https://leetcode.com/problems/24-game/description/
// Difficulty: Hard
// Time Taken: 05:31:28


public class Solution
{

    public class Fraction
    {
        public int Num;
        public int Den;

        public Fraction(int num, int den)
        {
            Num = num;
            Den = den;

            int gcd = GCD(Math.Abs(num), Math.Abs(den));
            if (gcd > 1)
            {
                Num /= gcd;
                Den /= gcd;
            }
        }

        public int GCD(int a, int b)
        {
            if (a == 0)
            {
                return b;
            }
            else if (b == 0)
            {
                return a;
            }
            else
            {
                return GCD(b, a % b);
            }
        }

        public Fraction Add(Fraction other)
        {
            int common = Den * other.Den;
            int a = Num * other.Den;
            int c = other.Num * Den;
            return new Fraction(a + c, common);
        }

        public Fraction Sub(Fraction other)
        {
            int common = Den * other.Den;
            int a = Num * other.Den;
            int c = other.Num * Den;
            return new Fraction(a - c, common);
        }

        public Fraction Mult(Fraction other)
        {
            return new Fraction(Num * other.Num, Den * other.Den);
        }

        public Fraction Div(Fraction other)
        {
            return new Fraction(Num * other.Den, Den * other.Num);
        }

        public bool IsEquals(Fraction other)
        {
            return Num == other.Num && Den == other.Den;
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", Num, Den);
        }
    }

    public IEnumerable<Fraction> Values(List<Fraction> cards)
    {
        if (cards.Count == 1)
        {
            yield return cards[0];
        }
        else if (cards.Count == 2)
        {
            List<Fraction> results = [
                cards[0].Add(cards[1]),
                cards[0].Sub(cards[1]),
                cards[0].Mult(cards[1]),
                cards[0].Div(cards[1]),
            ];
            foreach (var result in results)
            {
                yield return result;
            }
        }
        else
        {
            for (int split = 1; split < cards.Count; split++)
            {
                var left = cards[..split];
                var right = cards[split..];

                // var leftValues = Values(left);
                // var rightValues = Values(right);
                // IEnumerable<Fraction> results = leftValues.SelectMany(x => {
                //     return rightValues.SelectMany(y => {
                //         return (IEnumerable<Fraction>)[x.Add(y), x.Sub(y), x.Mult(y), x.Div(y)];
                //     });
                // });
                // foreach (var result in results) {
                //     yield return result;
                // }

                List<Fraction> results = [];
                foreach (var leftValue in Values(left))
                {
                    foreach (var rightValue in Values(right))
                    {
                        results.Add(leftValue.Add(rightValue));
                        results.Add(leftValue.Sub(rightValue));
                        results.Add(leftValue.Mult(rightValue));
                        results.Add(leftValue.Div(rightValue));
                    }
                }
                foreach (var result in results)
                {
                    yield return result;
                }
            }
        }
    }

    public static IEnumerable<List<T>> GetPermutations<T>(List<T> cards)
    {
        if (cards.Count == 1)
        {
            List<T> newList = [cards[0]];
            yield return newList;
        }
        for (int ci = 0; ci < cards.Count; ci++)
        {
            // List<T> slice = cards[..ci].Concat(cards[(ci + 1)..]).ToList();
            List<T> slice = [.. cards[..ci], .. cards[(ci + 1)..]];
            foreach (var perm in GetPermutations(slice))
            {
                List<T> newList = [cards[ci]];
                yield return newList.Concat(perm).ToList();
            }
        }
    }

    public bool JudgePoint24(int[] cards)
    {
        var cardsFraction = cards.ToList().Select(x => new Fraction(x, 1)).ToList();
        var target = new Fraction(24, 1);

        foreach (var perm in GetPermutations(cardsFraction))
        {
            foreach (var value in Values(perm))
            {
                if (value.IsEquals(target))
                {
                    return true;
                }
            }
        }
        return false;
    }
}

public class MainClass {
    record Case(int[] cards);

    public static void Main(String[] args) {
        Solution s = new Solution();

        List<Case> cases = new List<Case> {
            new Case([4,1,8,7]), // true
            new Case([1,2,1,2]), // false
            new Case([1,9,1,2]), // true
            new Case([1,3,4,6]), // true
        };

        foreach (var c in cases) {
            Console.WriteLine(s.JudgePoint24(c.cards));
        }
    }

}

