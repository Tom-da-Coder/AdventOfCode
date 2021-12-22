// See https://aka.ms/new-console-template for more information

Console.WriteLine("AOC 2021-16");

var bits = new BitStream();
var pkt = new Packet(bits);
Console.WriteLine(pkt.SumVersions().ToString());
Console.WriteLine(pkt.Value);


public class Packet
{
    public enum Operator {
        Sum = 0,
        Prod = 1,
        Min = 2,
        Max = 3,
        Const = 4,
        Gtr = 5,
        Less = 6,
        Eql = 7
    };

    public long V;
    public Operator T;
    public bool ltId;
    public List<Packet> Subpackets;
    public long ConstVal = 0;

    public Packet(BitStream bits)
    {
        V = bits.GetIntBits(3);
        T = (Operator)bits.GetIntBits(3);

        if (T == Operator.Const)
                ConstVal = ReadConst(bits);
        else
        {
            ltId = bits.GetBit();
            if (ltId)
            {
                int nSubPkts = (int)bits.GetIntBits(11);
                Subpackets = new List<Packet>();
                for (int i = 0; i < nSubPkts; i++)
                    Subpackets.Add(new Packet(bits));
            }
            else
            {
                int nBitsSubPkts = (int)bits.GetIntBits(15);
                var limit = bits.SetLimit(nBitsSubPkts);
                Subpackets = new List<Packet>();
                while (limit.counter > 0)
                    Subpackets.Add(new Packet(bits));
            }
        }
    }

    public long Value => T switch
    {
        Operator.Sum => Subpackets.Aggregate(0L, (a, p) => a + p.Value),
        Operator.Prod => Subpackets.Aggregate(1L, (a, p) => a * p.Value),
        Operator.Min => Subpackets.Min(p => p.Value),
        Operator.Max => Subpackets.Max(p => p.Value),
        Operator.Const => ConstVal,
        Operator.Gtr => Subpackets[0].Value > Subpackets[1].Value ? 1 : 0,
        Operator.Less => Subpackets[0].Value < Subpackets[1].Value ? 1 : 0,
        Operator.Eql => Subpackets[0].Value == Subpackets[1].Value ? 1 : 0
    };



    public long SumVersions()
    {
        return V + (Subpackets?.Sum(p => p.SumVersions()) ?? 0);
    }

    long ReadConst(BitStream bits)
    {
        bool notlast;
        long ret = 0;
        int nBits = -1;
        do
        {
            notlast = bits.GetBit();
            ret = (ret << 4) + bits.GetIntBits(4);
            nBits += 4;
        } while (notlast);
        if ((ret & (1 << nBits)) != 0)
            while (nBits < 64)
                ret |= (1L << nBits++);
        return ret;
    }
}

public class BitStream
{
    private StreamReader rdr;
    private int part;
    private byte mask = 0;
    private List<Limiter> limiter = new List<Limiter>();

    public BitStream()
    {
        rdr = new StreamReader("input.txt");
    }


    public long GetIntBits(int n)
    {
        if (n > 32)
            throw new Exception();
        long ret = 0;
        long mask = 1U << (n - 1);
        for (int i = 0; i < n; i++)
        {
            if (GetBit())
                ret |= mask;
            mask >>= 1;
        }
        return ret;
    }

    public Limiter SetLimit(int lim)
    {
        var l = new Limiter(lim);
        limiter.Add(l);
        return l;
    }

    public bool GetBit()
    {
        foreach (var limit in limiter)
            limit.counter--;
        limiter.Where(l => l.counter == 0).ToList().ForEach(l => limiter.Remove(l));
        if (mask == 0)
        {
            part = "0123456789ABCDEF".IndexOf((char)rdr.Read());
            mask = 8;
        }
        var ret = (part & mask) != 0;
        mask >>= 1;
        return ret;
    }

    public class Limiter
    {
        public int counter;

        internal Limiter(int cnt)
        {
            counter = cnt;
        }
    }
}