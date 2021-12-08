// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("AOC 2021-08");

var lens = new int[] { 2, 3, 4, 7 };
int totCount = 0;
var input = File.ReadAllLines("input2.txt");
foreach (var line in input)
{
    //var match = Regex.Match(line, @"\|( (?\w+))+$", RegexOptions.ExplicitCapture);
    //var n = match.Captures;

    var spl = line.Split('|');
    var (first, last) = (spl[0], spl[1]);
    var firsts = first.Split(' ').Select(s => s.Trim());
    var lasts = last.Split(' ').Select(s => s.Trim());
    totCount += lasts.Count(t => lens.Contains(t.Length));

    var map = new string[] { "abcdef", "abcdef", "abcdef", "abcdef", "abcdef", "abcdef", "abcdef" };

    foreach (var str in firsts)
    {
        if (str.Length == 2)
        {
            exclude(ref map, str, "cf"); // Must be 1
        }
    }

}
Console.WriteLine(totCount);

void exclude(ref string[] map, string str, string segments)
{
    foreach (var segment in segments)
        foreach (var t in map)
            t = t.Replace(segment)
}