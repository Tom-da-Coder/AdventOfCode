// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("AOC 2021-10");

var stack = new Stack<int>();
string parens = "()[]{}<>";
int[] points = new[] { 3, 57, 1197, 25137 };
int[] bScore = new[] { 1, 2, 3, 4 };
int Asum = 0;
var bScores = new List<long>();

var input = File.ReadAllLines("input.txt");
foreach (var line in input)
{
    var (a, b) = GetLinePoints(line);
    Asum += a;
    if (b > 0)
        bScores.Add(b);
}
Console.WriteLine(Asum);
bScores.Sort();
Console.WriteLine(bScores[bScores.Count / 2]);


(int,long) GetLinePoints(string str)
{
    stack.Clear();
    for (int i = 0; i < str.Length; i++)
    {
        int c = parens.IndexOf(str[i]);
        if (c % 2 == 0)
            stack.Push(c);
        else
        {
            var c1 = stack.Pop();
            if (c1 + 1 != c)
                return (points[c / 2], 0);
        }
    }
    var bsum = stack.Aggregate(0L, (long a, int b) => 5 * a + b / 2 + 1);
    return (0, bsum);
}