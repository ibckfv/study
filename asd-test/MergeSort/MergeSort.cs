namespace MergeSort;

public class MergeSort
{
    public static void Sort(int[] array, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;
            
            Sort(array, left, middle);
            Sort(array, middle + 1, right);
            
            Merge(array, left, middle, right);
        }
    }
    
    private static void Merge(int[] array, int left, int middle, int right)
    {
        int leftLength = middle - left + 1;
        int rightLength = right - middle;

        int[] leftArray = new int[leftLength];
        int[] rightArray = new int[rightLength];

        Array.Copy(array, left, leftArray, 0, leftLength);
        Array.Copy(array, middle + 1, rightArray, 0, rightLength);

        int i = 0, j = 0, k = left;

        while (i < leftLength && j < rightLength)
        {
            if (leftArray[i] <= rightArray[j])
            {
                array[k++] = leftArray[i++];
            }
            else
            {
                array[k++] = rightArray[j++];
            }
        }

        while (i < leftLength)
        {
            array[k++] = leftArray[i++];
        }

        while (j < rightLength)
        {
            array[k++] = rightArray[j++];
        }
    }
}