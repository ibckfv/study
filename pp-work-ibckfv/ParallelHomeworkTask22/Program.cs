using ParallelHomeworkTask22;


var g = new Graph(6);
g.AddEdge(0, 1, 2);
g.AddEdge(0, 2, 3);
g.AddEdge(1, 3, 2);
g.AddEdge(2, 3, 1);
g.AddEdge(3, 4, 4);
g.AddEdge(4, 5, 10);

var kl = new KerniganLinAlgorithm(g);
var (A, B) = kl.FillParts();