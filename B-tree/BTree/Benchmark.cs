using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using B_tree;

[SimpleJob(launchCount:1, warmupCount:1, iterationCount:3)]
[MemoryDiagnoser]
public class TreeBenchmark
{
    [Params(1000, 10000, 100000, 1000000, 10000000)]
    public int N;

    private int[] values;
    private BTree btree;
    private AVLTree avl;
    private RBTree rbt;

    [GlobalSetup]
    public void Setup()
    {
        var rand = new Random(42);
        values = new int[N];
        for (int i = 0; i < N; i++)
            values[i] = rand.Next();
        btree = new BTree(3);
        avl = new AVLTree();
        rbt = new RBTree();
    }

    [Benchmark(OperationsPerInvoke = 1)]
    public void Insert_BTree()
    {
        for (int i = 0; i < N; i++)
            btree.Insert(values[i]);
    }

    [Benchmark(OperationsPerInvoke = 1)]
    public void Search_BTree()
    {
        for (int i = 0; i < N; i++)
            btree.Search(values[i]);
    }

    [Benchmark(OperationsPerInvoke = 1)]
    public void Delete_BTree()
    {
        for (int i = 0; i < N; i++)
            btree.Delete(values[i]);
    }

    [Benchmark(OperationsPerInvoke = 1)]
    public void Insert_RBTree()
    {
        for (int i = 0; i < N; i++)
            rbt.Insert(values[i]);
    }

    [Benchmark(OperationsPerInvoke = 1)]
    public void Search_RBTree()
    {
        for (int i = 0; i < N; i++)
            rbt.Search(values[i]);
    }

    [Benchmark(OperationsPerInvoke = 1)]
    public void Delete_RBTree()
    {
        for (int i = 0; i < N; i++)
            rbt.Delete(values[i]);
    }

    [Benchmark(OperationsPerInvoke = 1)]
    public void Insert_AvlTree()
    {
        for (int i = 0; i < N; i++)
            avl.Insert(values[i]);
    }

    [Benchmark(OperationsPerInvoke = 1)]
    public void Search_AvlTree()
    {
        for (int i = 0; i < N; i++)
            avl.Search(values[i]);
    }

    [Benchmark(OperationsPerInvoke = 1)]
    public void Delete_AvlTree()
    {
        for (int i = 0; i < N; i++)
            avl.Delete(values[i]);
    }
}