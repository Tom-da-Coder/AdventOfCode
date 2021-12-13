﻿// See https://aka.ms/new-console-template for more information

Console.WriteLine("AOC 2021-");
int answer = 0;
bool haveOneTwice = false;
Stack<string> path = new Stack<string>();

Dictionary<string, Node> NodeList = new Dictionary<string, Node>();

var input = File.ReadAllLines("input3.txt");
foreach (var line in input)
{
    var nodes = line.Split('-').Select(x => (name: x, node: new Node(x))).Select(q => NodeList.TryAdd(q.name, q.node) ? q.node : NodeList[q.name]).ToList();
    nodes[0].Neighbours.Add(nodes[1]);
    nodes[1].Neighbours.Add(nodes[0]);
}
NodeList["start"].BinHere = 2;
ScanOneNode(NodeList["start"]);

Console.WriteLine(answer);


void ScanOneNode(Node node)
{
    path.Push(node.Name);
    var twiceSave = haveOneTwice;
    if (!node.BigCave && ++node.BinHere == 2)
        haveOneTwice = true;
    foreach (var neighbour in node.Neighbours)
    {
        if (neighbour.Name == "end")    // Mark full path
        {
            answer++;
            Console.WriteLine(string.Join('-', path.Reverse()));
        }
        else if (neighbour.BigCave || neighbour.BinHere < 2 && !haveOneTwice)
            ScanOneNode(neighbour);
    }
    haveOneTwice = twiceSave;
    if (!node.BigCave)
        node.BinHere--;
    path.Pop();
}


public class Node
{
    public readonly string Name;
    public List<Node> Neighbours = new List<Node>();
    public int BinHere = 0;
    public readonly bool BigCave;

    public Node(string name)
    {
        Name = name;
        BigCave = Char.IsUpper(name[0]);
    }
}