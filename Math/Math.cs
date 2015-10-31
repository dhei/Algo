using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Math
{

    /// <summary>
    /// square root function
    /// </summary>
    public class SQRT
    {
        public static readonly double eps = 1e-10;

        // newton's method, very efficient
        // f(y) = y^2 - x and f'(y) = 2y
        // http://www.cnblogs.com/pkuoliver/archive/2010/10/06/1844725.html
        public static double SqrtNewtonMethod(double x)
        {
            if (x < 0) return -1;
            double val = x;
            double temp;
            do
            {
                temp = val;
                val = (val + x / val) / 2;
            } while (System.Math.Abs(val - temp) > eps);
            return val;
        }

        // binary search, not efficient
        // pitfalls: (1) error handling for x < 0
        //           (2) pick different (low, high) initial range for 0<x<1 and x>1
        public static double SqrtBinarySearch(double x)
        {
            if (x < 0) return -1; // error handling
            double low;
            double high;
            if (x < 1)
            { // pitfall
                low = 0;
                high = 1;
            }
            else
            {
                low = 1;
                high = x;
            }
            double mid = (low + high) / 2;
            double temp;
            do
            {
                if (mid * mid > x)
                    high = mid;
                else
                    low = mid;
                temp = mid;
                mid = (low + high) / 2;
            } while (System.Math.Abs(mid - temp) > eps);
            return mid;
        }

        public static void Test()
        {
            for (int i = -2; i < 5; i++)
            {
                Console.WriteLine("X = {0} Sqrt by Newton       {1}",
                    i, SqrtNewtonMethod(i));
                Console.WriteLine("X = {0} Sqrt by BinarySearch {1}",
                    i, SqrtBinarySearch(i));
                Console.WriteLine("X = {0} Sqrt by System.Math  {1}\n",
                    i, System.Math.Sqrt(i));
            }

            for (double i = 0; i < 0.6; i += 0.1)
            {
                Console.WriteLine("X = {0} Sqrt by Newton       {1}",
                    i, SqrtNewtonMethod(i));
                Console.WriteLine("X = {0} Sqrt by BinarySearch {1}",
                    i, SqrtBinarySearch(i));
                Console.WriteLine("X = {0} Sqrt by System.Math  {1}\n",
                    i, System.Math.Sqrt(i));
            }
        }



    }

    /// <summary>
    /// shuffling algorithm
    /// </summary>
    public class ShufflingAlgo
    {

        private static readonly System.Random rand = new System.Random();

        // generate a pseudorandom permutation of number 1,2,3...N
        // Knuth shuffle algorithm: 
        //     for i = 0:N-1
        //         generate randomIndex between (0, N-1) range 
        //         swap(array, i, randomIndex)    
        //     end 
        public static int[] KnuthShuffle(int N)
        {
            int[] a = new int[N];
            for (int i = 0; i < N; i++)
                a[i] = i + 1;
            for (int i = 0; i < N; i++)
            {
                int n = rand.Next(i + 1);
                int temp = a[i];
                a[i] = a[n];
                a[n] = temp;
            }
            return a;
        }

        public static void Test()
        {
            for (int i = 0; i < 10; i++)
            {
                int[] randomPermu = KnuthShuffle(10);
                foreach (int x in randomPermu) Console.Write(x + " ");
                Console.WriteLine();
            }
        }

    }

    /// <summary>
    /// Generate m random records from an array of n records dynamically (we don't know n)
    /// This is an online algorithm
    /// </summary>
    public class ReseviorSampling
    {
        private static readonly Random rand = new Random();

        public static int[] RandomRecords(IEnumerator<int> e, int m)
        {
            int[] result = new int[m];
            int n = 0;
            while (e.MoveNext())
            {
                n++;
                if (n <= m)
                { // store the first m records from the enumerator
                    result[n - 1] = e.Current;
                }
                else
                { // **tricky**: with probability m/n replace a existing element r with the newest element n
                    int r = rand.Next(n + 1);
                    if (r < m)
                        result[r] = n;
                }
            }
            return result;
        }


        public static void Test()
        {
            /* This example shows:
             * Given an enumerator of list (1, 2, 3, ... 10), generate 5 random numbers from the list,
             * run the tests 10000 times, we see that probability of each generating each number is equal to 1/N
             */
            int N = 10;
            int m = 5;
            int trails = 10000;
            int[] counts = new int[N];
            List<int> list = new List<int>();
            for (int i = 1; i <= N; i++)
                list.Add(i);
            for (int j = 0; j < trails; j++)
            {
                List<int>.Enumerator e = list.GetEnumerator();
                int[] result = RandomRecords(e, m);
                for (int i = 0; i < result.Length; i++)
                {
                    counts[result[i] - 1]++;
                    Console.Write(result[i] + " ");
                }
                Console.WriteLine();
            }
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine("number {0} probability = {1}", i + 1, (double)counts[i] / (trails * m));
            }
        }
    }

    /// <summary>
    /// Given a random number generator rand5() which generate number between (0, 4),
    /// build a new random number generator rand7() that generate number between (0, 6)
    /// </summary>
    public class RandomGenerator
    {
        private static readonly Random rand = new Random();
        private static int Rand5()
        {
            return rand.Next(5);
        }

        // utlize Rand5() to build Rand7()
        // result is an uniform distribution of (0, 20)
        public static int Rand7()
        {
            int result;
            do
            {
                result = 5 * Rand5() + Rand5(); // uniformly distribution between (0,24)
            } while (result > 20); // only keep the part of the distribution between (0, 20)
            return result % 7;
        }

        public static void Test()
        {
            int[] results = new int[7];
            int N = 1000000;
            for (int i = 0; i < N; i++)
            {
                results[Rand7()]++;
            }
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine("Rand7() = {0} Probability = {1}", i, (double)results[i] / N);
            }
        }
    }

    /// <summary>
    /// Find the smallest palindrom number larger than the given number
    /// </summary>
    public class NextPalindromeNumber
    {
        // (1) length of string should be handled differently of odd or even
        // (2) compare the left-half and the reverse-right-half, eg: compare "123" with "421" when s="1234421" 
        public static string NextPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s)) return ""; // error handling
            string leftHalf, rightHalf;
            if (s.Length % 2 == 0)
            {
                leftHalf = s.Substring(0, s.Length / 2);
                rightHalf = s.Substring(s.Length / 2);
            }
            else
            {
                leftHalf = s.Substring(0, s.Length / 2);
                rightHalf = s.Substring(s.Length / 2 + 1);
            }
            string result;
            if (reverselyCompare(leftHalf, rightHalf) <= 0)
            {
                if (s.Length % 2 == 0)
                    result = (Convert.ToInt32(leftHalf, 10) + 1).ToString();
                else
                    result = (Convert.ToInt32(s.Substring(0, s.Length / 2 + 1), 10) + 1).ToString();
            }
            else
            {
                result = s.Substring(0, s.Length / 2 + 1);
            }
            for (int i = leftHalf.Length - 1; i >= 0; i--)
            {
                result += result[i];
            }
            return result;
        }

        // compare s1 with reverse of s2
        // eg: "123" < "421", "123" > "221", "123" = "321"
        private static int reverselyCompare(string s1, string s2)
        {
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] < s2[s2.Length - 1 - i]) return -1;
                else if (s1[i] > s2[s2.Length - 1 - i]) return 1;
            }
            return 0;
        }

        public static void Test()
        {
            Console.WriteLine(NextPalindrome("123321")); // 124421
            Console.WriteLine(NextPalindrome("123421")); // 124421
            Console.WriteLine(NextPalindrome("123221")); // 123321
            Console.WriteLine(NextPalindrome("1234321")); // 1235321
            Console.WriteLine(NextPalindrome("1234421")); // 1235321
            Console.WriteLine(NextPalindrome("1234221")); // 1234321
        }
    }

}
