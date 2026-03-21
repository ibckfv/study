namespace Test;

public class Task2
{
    public LinkedList.MyLinkedList<int> DeleteDuplicates(LinkedList.MyLinkedList<int> head) 
    {
        LinkedList.Node<int> current = head.Head;
        
        while (current != null) 
        {
            LinkedList.Node<int> r = current;
            while (r.Next != null) 
            {
                if (r.Next.Value == current.Value) 
                {
                    r.Next = r.Next.Next;
                } 
                else
                {
                    r = r.Next;
                }
            }
            current = current. Next;
        }
        
        return head;
    }
}