// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

Console.WriteLine("AOC 2021-08");

var lens = new int[] { 2, 3, 4, 7 };
int[] digits = new int[] { 0x77, 0x24, 0x5d, 0x6d, 0x2e, 0x6b, 0x7b, 0x25, 0x7f, 0x6f };
var KeyList = GenKeys();

int totCount = 0;
int sumBpart = 0;

var input = File.ReadAllLines("input.txt");
foreach (var line in input)
{
    //var match = Regex.Match(line, @"\|( (?\w+))+$", RegexOptions.ExplicitCapture);
    //var n = match.Captures;

    var spl = line.Split('|');
    var (first, last) = (spl[0], spl[1]);
    var firsts = first.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var lasts = last.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    totCount += lasts.Count(t => lens.Contains(t.Length));

    //var match = Regex.Match(line, @"\w+");
    var allDigits = firsts.Concat(lasts).ToList();
    var allBinary = allDigits.Select(t => ToByte(t)).ToList();

    byte[] foundKey = FindKey(allBinary);
    if (foundKey != null)
    {
        allBinary = allBinary.Select(t => IndexOf(Decode(t, foundKey))).ToList();
    }

    var val = allBinary.Skip(10).Aggregate(0, (a, b) => 10 * a + b);
    sumBpart += val;

    //foreach (var s in allBinary)
    //{
    //    Console.Write($" {s}");
    //}
    //Console.WriteLine();
}
Console.WriteLine(totCount);
Console.WriteLine(sumBpart);


byte[] FindKey(List<int> testData)
{
    foreach (var key in KeyList)
        if (testData.All(t => digits.Contains(Decode(t, key))))    // Key is found!
            return key;    
    return null;
}

int IndexOf(int digit)
{
    for (int i = 0; i < 10; i++)
        if (digits[i] == digit)
            return i;
    return -1;
}


int Decode(int input, byte[] key)
{
    int res = 0;
    for (int i = 0; i < 7; i++)
    {
        if ((input & 1) > 0)
            res |= key[i];
        if ((input >>= 1) == 0)
            break;
    }
    return res;
}


int ToByte(string digit)
{
    int ret = 0;
    for (int i = 0; i < digit.Length; i++)
    {
        ret |= 1 << (digit[i] - 'a');
    }
    return ret;
}


List<byte[]> GenKeys()
{
    var retval = new List<byte[]>();
    var ret = new byte[7];
    for (int i = 0; i < 7; i++)
    {
        ret[0] = (byte)(1 << i);
        for (int j = 0; j < 7; j++)
        {
            if (i == j) continue;
            ret[1] = (byte)(1 << j);
            for (int k = 0; k < 7; k++)
            {
                if (k == j || k == i) continue;
                ret[2] = (byte)(1 << k);
                for (int l = 0; l < 7; l++)
                {
                    if (l == k || l == j || l == i) continue;
                    ret[3] = (byte)(1 << l);
                    for (int m = 0; m < 7; m++)
                    {
                        if (m == l || m == k || m == j || m == i) continue;
                        ret[4] = (byte)(1 << m);
                        for (int n = 0; n < 7; n++)
                        {
                            if (n == m || n == l || n == k || n == j || n == i) continue;
                            ret[5] = (byte)(1 << n);
                            for (int o = 0; o < 7; o++)
                            {
                                if (o == n || o == m || o == l || o == k || o == j || o == i) continue;
                                ret[6] = (byte)(1 << o);
                                var cp = new byte[7];
                                ret.CopyTo(cp, 0);
                                retval.Add(cp);
                            }
                        }
                    }
                }
            }
        }
    }
    return retval;
}