// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("AOC 2021-13");
int totCount = 0;
var points = new List<Point>();

var input = File.ReadAllLines("input.txt");
foreach (var line in input)
{
    if (line.Length < 1) continue;

    if (Char.IsDigit(line[0]))
        points.Add(new Point(line));
    else
    {
        var eqpos = line.IndexOf('=');
        if (eqpos > 0)
        {
            var foldat = int.Parse(line.Substring(eqpos + 1));
            if (line[eqpos - 1] == 'x')
                foreach (var p in points) p.FoldX(foldat);
            else
                foreach (var p in points) p.FoldY(foldat);
        }
#if A_PART
        break;
#else
#endif
    }
}

#if A_PART

var uniq = points.Distinct().ToList();
totCount = uniq.Count();
Console.WriteLine(totCount);

#else

PrintPaper();

#endif

void PrintPaper()
{
    int wid = points.Max(p => p.X) + 1;
    int hgt = points.Max(p => p.Y) + 1;

    var lines = points.OrderBy(p => p.X).OrderBy(p => p.Y).ToList();
    for (int y = 0; y < hgt; y++)
    {
        for (int x = 0; x < wid; x++)
        {
            if (lines.Any(p => p.X == x && p.Y == y))
                Console.Write('*');
            else
                Console.Write('.');
        }
        Console.WriteLine();
    }
}

public class Point : IEquatable<Point>
{
    public int X;
    public int Y;

    public Point(string line)
    {
        var d = line.Split(',');
        X = int.Parse(d[0]);
        Y = int.Parse(d[1]);
    }

    public bool Equals(Point other)
    {
        return X == other.X && Y == other.Y;
    }

    public override int GetHashCode()
    {
        return 3 * X + 111113 * Y;
    }

    public void FoldX(int x) => X = X < x ? X : 2 * x - X;
    public void FoldY(int y) => Y = Y < y ? Y : 2 * y - Y;
}