namespace B_tree;

public class BTreeNode
{
    public int[] Keys { get; }         // массив ключей
    public BTreeNode[] Childs { get; }     // массив потомков
    public bool IsLeaf { get; }     // признак листа
    public int CurrentKeysCount { get; set; }      // текущее число ключей
    private int _degree;             // минимальная степень

    public BTreeNode(int degree, bool isLeaf)
    {
        _degree = degree;
        IsLeaf = isLeaf;
        Keys = new int[2 * degree - 1];
        Childs = new BTreeNode[2 * degree];
        CurrentKeysCount = 0;
    }
    
    // Поиск ключа k в поддереве с корнем в этом узле
    public BTreeNode Search(int k)
    {
        int i = 0;
        while (i < CurrentKeysCount && k > Keys[i])
            i++;
        if (i < CurrentKeysCount && Keys[i] == k)  // нашли ключ
            return this;
        if (IsLeaf)                   // ключа нет и мы в листе
            return null;
        return Childs[i].Search(k);      // спускаемся в i-ый потомок
    }
    
    // Вставка ключа в неполный узел
    public void InsertNonFull(int k)
    {
        int i = CurrentKeysCount - 1;
        if (IsLeaf)
        {
            // вставляем ключ в отсортированный массив ключей узла-листа
            while (i >= 0 && Keys[i] > k)
            {
                Keys[i + 1] = Keys[i];
                i--;
            }
            Keys[i + 1] = k;
            CurrentKeysCount++;
        }
        else
        {
            // находим ребенка, куда спуститься
            while (i >= 0 && Keys[i] > k)
                i--;
            i++;
            // если потомок полон, разделяем его
            if (Childs[i].CurrentKeysCount == 2*_degree - 1)
            {
                SplitChild(i, Childs[i]);
                if (Keys[i] < k)
                    i++;
            }
            Childs[i].InsertNonFull(k);
        }
    }
    
    // Разделить i-й полный потомок y этого узла
    public void SplitChild(int i, BTreeNode y)
    {
        BTreeNode z = new BTreeNode(y._degree, y.IsLeaf);
        z.CurrentKeysCount = _degree - 1;
        // копируем последние t-1 ключей y в z
        for (int j = 0; j < _degree - 1; j++)
            z.Keys[j] = y.Keys[j + _degree];
        // если не лист, копируем потомков
        if (!y.IsLeaf)
        {
            for (int j = 0; j < _degree; j++)
                z.Childs[j] = y.Childs[j + _degree];
        }
        y.CurrentKeysCount = _degree - 1;
        // сдвигаем указатели и ключи в этом узле, вставляем z
        for (int j = CurrentKeysCount; j >= i + 1; j--)
            Childs[j + 1] = Childs[j];
        Childs[i + 1] = z;
        for (int j = CurrentKeysCount - 1; j >= i; j--)
            Keys[j + 1] = Keys[j];
        Keys[i] = y.Keys[_degree - 1];
        CurrentKeysCount++;
    }
    
    public void Remove(int k)
    {
        int idx = FindKey(k);
        if (idx < CurrentKeysCount && Keys[idx] == k)
        {
            if (IsLeaf)
                RemoveFromLeaf(idx);
            else
                RemoveFromNonLeaf(idx);
        }
        else
        {
            if (IsLeaf) return; // ключа нет в дереве
            bool flag = (idx == CurrentKeysCount);
            if (Childs[idx].CurrentKeysCount < _degree) Fill(idx);
            if (flag && idx > CurrentKeysCount)
                Childs[idx - 1].Remove(k);
            else
                Childs[idx].Remove(k);
        }
    }
    
    // Найти индекс первого ключа >= k
    private int FindKey(int k)
    {
        int idx = 0;
        while (idx < CurrentKeysCount && Keys[idx] < k) idx++;
        return idx;
    }
    
    private void RemoveFromLeaf(int idx)
    {
        for (int i = idx + 1; i < CurrentKeysCount; i++)
            Keys[i - 1] = Keys[i];
        CurrentKeysCount--;
    }
    
    private void RemoveFromNonLeaf(int idx)
    {
        int k = Keys[idx];
        if (Childs[idx].CurrentKeysCount >= _degree)
        {
            int pred = GetPredecessor(idx);
            Keys[idx] = pred;
            Childs[idx].Remove(pred);
        }
        else if (Childs[idx + 1].CurrentKeysCount >= _degree)
        {
            int succ = GetSuccessor(idx);
            Keys[idx] = succ;
            Childs[idx + 1].Remove(succ);
        }
        else
        {
            Merge(idx);
            Childs[idx].Remove(k);
        }
    }
    
    private int GetPredecessor(int idx)
    {
        BTreeNode cur = Childs[idx];
        while (!cur.IsLeaf) cur = cur.Childs[cur.CurrentKeysCount];
        return cur.Keys[cur.CurrentKeysCount - 1];
    }
    
    private int GetSuccessor(int idx)
    {
        BTreeNode cur = Childs[idx + 1];
        while (!cur.IsLeaf) cur = cur.Childs[0];
        return cur.Keys[0];
    }
    
    private void Fill(int idx)
    {
        if (idx != 0 && Childs[idx - 1].CurrentKeysCount >= _degree)
            BorrowFromPrev(idx);
        else if (idx != CurrentKeysCount && Childs[idx + 1].CurrentKeysCount >= _degree)
            BorrowFromNext(idx);
        else
        {
            if (idx != CurrentKeysCount) Merge(idx);
            else Merge(idx - 1);
        }
    }
    
    private void BorrowFromPrev(int idx)
    {
        BTreeNode child = Childs[idx];
        BTreeNode sibling = Childs[idx - 1];
        for (int i = child.CurrentKeysCount - 1; i >= 0; --i)
            child.Keys[i + 1] = child.Keys[i];
        if (!child.IsLeaf)
        {
            for (int i = child.CurrentKeysCount; i >= 0; --i)
                child.Childs[i + 1] = child.Childs[i];
        }
        child.Keys[0] = Keys[idx - 1];
        if (!child.IsLeaf)
            child.Childs[0] = sibling.Childs[sibling.CurrentKeysCount];
        Keys[idx - 1] = sibling.Keys[sibling.CurrentKeysCount - 1];
        child.CurrentKeysCount++;
        sibling.CurrentKeysCount--;
    }
    
    private void BorrowFromNext(int idx)
    {
        BTreeNode child = Childs[idx];
        BTreeNode sibling = Childs[idx + 1];
        child.Keys[child.CurrentKeysCount] = Keys[idx];
        if (!child.IsLeaf)
            child.Childs[child.CurrentKeysCount + 1] = sibling.Childs[0];
        Keys[idx] = sibling.Keys[0];
        for (int i = 1; i < sibling.CurrentKeysCount; ++i)
            sibling.Keys[i - 1] = sibling.Keys[i];
        if (!sibling.IsLeaf)
        {
            for (int i = 1; i <= sibling.CurrentKeysCount; ++i)
                sibling.Childs[i - 1] = sibling.Childs[i];
        }
        child.CurrentKeysCount++;
        sibling.CurrentKeysCount--;
    }
    
    private void Merge(int idx)
    {
        BTreeNode child = Childs[idx];
        BTreeNode sibling = Childs[idx + 1];
        child.Keys[_degree - 1] = Keys[idx];
        for (int i = 0; i < sibling.CurrentKeysCount; ++i)
            child.Keys[i + _degree] = sibling.Keys[i];
        if (!child.IsLeaf)
        {
            for (int i = 0; i <= sibling.CurrentKeysCount; ++i)
                child.Childs[i + _degree] = sibling.Childs[i];
        }
        for (int i = idx + 1; i < CurrentKeysCount; ++i)
            Keys[i - 1] = Keys[i];
        for (int i = idx + 2; i <= CurrentKeysCount; ++i)
            Childs[i - 1] = Childs[i];
        child.CurrentKeysCount += sibling.CurrentKeysCount + 1;
        CurrentKeysCount--;
    }
}
