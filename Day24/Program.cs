// See https://aka.ms/new-console-template for more information

using System.IO;
using System.Text.RegularExpressions;

Console.WriteLine("AOC 2021-24");

var program = File.ReadAllLines("input.txt");
long w = 0;
long x = 0;
long y = 0;
long z = 0;

Execute("11111111111111");
//Execute("22222222222222");
//Execute("10");

void Execute(string inpNum)
{
    w = 0;
    x = 0;
    y = 0;
    z = 0;
    int impNo = 0;
    foreach (var line in program)
    {
        var match = Regex.Match(line, @"(?<op>\w+) (?<dst>\w) ?((?<srcnum>[-0-9]+)|(?<srcreg>\w))?");
        var op = match.Groups["op"].Value;
        var dst = match.Groups["dst"].Value;
        long? srcnum = match.Groups["srcnum"].Success ? long.Parse(match.Groups["srcnum"].Value) : null;
        var srcreg = match.Groups["srcreg"].Value;

        switch (op)
        {
            case "inp":
                Console.Write($"Input {++impNo}: ");
                SetReg(dst, Console.ReadLine()[0] - '0');
                //SetReg(dst, inpNum[0] - '0');
                inpNum = inpNum.Substring(1);
                break;
            case "add":
                SetReg(dst, GetReg(dst) + (srcnum ?? GetReg(srcreg)));
                break;
            case "mul":
                SetReg(dst, GetReg(dst) * (srcnum ?? GetReg(srcreg)));
                break;
            case "div":
                SetReg(dst, GetReg(dst) / (srcnum ?? GetReg(srcreg)));
                break;
            case "mod":
                SetReg(dst, GetReg(dst) % (srcnum ?? GetReg(srcreg)));
                break;
            case "eql":
                SetReg(dst, GetReg(dst) == (srcnum ?? GetReg(srcreg)) ? 1 : 0);
                break;
            default:
                Console.WriteLine($"Unexpected op: '{match.Groups["op"].Value}' in {line}");
                break;
        }
        Console.WriteLine($"{match.Captures[0].Value,-10} w:{w,5}  x:{x,5}  y:{y,5}  z:{z,5}");
    }
}

void SetReg(string reg, long val)
{
    switch (reg)
    {
        case "w": w = val; break;
        case "x": x = val; break;
        case "y": y = val; break;
        case "z": z = val; break;
    }
}

long GetReg(string reg) => reg switch { "w" => w, "x" => x, "y" => y, "z" => z };