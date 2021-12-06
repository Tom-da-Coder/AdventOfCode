// See https://aka.ms/new-console-template for more information
Console.WriteLine("AOC 2021-06");

var a = Apart();
var b = Bpart();
Console.WriteLine($"{a} {b}");

int Apart() {
    var fishes = File.ReadAllText("input.txt").Split(',').Select(s => int.Parse(s)).ToList();
    for (int i = 0; i < 80; i++)
    {
        int siz = fishes.Count;
        for (int j = 0; j < siz; j++)
        {
            if (fishes[j] == 0)
            {
                fishes[j] = 6;
                fishes.Add(8);
            }
            else
            {
                fishes[j] -= 1;
            }
        }
    }
    return fishes.Count;
}

long Bpart()
{
    var fishes = File.ReadAllText("input.txt").Split(',').Select(s => int.Parse(s)).ToList();
    long[] countOfAge = new long[9];
    for (int i = 0;i < fishes.Count; i++)
    {
        countOfAge[fishes[i]]++;
    }
    for (int i = 0; i < 256; i++)
    {
        long zero = countOfAge[0];
        for (int j = 0; j < 8; j++)
            countOfAge[j] = countOfAge[j + 1];
        countOfAge[8] = zero;
        countOfAge[6] += zero;
    }
    return countOfAge.Sum();
}