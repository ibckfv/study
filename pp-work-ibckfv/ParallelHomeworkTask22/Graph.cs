namespace ParallelHomeworkTask22;

public class Graph
{
    public int NodesCount { get; }
    public int[,] Weights { get; }

    public Graph(int nodesCount)
    {
        NodesCount = nodesCount;
        Weights = new int[nodesCount, nodesCount];
    }

    public void AddEdge(int u, int v, int weight)
    {
        Weights[u, v] = weight;
        Weights[v, u] = weight;
    }
}