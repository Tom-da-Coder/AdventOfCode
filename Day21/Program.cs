// See https://aka.ms/new-console-template for more information
Console.WriteLine("AOC 2021 day 21");

Dice dice = new Dice();
var player1 = new Player(8);
var player2 = new Player(6);

while (true) {
    if (player1.TakeTurn(dice))
    {
        Console.WriteLine(player2.Score * dice.turns);
        break;
    }
    if (player2.TakeTurn(dice))
    {
        Console.WriteLine(player1.Score * dice.turns);
        break;
    }
}

public class Player
{
    public int Score = 0;
    public int Pos;

    public Player(int startPos)
    {
        Pos = startPos - 1;
    }

    public bool TakeTurn(Dice dice)
    {
        for (int i = 0; i < 3; i++)
        {
            Pos = (Pos + dice.Roll()) % 10;
        }
        Score += Pos + 1;
        return Score >= 1000;
    }
}


public class Dice
{
    int val = 0;
    public int turns = 0;
    public int Roll()
    {
        turns++;
        var ret = val;
        val = (val + 1) % 100;
        return ret + 1;
    }
}
