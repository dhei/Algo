using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using NUnit.Framework;

namespace StackQueueHeapAlgo
{
    /// <summary>
    /// Question 2
    /// implement a stack with push(E), pop(), min() of O(1) time
    /// use auxilary stack to store the min value, the current min value is always 
    /// on the top of the auxilary stack
    /// </summary>
    class StackWithMin
    {
        private Stack<int> stack1 = new Stack<int>();
        private Stack<int> stack2 = new Stack<int>();

        // push the current min onto auxilary stack
        public void Push(int x)
        {
            stack1.Push(x);
            if (stack2.Count == 0) stack2.Push(x);
            else if (x < stack2.Peek()) stack2.Push(x);
        }

        // pop the current min from the auxilary stack when necessary 
        public int Pop()
        {
            int y = stack1.Pop();
            if (y == stack2.Peek()) stack2.Pop();
            return y;
        }

        public int Min()
        {
            return stack2.Peek();
        }

        public int Count()
        {
            return stack1.Count;
        }

        [Test]
        public static void StackWithMin_Test()
        {
            StackWithMin swm = new StackWithMin();
            swm.Push(2);
            swm.Push(1);
            swm.Push(3);
            swm.Push(4);
            int min = swm.Min();
            int[] poppedItem = new int[4];
            int i = 0;
            while (swm.Count() > 0)
            {
                poppedItem[i++] = swm.Pop();
            }
            Assert.AreEqual(new int[] { 4, 3, 1, 2 }, poppedItem);
            Assert.AreEqual(1, min);
        }
    }

    /// <summary>
    /// Implement a queue with enqueue() dequeue() in O(1) time and min() in O(1) on average
    /// </summary>
    public class QueueWithMin
    {
        // C# doesn't provide Deque API (Java does), so instead use a List
        private List<int> q = new List<int>();
        private List<int> minQ = new List<int>();

        public void Enqueue(int element)
        {
            q.Add(element);
            while (minQ.Count > 0 && minQ[minQ.Count - 1] > element)
            { // worst case O(N) time, eg: have 2,3,4,5,...N on the queue, enqueue 1 would take O(N) time
                minQ.RemoveAt(minQ.Count - 1);
            }
            minQ.Add(element);
        }

        public int Dequeue()
        {
            if (q.Count == 0) throw new OverflowException();
            if (minQ[0] == q[0])
            {
                minQ.RemoveAt(0);
            }
            int first = q[0];
            q.RemoveAt(0);
            return first;
        }

        public int Min()
        {
            if (q.Count == 0) throw new OverflowException();
            return minQ[0];
        }

        //[Test]
        public static void QueueWithMin_Test()
        {
            QueueWithMin qwm = new QueueWithMin();
            qwm.Enqueue(3);
            Console.WriteLine("min = " + qwm.Min());
            qwm.Enqueue(2);
            Console.WriteLine("min = " + qwm.Min());
            qwm.Enqueue(1);
            Console.WriteLine("min = " + qwm.Min());
            qwm.Enqueue(4);
            Console.WriteLine("min = " + qwm.Min());
            Console.WriteLine("dequeue = " + qwm.Dequeue());
            Console.WriteLine("min = " + qwm.Min());
            Console.WriteLine("dequeue = " + qwm.Dequeue());
            Console.WriteLine("min = " + qwm.Min());
            Console.WriteLine("dequeue = " + qwm.Dequeue());
            Console.WriteLine("min = " + qwm.Min());
            Console.WriteLine("dequeue = " + qwm.Dequeue());
            qwm.Enqueue(6);
            Console.WriteLine("min = " + qwm.Min());
            qwm.Enqueue(7);
            Console.WriteLine("min = " + qwm.Min());
            qwm.Enqueue(5);
            Console.WriteLine("min = " + qwm.Min());
            Console.WriteLine("dequeue = " + qwm.Dequeue());
            Console.WriteLine("min = " + qwm.Min());
            Console.WriteLine("dequeue = " + qwm.Dequeue());
            Console.WriteLine("min = " + qwm.Min());
            Console.WriteLine("dequeue = " + qwm.Dequeue());
            try
            {
                Console.WriteLine("min = " + qwm.Min());
            }
            catch (OverflowException)
            {
                Console.WriteLine("Overflow exception caught.");
            }
        }
    }

    /// <summary>
    /// implement queue by stack
    /// Enqueue() takes O(1) time, Dequeue() takes O(N) time
    /// </summary>
    class QueueImplByStack
    {
        private Stack<int> stack1;
        private Stack<int> stack2;

        public QueueImplByStack()
        {
            stack1 = new Stack<int>();
            stack2 = new Stack<int>();
        }
        public void Enqueue(int item)
        {
            stack1.Push(item);
        }

        public int Dequeue()
        {
            if (stack1.Count == 0 && stack2.Count == 0) throw new Exception("under flow error!");
            while (stack1.Count > 0)
            {
                int item = stack1.Pop();
                stack2.Push(item);
            }
            return stack2.Pop();
        }

        public int Count()
        {
            return stack1.Count;
        }

        [Test]
        public static void Test()
        {
            QueueImplByStack q = new QueueImplByStack();
            for (int i = 0; i < 10; i++) q.Enqueue(i);
            Assert.AreEqual(10, q.Count());
            int[] dequeuedItem = new int[10];
            for (int i = 0; i < 10; i++)
            {
                dequeuedItem[i] = q.Dequeue();
            }
            Assert.AreEqual(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, dequeuedItem);
        }
    }

    /// <summary>
    /// implement stack by queue
    /// Push() takes O(1) time, Pop() takes O(N) time
    /// </summary>
    class StackImplByQueue
    {
        private Queue<int> queue1;
        private Queue<int> queue2;

        public StackImplByQueue()
        {
            queue1 = new Queue<int>();
            queue2 = new Queue<int>();
        }
        public void Push(int item)
        {
            queue1.Enqueue(item);
        }

        public int Pop()
        {
            if (queue1.Count == 0) throw new Exception("under flow error!");
            while (queue1.Count > 1)
            {
                queue2.Enqueue(queue1.Dequeue());
            }
            int item = queue1.Dequeue();
            queue1 = queue2;
            queue2.Clear();
            return item;
        }

        //[Test]
        public static void Test()
        {
            StackImplByQueue stack = new StackImplByQueue();
            for (int i = 0; i < 10; i++)
            {
                stack.Push(i);
                Console.WriteLine("Push({0})", i);
                if ((i & 1) == 0)
                    Console.WriteLine("Pop() = " + stack.Pop());
            }
        }
    }

    /// <summary>
    /// Min Heap implementation
    /// 1-index based array, array[0] is no use
    /// </summary>
    public class MinHeap
    {
        private int[] heap; // heap[0] is dummy node
        private int size = 0;
        private int max;

        public MinHeap(int max)
        {
            this.max = max;
            heap = new int[max + 1];
        }
        private int Parent(int x)
        {
            return x / 2;
        }
        private int LeftChild(int x)
        {
            return x * 2;
        }
        private int RightChild(int x)
        {
            return x * 2 + 1;
        }
        private bool IsLeaf(int x)
        {
            return x > size / 2 && x <= size;
        }
        private void Swap(int x, int y)
        {
            int z = heap[x];
            heap[x] = heap[y];
            heap[y] = z;
        }

        public int Count()
        {
            return size;
        }

        // swim up from bottom towards the root
        public void Insert(int x)
        {
            if (size < max)
            {
                size++;
                heap[size] = x;
                int current = size;
                while (heap[current] < heap[Parent(current)])
                {
                    Swap(current, Parent(current));
                    current = Parent(current);
                }
            }
            else // heap is full
            {

            }
        }

        public int GetMin()
        {
            if (size > 0)
                return heap[1];
            else
                return int.MaxValue;
        }

        // sink down, reheapify
        // 1. swap root with the rightmost leaf
        // 2. reheapify the new root until it sinks down to leaf
        public int DeleteMin()
        {
            Swap(1, size);
            size--;
            if (size != 0)
            {
                int pos = 1; // from the root
                int smallest;
                while (!IsLeaf(pos))
                {
                    if (RightChild(pos) <= size) // pos has both children
                        smallest = heap[LeftChild(pos)] < heap[RightChild(pos)] ? LeftChild(pos) : RightChild(pos);
                    else // pos only has left child
                        smallest = heap[LeftChild(pos)];
                    if (heap[pos] <= heap[smallest]) break;
                    Swap(pos, smallest);
                    pos = smallest;
                }
            }
            return heap[size + 1];
        }


        public static void Test()
        {
            MinHeap h = new MinHeap(6);
            for (int i = 6; i >= 1; i--)
                h.Insert(i);
            while (h.Count() > 0)
                Console.Write(h.DeleteMin() + " ");
            Console.WriteLine();
        }

    }

    /// <summary>
    /// Min Priority Queue 
    /// implemented by resizing array
    /// http://algs4.cs.princeton.edu/24pq/MinPQ.java.html
    /// </summary>
    public class MinPQ
    {
        private int[] pq;
        private int size;

        // constructors
        public MinPQ(int max)
        {
            pq = new int[max + 1];
            size = 0;
        }
        public MinPQ(int[] array)
            : this(array.Length)
        {
            foreach (int i in array)
                Insert(i);
        }

        // public API
        public int Count()
        {
            return size;
        }
        public int GetMin()
        {
            if (size > 0)
                return pq[1];
            else
                return int.MaxValue;
        }

        public void Insert(int item)
        {
            if (size >= pq.Length - 1)
                Resize(pq.Length * 2);
            size++;
            pq[size] = item;
            Swim(size);
        }

        public int DeleteMin()
        {
            if (size == 0)
                throw new Exception("underflow error!");
            int min = pq[1];
            Swap(1, size);
            size--;
            Reheapify(1);
            return min;
        }

        // private methods
        private int Parent(int index)
        {
            return index / 2;
        }
        private int LeftChild(int index)
        {
            return index * 2;
        }
        private int RightChild(int index)
        {
            return index * 2 + 1;
        }
        private bool IsLeaf(int index)
        {
            return index > size / 2;
        }
        // swim up towards root
        private void Swim(int index)
        {
            while (index > 1 && pq[Parent(index)] > pq[index]) // not root yet
            {
                Swap(Parent(index), index);
                index = Parent(index);
            }
        }

        // sink down towards leaf
        private void Reheapify(int index)
        {
            int smallerChild;
            while (!IsLeaf(index))
            {
                if (RightChild(index) <= size)
                    smallerChild = pq[LeftChild(index)] < pq[RightChild(index)] ? LeftChild(index) : RightChild(index);
                else
                    smallerChild = LeftChild(index);
                if (pq[index] <= pq[smallerChild]) break;
                Swap(index, smallerChild);
                index = smallerChild;
            }
        }
        private void Resize(int capacity)
        {
            int[] temp = new int[capacity];
            for (int i = 0; i <= size; i++)
                temp[i] = pq[i];
            pq = temp;
        }
        private void Swap(int index1, int index2)
        {
            int x = pq[index1];
            pq[index1] = pq[index2];
            pq[index2] = x;
        }
        private void PrintHeap()
        {
            for (int i = 1; i < size; i++)
            {
                Console.Write(pq[i] + " ");
            }
            Console.WriteLine();
        }
        public static void Test()
        {
            int[] array = { 3, 1, 2, 5, 6, 7, 9, 10, 4, 8 };
            MinPQ pq = new MinPQ(array);
            for (int i = 0; i < array.Length; i++)
                Console.Write(pq.DeleteMin() + " ");
            Console.WriteLine();

            pq = new MinPQ(2);
            for (int i = 0; i < array.Length; i++)
                pq.Insert(array[i]);
            while (pq.Count() > 0)
                Console.Write(pq.DeleteMin() + " ");
            Console.WriteLine();
        }

    }

    /// <summary>
    /// 100% same implementation as MinPQ
    /// </summary>
    public class MaxPQ
    {
        private int[] pq;
        private int size;

        // constructors
        public MaxPQ(int max)
        {
            pq = new int[max + 1];
            size = 0;
        }
        public MaxPQ(int[] array)
            : this(array.Length)
        {
            foreach (int i in array)
                Insert(i);
        }

        // public API
        public int Count()
        {
            return size;
        }
        public int GetMax()
        {
            if (size > 0)
                return pq[1];
            else
                return int.MinValue;
        }

        public void Insert(int item)
        {
            if (size >= pq.Length - 1)
                Resize(pq.Length * 2);
            size++;
            pq[size] = item;
            Swim(size);
        }

        public int DeleteMax()
        {
            if (size == 0)
                throw new Exception("underflow error!");
            int max = pq[1];
            Swap(1, size);
            size--;
            Reheapify(1);
            return max;
        }

        // private methods
        private int Parent(int index)
        {
            return index / 2;
        }
        private int LeftChild(int index)
        {
            return index * 2;
        }
        private int RightChild(int index)
        {
            return index * 2 + 1;
        }
        private bool IsLeaf(int index)
        { // for a left balanced binary tree, any leaf has an index > (tree_size/2)
            return index > size / 2;
        }

        // swim up towards root
        private void Swim(int index)
        {
            while (index > 1 && pq[Parent(index)] < pq[index]) // not root yet
            {
                Swap(Parent(index), index);
                index = Parent(index);
            }
        }

        // sink down towards leaf
        private void Reheapify(int index)
        {
            int largerChild;
            while (!IsLeaf(index))
            {
                if (RightChild(index) <= size)
                    largerChild = pq[LeftChild(index)] > pq[RightChild(index)] ? LeftChild(index) : RightChild(index);
                else
                    largerChild = LeftChild(index);
                if (pq[index] >= pq[largerChild]) break;
                Swap(index, largerChild);
                index = largerChild;
            }
        }
        private void Resize(int capacity)
        {
            int[] temp = new int[capacity];
            for (int i = 0; i <= size; i++)
                temp[i] = pq[i];
            pq = temp;
        }
        private void Swap(int index1, int index2)
        {
            int x = pq[index1];
            pq[index1] = pq[index2];
            pq[index2] = x;
        }
        private void PrintHeap()
        {
            for (int i = 1; i < size; i++)
            {
                Console.Write(pq[i] + " ");
            }
            Console.WriteLine();
        }

        public static void Test()
        {
            int[] array = { 3, 1, 2, 5, 6, 7, 9, 10, 4, 8 };
            MaxPQ pq = new MaxPQ(array);
            for (int i = 0; i < array.Length; i++)
                Console.Write(pq.DeleteMax() + " "); // 10, 9, 8, 7, 6, 5, 4, 3, 2, 1
            Console.WriteLine();

            pq = new MaxPQ(2);
            for (int i = 0; i < array.Length; i++)
                pq.Insert(array[i]);
            while (pq.Count() > 0)
                Console.Write(pq.DeleteMax() + " "); // 10, 9, 8, 7, 6, 5, 4, 3, 2, 1
            Console.WriteLine();
        }


    }

    /// <summary>
    /// Find kth smallest element in an array
    /// </summary>
    public class KthSmallestElement
    {
        // use max priority queue of size k
        // since insert/delete are O(lg k), time complexity O(N lg k)
        public static int KthSmallest(int[] a, int k)
        {
            MaxPQ pq = new MaxPQ(k);
            for (int i = 0; i < a.Length; i++)
            {
                if (i < k)
                    pq.Insert(a[i]);
                else
                    if (pq.GetMax() > a[i])
                    {
                        pq.DeleteMax();
                        pq.Insert(a[i]);
                    }
            }
            return pq.GetMax();
        }

        public static void Test()
        {
            int[] a = { 3, 2, 1, 5, 8, 9, 10, 4, 7, 6 };
            Console.WriteLine(KthSmallest(a, 7) == 7);
            Console.WriteLine(KthSmallest(a, 2) == 2);
            Console.WriteLine(KthSmallest(a, 1) == 1);
        }


    }



    //public class SkipList
    //{
    //    private class SkipNode
    //    {
    //        public int value { get; private set; }

    //        public SkipNode[] next { get; private set; }

    //        public SkipNode(int value, int level)
    //        {
    //            this.value = value;
    //            this.next = new SkipNode[level];
    //        }

    //    }

    //    private SkipNode head = new SkipNode(0, 33);

    //    private Random rand = new Random();

    //    //private int level = 1;

    //    public void Add(int value)
    //    {
    //        int level = 1;
    //        int r = rand.Next();
    //        for (int i = 1; i <= 32; i++)
    //        {
    //            if ((r & 1) == 1) level++;
    //        }
    //        SkipNode node = new SkipNode(value, level);
    //        SkipNode current;
    //        for (int i = 1; i <= level; i++)
    //        {
    //            for (current = head; current.next[i] != null; current = current.next[i])
    //            {
    //                if (current.next[i].value > value) break;
    //            }
    //            node.next[i] = current.next[i];
    //            current.next[i] = node;
    //        }

    //    }

    //    public bool Remove(int value)
    //    {

    //        return false;

    //    }


    //    public bool Contains(int value)
    //    {

    //        return false;

    //    }
    //}








}
