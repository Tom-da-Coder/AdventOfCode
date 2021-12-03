// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string s;
int pos = 0, aim = 0, depth = 0;
while ((s = Console.ReadLine()) != null)
{
    var pars = s.Split(' ');
    var amt = int.Parse(pars[1]);
    switch (pars[0])
    {
        case "forward":
            pos += amt;
            depth += amt * aim;
            break;
        case "up":
            aim -= amt;
            break;
        default:
            aim += amt;
            break;
    }
}
Console.WriteLine(pos * depth);
