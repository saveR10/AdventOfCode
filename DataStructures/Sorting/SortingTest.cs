using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.DataStructures.Sorting
{
    public static class SortingTest
    {
        //static void Main(string[] args)
        //{
        //    SortingTest.TestSortingAlgorithms();
        //}

        // Bubble Sort
        public static void BubbleSort(int[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }

        // QuickSort
        public static void QuickSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(array, left, right);
                QuickSort(array, left, pivot - 1);
                QuickSort(array, pivot + 1, right);
            }
        }

        private static int Partition(int[] array, int left, int right)
        {
            int pivot = array[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            int temp1 = array[i + 1];
            array[i + 1] = array[right];
            array[right] = temp1;

            return i + 1;
        }

        // MergeSort
        public static void MergeSort(int[] array, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                MergeSort(array, left, middle);
                MergeSort(array, middle + 1, right);
                Merge(array, left, middle, right);
            }
        }

        private static void Merge(int[] array, int left, int middle, int right)
        {
            int n1 = middle - left + 1;
            int n2 = right - middle;

            int[] leftArray = new int[n1];
            int[] rightArray = new int[n2];

            Array.Copy(array, left, leftArray, 0, n1);
            Array.Copy(array, middle + 1, rightArray, 0, n2);

            int i = 0, j = 0, k = left;

            while (i < n1 && j < n2)
            {
                if (leftArray[i] <= rightArray[j])
                {
                    array[k] = leftArray[i];
                    i++;
                }
                else
                {
                    array[k] = rightArray[j];
                    j++;
                }
                k++;
            }

            while (i < n1)
            {
                array[k] = leftArray[i];
                i++;
                k++;
            }

            while (j < n2)
            {
                array[k] = rightArray[j];
                j++;
                k++;
            }
        }

        // HeapSort
        public static void HeapSort(int[] array)
        {
            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(array, n, i);

            for (int i = n - 1; i > 0; i--)
            {
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;

                Heapify(array, i, 0);
            }
        }

        private static void Heapify(int[] array, int n, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && array[left] > array[largest])
                largest = left;

            if (right < n && array[right] > array[largest])
                largest = right;

            if (largest != i)
            {
                int swap = array[i];
                array[i] = array[largest];
                array[largest] = swap;

                Heapify(array, n, largest);
            }
        }

        // Test the sorting algorithms
        public static void TestSortingAlgorithms()
        {
            int[] originalArray = { 5, 2, 9, 1, 5, 6 };

            Console.WriteLine("Original Array: " + string.Join(", ", originalArray));

            // Bubble Sort
            int[] bubbleArray = (int[])originalArray.Clone();
            BubbleSort(bubbleArray);
            Console.WriteLine("Bubble Sort:    " + string.Join(", ", bubbleArray));

            // QuickSort
            int[] quickArray = (int[])originalArray.Clone();
            QuickSort(quickArray, 0, quickArray.Length - 1);
            Console.WriteLine("Quick Sort:     " + string.Join(", ", quickArray));

            // MergeSort
            int[] mergeArray = (int[])originalArray.Clone();
            MergeSort(mergeArray, 0, mergeArray.Length - 1);
            Console.WriteLine("Merge Sort:     " + string.Join(", ", mergeArray));

            // HeapSort
            int[] heapArray = (int[])originalArray.Clone();
            HeapSort(heapArray);
            Console.WriteLine("Heap Sort:      " + string.Join(", ", heapArray));

            // .NET Sort
            int[] dotNetArray = (int[])originalArray.Clone();
            Array.Sort(dotNetArray);
            Console.WriteLine(".NET Sort:      " + string.Join(", ", dotNetArray));
        }
    }
}