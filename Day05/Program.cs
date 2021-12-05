using System.Text.RegularExpressions;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("AOC 2021-05");

var input = File.ReadAllLines("input.txt").Select(s => new Line(s))
    //.Where(l => l.Horizontal || l.Vertical)
    .ToList();

var map = new byte[1000, 1000];

foreach (var line in input)
{
    var diff = Math.Max(Math.Abs(line.dx), Math.Abs(line.dy));
    var ddx = Math.Sign(line.dx);
    var ddy = Math.Sign(line.dy);
    var x = line.X1;
    var y = line.Y1;

    for (int i = 0; i <= Math.Abs(diff); i++)
    {
        map[x, y] += 1;
        x += ddx;
        y += ddy;
    }
}

int res = 0;
for (int x = 0; x < 1000; x++)
    for (int y = 0; y < 1000; y++)
        if (map[x, y] > 1)
            res++;

Console.WriteLine(res);

public class Line
{
    public int X1;
    public int X2;
    public int Y1;
    public int Y2;

    public int dx => X2 - X1;
    public int dy => Y2 - Y1;

    public bool Horizontal => Y1 == Y2;
    public bool Vertical => X1 == X2;

    public Line(string s)
    {
        var match = Regex.Match(s, @"(\d+),(\d+) -> (\d+),(\d+)");
        X1 = int.Parse(match.Groups[1].Value);
        Y1 = int.Parse(match.Groups[2].Value);
        X2 = int.Parse(match.Groups[3].Value);
        Y2 = int.Parse(match.Groups[4].Value);
        //if (X2 < X1)
        //{
        //    var q = X2;
        //    X2 = X1;
        //    X1 = q;
        //}
        //if (Y2 < Y1)
        //{
        //    var q = Y1;
        //    Y1 = Y2;
        //    Y2 = q;
        //}
    }
}