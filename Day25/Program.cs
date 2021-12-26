// See https://aka.ms/new-console-template for more information

Console.WriteLine("AOC 2021-");

var input = File.ReadAllLines("input.txt");
var mat = new char[input.Length, input[0].Length];
for (int i = 0; i < input.Length; i++)
    for (int j = 0; j < input[i].Length; j++)
        mat[i, j] = input[i][j];

int hgt = mat.GetLength(0);
int wid  =mat.GetLength(1);

int nMoves = 0;

bool e, s, go = false;
do
{
    e = MoveEast();
    s = MoveSouth();
    nMoves++;
    if (!go)
    {
        WriteOutput(mat);
        Console.WriteLine(nMoves);
        if (!(e || s))
            Console.WriteLine("No change");
        if (Console.ReadLine().StartsWith('g'))
            go = true;
    }
} while (e || s);

Console.WriteLine(nMoves);


bool MoveEast()
{
    bool moved = false;
    var tomove = new List<int>(wid);
    for (int i = 0; i < hgt; i++)
    {
        tomove.Clear();
        for (int j = 0; j < wid; j++)
            if (mat[i, j] == '>' && mat[i, (j + 1) % wid] == '.')
                tomove.Add(j);
        moved |= tomove.Count > 0;
        foreach (int j in tomove)
        {
            mat[i, (j + 1) % wid] = '>';
            mat[i, j] = '.';
        }
    }
    return moved;
}

bool MoveSouth()
{
    bool moved = false;
    var tomove = new List<int>(hgt);
    for (int j = 0; j < wid; j++)
    {
        tomove.Clear();
        for (int i = 0; i < hgt; i++)
            if (mat[i, j] == 'v' && mat[(i + 1) % hgt, j] == '.')
                tomove.Add(i);
        moved |= tomove.Count > 0;
        foreach (var i in tomove)
        {
            mat[(i + 1) % hgt, j] = 'v';
            mat[i, j] = '.';
        }
    }
    return moved;
}

void WriteOutput(char[,] matx)
{
    for (int i = 0; i < matx.GetLength(0); i++)
    {
        for (int j = 0;j < matx.GetLength(1); j++)
            Console.Write(matx[i, j]);
        Console.WriteLine();
    }
}