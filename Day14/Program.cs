// See https://aka.ms/new-console-template for more information

using System.Text;

const int NGENS = 10;
Console.WriteLine("AOC 2021-14");

var input = File.ReadAllLines("input.txt");
string generation = input[0];
StringBuilder sb;
List<Insertion> insertions = new();
var rules = input.Skip(2).Select(s => new Rule(s)).ToList();

for (int gen = 0; gen < NGENS; gen++)
{
    MakeInsertions();
    MergeInsertions();
}

var histo = generation.ToArray().GroupBy(c => c).Select(q => (key: q.Key, antal: q.Count())).OrderBy(r => r.antal).ToList();

Console.WriteLine(histo.Last().antal - histo.First().antal);

void MakeInsertions()
{
    insertions.Clear();
    foreach (var rule in rules)
    {
        int pos = 0;
        while ((pos = generation.IndexOf(rule.Pattern, pos)) >= 0)
        {
            pos++;
            insertions.Add(new Insertion { Char = rule.NewChar, Pos = pos });
        }
    }
}

void MergeInsertions()
{
    sb = new StringBuilder(2 * generation.Length);
    insertions = insertions.OrderBy(i => i.Pos).ToList();
    int pos = 0;
    foreach (var ins in insertions)
    {
        sb.Append(generation, pos, ins.Pos - pos);
        sb.Append(ins.Char);
        pos = ins.Pos;
    }
    sb.Append(generation, pos, generation.Length - pos);
    generation = sb.ToString();
}

public class Insertion
{
    public int Pos;
    public string Char;
}

public class Rule
{
    public readonly string Pattern;
    public readonly string NewChar;

    public Rule(string line)
    {
        Pattern = line.Substring(0,2);
        NewChar = line.Substring(6,1);
    }
}