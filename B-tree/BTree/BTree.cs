namespace B_tree;

public class BTree
{
    private BTreeNode _root;
    private int _degree;  // минимальная степень

    public BTree(int degree)
    {
        _root = null;
        _degree = degree;
    }
    
    // Поиск ключа в дереве
    public bool Search(int k)
    {
        return (_root != null) && (_root.Search(k) != null);
    }
    
    // Вставка ключа в дерево
    public void Insert(int k)
    {
        if (_root == null)
        {
            _root = new BTreeNode(_degree, true);
            _root.Keys[0] = k;
            _root.CurrentKeysCount = 1;
        }
        else
        {
            if (_root.CurrentKeysCount == 2*_degree - 1)
            {
                BTreeNode s = new BTreeNode(_degree, false);
                s.Childs[0] = _root;
                s.SplitChild(0, _root);
                int i = (s.Keys[0] < k) ? 1 : 0;
                s.Childs[i].InsertNonFull(k);
                _root = s;
            }
            else
            {
                _root.InsertNonFull(k);
            }
        }
    }
    
    // Удаление ключа из дерева
    public void Delete(int k)
    {
        if (_root == null) return;
        _root.Remove(k);
        if (_root.CurrentKeysCount == 0)
        {
            if (_root.IsLeaf) _root = null;
            else _root = _root.Childs[0];
        }
    }
}