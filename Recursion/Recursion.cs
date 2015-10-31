using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recursion
{

    /// <summary>
    /// Question 58 
    /// N-Queens problem
    /// </summary>
    public class NQueens
    {
        public static void FindAll(int N)
        {
            int[] queens = new int[N];
            FindAll(queens, 0);
        }

        private static void FindAll(int[] queens, int n)
        {
            if (n == queens.Length)
            {
                PrintQueens(queens);  // found it
                return;
            }

            for (int i = 0; i < queens.Length; i++)
            {
                queens[n] = i;
                if (IsLegal(queens, n))
                    FindAll(queens, n + 1);
            }

        }

        private static bool IsLegal(int[] queens, int n)
        {
            for (int i = 0; i < n; i++)
            {
                if (queens[n] - queens[i] == n - i) return false;
                if (queens[n] - queens[i] == i - n) return false;
                if (queens[n] == queens[i]) return false;
            }
            return true;
        }

        private static void PrintQueens(int[] queens)
        {
            for (int i = 0; i < queens.Length; i++)
            {
                for (int j = 0; j < queens.Length; j++)
                {
                    if (queens[i] == j) Console.Write("Q");
                    else Console.Write("-");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static void NQueensTest()
        {
            FindAll(8);
        }


        public static void Count(int N)
        {
            int[] queens = new int[N];
            num = 0;
            Count(queens, 0);
            Console.WriteLine("N = {0}, Count = {1}", N, num);
        }
        private static int num;
        private static void Count(int[] queens, int n)
        {
            if (n < queens.Length)
            {
                for (int i = 0; i < queens.Length; i++)
                {
                    queens[n] = i;
                    if (IsLegal(queens, n)) Count(queens, n + 1);
                }
            }
            else ++num; // found!
        }

        public static void NQueensCountTest()
        {
            /*
                N = 1, Count = 1
                N = 2, Count = 0
                N = 3, Count = 0
                N = 4, Count = 2
                N = 5, Count = 10
                N = 6, Count = 4
                N = 7, Count = 40
                N = 8, Count = 92
                N = 9, Count = 352
                N = 10, Count = 724
                N = 11, Count = 2680
                N = 12, Count = 14200
             */
            for (int i = 1; i <= 12; i++) Count(i);
        }
    }
    /// <summary>
    /// Math.Pow(double base, double n)
    /// </summary>
    public class Power
    {
        public static double ComputePower(double a, int n)
        {
            if (a == 0) return 0;
            if (a == 1) return 1;
            if (n < 0) return 1 / ComputePower(a, -n); // negative power
            if (n == 0) return 1;
            if (n == 1) return a;
            // a^n = a^(n/2) * a^(n/2) when a is even
            double result = ComputePower(a, n >> 1);
            result *= result;
            // a^n = a^(n/2) * a^(n/2) * a when a is odd
            if ((n & 1) == 1) result *= a;
            return result;
        }

        public static void ComputePowerTest()
        {
            Console.WriteLine("{0}^{1} = {2}", 2, 0, ComputePower(2, 0));
            Console.WriteLine("{0}^{1} = {2}", 2, 1, ComputePower(2, 1));
            Console.WriteLine("{0}^{1} = {2}", 2, 10, ComputePower(2, 10));
            Console.WriteLine("{0}^{1} = {2}", 2, -10, ComputePower(2, -10));
        }
    }

    /// <summary>
    /// Careercup 8.1
    /// Fibonacci number
    /// F(0) = 0, F(1) = 1, F(n + 2) = F(n + 1) + F(n) 
    /// </summary>
    public class Fibonacci
    {
        // O(2^N) time, O(N) space
        public static int InefficientFib(int n)
        {
            if (n <= 1) return n;
            return InefficientFib(n - 1) + InefficientFib(n - 2);
        }

        // O(N) time, O(N) space
        public static int IterativeFib(int n)
        {
            if (n <= 1) return n;
            int fib0 = 0;
            int fib1 = 1;
            for (int i = 1; i < n; i++)
            {
                int fib2 = fib0 + fib1;
                fib0 = fib1;
                fib1 = fib2;
            }
            return fib1;
        }

        // TODO
        public static int EfficientFib(int n)
        {
            return -1;
        }

        public static void Test()
        {
            for (int i = 0; i < 40; i++)
                Console.WriteLine("Fib({0}) = {1}", i, InefficientFib(i));
            Console.WriteLine();
            for (int i = 0; i < 40; i++)
                Console.WriteLine("Fib({0}) = {1}", i, IterativeFib(i));
            Console.WriteLine();
            for (int i = 0; i < 40; i++)
                Console.WriteLine("Fib({0}) = {1}", i, EfficientFib(i));
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Careercup 8.2
    /// Given a NxN grid, move (either right or down) from the left upper corner to the right buttom corner
    /// Count the number of paths
    /// </summary>
    public class NxNGrid
    {



    }

    /// <summary>
    /// Careercup 8.3
    /// Given a set, return all the subsets of the set
    /// </summary>
    public class AllSubsets
    {
        // recursive version
        // if list is empty, add the empty set and return
        // remove the first element from the list, recursively get the subsets of the current list
        // foreach of the current subsets, add the first element to it
        public static List<List<int>> Subsets_recursive(List<int> list)
        {
            List<List<int>> result = new List<List<int>>();
            if (list.Count == 0)
            {
                result.Add(new List<int>()); // caution: don't forget the empty set
                return result;
            }
            int first = list[0];
            list.RemoveAt(0);
            List<List<int>> restList = Subsets_recursive(list);
            for (int i = 0; i < restList.Count; i++)
            {
                List<int> temp = restList[i];
                result.Add(temp);
                temp.Add(first);
                result.Add(temp);
            }
            return result;
        }

        // another recursive version
        public static List<List<int>> Subsets_recursive_2(List<int> list, int index)
        {
            List<List<int>> result = new List<List<int>>();
            if (index == list.Count)
            {
                result.Add(new List<int>()); // add empty set
            }
            else
            {
                result = Subsets_recursive_2(list, index + 1);
                int first = list[index];
                List<List<int>> moreList = new List<List<int>>();
                for (int i = 0; i < result.Count; i++)
                {
                    List<int> li = new List<int>();
                    li.AddRange(result[i]);
                    li.Add(first);
                    moreList.Add(li);
                }
                result.AddRange(moreList);
            }
            return result;
        }

        public static List<List<int>> Subsets_recursive_2(List<int> list)
        {
            return Subsets_recursive_2(list, 0);
        }

        private static void Print(List<List<int>> list)
        {

            foreach (List<int> li in list)
            {
                Console.Write("{ ");
                foreach (int i in li)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine("}");
            }

        }

        public static void Test()
        {
            int[] array = { 1, 2, 3, 4 };
            List<int> list = new List<int>(array);
            Print(Subsets_recursive_2(list));
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Careercup 8.4
    /// Print all the permutations of a string
    /// </summary>
    public class StringPermutation
    {
        public static void Permu(string s)
        {
            if (s == null) return;
            Permu(s, 0);
        }

        // recursive swap (index, j) of string
        private static void Permu(string s, int index)
        {
            if (index == s.Length)
            {
                Console.WriteLine(s);
                return;
            }
            for (int i = index; i < s.Length; i++)
            {
                s = Swap(s, index, i);
                Permu(s, index + 1);
            }
        }

        private static string Swap(string s, int i, int j)
        {
            char[] chs = new char[s.Length];
            for (int k = 0; k < chs.Length; k++)
            {
                if (k == i) chs[k] = s[j];
                else if (k == j) chs[k] = s[i];
                else chs[k] = s[k];
            }
            return new String(chs);
        }

        public static void Test()
        {
            Permu("abc");
            Permu("abcd");
            Permu("abcde");
        }
    }

    /// <summary>
    /// Given NxN board, a knight start from upper left corner, visit every box of the board
    /// </summary>
    public class KnightTourProblem
    {
        // global var
        private static int[,] board;
        private static int N;

        // legal move for x and y
        private static int[] xMove = { 2, 1, -1, -2, -2, -1, 1, 2 };
        private static int[] yMove = { 1, 2, 2, 1, -1, -2, -2, -1 };


        public static bool Solve(int n)
        {
            N = n;
            board = new int[N, N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    board[i, j] = -1; // init global variable 
            board[0, 0] = 0;
            return Solve(0, 0, 1);
        }

        /*
         * (1) check the base case: steps = 64, found a solution!
         * (2) do BFS, branching factor is 8
         *        (1) make a move, check if legal move
         *        (2) update the status
         *        (3) recursively solve the subproblem
         *               (1) return true if we can find a solution
         *               (2) revert the status back to previous step
         * (3) if BFS doesn't found a solution, return false
         */
        private static bool Solve(int x, int y, int steps)
        {
            if (steps == N * N) return true;
            for (int i = 0; i < xMove.Length; i++)
            {
                int xNext = x + xMove[i];
                int yNext = y + yMove[i];
                if (Islegal(xNext, yNext))
                {
                    board[xNext, yNext] = steps;
                    if (Solve(xNext, yNext, steps + 1))
                        return true; // found it!
                    else
                        board[xNext, yNext] = -1; // backtrack
                }
            }
            return false;
        }
        // check is legal move
        private static bool Islegal(int x, int y)
        {
            return x >= 0 && x < N
                && y >= 0 && y < N
                && board[x, y] == -1; // cautions: can't be visited
        }
        private static void PrintBoard(int[,] board)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    Console.Write(board[i, j] + " ");
                Console.WriteLine();
            }
        }

        public static bool FindPath(int xStart, int yStart, int xEnd, int yEnd)
        {
            board = new int[N, N];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < N; j++)
                    board[i, j] = -1; // init global variable 
            board[xStart, yStart] = 0;
            return FindPath(xStart, yStart, xEnd, yEnd, 1);
        }

        private static bool FindPath(int x, int y, int xEnd, int yEnd, int steps)
        {
            if (x == xEnd && y == yEnd) return true;
            for (int i = 0; i < xMove.Length; i++)
            {
                int xNext = x + xMove[i];
                int yNext = y + yMove[i];
                if (Islegal(xNext, yNext))
                {
                    board[xNext, yNext] = steps;
                    if (FindPath(xNext, yNext, xEnd, yEnd, steps + 1))
                        return true; // found it!
                    else
                        board[xNext, yNext] = -1; // backtrack
                }
            }
            return false;
        }


        private static void PrintPath(int[,] board)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    Console.Write((board[i, j] != -1 ? board[i, j].ToString() : " ") + " ");
                Console.WriteLine();
            }
        }
        public static void Test()
        {
            for (int i = 3; i <= 8; i++)
            {
                bool result = Solve(i);
                if (result)
                    PrintBoard(board);
                else
                    Console.WriteLine("No solution for N = " + N);
                Console.WriteLine();
            }
            //Random rand = new Random();
            //N = 8;
            //for (int i = 0; i < 1; i++)
            //{
            //    int x1 = 2;//rand.Next(8);
            //    int y1 = 2;// rand.Next(8);
            //    int x2 = 3;// rand.Next(8);
            //    int y2 = 4;// rand.Next(8);
            //    bool result = FindPath(x1, y1, x2, y2, 8);
            //    if (result)
            //    {
            //        Console.WriteLine("Solution for ({0}, {1}) -> ({2}, {3})", x1, y1, x2, y2);
            //        PrintPath(board);
            //    }
            //    else
            //        Console.WriteLine("No solution for ({0}, {1}) -> ({2}, {3})", x1, y1, x2, y2);
            //    Console.WriteLine();
            //}
        }

    }

    /// <summary>
    /// Print all permuatations of wel-formed N pairs of parentheses
    /// </summary>
    public class ParenthesesPermutation
    {

        public static void Permu(int N)
        {
            char[] a = new char[N * 2];
            Permu(N, a, N, N);
        }

        // version 1, starts with N left N right
        private static void Permu(int N, char[] a, int left, int right)
        {
            if (left == 0 && right == 0)
            {
                PrintArray(a);
                return;
            }
            if (left > 0)
            { // if there are left paren, try a left paren
                a[N * 2 - left - right] = '(';
                Permu(N, a, left - 1, right);
            }
            if (right > 0 && left < right)
            { // if there are right paren, more right paren than left paren
                a[N * 2 - left - right] = ')';
                Permu(N, a, left, right - 1);
            }

        }

        public static void Permu2(int N)
        {
            char[] a = new char[N * 2];
            Permu2(N, a, 0, 0);
        }

        // version 2, starts with 0 left 0 right
        private static void Permu2(int N, char[] a, int left, int right)
        {
            if (left == N && right == N)
            {
                PrintArray(a);
                return;
            }
            if (left < N)
            {
                a[left + right] = '(';
                Permu2(N, a, left + 1, right);
            }
            if (right < N && left > right)
            {
                a[left + right] = ')';
                Permu2(N, a, left, right + 1);
            }

        }

        public static void Permu3(int N)
        {
            Permu3(N, "", N, 0);
        }

        // version 3, starts with N left 0 right
        private static void Permu3(int N, string s, int left, int right)
        {
            if (left == 0 && right == 0)
            {
                Console.WriteLine(s);
                return;
            }
            if (left > 0)
            {
                s += '(';
                Permu3(N, s, left - 1, right + 1);
            }
            if (right > 0)
            {
                s += ')';
                Permu3(N, s, left, right - 1);
            }
        }

        private static void PrintArray(char[] a)
        {
            foreach (char c in a) Console.Write(c);
            Console.WriteLine();
        }

        public static void Test()
        {
            Permu(3);
            Console.WriteLine();
            Permu2(4);
            Console.WriteLine();
            Permu3(5);
        }


    }

}
