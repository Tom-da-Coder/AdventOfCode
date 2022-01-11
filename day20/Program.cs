// See https://aka.ms/new-console-template for more information

Console.WriteLine("AOC 2021-20");

var input = File.ReadAllLines("input.txt");
var map = input[0];

var image = new byte[input[2].Length, input.Length];

for (int y = 0; y < input.Length - 2; y++)
{
    var line = input[y + 2];
    for (int x = 0; x < line.Length; x++)
        image[x, y] = line[x]
}
foreach (var line in input.Skip(2))
{

}

Console.WriteLine(totCount);
