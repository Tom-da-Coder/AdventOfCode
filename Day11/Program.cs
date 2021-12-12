// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("AOC 2021-11");

int totCount = 0;
int[,] octos = new int[12, 12];
var input = File.ReadAllLines("input.txt");

for (var k = 0; k < 10; k++)
{
    var line = input[k];
    for (int i = 0; i < 10; i++)
        octos[k + 1, i + 1] = line[i] - '0';
}

for (int gen = 0; gen < 100; gen++)
{
    for (int r = 1; r < 11; r++)
        for (int c = 1; c < 11; c++)
            octos[r, c]++;

    int nflash = 0;
    do
    {
        nflash = 0;
        for (int r = 1; r < 11; r++)
            for (int c = 1; c < 11; c++)
            {
                if (octos[r, c] >= 10)
                {
                    nflash++;
                    octos[r, c] = -100;
                    kickaround(r, c);
                }
            }
        totCount += nflash;
    } while (nflash > 0);

    for (int r = 1; r < 11; r++)
        for (int c = 1; c < 11; c++)
            if (octos[r, c] < 0)
                octos[r, c] = 0;
}

Console.WriteLine(totCount);

void kickaround(int r, int c)
{
    for (var i = r - 1; i <= r + 1; i++)
        for (var j = c - 1; j <= c + 1; j++)
            octos[i, j]++;
}