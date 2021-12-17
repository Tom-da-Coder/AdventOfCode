// See https://aka.ms/new-console-template for more information

Console.WriteLine("AOC 2021-");


foreach (var line in input)
{

}
Console.WriteLine(totCount);

public class BitStream
{
    private StreamReader rdr;
    private byte part;
    private int nBits;
    public BitStream()
    {
        rdr = new StreamReader("input.txt");
        part = (byte)rdr.Read();
        nBits = 8;
    }

    public int GetBits(int n)
    {
        if (n > 32)
            throw new Exception();
        int ret = 0;

    }
}