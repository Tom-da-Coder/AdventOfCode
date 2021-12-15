// See https://aka.ms/new-console-template for more information

namespace AOC2115;

class Program
{

    public enum dir { up, right, down, left };

       static byte[,] risk;
       static int[,] cost;
       static dir[,] predecessor;
       static List<Pos> PosList = new List<Pos>();

    public static void Main(String[] args)
    {
        Console.WriteLine("AOC 2021-15");

        var input = File.ReadAllLines("input.txt");
        int rows = input.Length;
        int cols = input[0].Length;
        risk = new byte[rows, cols];
        int r = 0;
        foreach (var line in input)
        {
            for (int i = 0; i < cols; i++)
                risk[r, i] = (byte)(line[i] - '0');
            r++;
        }

        MakeBmap();

        cost = new int[rows, cols];
        predecessor = new dir[rows, cols];



        PosList.Add(new Pos(0, 0));

        while (PosList.Count > 0)
        {
            Pos p = PosList[0];
            int n = 0;
            for (int i = 0; i < PosList.Count; i++)
            {
                var q = PosList[i];
                if (cost[q.r, q.c] < cost[p.r, p.c])
                {
                    p = q;
                    n = i;
                }
            }
            Pos current = p;
            PosList.RemoveAt(n);
            if (current.r > 0) TestPos(new Pos(current.r - 1, current.c), cost[current.r, current.c], dir.down);
            if (current.r < rows - 1) TestPos(new Pos(current.r + 1, current.c), cost[current.r, current.c], dir.up);
            if (current.c > 0) TestPos(new Pos(current.r, current.c - 1), cost[current.r, current.c], dir.right);
            if (current.c < cols - 1) TestPos(new Pos(current.r, current.c + 1), cost[current.r, current.c], dir.left);
        }


        Console.WriteLine(cost[rows - 1, cols - 1]);

        void MakeBmap()
        {
            var newrisk = new byte[rows * 5, cols * 5];

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    for (int r = 0; r < rows; r++)
                        for (int c = 0; c < cols; c++)
                            newrisk[r + rows * i, c + cols * j] = (byte)Incr(risk[r, c], i + j);
                }
            }
            risk = newrisk;
            rows *= 5;
            cols *= 5;
        }
    }


    private static int Incr(int val, int add)
    {
        val += add;
        if (val > 9)
            val -= 9;
        return val;
    }

    private static void TestPos(Pos pos, int lastcost, dir from)
    {
        if (cost[pos.r, pos.c] == 0 || lastcost + risk[pos.r, pos.c] < cost[pos.r, pos.c])
        {
            cost[pos.r, pos.c] = lastcost + risk[pos.r, pos.c];
            predecessor[pos.r, pos.c] = from;
            PosList.Add(pos);
        }
    }
}

public class Pos { public int r; public int c;

    public Pos(int r, int c)
    {
        this.r = r;
        this.c = c;
    }
}
