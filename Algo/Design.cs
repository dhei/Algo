using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using NUnit.Framework;

namespace Design
{
    /// <summary>
    /// Design a data structure, which get(int index), set(int index, object value) and 
    /// setAll(object value) are O(1) operation
    /// </summary>
    public class QuickSetAllObject
    {
        public class SpecialObject
        {
            public int version { get; set; }
            public object value { get; set; }
        }

        // member variables: version number, value
        private int version;
        private object value;
        private SpecialObject[] array;


        // if the global version is greater than local version number, update the 
        // local element to global version and value
        public SpecialObject get(int index)
        {
            SpecialObject element = array[index];
            if (element.version < this.version)
            {
                element.version = this.version;
                element.value = this.value;
            }
            return element;
        }

        public void set(int index, object value)
        {
            array[index].value = value;
        }

        // update the global version number and value
        public void setAll(object value)
        {
            this.version++;
            this.value = value;
        }

        // helper function for testing
        public QuickSetAllObject(int size)
        {
            this.version = 0;
            this.value = null;
            this.array = new SpecialObject[size];
            for (int i = 0; i < size; i++)
                this.array[i] = new SpecialObject();
        }

        //[Test]
        public static void Test()
        {
            QuickSetAllObject sd = new QuickSetAllObject(10);
            for (int i = 0; i < 10; i++)
                sd.set(i, i);
            for (int i = 0; i < 10; i++)
                Console.WriteLine("get[{0}] = {1}", i, sd.get(i).value);
            sd.setAll(10);
            for (int i = 0; i < 10; i++)
                Console.WriteLine("get[{0}] = {1}", i, sd.get(i).value);
        }
    }


    /// <summary>
    /// Quicksort worst case O(N^2) time
    /// </summary>
    public class QuicksortKiller : IComparer<int>
    {
        Dictionary<int, int> keys = new Dictionary<int, int>();

        int candidate = 0;
        public int Compare(int x, int y)
        {
            if (!keys.ContainsKey(x) && !keys.ContainsKey(y))
            {
                if (x == candidate) keys[x] = keys.Count;
                else keys[y] = keys.Count;
            }

            if (!keys.ContainsKey(x)) { candidate = x; return 1; }
            if (!keys.ContainsKey(y)) { candidate = y; return -1; }
            return keys[x] - keys[y];
        }

        public static int[] MakeBadArray(int length)
        {
            int[] arr = Enumerable.Range(0, length).ToArray();
            Array.Sort(arr, new QuicksortKiller());
            int[] ret = new int[length];

            for (int i = 0; i < length; i++)
            {
                ret[arr[i]] = i;
            }

            return ret;
        }

        public static void Test()
        {
            Stopwatch sw;
            int N = 14;
            long[] milliseconds = new long[N];
            int size = 1;
            for (int i = 1; i <= N; i++)
            {
                size *= 2;
                sw = Stopwatch.StartNew();
                MakeBadArray(size);
                sw.Stop();
                milliseconds[i - 1] = sw.ElapsedMilliseconds;

            }
            size = 1;
            for (int i = 1; i <= N; i++)
            {
                size *= 2;

                /*
                size = 512, time = 4
                size = 1024, time = 22
                size = 2048, time = 106
                size = 4096, time = 276
                size = 8192, time = 1088
                size = 16384, time = 4377
                */
                Console.WriteLine("size = {0}, time = {1}", size, milliseconds[i - 1]);
            }
        }
    }

    /// <summary>
    /// Design a LRU cache, insert/delete/search in O(1) time
    /// </summary>
    public class LRUCache
    {

        int limit;

        // sequence number of each value
        List<int> keyList;  /* a doubly-linked list is needed*/
        // hashmap of <sequence_number, value>
        Dictionary<int, string> map;

        public LRUCache(int limit)
        {
            this.limit = limit;
            this.keyList = new List<int>();
            this.map = new Dictionary<int, string>();
        }


        // kick-out the first element from the keyList if the cache is full
        // then add the key to the end of the keyList
        public void insert(int key, string value)
        {
            if (map.ContainsKey(key))
            {
                map.Remove(key);
            }
            if (keyList.Count == limit)
            {
                int key2 = keyList[0];
                keyList.Remove(key2);
                map.Remove(key2);
            }
            keyList.Add(key);
            map.Add(key, value);
        }

        public void delete(int key)
        {
            if (map.ContainsKey(key))
            {
                map.Remove(key);
                keyList.Remove(key);
            }
        }

        // every search will remove the first element in the keyList to the end of the list
        public string search(int key)
        {
            if (keyList.Contains(key))
            {
                keyList.Remove(key);
                keyList.Add(key);
            }
            if (map.ContainsKey(key))
                return map[key];
            else return null;
        }

        private void PrintAll()
        {
            foreach (var pair in map)
            {
                Console.Write(pair.Key + ": " + pair.Value + "  ");
            }
            Console.WriteLine();
        }

        //[Test]
        public static void Test()
        {
            LRUCache lru = new LRUCache(3);
            lru.insert(1, "a");
            lru.insert(2, "b");
            lru.insert(3, "c");
            lru.PrintAll();
            lru.insert(4, "d");
            lru.PrintAll();
            lru.insert(5, "e");
            lru.PrintAll();
            lru.insert(1, "c");
            lru.PrintAll();
            lru.insert(1, "c");
            lru.PrintAll();
            lru.delete(1);
            lru.PrintAll();
            Console.WriteLine("search for 1: " + lru.search(1));
            lru.delete(1);
            lru.PrintAll();
            Console.WriteLine("search for 1: " + lru.search(1));
        }
    }
}
