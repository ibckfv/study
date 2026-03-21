namespace Test;

public class Task1
{
    public static void SortByDistanceToX(int[] arr, int x)
    { 
        QuickSort.QuickSortForTask1.Sort(arr, 0, arr.Length - 1, x);
    }
}