// See https://aka.ms/new-console-template for more information

using System.Text;

Console.WriteLine("AOC 2021-20");

var input = File.ReadAllLines("input.txt");
var map = input[0];

var image = input.Skip(2).ToList();

PrintImage();
for (int i = 0; i < 25; i++)
{
    ProcessImage(false);
    ProcessImage(true);
}
PrintImage();
var totCount = image.Sum(t => t.Count(c => c == '#'));
Console.WriteLine(totCount);

void ProcessImage(bool inv)
{
    var newImage = new List<string>();
    for (int y = -1; y < image.Count() + 1; y++)
    {
        var sb = new StringBuilder();
        for (int x = -1; x < image[0].Count() + 1; x++)
            sb.Append(Kernel(x, y, inv));
        newImage.Add(sb.ToString());
    }
    image = newImage;
}

char Kernel(int x, int y, bool inv)
{
    int bin9 = 0;
    for (int y2 = y - 1; y2 <= y + 1; y2++)
        for (int x2 = x - 1; x2 <= x + 1; x2++)
        {
            var fg = (x2 >= 0 && y2 >= 0 && x2 < image[0].Count() && y2 < image.Count());
            if (fg && image[y2][x2] == '#' || !fg && inv)
                bin9 += 1 << ((y + 1 - y2) * 3 + (x + 1 - x2));
        }
    return map[bin9];
}

void PrintImage()
{
    for (int y = 0; y < image.Count(); y++)
        Console.WriteLine(image[y]);
    Console.WriteLine();
}