using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using NUnit.Framework;

namespace BitAlgo
{
    public class BitAlgos
    {
        // Q1: toggle from bit i to bit j
        // XOR trick: X ^ 111...1 can toggle all the bits in X
        public static int ToggleBits(int x, int i, int j)
        {
            int mask = 0;
            while (i < j)
            {
                mask = mask | (1 << i); // generate binary of bit i to j of 1
                i++;
            }
            return x ^ mask;
        }

        [Test]
        public static void ToggleBits_Test()
        {
            int x = 100; // "1100100"
            Assert.AreEqual("1100010", Convert.ToString(ToggleBits(x, 1, 3), 2));
            Assert.AreEqual("1101000", Convert.ToString(ToggleBits(x, 2, 4), 2));
            Assert.AreEqual("1111100", Convert.ToString(ToggleBits(x, 3, 5), 2));
        }

        // Q2: swap bit i and j of integer x
        // XOR trick: X ^ 0 = X
        public static int SwapBits(int x, int i, int j)
        {
            // if bit i and bit j is he same, no need to do anything
            if ((x >> i & 1) == (x >> j & 1)) return x;
            // else, toggle bit i and j
            else return x ^ ((1 << i) | (1 << j));
        }

        [Test]
        public static void SwapBitsTest()
        {
            int x = 100; // "1100100"
            Assert.AreEqual("1100100", Convert.ToString(SwapBits(x, 1, 3), 2));
            Assert.AreEqual("1110000", Convert.ToString(SwapBits(x, 2, 4), 2));
            Assert.AreEqual("1001100", Convert.ToString(SwapBits(x, 3, 5), 2));
        }

        // Q3: turn off the right-most 1 bit
        public static int TurnOffRightestOne(int x)
        {
            return x & (x - 1);
        }

        [Test]
        public static void TurnOffRightestOneTest()
        {
            int x = 100; // "1100100"
            Assert.AreEqual("1100000", Convert.ToString(TurnOffRightestOne(x), 2));
        }

        // Q4: count the number of 1 bit in x
        public static int CountBitOne(int x)
        {
            int count = 0;
            while (x != 0)
            {
                x &= x - 1;
                count++;
            }
            return count;
        }

        [Test]
        public static void CountBitOneTest()
        {
            Assert.AreEqual(3, CountBitOne(100));
            Assert.AreEqual(1, CountBitOne(1));
        }

        // Q5: swap every two bits, eg: 10100101 -> 01011010
        // Careercup 5.6
        public static int SwapPairs(int x)
        {
            for (int i = 0; i < 32; i += 2)
            {
                x = SwapBits(x, i, i + 1);
            }
            return x;
        }

        public static Int64 SwapPairs_manually(Int64 x)
        {
            return ((x & 0xaaaaaaaa) >> 1) | ((x & 0x55555555) << 1);
        }

        [Test]
        public static void SwapPairs_Test()
        {
            int x = 165;
            Assert.AreEqual("1011010", Convert.ToString(SwapPairs(x), 2));
            Assert.AreEqual("1011010", Convert.ToString(SwapPairs_manually(x), 2));
        }

        // http://www.leetcode.com/2011/08/reverse-bits.html 
        // Swap each pair by XOR tricks
        // Q6: reverse 32-bit integer, eg: 10101111 -> 11110101
        public static int ReverseInteger(int x)
        {
            for (int i = 0; i < 16; i++)
            {
                x = SwapBits(x, i, 31 - i);
            }
            return x;
        }

        [Test]
        public static void ReverseInteger_Test()
        {
            int x = (int)Math.Pow(2, 16) - 1;
            Assert.AreEqual("11111111111111110000000000000000", Convert.ToString(ReverseInteger(x), 2));
        }

        public static byte ReverseByte(byte b)
        {
            int rev = (b >> 4) | ((b & 0xf) << 4);
            // (rev & 11001100) | (rev & 00110011) 
            rev = ((rev & 0xcc) >> 2) | ((rev & 0x33) << 2);
            // (rev & 10101010) | (rev & 01010101)
            rev = ((rev & 0xaa) >> 1) | ((rev & 0x55) << 1);
            return (byte)rev;
        }

        [Test]
        public static void ReverseByte_Test()
        {
            byte y = 0xcc;
            Assert.AreEqual("110011", Convert.ToString(ReverseByte(y), 2));
        }

        // Q8: add operation without operator +-*/, only bitwise operation
        public static int Add(int x, int y)
        {
            if (x == 0) return y;
            if (y == 0) return x;
            int a = x ^ y; // result without carry
            int b = (x & y) << 1; // carry
            return Add(a, b);
        }

        [Test]
        public static void AddTest()
        {
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                int x = rand.Next();
                int y = rand.Next();
                Assert.AreEqual(x + y, Add(x, y));
            }
        }

        // Q9: compare two integer without comparator ">" or "<", return the larger number
        // trick: use the sign of signed interger
        public static int Compare(int x, int y)
        {
            int diff = x - y;
            return (diff >> 31 & 1) == 0 ? x : y;
        }

        [Test]
        public static void CompareTest()
        {
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                int x = rand.Next();
                int y = rand.Next();

                if (x > y) Assert.Greater(x, y);
                else if (x < y) Assert.Less(x, y);
                else Assert.AreEqual(x, y);
            }
        }

        // Q10: swap two integer without temperary variable
        public static int[] SwapNum_XOR(int x, int y)
        { 
            // XOR trick
            x = x ^ y;
            y = x ^ y;
            x = x ^ y;
            return new int[] { x, y };
        }

        public static int[] SwapNum_Minus(int x, int y)
        {
            x = x - y;
            y = x + y;
            x = y - x;
            return new int[] { x, y };
        }

        public static int[] SwapNum_Plus(int x, int y)
        {
            x = x + y;
            y = x - y;
            x = x - y;
            return new int[] { x, y };
        }

        [Test]
        public static void SwapNum_Test()
        {
            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                int x = rand.Next();
                int y = rand.Next();
                int xCopy = x;
                int yCopy = y;
                int[] result = SwapNum_XOR(x, y);
                Assert.AreEqual(yCopy, result[0]);
                Assert.AreEqual(xCopy, result[1]);
                result = SwapNum_Minus(x, y);
                Assert.AreEqual(yCopy, result[0]);
                Assert.AreEqual(xCopy, result[1]);
                result = SwapNum_Plus(x, y);
                Assert.AreEqual(yCopy, result[0]);
                Assert.AreEqual(xCopy, result[1]);
            }
        }

    }

    /// <summary>
    /// Question 34
    /// Given an array, of which two numbers appear once and other numbers appear exact twice
    /// Find the two number that appear once, return true if they are found
    /// </summary>
    public class TwoUniqueNumber
    {
        // 1. XOR the whole array
        // 2. find the right-most kth bit-one in the XOR result
        // 3. divide the array into two subarray: if the kth bit is 1 put in first subarray, if not put in the second subarray
        // 4. xor all the element in first subarray, the result is the first unique number
        //    xor all the element in second subarray, the result is the second unique number
        public static bool FindTwoUniqueNumbers(int[] a, out int num1, out int num2)
        {
            int xorAll = 0;
            for (int i = 0; i < a.Length; i++)
                xorAll ^= a[i];
            int rightBitOne = 0; // find the right-most bit one
            while (true)
            {
                if (((xorAll >> rightBitOne++) & 1) == 1) break;
                if (rightBitOne >= 31) // IMPORTANT!!! checking the boundary case!
                {
                    num1 = num2 = 0;
                    return false;
                }
            }
            num1 = 0; num2 = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (((a[i] >> (rightBitOne - 1) & 1) == 1))
                    num1 ^= a[i];
                else
                    num2 ^= a[i];
            }
            if (num1 == num2) return false;
            else return true;
        }

        [Test]
        public static void TwoUniqueNumber_Test()
        {
            int num1, num2;
            int[] a = new int[] { 1, 2, 1, 2, 3, 4, 5, 6, 6, 5, 4, 7, 8, 8, 9, 9 };
            bool found = FindTwoUniqueNumbers(a, out num1, out num2);
            Assert.True(found);
            Assert.AreEqual(7, num1);
            Assert.AreEqual(3, num2);

            a = new int[]{ 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
            found = FindTwoUniqueNumbers(a, out num1, out num2);
            Assert.False(found);
        }
    }

    /// <summary>
    /// Careercup 5.5
    /// Count how many bits are needed to toggle to convert inteter a into integer b
    /// </summary>
    public class BitDiff
    {
        // count number of 1 of the xor result of the two number
        public static int BitsDiffCount(int a, int b)
        {
            int xor = a ^ b;
            int count = 0;
            while (xor != 0)
            {
                xor &= xor - 1;
                count++;
            }
            return count;
        }

        [Test]
        public static void BitDiff_Test()
        {
            int a = 10;
            int b = 23;
            Assert.AreEqual(4, BitsDiffCount(a, b));
        }
    }

    /// <summary>
    /// Leetcode
    /// Given two binary strings(represent unsigned integer), return their sum as a binary string 
    /// For example, a = "11", b = "1", Return "100". 
    /// </summary>
    public class BinaryAddition
    {
        public static string Addition(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return ""; // invalid input
            string longer = s1.Length > s2.Length ? s1 : s2;
            string shorter = s1.Length <= s2.Length ? s1 : s2;
            int diff = longer.Length - shorter.Length;
            while (diff-- > 0)
            {
                shorter = "0" + shorter;
            }
            string result = "";
            bool carry = false;
            for (int i = longer.Length - 1; i >= 0; i--)
            {
                if (longer[i] == '0' && shorter[i] == '0' && carry)
                {
                    result = "1" + result; carry = false;
                }
                else if (longer[i] == '0' && shorter[i] == '1' && carry)
                {
                    result = "0" + result;
                }
                else if (longer[i] == '1' && shorter[i] == '0' && carry)
                {
                    result = "0" + result;
                }
                else if (longer[i] == '1' && shorter[i] == '1' && carry)
                {
                    result = "1" + result;
                }
                else if (longer[i] == '0' && shorter[i] == '0' && !carry)
                {
                    result = "0" + result;
                }
                else if (longer[i] == '0' && shorter[i] == '1' && !carry)
                {
                    result = "1" + result;
                }
                else if (longer[i] == '1' && shorter[i] == '0' && !carry)
                {
                    result = "1" + result;
                }
                else if (longer[i] == '1' && shorter[i] == '1' && !carry)
                {
                    result = "0" + result; carry = true;
                }
            }
            if (carry) result = "1" + result;
            return result;
        }

        [Test]
        public static void BinaryAddition_Test()
        {
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                int x = r.Next(100);
                int y = r.Next(100);
                string z = Addition(Convert.ToString(x, 2), Convert.ToString(y, 2));
                Assert.AreEqual(x + y, Convert.ToInt32(z, 2));
            }
        }

    }

    /// <summary>
    /// You have given n numbers from 1 to n. You have to sort numbers with increasing number of set bits.
    /// If you have two number with equal number of set bits, then number with lowest value come first in the output.
    /// for ex: n=5. output: 1,2,4,3,5
    /// </summary>
    public class SetBitsSorting
    {

        // (1) traverse through the number, for each number, reset the lowest bit using n & (n - 1), 
        // if the number becomes zero, append it to the result list. 
        // (2) repeat (1) until the result list length equals to n
        public static int[] SortBySetBits(int n)
        {
            int[] nums = new int[n];
            for (int i = 1; i <= nums.Length; i++)
            {
                nums[i - 1] = i;
            }
            int zeros = 0;
            int index = 0;
            int[] result = new int[n];
            while (zeros < n)
            {
                for (int i = 0; i < nums.Length; i++)
                {
                    if (nums[i] != 0)
                    {
                        nums[i] = nums[i] & (nums[i] - 1);
                        if (nums[i] == 0)
                        {
                            zeros++;
                            result[index++] = i + 1;
                        }
                    }
                }
            }
            return result;
        }

        [Test]
        public static void SetBitsSorting_Test()
        {
            Assert.AreEqual(new int[] { 1, 2, 4, 3, 5 }, SortBySetBits(5));
        }
    }

    ///// <summary>
    ///// Careercup 5.2
    ///// Given a string representing a decimal number, print the binary represetation
    ///// </summary>
    //public class BinaryConversion
    //{
    //    public static string ToBinary(string s)
    //    {
    //        return null;
    //    }

    //    public static void Test()
    //    {
    //        string s = "0.625";
    //        Console.WriteLine(s + " -> " + ToBinary(s));
    //    }
    //}
}