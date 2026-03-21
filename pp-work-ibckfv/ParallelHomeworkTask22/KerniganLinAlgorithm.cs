using System.Collections.Concurrent;

namespace ParallelHomeworkTask22;

public class KerniganLinAlgorithm
{
    private Graph _graph;
    private List<int> A = new List<int>();
    private List<int> B = new List<int>();

    public KerniganLinAlgorithm(Graph graph)
    {
        _graph = graph;
        InitializeParts();
    }

    private void InitializeParts()
    {
        for (int i = 0; i < _graph.NodesCount; i++)
        {
            if (i < _graph.NodesCount / 2)
            {
                A.Add(i);
            }
            else
            {
                B.Add(i);
            }
        }
    }
    
    private int GetGain(int node, List<int> ownPart, List<int> otherPart)
    {
        int externalW = otherPart.Sum(v => _graph.Weights[node, v]);
        int internalW = ownPart.Sum(v => _graph.Weights[node, v]);
        return externalW - internalW;
    }

    public (List<int>, List<int>) FillParts()
    {
        bool isImprove = true;

        while (isImprove)
        {
            isImprove = false;
            var locked = new HashSet<int>();
            var partSize = A.Count;

            for (int step = 0; step < partSize; step++)
            {
                var lockObj = new object();
                var bestGain = int.MinValue;
                var bestA = -1;
                var bestB = -1;

                var candidatesA = A.Where(x => !locked.Contains(x)).ToList();
                var candidatesB = B.Where(y => !locked.Contains(y)).ToList();

                Parallel.ForEach(candidatesA, a =>
                {
                    foreach (var b in candidatesB)
                    {
                        var dA = GetGain(a, A, B);
                        var dB = GetGain(b, B, A);
                        var cAB = _graph.Weights[a, b];
                        var gain = dA + dB - 2 * cAB;

                        lock (lockObj)
                        {
                            if (gain > bestGain)
                            {
                                bestGain = gain;
                                bestA = a;
                                bestB = b;
                            }
                        }
                    }
                });

                if (bestGain <= 0)
                {
                    break;
                }
                
                A.Remove(bestA);
                B.Remove(bestB);
                A.Add(bestB);
                B.Add(bestA);

                locked.Add(bestA);
                locked.Add(bestB);

                isImprove = true;
            }
        }

        return (A, B);
    }
}