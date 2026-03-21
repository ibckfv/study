namespace QuickSort;

public class QuickSortForTask1
{
    public static void Sort(int[] array, int left, int right, int x)
    {
        if (left < right)
        {
            int pivotIndex = Partition(array, left, right, x);
            
            Sort(array, left, pivotIndex - 1, x);
            Sort(array, pivotIndex + 1, right, x);
        }
    }
    
    public static int Partition(int[] array, int left, int right, int x)
    {
        int pivot = array[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (Compare(array[j], pivot, x))
            {
                i++;
                Swap(array, i, j);
            }
        }

        Swap(array, i + 1, right);
        return i + 1;
    }
    
    public static void Swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
    
    public static bool Compare(int a, int b, int x)
    {
        int distA = Math.Abs(a - x);
        int distB = Math.Abs(b - x);

        return distA != distB ? distA < distB : a < b;
    }
}