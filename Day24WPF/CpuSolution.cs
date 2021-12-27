using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day24WPF
{
    internal class CpuSolution
    {
        string[] program;
        long w = 0;
        long x = 0;
        long y = 0;
        long z = 0;

        public CpuSolution()
        {
            program = File.ReadAllLines("input.txt");
        }

        public void Execute(Func<string, int> getInput)
        {
            w = 0;
            x = 0;
            y = 0;
            z = 0;
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
                        var inp = getInput($"  w:{w,5}  x:{x,5}  y:{y,5}  z:{z,5}");
                        SetReg(dst, inp);
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

        private void SetReg(string reg, long val)
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
    }
}
