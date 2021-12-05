// See https://aka.ms/new-console-template for more information
Console.WriteLine("AOC 2021-04A!");

var rdr = new StreamReader("input.txt");

List<int> callNums = rdr.ReadLine().Split(',').Select(s => int.Parse(s)).ToList();
rdr.ReadLine();

var Boards = new List<Board>();
Board brd;

while (!rdr.EndOfStream)
{
    Boards.Add(new Board(rdr));
    if (rdr.EndOfStream)
        break;
    rdr.ReadLine();
}

// ****************************************************
// Play

int firstWin = 0;
int lastWin = 0;

foreach (var num in callNums)
{
    foreach (var board in Boards)
    {
        if (board.HasBingo) continue;
        var bingo = board.AddNum(num);
        switch (bingo)
        {
            case 0:
                break;
            case 1:
            case 2:
                lastWin = num * board.SumUnmarked();
                if (firstWin == 0) firstWin = lastWin;
                break;
        }
    }
}

Console.WriteLine(firstWin);
Console.WriteLine(lastWin);

// ****************************************************

public struct Cell
{
    public int X;
    public bool Checked;

    public Cell(int x)
    {
        X = x;
        Checked = false;
    }
}


public class Board
{
    public Cell[,] Cells = new Cell[5, 5];
    public bool HasBingo = false;

    public Board(StreamReader rdr)
    {
        for (int r = 0; r < 5; r++)
        {
            var colsStr = rdr.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var cols = colsStr.Select(q => int.Parse(q)).ToList();
            for (int c = 0; c < 5; c++)
                Cells[r, c] = new Cell(cols[c]);
        }
    }

    public int AddNum(int num)
    {
        for (int r = 0; r < 5; r++)
            for (int c = 0; c < 5; c++)
                if (Cells[r, c].X == num)
                {
                    Cells[r, c].Checked = true;
                    var bingo = CheckBingo(r, c);
                    if (bingo != 0)
                    {
                        HasBingo = true;
                        return bingo;
                    }
                }
        return 0;
    }

    // Returns:
    //  0 = no bingo
    //  1 = bingo on row r
    //  2 = bingo on column c
    public int CheckBingo(int r, int c)
    {
        if (Enumerable.Range(0, 5).All(x => Cells[r, x].Checked))
            return 1;
        if (Enumerable.Range(0, 5).All(x => Cells[x, c].Checked))
            return 2;
        return 0;
    }

    public int SumUnmarked()
    {
        int sum = 0;
        for (int r = 0; r < 5; r++)
            for (int c = 0; c < 5; c++)
                if (!Cells[r, c].Checked)
                    sum += Cells[r, c].X;
        return sum;
    }
}