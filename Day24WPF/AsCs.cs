// See https://aka.ms/new-console-template for more information

using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace Day24WPF;

public class Day24cs
{
        long w = 0;
        long x = 0;
        long y = 0;
        long z = 0;

    public Day24cs()
    {
    }

    public void Execute(Func<string, int> getInput)
    {
        ExecutePart(1, 1,  10, 15);
        ExecutePart(2, 1,  12,  8);
        ExecutePart(3, 1,  15,  2);
        ExecutePart(4, 26, -9,  6);
        ExecutePart(5, 1,  15, 13);
        ExecutePart(6, 1,  10,  4);
        ExecutePart(7, 1,  14,  1);
        ExecutePart(8, 26, -5,  9);
        ExecutePart(9, 1,  14,  5);
        ExecutePart(10, 26, -7, 13);
        ExecutePart(11, 26, -12, 9);
        ExecutePart(12, 26, -10, 6);
        ExecutePart(13, 26, -1,  2);
        ExecutePart(14, 26, -11, 2);

        void ExecutePart(int callNo, long firstDiv, long firstAdd, long secondAdd)
        {
            x = z;
            var inp = getInput($"  w:{w,5}  x:{x,5}  y:{y,5}  z:{z,5}");
            w = inp;
            x %= 26;
            z /= firstDiv;
            x += firstAdd;
            x = x == w ? 0 : 1;
            y = x == 1 ? 26 : 1;
            z *= y;
            y = w + secondAdd;
            y *= x;
            z += y;
        }
    }
}