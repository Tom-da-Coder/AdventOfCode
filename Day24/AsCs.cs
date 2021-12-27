// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
using System.Windows;

namespace Day24;

public class Day24cs
{

    public void Main(StreamWriter @out, StreamReader inp)
    {
        var win = new Window();

        @out.WriteLine("AOC 2021-24");

        var program = File.ReadAllLines("input.txt");
        long w = 0;
        long x = 0;
        long y = 0;
        long z = 0;

        ExecutePart(1, 1,  10, 15);
        ExecutePart(1, 1,  12,  8);
        ExecutePart(1, 1,  15,  2);
        ExecutePart(1, 26, -9,  6);
        ExecutePart(1, 1,  15, 13);
        ExecutePart(1, 1,  10,  4);
        ExecutePart(1, 1,  14,  1);
        ExecutePart(1, 26, -5,  9);
        ExecutePart(1, 1,  14,  5);
        ExecutePart(1, 26, -7, 13);
        ExecutePart(1, 26, -12, 9);
        ExecutePart(1, 26, -10, 6);
        ExecutePart(1, 26, -1,  2);
        ExecutePart(1, 26, -11, 2);

        void ExecutePart(int callNo, long firstDiv, long firstAdd, long secondAdd)
        {
            @out.Write($"Input {callNo}: ");
            x = z;
            w = inp.ReadLine()[0] - '0';
            x %= 26;
            z /= firstDiv;
            x += firstAdd;
            x = x == w ? 0 : 1;
            y = x == 1 ? 26 : 1;
            z *= y;
            y = w + secondAdd;
            y *= x;
            z += y;
            @out.WriteLine($"  w:{w,5}  x:{x,5}  y:{y,5}  z:{z,5}");
        }
    }
}