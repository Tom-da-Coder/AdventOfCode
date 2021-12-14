// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("AOC 2021-13");
int totCount = 0;

var input = File.ReadAllLines("input.txt");
foreach (var line in input)
{
    var match = Regex.Match(line, @"^(\d+,\d+)|(fold along (x|y)=(\d+))$");
    if (!match.Success)
        continue;

}
Console.WriteLine(totCount);
