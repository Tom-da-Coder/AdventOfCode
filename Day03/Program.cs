// See https://aka.ms/new-console-template for more information
Console.WriteLine("AOC 2021 day 03, part 1 & 2");
var words = new List<string>();
string s;
var fil = new StreamReader("input.txt");

while ((s = fil.ReadLine()) != null)
{
    words.Add(s);
}

int[] counts = new int[words[0].Length];

for (int i = 0; i < words[0].Length; i++)
    foreach (string word in words)
        if (word[i] == '1')
            counts[i]++;
int a = 0, b = 0;

int n = words.Count / 2;

for (int i = 0;i < counts.Length; i++)
{
    a = 2 * a + (counts[i] > n ? 1 : 0);
    b = 2 * b + (counts[i] > n ? 0 : 1);
}

Console.WriteLine(a * b);

// Part 2

var res = words;
string sA = null, sB = null;

for (int i = 0; i < words[0].Length; i++)
{
    res = filterout(res, i, false);
    if (res.Count == 1)
    {
        sA = res[0];
        break;
    }
}

res = words;
for (int i = 0; i < words[0].Length; i++)
{
    res = filterout(res, i, true);
    if (res.Count == 1)
    {
        sB = res[0];
        break;
    }
}

Console.WriteLine(binstrparse(sA) * binstrparse(sB));

List<string> filterout(List<string> words, int pos, bool rev)
{
    int count = 0;
    for (int i = 0; i < words.Count; i++)
        if (words[i][pos] == '1')
            count++;
    char selector = rev ^ (count >= words.Count / 2) ? '1' : '0';
    return words.Where(x => x[pos] == selector).ToList();
}

int binstrparse(string s)
{
    int ret = 0;
    for (int i = 0; i < s.Length; i++)
    {
        ret *= 2;
        if (s[i] == '1')
            ret++;
    }
    return ret;
}