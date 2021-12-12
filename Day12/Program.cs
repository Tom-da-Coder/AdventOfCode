// See https://aka.ms/new-console-template for more information

Console.WriteLine("AOC 2021-");
int answer = 0;

Dictionary<string, Node> NodeList = new Dictionary<string, Node>();

var input = File.ReadAllLines("input2.txt");
foreach (var line in input)
{
    var nodes = line.Split('-').Select(x => (name: x, node: new Node(x))).Select(q => NodeList.TryAdd(q.name, q.node) ? q.node : NodeList[q.name]).ToList();
    nodes[0].Neighbours.Add(nodes[1]);
    nodes[1].Neighbours.Add(nodes[0]);
}



Console.WriteLine(answer);


public class Node
{
    public readonly string Name;
    public List<Node> Neighbours = new List<Node>();
    public bool BinHere = false;
    public readonly bool BigCave;

    public Node(string name)
    {
        Name = name;
        BigCave = Char.IsUpper(name[0]);
    }
}