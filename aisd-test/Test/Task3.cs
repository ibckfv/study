namespace Test;

public class Task3
{
    public int SumSubtree(BST.Node root, int target) 
    {
        BST.Node targetNode = FindNode(root, target);
        return SumTree(targetNode);
    }

    public BST.Node FindNode(BST.Node root, int target) 
    {
        if (root == null) return null;
        if (root.Value == target) return root;

        BST.Node left = FindNode(root.Left, target);
        if (left != null) return left;

        return FindNode(root.Right, target);
    }

    public int SumTree(BST.Node root) 
    {
        if (root == null) return 0;
        return root.Value + SumTree(root.Left) + SumTree(root.Right);
    }
}