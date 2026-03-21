namespace BST;

public class Tree
{
    public Node Root;

    public void Insert(int value)
    {
        Root = InsertRecursive(Root, value);
    }

    private Node InsertRecursive(Node root, int value)
    {
        if (root == null)
            return new Node(value);

        if (value < root.Value)
            root.Left = InsertRecursive(root.Left, value);
        else if (value > root.Value)
            root.Right = InsertRecursive(root.Right, value);

        return root;
    }

    public bool Search(int value)
    {
        return SearchRecursive(Root, value);
    }

    private bool SearchRecursive(Node root, int value)
    {
        if (root == null)
            return false;

        if (value == root.Value)
            return true;
        else if (value < root.Value)
            return SearchRecursive(root.Left, value);
        else
            return SearchRecursive(root.Right, value);
    }

    public void InOrderTraversal()
    {
        InOrderRecursive(Root);
        Console.WriteLine();
    }

    private void InOrderRecursive(Node node)
    {
        if (node != null)
        {
            InOrderRecursive(node.Left);
            Console.Write($"{node.Value} ");
            InOrderRecursive(node.Right);
        }
    }
}