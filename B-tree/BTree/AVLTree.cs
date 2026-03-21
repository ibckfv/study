namespace B_tree;

public class AVLTree
{
    private class Node
    {
        public int Key;
        public Node Left, Right;
        public int Height;
        public Node(int key)
        {
            Key = key;
            Left = Right = null;
            Height = 1;
        }
    }

    private Node root;

    public void Insert(int key)
    {
        root = InsertInternal(root, key);
    }

    private Node InsertInternal(Node node, int key)
    {
        if (node == null)
            return new Node(key);
        if (key < node.Key)
            node.Left = InsertInternal(node.Left, key);
        else if (key > node.Key)
            node.Right = InsertInternal(node.Right, key);
        else
            return node; // повторяющиеся ключи не храним

        // обновляем высоту и баланс
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        int balance = GetBalance(node);
        // лево-лево
        if (balance > 1 && key < node.Left.Key)
            return RotateRight(node);
        // право-право
        if (balance < -1 && key > node.Right.Key)
            return RotateLeft(node);
        // лево-право
        if (balance > 1 && key > node.Left.Key)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }
        // право-лево
        if (balance < -1 && key < node.Right.Key)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }
        return node;
    }

    public void Delete(int key)
    {
        root = DeleteInternal(root, key);
    }

    private Node DeleteInternal(Node node, int key)
    {
        if (node == null) return node;
        if (key < node.Key)
            node.Left = DeleteInternal(node.Left, key);
        else if (key > node.Key)
            node.Right = DeleteInternal(node.Right, key);
        else
        {
            // узел с одним или нулевым потомком
            if (node.Left == null || node.Right == null)
            {
                Node temp = (node.Left != null) ? node.Left : node.Right;
                if (temp == null)
                {
                    // нет потомков
                    node = null;
                }
                else
                {
                    node = temp;
                }
            }
            else
            {
                // два потомка: берем следующий по величине (минимум из правого поддерева)
                Node temp = MinValueNode(node.Right);
                node.Key = temp.Key;
                node.Right = DeleteInternal(node.Right, temp.Key);
            }
        }
        if (node == null) return null;
        // обновляем высоту и баланс
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        int balance = GetBalance(node);
        // левое тяжелое
        if (balance > 1)
        {
            if (GetBalance(node.Left) >= 0)
                return RotateRight(node);
            else
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
        }
        // правое тяжелое
        if (balance < -1)
        {
            if (GetBalance(node.Right) <= 0)
                return RotateLeft(node);
            else
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }
        }
        return node;
    }

    private Node MinValueNode(Node node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }

    public bool Search(int key)
    {
        return SearchInternal(root, key);
    }

    private bool SearchInternal(Node node, int key)
    {
        if (node == null) return false;
        if (key < node.Key) return SearchInternal(node.Left, key);
        if (key > node.Key) return SearchInternal(node.Right, key);
        return true;
    }

    private int GetHeight(Node node)
    {
        return node?.Height ?? 0;
    }

    private int GetBalance(Node node)
    {
        return (node == null) ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
    }

    private Node RotateRight(Node y)
    {
        Node x = y.Left;
        Node T2 = x.Right;
        // поворот
        x.Right = y;
        y.Left = T2;
        // обновление высот
        y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));
        x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));
        return x;
    }

    private Node RotateLeft(Node x)
    {
        Node y = x.Right;
        Node T2 = y.Left;
        // поворот
        y.Left = x;
        x.Right = T2;
        // обновление высот
        x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));
        y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));
        return y;
    }
}