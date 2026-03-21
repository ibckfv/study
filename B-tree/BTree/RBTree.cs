namespace B_tree;

public class RBTree
{
    private enum Color { Red, Black };

    private class Node
    {
        public int Key;
        public Node Left, Right, Parent;
        public Color NodeColor;
        public Node(int key)
        {
            Key = key;
            Left = Right = Parent = null;
            NodeColor = Color.Red;
        }
    }

    private Node root;
    private Node TNULL; // "пустой" узел

    public RBTree()
    {
        TNULL = new Node(0);
        TNULL.NodeColor = Color.Black;
        TNULL.Left = TNULL.Right = null;
        root = TNULL;
    }

    public bool Search(int key)
    {
        return SearchTreeHelper(root, key);
    }

    private bool SearchTreeHelper(Node node, int key)
    {
        if (node == TNULL || node == null) return false;
        if (key == node.Key) return true;
        return (key < node.Key) ? SearchTreeHelper(node.Left, key)
                                : SearchTreeHelper(node.Right, key);
    }

    public void Insert(int key)
    {
        Node node = new Node(key) { Left = TNULL, Right = TNULL, NodeColor = Color.Red };
        Node y = null;
        Node x = this.root;

        while (x != TNULL)
        {
            y = x;
            x = (node.Key < x.Key) ? x.Left : x.Right;
        }
        node.Parent = y;
        if (y == null)
        {
            root = node;
        }
        else if (node.Key < y.Key)
        {
            y.Left = node;
        }
        else
        {
            y.Right = node;
        }
        if (node.Parent == null)
        {
            node.NodeColor = Color.Black;
            return;
        }
        if (node.Parent.Parent == null)
        {
            return;
        }
        InsertFix(node);
    }

    private void InsertFix(Node k)
    {
        Node u;
        while (k.Parent != null && k.Parent.NodeColor == Color.Red)
        {
            if (k.Parent == k.Parent.Parent.Right)
            {
                u = k.Parent.Parent.Left; // дядя
                if (u != null && u.NodeColor == Color.Red)
                {
                    // случай 1
                    k.Parent.NodeColor = Color.Black;
                    u.NodeColor = Color.Black;
                    k.Parent.Parent.NodeColor = Color.Red;
                    k = k.Parent.Parent;
                }
                else
                {
                    if (k == k.Parent.Left)
                    {
                        // случай 2
                        k = k.Parent;
                        RotateRight(k);
                    }
                    // случай 3
                    k.Parent.NodeColor = Color.Black;
                    k.Parent.Parent.NodeColor = Color.Red;
                    RotateLeft(k.Parent.Parent);
                }
            }
            else
            {
                u = k.Parent.Parent.Right; // дядя
                if (u != null && u.NodeColor == Color.Red)
                {
                    // зеркальный случай 1
                    k.Parent.NodeColor = Color.Black;
                    u.NodeColor = Color.Black;
                    k.Parent.Parent.NodeColor = Color.Red;
                    k = k.Parent.Parent;
                }
                else
                {
                    if (k == k.Parent.Right)
                    {
                        // зеркальный случай 2
                        k = k.Parent;
                        RotateLeft(k);
                    }
                    // зеркальный случай 3
                    k.Parent.NodeColor = Color.Black;
                    k.Parent.Parent.NodeColor = Color.Red;
                    RotateRight(k.Parent.Parent);
                }
            }
            if (k == root)
                break;
        }
        root.NodeColor = Color.Black;
    }

    private void RotateLeft(Node x)
    {
        Node y = x.Right;
        x.Right = y.Left;
        if (y.Left != TNULL)
            y.Left.Parent = x;
        y.Parent = x.Parent;
        if (x.Parent == null)
            root = y;
        else if (x == x.Parent.Left)
            x.Parent.Left = y;
        else
            x.Parent.Right = y;
        y.Left = x;
        x.Parent = y;
    }

    private void RotateRight(Node x)
    {
        Node y = x.Left;
        x.Left = y.Right;
        if (y.Right != TNULL)
            y.Right.Parent = x;
        y.Parent = x.Parent;
        if (x.Parent == null)
            root = y;
        else if (x == x.Parent.Right)
            x.Parent.Right = y;
        else
            x.Parent.Left = y;
        y.Right = x;
        x.Parent = y;
    }

    public bool Delete(int key)
    {
        return DeleteNodeHelper(this.root, key);
    }

    private void Transplant(Node u, Node v)
    {
        if (u.Parent == null)
        {
            root = v;
        }
        else if (u == u.Parent.Left)
        {
            u.Parent.Left = v;
        }
        else
        {
            u.Parent.Right = v;
        }
        v.Parent = u.Parent;
    }

    private Node Minimum(Node node)
    {
        while (node.Left != TNULL)
            node = node.Left;
        return node;
    }

    private bool DeleteNodeHelper(Node node, int key)
    {
        Node z = TNULL;
        Node x, y;
        while (node != TNULL)
        {
            if (node.Key == key)
            {
                z = node;
                break;
            }
            node = (key < node.Key) ? node.Left : node.Right;
        }
        if (z == TNULL) // ключ не найден
            return false;
        y = z;
        Color yOriginalColor = y.NodeColor;
        if (z.Left == TNULL)
        {
            x = z.Right;
            Transplant(z, z.Right);
        }
        else if (z.Right == TNULL)
        {
            x = z.Left;
            Transplant(z, z.Left);
        }
        else
        {
            y = Minimum(z.Right);
            yOriginalColor = y.NodeColor;
            x = y.Right;
            if (y.Parent == z)
            {
                x.Parent = y;
            }
            else
            {
                Transplant(y, y.Right);
                y.Right = z.Right;
                y.Right.Parent = y;
            }
            Transplant(z, y);
            y.Left = z.Left;
            y.Left.Parent = y;
            y.NodeColor = z.NodeColor;
        }
        if (yOriginalColor == Color.Black)
            DeleteFix(x);
        return true;
    }

    private void DeleteFix(Node x)
    {
        Node s;
        while (x != root && x.NodeColor == Color.Black)
        {
            if (x == x.Parent.Left)
            {
                s = x.Parent.Right;
                if (s.NodeColor == Color.Red)
                {
                    s.NodeColor = Color.Black;
                    x.Parent.NodeColor = Color.Red;
                    RotateLeft(x.Parent);
                    s = x.Parent.Right;
                }
                if (s.Left.NodeColor == Color.Black && s.Right.NodeColor == Color.Black)
                {
                    s.NodeColor = Color.Red;
                    x = x.Parent;
                }
                else
                {
                    if (s.Right.NodeColor == Color.Black)
                    {
                        s.Left.NodeColor = Color.Black;
                        s.NodeColor = Color.Red;
                        RotateRight(s);
                        s = x.Parent.Right;
                    }
                    s.NodeColor = x.Parent.NodeColor;
                    x.Parent.NodeColor = Color.Black;
                    s.Right.NodeColor = Color.Black;
                    RotateLeft(x.Parent);
                    x = root;
                }
            }
            else
            {
                // зеркальные случаи
                s = x.Parent.Left;
                if (s.NodeColor == Color.Red)
                {
                    s.NodeColor = Color.Black;
                    x.Parent.NodeColor = Color.Red;
                    RotateRight(x.Parent);
                    s = x.Parent.Left;
                }
                if (s.Right.NodeColor == Color.Black && s.Left.NodeColor == Color.Black)
                {
                    s.NodeColor = Color.Red;
                    x = x.Parent;
                }
                else
                {
                    if (s.Left.NodeColor == Color.Black)
                    {
                        s.Right.NodeColor = Color.Black;
                        s.NodeColor = Color.Red;
                        RotateLeft(s);
                        s = x.Parent.Left;
                    }
                    s.NodeColor = x.Parent.NodeColor;
                    x.Parent.NodeColor = Color.Black;
                    s.Left.NodeColor = Color.Black;
                    RotateRight(x.Parent);
                    x = root;
                }
            }
        }
        x.NodeColor = Color.Black;
    }
}