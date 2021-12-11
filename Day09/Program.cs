// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("AOC 2021-09");

int[,] basins;
List<List<int>> points = new List<List<int>>();
long sum = 0;
int basinNo = 0;
List<int> basinSizes = new List<int>();

var input = File.ReadAllLines("input.txt");
foreach (var line in input)
{
    var hl = new List<int>();
    for (int i = 0; i < line.Length; i++)
        hl.Add(line[i] - '0');
    points.Add(hl);
}

basins = new int[points.Count, points[0].Count];
for (int i = 0; i < points.Count; i++)
{
    var l = points[i];
    var linlen = l.Count;
    for (int j = 0; j < linlen; j++)
    {
        var p = l[j];
        if ((j == 0 || l[j - 1] > p)
            && ((j == linlen - 1) || p < l[j + 1])
            && (i == 0 || points[i - 1][j] > p)
            && (i == points.Count - 1 || points[i + 1][j] > p)
           )
        {
            sum += p + 1;
            basinSizes.Add(measureBasin(i, j));
        }
    }
}

basinSizes.Sort();
basinSizes.Reverse();
var answer = basinSizes.Take(3).Aggregate(1, (a, b) => a * b);

Console.WriteLine(sum);
Console.WriteLine(answer);


int measureBasin(int i, int j)
{
    int size = 0;
    Stack<Pos> stack = new Stack<Pos>();
    ++basinNo;
    stack.Push(new Pos(i, j));
    while (stack.Count > 0)
    {
        var p = stack.Pop();
        (i, j) = (p.i, p.j);
        if (basins[i, j] > 0)
            continue;
        size++;
        basins[i, j] = basinNo;
        if (i > 0 && points[i - 1][j] < 9) stack.Push(new Pos(i - 1, j));
        if (j > 0 && points[i][j - 1] < 9) stack.Push(new Pos(i, j - 1));
        if (j < points[0].Count - 1 && points[i][j + 1] < 9) stack.Push(new Pos(i, j + 1));
        if (i < points.Count - 1 && points[i + 1][j] < 9) stack.Push(new Pos(i + 1, j));
    }
    return size;
}

public struct Pos
{
    public int i, j;
    public Pos(int i1, int j1)
    {
        i = i1;
        j = j1;
    }
}