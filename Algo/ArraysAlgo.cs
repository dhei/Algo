using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;
using NUnit.Framework;

namespace ArraysAlgo
{
    /// <summary>
    /// Question 5
    /// find the kth smallest number of the given array
    /// </summary>
    public class ArrayKthSmallest
    {

        // use a max heap (priority queue) of size k
        // (C# doesn't provide priority queue, use SortedList instead)
        public static int KthSmallest(int[] a, int k)
        {
            SortedList list = new SortedList();
            int m = a.Length - k + 1;
            for (int i = 0; i < m; i++)
                list.Add(a[i], null);
            for (int i = m; i < a.Length; i++)
            {
                if ((int)list.GetKey(0) < a[i])
                {
                    list.RemoveAt(0);
                    list.Add(a[i], null);
                }
            }
            return (int)list.GetKey(0);
        }

        [Test]
        public static void ArrayKthSmallest_Test()
        {
            int[] a = { 2, 1, 3, 5, 4, 7, 6, 8, 9, 10 };
            Console.WriteLine(ArrayKthSmallest.KthSmallest(a, 7));
            Assert.AreEqual(7, ArrayKthSmallest.KthSmallest(a, 7));
        }
    }

    /// <summary>
    /// Question 10
    /// Given an unsorted array and N, find two elements in the array with sum N
    /// </summary>
    public class TwoNumWithSum
    {
        public static int num1;
        public static int num2;

        // find two numbers with given sum in the array
        // return ture if there are found, otherwise false
        public static bool FindTwoNumWithSum(int[] a, int N)
        {
            if (a.Length < 2) return false;
            Array.Sort(a); // 1. sort the array
            int p1 = 0;
            int p2 = a.Length - 1;
            while (a[p1] + a[p2] != N)
            {
                if (a[p1] + a[p2] < N && p1 + 1 < p2) p1++;
                else if (a[p1] + a[p2] > N && p1 + 1 < p2) p2--;
                else return false; // not found
            }
            // found it!
            num1 = a[p1];
            num2 = a[p2];
            return true;
        }

        // use hashtable, O(N) time
        public static bool FindTwoNumWithSum_By_Hashmap(int[] a, int N)
        {
            if (a.Length < 2) return false;
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < a.Length; i++)
                set.Add(a[i]);
            for (int i = 0; i < a.Length; i++)
            {
                if (set.Contains(N - a[i]))
                {
                    num1 = a[i];
                    num2 = N - a[i];
                    return true;
                }
            }
            return false;
        }

        [Test]
        public static void TwoNumWithSum_Test()
        {
            int[] a = { 2, 4, 7, 11, 15 };
            num1 = num2 = 0;
            bool result = TwoNumWithSum.FindTwoNumWithSum(a, 15);
            Console.WriteLine("N = 15, num1 = {0}, num2 = {1}", num1, num2);
            Assert.AreEqual(4, num1);
            Assert.AreEqual(11, num2);
            num1 = num2 = 0;
            result = TwoNumWithSum.FindTwoNumWithSum_By_Hashmap(a, 12);
            if (result) Console.WriteLine("N = 12, num1 = {0}, num2 = {1}", num1, num2);
            Assert.IsFalse(result);
        }
    }

    /// <summary>
    /// Find three numbers in an array with given sum N
    /// </summary>
    public class ThreeNumWithSum
    {
        private static int num1, num2, num3;

        public static bool ThreeSum_by_hashtable(int[] a, int N)
        {
            if (a.Length < 3) return false;
            HashSet<int> hm = new HashSet<int>();
            Array.Sort(a); // O(N lgN) time
            for (int i = 0; i < a.Length; i++) // O(N^2) time and space
            {
                for (int j = 1; j < a.Length; j++)
                {
                    hm.Add(a[i] + a[j]);
                }
            }
            Dictionary<int, int> result;
            for (int i = 0; i < a.Length; i++) // O(N^2) time and space
            {
                if (hm.Contains(N - a[i]))
                {
                    result = FindTwoNumWithSum(a, N - a[i]); // O(N) time and space
                    foreach (KeyValuePair<int, int> pair in result) // O(N) time
                    {
                        if (i != pair.Key && i != pair.Value)
                        {
                            num1 = a[pair.Key]; num2 = a[pair.Value]; num3 = a[i];
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static Dictionary<int, int> FindTwoNumWithSum(int[] a, int N)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            if (a.Length < 2) return result;
            int p1 = 0;
            int p2 = a.Length - 1;

            while (p1 < p2)
            {
                if (a[p1] + a[p2] == N)
                {
                    result.Add(p1, p2);
                    p1++; p2--;
                }
                else if (a[p1] + a[p2] < N && p1 + 1 < p2)
                    p1++;
                else if (a[p1] + a[p2] > N && p1 + 1 < p2)
                    p2--;

            }
            return result;
        }
        
        //[Test]
        public static void ThreeNumWithSum_Test()
        {
            int[] a1 = { 2, 4, 3, 2, 1, 2 };
            bool result;
            result = ThreeSum_by_hashtable(a1, 3);
            if (result)
                Console.WriteLine(num1 + " + " + num2 + " + " + num3 + " = " + 3);
            else
                Console.WriteLine("no solution");
            
            result = ThreeSum_by_hashtable(a1, 6);
            if (result)
                Console.WriteLine(num1 + " + " + num2 + " + " + num3 + " = " + 3);
            else
                Console.WriteLine("no solution");
            result = ThreeSum_by_hashtable(a1, 5);
            if (result)
                Console.WriteLine(num1 + " + " + num2 + " + " + num3 + " = " + 3);
            else
                Console.WriteLine("no solution");
        }

    }

    /// <summary>
    /// Given an unsorted array and N, find two elements of difference N
    /// </summary>
    public class TwoNumWithDiff
    {
        static int num1, num2;
        // 1. sort the array
        // 2. use two pointers p1, p2
        // time O(N lg N) + O(lg N), space O(1)
        public static bool FindTwoNumWithDiff(int[] a, int N)
        {
            if (a.Length < 2) return false;
            Array.Sort(a);
            int p1 = 0;
            int p2 = 1;
            while (p1 < a.Length && p2 < a.Length)
            {
                if (a[p2] - a[p1] < N) p2++;
                else if (a[p2] - a[p1] > N) p1++;
                else // a[p2] - a[p1] == N
                {
                    if (p1 != p2) // corner case: when N is 0, we must make sure p1 != p2
                    {
                        num1 = a[p1];
                        num2 = a[p2];
                        return true;
                    }
                }
            }
            return false;
        }
        // hashmap, O(N) time, O(N) space
        public static bool FindTwoNumWithDiff_By_Hashmap(int[] a, int N)
        {
            if (a.Length < 2) return false;
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < a.Length; i++)
                set.Add(a[i]);
            for (int i = 0; i < a.Length; i++)
            {
                if (set.Contains(a[i] - N))
                {
                    num1 = a[i];
                    num2 = a[i] - N;
                    return true;
                }
                else if (set.Contains(a[i] + N))
                {
                    num1 = a[i];
                    num2 = a[i] + N;
                }
            }
            return false;
        }

        [Test]
        public static void TwoNumWithDiff_Test()
        {
            int[] a = { 5, 20, 3, 50, 80, 81 };
            bool result = FindTwoNumWithDiff(a, 1);
            if (result)
                Console.WriteLine("N = 1, num1 = {0}, num2 = {1}", num1, num2);
            Assert.IsTrue(result);
            bool result2 = FindTwoNumWithDiff_By_Hashmap(a, 75);
            if (result)
                Console.WriteLine("N = 75, num1 = {0}, num2 = {1}", num1, num2);
            Assert.IsTrue(result2);
        }
    }

    /// <summary>
    /// Question 13
    /// </summary>
    public class FirstUniqueChar
    {
        // find the first unique character in the given string
        public static char FirstUniqueCharacter(string s)
        {
            int[] ascii = new int[256];
            foreach (char c in s)
            {
                ascii[c]++;
            }
            for (int i = 0; i < 256; i++)
            {
                if (ascii[i] == 1) return (char)i; // found it
            }
            return ' '; // not found
        }

        [Test]
        public static void FirstUniqueCharacter_Test()
        {
            string s = "abcdabc";
            Console.WriteLine("FirstUniqueChar of {0} : {1}", s, FirstUniqueCharacter(s));
            Assert.AreEqual('d', FirstUniqueCharacter(s));
        }
    }

    /// <summary>
    /// Question 26
    /// Given N, find a group of continuous integers which sum up to N
    /// </summary>
    public class ContinuousSeq
    {
        // use 2 pointers, O(N) time, O(1) space
        public static List<int[]> ContinuousSequence(int N)
        {
            List<int[]> result = new List<int[]>();
            result.Add(new int[] { N }); // {N} is included
            int limit = (N + 1) / 2;
            // two pointers iterate through (1, 2...limit)
            // p1 and p2 will always move to the right
            int p1 = 1;
            int p2 = 2;
            int sum = p1 + p2;
            while (p2 <= limit)
            {
                if (sum < N) // move p2 right by 1
                {
                    sum += ++p2;
                }
                else if (sum > N) // move p1 right by 1
                {
                    sum -= p1++;
                }
                else if (sum == N)
                {
                    int[] array = new int[p2 - p1 + 1];
                    for (int i = 0; i < array.Length; i++) array[i] = p1 + i;
                    result.Add(array);
                    p2++; // move p2 right by 1
                    sum += p2;
                }
            }
            return result;
        }

        //[Test]
        public static void ContinuousSeq_Test()
        {
            int N = 15;
            Console.WriteLine("N = {0}", N);
            List<int[]> list = ContinuousSequence(N);
            foreach (int[] array in list)
            {
                Console.Write("{0} = ", N);
                for (int i = 0; i < array.Length - 1; i++)
                {
                    Console.Write("{0} + ", array[i]);
                }
                Console.WriteLine("{0}", array[array.Length - 1]);
            }
            Assert.AreEqual(new int[] { }, list);
        }
    }

    /// <summary>
    /// Question 41
    /// given an integer array, concat all the numbers in the array to make a new integer as
    /// small as possible
    /// </summary>
    public class MakeSmallestNumber
    {
        // O(N lg N) time
        // 1. convert all the inteter to string
        // 2. quicksort the string array by custom comparator
        // 3. concat all the string, print it out
        public static void PrintMinNumber(int[] a)
        {
            string[] ss = new string[a.Length];
            for (int i = 0; i < ss.Length; i++) ss[i] = a[i].ToString();
            Array.Sort(ss, Compare);// qsort
            for (int i = 0; i < ss.Length; i++) Console.Write(ss[i]);
        }
        // if s1 > s2, then s1 + s2 > s2 + s1; vice vesa
        public static int Compare(string s1, string s2)
        {
            return (s1 + s2).CompareTo(s2 + s1);
        }

        //[Test]
        public static void MakeSmallestNumber_Test()
        {
            int[] a = { 43, 4, 432, 4321, 54321 };
            foreach (int i in a) Console.Write(i + " ");
            Console.WriteLine();
            PrintMinNumber(a); // "432143243454321"
            Console.WriteLine();
        }

    }
    /// <summary>
    /// Question 47
    /// find the number that appear in the given array more than half of the times
    /// </summary>
    public class MajorityElement
    {

        // use a counter, and a variable to hold the temperal most frequent number
        public static bool FrequencyMoreThanHalf(int[] a, out int b)
        {
            int count = 1;
            b = a[0];
            if (a.Length == 1) return true;
            for (int i = 1; i < a.Length; i++)
            {
                if (b == a[i])
                {
                    count++;
                }
                else
                {
                    count--;
                    if (count == 0) b = a[i]; // change the temp variable to the new value
                }
            }
            if (count > 0) return true;
            else return false;
        }

        //[Test]
        public static void MajorityElement_Test()
        {
            int[] a = { 2, 2, 2, 3, 2, 3, 3, 3 };
            Console.Write("int array: ");
            foreach (int i in a) Console.Write(i + " ");
            int result;
            bool found = FrequencyMoreThanHalf(a, out result);
            if (found)
                Console.WriteLine("\nFrequence more than 50% is " + result);
            else
                Console.WriteLine("\nNot found!");
        }

    }

    /// <summary>
    /// Careercup 1.4
    /// Determine if two strings are anagrams or not
    /// </summary>
    public class Anagram
    {
        // use int[256] array to hold count of each character, O(N) time, O(1) space
        public static bool IsAnagram(string s1, string s2)
        {
            if (s1 == null || s2 == null) return false;
            if (s1.Length != s2.Length) return false;
            int[] ascii = new int[256];
            for (int i = 0; i < s1.Length; i++)
                ascii[s1[i]]++;
            for (int i = 0; i < s2.Length; i++)
            {
                if (ascii[s2[i]] > 0) ascii[s2[i]]--;
                else return false;
            }
            return true;
        }

        //[Test]
        public static void Anagram_Test()
        {
            string[] ss = { null, "", "a", "aabb", "abab", "bbaa", "aaab" };
            for (int i = 0; i < ss.Length - 1; i++)
            {
                Console.WriteLine(ss[i] + " vs " + ss[i + 1] + " : " + IsAnagram(ss[i], ss[i + 1]));
            }
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Careercup 1.7
    /// Given a matrix of 0s and 1s, if an element is 0 set the entire row and column to 0
    /// </summary>
    public class SetMatrixZeros
    {
        // O(N+M) space
        public static int[,] SetZero(int[,] matrix)
        {
            bool[] rows = new bool[matrix.GetLength(0)];
            bool[] cols = new bool[matrix.GetLength(1)];
            int[,] m = new int[rows.Length, cols.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < cols.Length; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        rows[i] = cols[j] = true;
                    }
                }
            }
            for (int i = 0; i < rows.Length; i++)
            {
                if (!rows[i])
                {
                    for (int j = 0; j < cols.Length; j++)
                        m[i, j] = matrix[i, j];
                }
            }
            for (int j = 0; j < cols.Length; j++)
            {
                if (!cols[j])
                {
                    for (int i = 0; i < rows.Length; i++)
                        m[i, j] = matrix[i, j];
                }
            }
            return m;
        }

        public static void Test()
        {
            int[,] mr = { {1, 0, 1, 1},
                         {1, 1, 0, 1},
                         {1, 1, 1, 0},
                         {1, 1, 1, 1}};
            //RotateMatrix.PrintMatrix(mr);
            Console.WriteLine(@"       ||");
            Console.WriteLine(@"       \/");
            //RotateMatrix.PrintMatrix(SetZero(mr));
        }

    }

    /// <summary>
    /// atoi/itoa conversion
    /// </summary>
    public class IntegerStringConversion
    {
        // string to int, special cases:
        // 1. null or empty string
        // 2. negative numbers
        // 3. overflow/underflow
        // 4. whitespace
        // 5. base value such as 0x or x
        public static int atoi(string s)
        {
            if (s == null || s.Length == 0) return 0;
            int n = 0;
            bool isNegative = false;
            s = s.Replace(" ", "");
            if (s.StartsWith("-"))
            {
                s = s.Substring(1);
                isNegative = true;
            }
            for (int i = 0; i < s.Length; i++)
            {
                int num = (int)Math.Pow(10, s.Length - i - 1) * (s[i] - '0');
                if (isNegative)
                {
                    n -= num;
                    if (n > num) return int.MinValue; // underflow, return Minvalue
                }
                else
                {
                    n += num;
                    if (n < num) return int.MaxValue; // overflow, return Maxvalue
                }
            }
            return n;
        }

        [Test]
        public static void IntegerStringConversion_Test()
        {
            string[] ss = { null, 
                          "", 
                          " 123",
                          "  123   ",
                          "123",
                          "-123", 
                          "2147483647", // int.MaxValue
                          "2147483648", // overflow
                          "-2147483648", // int.MinValue
                          "-2147483649" // underflow
                          };
            int[] ints = { 0,
                                0,
                                123,
                                123,
                                123,
                                -123,
                                int.MaxValue,
                                int.MaxValue,
                                int.MinValue,
                                int.MinValue
                              };
            for (int i = 0; i < ss.Length; i++)
            {
                Assert.AreEqual(ints[i], atoi(ss[i]));
            }            
        }
    }

    /// <summary>
    /// Given an integer array, find the smallest positive number missing from the array
    /// </summary>
    public class MissingNumber
    {
        // 1. move the positive numbers to the left part of the array, find the length of the subarray
        // 2. scan the subarray, set the a[a[i] - 1] to negative value
        // 3. scan the subarray, find the first postive number, return its index
        public static int SmallestMissingNumber(int[] a)
        {
            int i = 0;
            int j = a.Length - 1;
            for (i = 0; i < j; i++)
            {
                if (a[i] <= 0)
                {
                    while (a[j] <= 0) j--;
                    if (i >= j) break;
                    int temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;
                }
            }
            int k = 0; // k is the length of subarray
            for (k = 0; k < a.Length; k++)
            {
                if (a[k] <= 0) break;
            }
            if (k == 0) return 1;
            for (i = 0; i < k; i++)
            {
                int abs = Math.Abs(a[i]);
                if (abs < k)
                    a[abs - 1] = -a[abs - 1]; // trick: negate the value 
            }
            for (i = 0; i < k; i++)
            {
                if (a[i] > 0) return i + 1;
            }
            return k;
        }

        [Test]
        public static void MissingNumber_Test()
        {
            int[] a = { 2, 3, -7, 6, 8, 1, -10, 15 };
            Assert.AreEqual(4, SmallestMissingNumber(a));
        }
    }

    /// <summary>
    /// Given an array and two numbers x and y, find the minimum distance between 
    /// x and y in the array.
    /// eg: int[] a = {2, 5, 3, 5, 4, 4, 2, 3}, min distance is between 2 and 3 is 1
    /// http://www.geeksforgeeks.org/archives/13128
    /// </summary>
    public class MinDistanceOfTwoNumbers
    {
        // O(N) time
        public static int MinDist(int[] a, int x, int y)
        {
            int prev = -1;
            int dist = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (prev == -1 && (a[i] == x || a[i] == y))
                {
                    prev = i;
                }
                if (a[i] == x || a[i] == y)
                {
                    if (a[prev] != a[i] && i - prev < dist)
                    {
                        prev = i;
                        dist = i - prev;
                    }
                    else
                        prev = i;
                }
            }
            return dist;
        }

        [Test]
        public static void MinDistanceOfTwoNumbers_Test()
        {
            int x = 3;
            int y = 2;
            int[] a1 = { 2, 5, 3, 5, 4, 4, 2, 3 };
            int[] a2 = { 2, 1, 3, 1, 1, 2, 1, 1, 1, 3 };
            Console.WriteLine(MinDist(a1, x, y));
            Console.WriteLine(MinDist(a2, x, y));
            //Assert.AreEqual(0, MinDist(a1, x, y));
            //Assert.AreEqual(0, MinDist(a2, x, y));
        }

    }

    /// <summary>
    /// classical sorting algorithms
    /// 1. merge sort
    /// 2. quick sort
    /// </summary>
    public class SortingAlgo
    {

        public static void MergeSort(int[] a)
        {
            MergeSort(a, new int[a.Length], 0, a.Length - 1);
        }

        private static void MergeSort(int[] a, int[] aux, int low, int high)
        {
            if (low >= high) return; // base case: divide the arrray into subarray of length 1
            int mid = low + (high - low) / 2;
            MergeSort(a, aux, low, mid);
            MergeSort(a, aux, mid + 1, high);
            Merge(a, aux, low, mid, high);
        }

        private static void Merge(int[] a, int[] aux, int low, int mid, int hi)
        {
            for (int k = low; k <= hi; k++)
                aux[k] = a[k];
            int i = low;
            int j = mid + 1;
            for (int k = low; k <= hi; k++)
            {
                if (i <= mid && j <= hi)
                {
                    if (aux[j] < aux[i])
                        a[k] = aux[j++];
                    else
                        a[k] = aux[i++];
                }
                else if (i > mid)
                    a[k] = aux[j++];
                else if (j > hi)
                    a[k] = aux[i++];
            }
        }

        [Test]
        public static void SortingAlgo_Test()
        {
            int[] a1 = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }; // all identical
            int[] a2 = { 2, 3, 1, 4, 5, 8, 7, 10, 9, 6 }; // random
            int[] a3 = { 2, 2, 2, 1, 1, 1, 3, 3, 3, 1 }; // few elements
            int[] a4 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; // sorted
            int[] a5 = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }; // reverse sorted

            MergeSort(a1);
            Assert.AreEqual(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, a1);
            MergeSort(a2);
            Assert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, a2);
            MergeSort(a3);
            Assert.AreEqual(new int[] { 1, 1, 1, 1, 2, 2, 2, 3, 3, 3 }, a3);
            MergeSort(a4);
            Assert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, a4);
            MergeSort(a5);
            Assert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, a5);
        }
    }


    /// <summary>
    /// Merge k sorted arrays of length N
    /// solution 1. brute force
    /// solution 2. divide and conquer
    /// solution 3. min heap
    /// </summary>
    public class KWayMerge
    {

        // merge two sorted arrays
        private static int[] Merge(int[] a1, int[] a2)
        {
            int[] b = new int[a1.Length + a2.Length];
            int p1 = 0;
            int p2 = 0;
            while (p1 < a1.Length && p2 < a2.Length)
            {
                if (a1[p1] < a2[p2])
                {
                    b[p1 + p2] = a1[p1];
                    p1++;
                }
                else
                {
                    b[p1 + p2] = a2[p2];
                    p2++;
                }
            }
            if (p2 == a2.Length)
            {
                for (int i = p1; i < a1.Length; i++)
                {
                    b[a2.Length + i] = a1[i];
                }
            }
            else if (p1 == a1.Length)
            {
                for (int i = p2; i < a2.Length; i++)
                {
                    b[a1.Length + i] = a2[i];
                }
            }
            return b;
        }

        // time O(KN), space (N)
        // brute force
        public static int[] KWayMergeSort_BruteForce(params int[][] array)
        {
            int[] result = array[0];
            for (int i = 1; i < array.GetLength(0); i++)
            {
                result = Merge(result, array[i]);
            }
            return result;
        }

        // divide and conquer, O(N lgK) time, O(N) space
        // 1. group the k arrays by pairs
        // 2. merge a pair and move to next level until one array is left
        public static int[] KWayMergeSort_DivideAndConquer(params int[][] array)
        {
            int[][] result = array;
            while (result.GetLength(0) > 1)
            {
                int[][] temp = new int[result.GetLength(0) / 2][];
                for (int i = 0; i < result.GetLength(0) / 2; i++)
                {
                    temp[i] = Merge(result[i * 2], result[i * 2 + 1]);
                }
                if (result.GetLength(0) % 2 == 1)
                    temp[temp.GetLength(0) - 1] = Merge(temp[temp.GetLength(0) - 1], result[result.GetLength(0) - 1]);
                result = temp;
            }
            return result[0];
        }         
        
        public static int[] KWayMergeSort_MinHeap(params int[][] array)
        {
            // TODO
            return null;
        }

        [Test]
        public static void KWayMerge_Test()
        {
            int[] a1 = { 1, 3, 5 };
            int[] a2 = { 2, 4, 6 };
            int[] a3 = { 7, 8, 9 };
            int[] a4 = { 10, 11, 12 };
            int[] a5 = { 13, 15, 19 };
            int[] a6 = { 17, 20, 21 };
            int[] a7 = { 14, 16, 18 };
            int[] result = KWayMergeSort_BruteForce(a1, a2, a3, a4, a5, a6, a7);
            foreach (int i in result) Console.Write(i + " ");
            Console.WriteLine();
            Assert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 }, result);
            int[] result2 = KWayMergeSort_DivideAndConquer(a1, a2, a3, a4, a5, a6, a7);
            foreach (int i in result2) Console.Write(i + " ");
            Console.WriteLine();
            Assert.AreEqual(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 }, result2);
        }
    }

    /// <summary>
    /// integers whose only prime factors are 2, 3 or 5 (by convension 1 is included)
    /// i.e. 1, 2, 3, 4, 5, 6, 8, 9, 10...
    /// </summary>
    public class UglyNumber
    {
        // use 3 counters, increment one counter at a time, O(N) time
        public static int FindNthUglyNumber(int n)
        {
            if (n == 1) return 1;
            int i2 = 1;
            int i3 = 1;
            int i5 = 1;
            int ugly = 1;
            for (int i = 2; i <= n; i++)
            {
                ugly = Math.Min(i2 * 2, Math.Min(i3 * 3, i5 * 5));
                // increment all the counters that generate the current ugly number
                if (ugly == i2 * 2) i2++;
                if (ugly == i3 * 3) i3++;
                if (ugly == i5 * 5) i5++;
            }
            return ugly;
        }

        [Test]
        public static void UglyNumber_Test()
        {
            for (int i = 1; i <= 20; i++)
                Console.Write(FindNthUglyNumber(i) + " ");
            Console.WriteLine();
            Console.WriteLine("N = 1500, Ugly number = " + FindNthUglyNumber(1500));
            Assert.AreEqual(2044, FindNthUglyNumber(1500));
        }

    }
    /// <summary>
    /// Beauty 2.10
    /// Find the max number and the min number of an array, use only 1.5 * N times of comparasion 
    /// </summary>
    public class ArrayMaxMin
    {
        static int max = int.MinValue;
        static int min = int.MaxValue;

        public static void FindMaxAndMin(int[] a)
        {
            if (a.Length == 1)
            {
                max = min = a[0];
                return;
            }
            for (int i = 0; i <= a.Length - 2; i += 2)
            {
                int bigger, smaller;
                if (a[i] > a[i + 1])
                {
                    bigger = a[i];
                    smaller = a[i + 1];
                }
                else
                {
                    bigger = a[i + 1];
                    smaller = a[i];
                }
                max = bigger > max ? bigger : max;
                min = smaller < min ? smaller : min;
            }
            if (a.Length % 2 == 1)
            {
                max = a[a.Length - 1] > max ? a[a.Length - 1] : max;
                min = a[a.Length - 1] < min ? a[a.Length - 1] : min;
            }
        }

        [Test]
        public static void ArrayMaxMin_Test()
        {
            int[] a1 = { 1 };
            int[] a2 = { 1, 2, 3, 4, 5, 6 };
            int[] a3 = { 6, 5, 4, 3, 2 };
            max = int.MinValue;
            min = int.MaxValue;
            FindMaxAndMin(a1);
            Console.WriteLine(max + " " + min);
            Assert.AreEqual(1, max);
            Assert.AreEqual(1, min);
            max = int.MinValue;
            min = int.MaxValue;
            FindMaxAndMin(a2);
            Console.WriteLine(max + " " + min);
            Assert.AreEqual(6, max);
            Assert.AreEqual(1, min);
            max = int.MinValue;
            min = int.MaxValue;
            FindMaxAndMin(a3);
            Console.WriteLine(max + " " + min);
            Assert.AreEqual(6, max);
            Assert.AreEqual(2, min);
        }

    }

    /// <summary>
    /// Beauty 2.13
    /// Find the maximun sum of subarray of size N-1 of a given array (size N)
    /// </summary>
    public class ArrayMaxProduct
    {
        // O(N) time
        public static int[] FindMaxProduct(int[] a)
        {
            bool hasZero = false;
            int negatives = 0;
            int minPositive = int.MaxValue;
            int maxNegative = int.MinValue;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] < 0)
                {
                    negatives++;
                    maxNegative = a[i] > maxNegative ? a[i] : maxNegative;
                }
                else if (a[i] == 0)
                    hasZero = true;
                else
                    minPositive = a[i] < minPositive ? a[i] : minPositive;
            }
            List<int> result = a.ToList();
            if (hasZero)
            {
                if (negatives % 2 == 0)
                { // remove 0
                    result.Remove(0);
                }
                else
                { // remove any number but a zero
                    foreach (int item in result)
                    {
                        if (item != 0)
                        {
                            result.Remove(item);
                            break;
                        }
                    }
                }
            }
            else if (negatives % 2 == 0)
            { // remove smallest positive number
                result.Remove(minPositive);
            }
            else
            { // remove biggest negative number
                result.Remove(maxNegative);
            }
            return result.ToArray();
        }

        private static long Product(int[] a)
        {
            long sum = 1;
            foreach (int i in a)
                sum *= i;
            return sum;
        }

        [Test]
        public static void ArrayMaxProduct_Test()
        {
            int[] a1 = { 1, 2, 3, -3, -4, -5 };
            int[] a2 = { 1, 3, -1, -2, 4, 5 };
            int[] a3 = { 1, 2, 0, 3, 4, 5 };
            int[] a4 = { 0, -1, -2, 3, 4, 5 };
            int[] a5 = { 0, -1, -2, -3, 4, 5 };
            Assert.AreEqual(120, Product(FindMaxProduct(a1)));
            Assert.AreEqual(120, Product(FindMaxProduct(a2)));
            Assert.AreEqual(120, Product(FindMaxProduct(a3)));
            Assert.AreEqual(120, Product(FindMaxProduct(a4)));
            Assert.AreEqual(0, Product(FindMaxProduct(a5)));
        }

    }

    /// <summary>
    /// Given an array of size n-2 which contains distinct numbers from 1, 2...n
    /// Find the two missing numbers
    /// </summary>
    public class TwoMissingNumbers
    {
        static int num1;
        static int num2;

        // x + y = sum(1, 2...n) - sum(a)
        // x^2 + y^2 = sum(1, 4, 9...n^2) - sum(a[i]^2)
        // x * y = ((x + y) ^ 2 - (x^2 + y^2)) / 2
        // solve the equation of x and y
        public static void FindTwoNumbers(int[] a)
        {
            int N = a.Length + 2;
            int sumOfArray = 0;
            foreach (int i in a)
                sumOfArray += i;
            int sumOfTwoNumbers = N * (N + 1) / 2 - sumOfArray;
            int squareSum = 0;
            for (int i = 1; i <= N; i++)
                squareSum += i * i;
            foreach (int i in a)
                squareSum -= i * i;
            int productOfTwoNumbers = (sumOfTwoNumbers * sumOfTwoNumbers - squareSum) / 2;
            int temp = (int)Math.Sqrt(sumOfTwoNumbers * sumOfTwoNumbers - 4 * productOfTwoNumbers);
            num1 = (sumOfTwoNumbers + temp) / 2;
            num2 = (sumOfTwoNumbers - temp) / 2;
        }

        [Test]
        public static void TwoMissingNumbers_Test()
        {
            int[] a = { 3, 5, 2, 1, 6, 7, 10, 9 };
            FindTwoNumbers(a);
            Console.WriteLine("Two missing numbers are: " + num1 + " " + num2);
            Assert.AreEqual(8, num1);
            Assert.AreEqual(4, num2);
        }
    }


    /// <summary>
    /// Sort an array of 0, 1 and 2s
    /// 3-way partition
    /// </summary>
    public class DutchNationalFlagProblem
    {
        // use 3 pointers to partition: 1st part is all 0s, 2nd part is all 1s, 3rd part is mix of 0s 1s 2s, 4th part is all 2s
        // eg: {0 0 0 | 1 1 1 1 | 0 2 1 2 0 | 2 2 2 2}
        //
        // invariants: (1)low pointer is the first of the 2nd part, init value 0
        // (2)i pointer is the start of the 3rd part, init value 0
        // (3)high is the end of the 3rd part, init value a.length - 1
        // iterate i pointer through the array, (1) if element is 0, swap low element with i element, increment low and i
        // (2) if element is 1, increment i pointer 
        // (3) if element is 2, swap i element with high element, decrement high
        // 
        // O(N) time
        public static int[] SortThreeColors(int[] a)
        {
            int low = 0;
            int i = 0;
            int high = a.Length - 1;
            while (i <= high)
            {
                if (a[i] == 0)
                {
                    swap(a, low, i);
                    low++;
                    i++;
                }
                else if (a[i] == 1)
                {
                    i++;
                }
                else // a[i] == 2
                {
                    swap(a, i, high);
                    high--;
                }
            }
            return a;
        }


        // variant: sorting 2 colors: 0s and 1s
        public static int[] SortTwoColors(int[] a)
        {
            int low = 0;
            int high = a.Length - 1;
            while (low <= high)
            {
                if (a[low] == 0)
                {
                    low++;
                }
                else
                {
                    swap(a, low, high);
                    high--;
                }
            }
            return a;
        }

        private static int[] swap(int[] a, int i, int j)
        {
            int temp = a[i];
            a[i] = a[j];
            a[j] = temp;
            return a;
        }

        [Test]
        public static void DutchNationalFlagProblem_Test()
        {
            // three colors sorting
            int[] a1 = { 1, 2, 0, 0, 1, 2 };
            int[] a2 = { 1, 2, 2, 2, 0, 1 };
            int[] a3 = { 1, 1, 2, 1, 1, 1 };
            a1 = SortThreeColors(a1);
            a2 = SortThreeColors(a2);
            a3 = SortThreeColors(a3);
            foreach (int i in a1) Console.Write(i + " ");
            Console.WriteLine();
            Assert.AreEqual(new int[] { 0, 0, 1, 1, 2, 2 }, a1);
            foreach (int i in a2) Console.Write(i + " ");
            Console.WriteLine();
            Assert.AreEqual(new int[] { 0, 1, 1, 2, 2, 2 }, a2);
            foreach (int i in a3) Console.Write(i + " ");
            Console.WriteLine();
            Assert.AreEqual(new int[] { 1, 1, 1, 1, 1, 2 }, a3);

            // two colors sorting
            int[] a4 = { 1, 1, 0, 0, 1, 0 };
            int[] a5 = { 1, 1, 1, 1, 1, 1 };
            a4 = SortTwoColors(a4);
            a5 = SortTwoColors(a5);
            foreach (int i in a4) Console.Write(i + " ");
            Console.WriteLine();
            Assert.AreEqual(new int[] { 0, 0, 0, 1, 1, 1 }, a4);
            foreach (int i in a5) Console.Write(i + " ");
            Console.WriteLine();
            Assert.AreEqual(new int[] { 1, 1, 1, 1, 1, 1 }, a5);
        }

    }





    /// <summary>
    /// Leetcode
    /// Given n non-negative integers representing an elevation map where the width of each bar is 1, 
    /// compute how much water it is able to trap after raining.
    /// For example, Given [0,1,0,2,1,0,1,3,2,1,2,1], return 6.
    /// </summary>
    public class TrapRainWater
    {
        // find the index of local-max node of the array, given above example, local-max value should be {1, 2, 3, 2}, list of index is {1, 3, 7, 10}
        // use every adjacent pair of local-max value, compute sum of how much water within each slot
        public static int MaxTrappedWater(int[] a)
        {
            bool IsUp = true;
            List<int> list = new List<int>();
            for (int i = 1; i < a.Length; i++)
            {
                if (i == a.Length - 1 && a[i] > a[i - 1]) // [tricky]special case: last node is local-max but not turing point
                {
                    list.Add(i);
                }
                if (IsUp && a[i] <= a[i - 1]) // this node is turing point, "local-max" node
                {
                    list.Add(i - 1);
                    IsUp = false;
                }
                else if (!IsUp && a[i] > a[i - 1]) // this node is turing point, "local-min" node
                {
                    IsUp = true;
                }
            }
            if (list.Count <= 1) return 0;
            int sum = 0;
            for (int i = 0; i < list.Count - 1; i++)
            {
                int lower = Math.Min(a[list[i]], a[list[i + 1]]);
                for (int j = list[i] + 1; j < list[i + 1]; j++)
                    sum += lower - a[j];
            }
            return sum;
        }

        [Test]
        public static void TrapRainWater_Test()
        {
            int[] a1 = { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 }; // 6
            int[] a2 = { 0, 1, 2, 3, 4, 5, 5, 5, 6 }; // 0
            int[] a3 = { 6, 5, 4, 3, 3, 3, 2, 1 }; // 0
            int[] a4 = { 1, 2, 3, 4, 3, 2, 1 }; // 0
            int[] a5 = { 3, 1, 2 }; // 1
            int[] a6 = { 3, 1, 2, 2 }; // 1
            Assert.AreEqual(6, MaxTrappedWater(a1));
            Assert.AreEqual(0, MaxTrappedWater(a2));
            Assert.AreEqual(0, MaxTrappedWater(a3));
            Assert.AreEqual(0, MaxTrappedWater(a4));
            Assert.AreEqual(1, MaxTrappedWater(a5));
            Assert.AreEqual(1, MaxTrappedWater(a6));
        }

    }

    /// <summary>
    /// An array only contains non-zero integers.
    /// Partition the array by zero, negative number put on the left and positive number put on the right,
    /// in the original order. 
    /// </summary>
    public class PartitionArrayByZero
    {
        public static int[] partition(int[] a)
        {
            return partition(a, 0, a.Length - 1);
        }

        private static int[] partition(int[] a, int low, int high)
        {
            if (low >= high)
                return a;
            int mid = (low + high) / 2;
            a = partition(a, low, mid);
            a = partition(a, mid + 1, high);
            int leftNegative = low;
            while (leftNegative <= high && a[leftNegative] < 0)
            {
                leftNegative++;
            }
            int rightNegative = high - 1;
            while (rightNegative >= 0 && a[rightNegative] >= 0)
            {
                rightNegative--;
            }
            if (leftNegative >= rightNegative) return a;
            a = ReverseRange(a, leftNegative, rightNegative);
            int midNegative = leftNegative;
            while (a[midNegative] < 0)
            {
                midNegative++;
            }
            a = ReverseRange(a, leftNegative, midNegative - 1);
            a = ReverseRange(a, midNegative, rightNegative);
            return a;
        }

        private static int[] ReverseRange(int[] a, int low, int high)
        {
            while (low < high)
            {
                int temp = a[low];
                a[low] = a[high];
                a[high] = temp;
                low++;
                high--;
            }
            return a;
        }

        //[Test]
        public static void PartitionArrayByZero_Test()
        {
            int[] a1 = { 1, 7, -5, 9, -12, 15 };
            Assert.AreEqual(new int[] { -5, -12, 1, 7, 9, 15 }, partition(a1));

            int[] a2 = { 3, 2, 1, -3, -2, -1 };
            Assert.AreEqual(new int[] { -3, -2, -1, 3, 2, 1 }, partition(a2));
        }
    }


    /// <summary>
    /// check if a integer is palindrome number, can't use extra memory
    /// </summary>
    public class PalindromeNumber
    {
        // O(N) time and O(1) space
        public static bool IsPalindrome(int x)
        {
            if (x < 0) return false;
            if (x == 0) return true;
            int n = (int)Math.Log10(x) + 1; // number of digits
            for (int i = 1; i <= n / 2; i++)
            {
                if ((x % (int)Math.Pow(10, i)) / (int)Math.Pow(10, i - 1)
                    != (x / (int)Math.Pow(10, (n - i))) % 10)
                {
                    return false;
                }
            }
            return true;
        }

        // recursive solution
        public static bool IsPalindrome_recursive(int x)
        {
            return IsPalindrome_recursive(x, ref x);
        }
        private static bool IsPalindrome_recursive(int x, ref int y)
        {
            if (x < 0) return false;
            if (x == 0) return true;
            if (IsPalindrome_recursive(x / 10, ref y) && (x % 10 == y % 10))
            {
                y /= 10;
                return true;
            }
            else
            {
                return false;
            }
        }

        [Test]
        public static void PalindromeNumber_Test()
        {
            Assert.IsTrue(IsPalindrome(1));
            Assert.IsTrue(IsPalindrome(123321));
            Assert.IsTrue(IsPalindrome(1234321));
            Assert.IsFalse(IsPalindrome(1234521));
            Assert.IsTrue(IsPalindrome_recursive(1));
            Assert.IsTrue(IsPalindrome_recursive(123321));
            Assert.IsTrue(IsPalindrome_recursive(1234321));
            Assert.IsFalse(IsPalindrome_recursive(1234521));
        }

    }

    ///// <summary>
    ///// Find the median of two sorted arrays
    ///// </summary>
    //public class MedianOfTwoSortedArray
    //{    
    //    public static int FindMedian(int[] a1, int[] a2)
    //    {

    //    }

    //    public static void Test()
    //    {
    //        int[] a1 = { 1, 12, 15, 26, 38 };
    //        int[] a2 = { 2, 13, 17, 30, 45 };
    //        Console.WriteLine(FindMedian(a1, a2));

    //    }
    //}

    ///// <summary>
    ///// Find the kth smallest element of the union of two sorted array
    ///// </summary>
    //public class KthElementOfTwoSortedArray
    //{
    //    /*
    //     def kthlargest(arr1, arr2, k):
    //        if len(arr1) == 0:
    //            return arr2[k]
    //        elif len(arr2) == 0:
    //            return arr1[k]
    //        mida1 = len(arr1)/2
    //        mida2 = len(arr2)/2
    //        if mida1+mida2<k:
    //            if arr1[mida1]>arr2[mida2]:
    //                return kthlargest(arr1, arr2[mida2+1:], k-mida2-1)
    //            else:
    //                return kthlargest(arr1[mida1+1:], arr2, k-mida1-1)
    //        else:
    //            if arr1[mida1]>arr2[mida2]:
    //                return kthlargest(arr1[:mida1], arr2, k)
    //            else:
    //                return kthlargest(arr1, arr2[:mida2], k)

    //     */

    //    /*
    //    // N(lg N1 + lg N2) time
    //    public static int KthSmallest(int[] a1, int[] a2)
    //    {


    //    }
    //    */

    //    public static void Test()
    //    {


    //    }

    //}


    ///// <summary>
    ///// Given an nearly sorted array where each element is at most k away of its target position
    ///// Sort the array in O(N lgk) time
    ///// </summary>
    //public class NearlySortedArray
    //{
    //    /*
    //    // use a min heap of size k+1, add the first k+1 elements to the heap
    //    // every time remove the min element from the heap and add a new one in
    //    public static int[] KSort(int[] a)
    //    {

    //    }
    //    */

    //    public static void Test()
    //    {

    //    }

    //}

    ///// <summary>
    ///// Careercup 1.6
    ///// Rotate a NxN matrix by 90 degrees clockwise, inplace
    ///// </summary>
    //public class RotateMatrix
    //{

    //    public static void Rotate90Degrees(int[,] matrix)
    //    {

    //    }

    //    public static void PrintMatrix(int[,] matrix)
    //    {
    //        int N = matrix.GetLength(0);
    //        Console.WriteLine("-------------------");
    //        for (int i = 0; i < N; i++)
    //        {
    //            for (int j = 0; j < N; j++)
    //            {
    //                Console.Write(matrix[i, j] + " ");
    //            }
    //            Console.WriteLine();
    //        }
    //        Console.WriteLine("-------------------");
    //    }

    //    public static void RotateMatrix_Test()
    //    {
    //        int[,] mr = { {1, 2, 3, 4},
    //                     {5, 6, 7, 8},
    //                     {9, 10, 11, 12},
    //                     {13, 14, 15, 16}};
    //        PrintMatrix(mr);
    //        Rotate90Degrees(mr);
    //        Console.WriteLine(@"       ||");
    //        Console.WriteLine(@"       \/");
    //        PrintMatrix(mr);
    //    }
    //}
}
