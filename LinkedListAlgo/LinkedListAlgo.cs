using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace LinkedListAlgo
{

    // LinkedList implementation
    public class LinkedListNode
    {
        public int value { get; set; }
        public LinkedListNode next { get; set; }
        private bool isEmpty = true;
        public LinkedListNode()
        { }
        public LinkedListNode(int value)
        {
            this.value = value;
            this.next = null;
        }
        public LinkedListNode(int value, LinkedListNode next)
        {
            this.value = value;
            this.next = next;
        }
        public void Add(int value)
        {
            if (isEmpty)
            {
                this.value = value;
                this.isEmpty = false;
            }
            else
            {
                LinkedListNode end = new LinkedListNode(value);
                LinkedListNode node = this;
                while (node.next != null)
                {
                    node = node.next;
                }
                node.next = end;
            }
        }
        public static void Print(LinkedListNode list)
        {
            while (list.next != null)
            {
                Console.Write(list.value + " -> ");
                list = list.next;
            }
            if (list != null && list.next == null)
            {
                Console.WriteLine(list.value);
            }
        }
    }

    /// <summary>
    /// Reverse a linkedlist
    /// </summary>
    public class ReverseLinkedList
    {

        // revsrse linkedlist
        // keep track of pervious/current/next node
        //         
        // [tricky]: two special cases: head of list and tail of list
        //
        public static LinkedListNode ReverseByIterative(LinkedListNode list)
        {
            if (list == null) return null;
            LinkedListNode prevNode = null;
            LinkedListNode currNode = list;
            LinkedListNode nextNode = null;
            while (currNode != null)
            {
                // 1. save next node
                nextNode = currNode.next;
                // 2. curr node points to prev node
                currNode.next = prevNode;
                // 3. proceed to the next iteration
                prevNode = currNode;
                currNode = nextNode;
            }
            return prevNode;
        }

        // reverse linkedlist
        // special case: list is null, return null; list has one node, return itself
        // get the second node and recursively reverse it, return the head of the 
        // reversed list (last node before reversing)
        // connect the second node with the first node, done!
        public static LinkedListNode ReverseByRecursion(LinkedListNode list)
        {
            if (list == null) return null;
            if (list.next == null) return list;
            LinkedListNode node2 = list.next;
            LinkedListNode reversedRest = ReverseByRecursion(node2);
            node2.next = list;
            list.next = null;
            return reversedRest;
        }

        public static void ReverseTest()
        {
            LinkedListNode list = new LinkedListNode(1,
                new LinkedListNode(2,
                new LinkedListNode(3,
                new LinkedListNode(4,
                new LinkedListNode(5,
                new LinkedListNode(6,
                new LinkedListNode(7, null)))))));
            Console.Write("Original: ");
            LinkedListNode.Print(list);
            Console.Write("Reversed: ");
            LinkedListNode.Print(ReverseByIterative(list));
            Console.Write("Reversed again: ");
            ReverseByRecursion(list);
            LinkedListNode.Print(list);
        }

    }

    /// <summary>
    /// Question 9
    /// Find the kth elment to tail of a linkedlist
    /// </summary>
    public class LinkedListOrdering
    {
        // LinkedList
        public class LinkedListNode
        {
            public int value;
            public LinkedListNode next;
            public LinkedListNode(int value, LinkedListNode next)
            {
                this.value = value;
                this.next = next;
            }
        }

        // find the kth element to tail in the given linkedlist
        public static LinkedListNode KthToTail(LinkedListNode head, int k)
        {
            if (head == null) return null;
            if (k <= 0) return null;
            LinkedListNode p1 = head;
            LinkedListNode p2 = head;
            int count = 0;
            while (p2 != null && count < k)
            {
                p2 = p2.next;
                count++;
            }
            while (p2 != null)
            {
                p1 = p1.next;
                p2 = p2.next;
            }
            return p1;
        }

        public static void KthToTailTest()
        {
            LinkedListNode list = new LinkedListNode(1,
                new LinkedListNode(2,
                new LinkedListNode(3,
                new LinkedListNode(4,
                new LinkedListNode(5,
                new LinkedListNode(6,
                new LinkedListNode(7, null)))))));
            LinkedListNode node = KthToTail(list, 6);
            Console.Write("list: 1-2-3-4-5-6-7   ");
            Console.WriteLine("6th node is " + node.value);
            Debug.Assert(2 == node.value);
        }
    }

    /// <summary>
    /// Question 35
    /// Find the first shared node in two linkedlist
    /// </summary>
    public class SharedNode
    {
        // LinkedList
        public class LinkedListNode
        {
            public int value;
            public LinkedListNode next;
            public LinkedListNode(int value, LinkedListNode next)
            {
                this.value = value;
                this.next = next;
            }
        }

        // O(N + M) time, traverse the list twice
        // travese both lists, find the diff of length of this two lists
        // use two counter and count the diff more steps at the longer list 
        public static LinkedListNode FirstSharedNode(LinkedListNode list1, LinkedListNode list2)
        {
            if (list1 == null || list2 == null) return null;
            int len1 = Length(list1);
            int len2 = Length(list2);
            if (len1 > len2)
            {
                for (int i = 0; i < len1 - len2; i++)
                    list1 = list1.next;
            }
            else if (len1 < len2)
            {
                for (int i = 0; i < len2 - len1; i++)
                    list2 = list2.next;
            }
            while (list1 != null)
            {
                if (list1 == list2) return list1;
                list1 = list1.next;
                list2 = list2.next;
            }
            return null;
        }

        private static int Length(LinkedListNode list)
        {
            int len = 0;
            while (list != null)
            {
                list = list.next;
                len++;
            }
            return len;
        }

        public static void FirstSharedNodeTest()
        {
            /*
                1 -> 2 -> 3 -> 6 -> 7 -> null
                4 -> 5 -> 6 -> 7 -> null
                First shared node: 6
             */
            LinkedListNode n1 = new LinkedListNode(1, null);
            LinkedListNode n2 = new LinkedListNode(2, null);
            LinkedListNode n3 = new LinkedListNode(3, null);
            LinkedListNode n4 = new LinkedListNode(4, null);
            LinkedListNode n5 = new LinkedListNode(5, null);
            LinkedListNode n6 = new LinkedListNode(6, null);
            LinkedListNode n7 = new LinkedListNode(7, null);
            n1.next = n2;
            n2.next = n3;
            n3.next = n6;
            n4.next = n5;
            n5.next = n6;
            n6.next = n7;
            LinkedListNode sharedNode = FirstSharedNode(n1, n4);
            PrintLinkedList(n1);
            PrintLinkedList(n4);
            Console.WriteLine("First shared node: " + sharedNode.value);
        }

        private static void PrintLinkedList(LinkedListNode list)
        {
            while (list != null)
            {
                Console.Write(list.value + " -> ");
                list = list.next;
            }
            Console.WriteLine("null");// print NULL at the end
        }

    }

    /// <summary>
    /// Careercup 2.1
    /// Remove duplicates from an unsorted linkedlist
    /// </summary>
    public class RemoveDuplicates
    {
        // use hashtable, O(N) time, O(N) space
        public static void Remove(LinkedListNode list)
        {
            LinkedListNode node = null;
            HashSet<int> set = new HashSet<int>();
            while (list != null)
            {
                if (!set.Contains(list.value))
                {
                    set.Add(list.value);
                    node = list;
                }
                else
                {
                    node.next = list.next;
                }
                list = list.next;
            }
        }

        public static void Test()
        {
            LinkedListNode list = new LinkedListNode(1,
                new LinkedListNode(2,
                new LinkedListNode(3,
                new LinkedListNode(2,
                new LinkedListNode(3,
                new LinkedListNode(4,
                new LinkedListNode(5, null)))))));
            LinkedListNode.Print(list);
            Remove(list);
            LinkedListNode.Print(list);
        }

    }

    /// <summary>
    /// Careercup 2.3
    /// Delete a node in the middle (not the tail) of a linkedlist, given only that node
    /// (assume that node is not the tail node)
    /// </summary>
    public class SmartDelete
    {
        public static bool Delete(LinkedListNode node)
        {
            if (node == null) return false;
            if (node.next == null) return false; // if node is the tail, can't delete
            node.value = node.next.value;
            node.next = node.next.next;
            return true;
        }

        public static void Test()
        {
            LinkedListNode node = new LinkedListNode(4,
                new LinkedListNode(5, null));
            LinkedListNode list = new LinkedListNode(1,
                new LinkedListNode(2,
                new LinkedListNode(3, node)));
            LinkedListNode.Print(list);
            bool result = Delete(node);
            if (result)
            {
                Console.Write("Node 4 is deleted: ");
                LinkedListNode.Print(list);
            }
        }
    }

    /// <summary>
    /// Careercup 2.4
    /// Given two linkedlist, each node has a digit, reverse digits of the list forms a number
    /// output a linkedlist as the sum of the sum of the two number
    /// eg: list1: 3->1->2  list2: 5->9->2  output: 5->0->8
    /// </summary>
    public class ReverselyMergeList
    {
        // use a boolean variable carry
        public static LinkedListNode SpecialAdd(LinkedListNode list1, LinkedListNode list2)
        {
            if (list1 == null && list2 == null) return null;
            LinkedListNode newList = new LinkedListNode();
            bool carry = false;
            int value;
            while (list1 != null || list2 != null)
            {
                if (list1 == null)
                    value = list2.value;
                if (list2 == null)
                    value = list1.value;
                else
                    value = list1.value + list2.value;
                if (carry)
                {
                    newList.Add(++value % 10);
                    if (value < 10) carry = false;
                }
                else
                {
                    newList.Add(value % 10);
                    if (value >= 10) carry = true;
                }
                if (list1 != null)
                    list1 = list1.next;
                if (list2 != null)
                    list2 = list2.next;
            }
            return newList;
        }

        public static void Test()
        {
            LinkedListNode list1 = new LinkedListNode(3, new LinkedListNode(1, new LinkedListNode(5)));
            LinkedListNode list2 = new LinkedListNode(5, new LinkedListNode(9, new LinkedListNode(2)));
            LinkedListNode list3 = new LinkedListNode(5, new LinkedListNode(9));
            LinkedListNode.Print(list1);
            LinkedListNode.Print(list2);
            LinkedListNode.Print(list3);
            Console.Write("list1 + list2: ");
            LinkedListNode.Print(SpecialAdd(list1, list2));
            Console.Write("list1 + list3: ");
            LinkedListNode.Print(SpecialAdd(list1, list3));
        }
    }

    /// <summary>
    /// Careercup 2.5
    /// Given a circurlar linkedlist, find the first node of the loop
    /// </summary>
    public class CircularLinkedList
    {
        // tortoise and hare algorithm, O(N) time
        // one slow node, one fast (2x) node, count how many steps the slow node
        // moves when they first meet, and move the slow node as many steps forward
        public static LinkedListNode TortoiseAndHareCycleDetection(LinkedListNode list)
        {
            LinkedListNode fastNode = list;
            LinkedListNode slowNode = list;
            int count = 0;
            while (true)
            {
                count++;
                slowNode = slowNode.next;
                fastNode = fastNode.next.next;
                if (slowNode.value == fastNode.value) break; // two nodes meets
            }
            for (int i = 0; i < count; i++)
                slowNode = slowNode.next;
            return slowNode;
        }

        // http://www.siafoo.net/algorithm/11
        // Brent's cycle detection, O(N) time
        public static LinkedListNode BrentCycleDetection(LinkedListNode list)
        {
            if (list == null) return null;
            LinkedListNode fastNode = list;
            LinkedListNode slowNode = list;
            int stepCount = 0;
            int limit = 2;
            while (true)
            {
                if (fastNode.next == null)
                    return null; // no loop found
                fastNode = fastNode.next;
                stepCount += 1;
                if (fastNode.value == slowNode.value)
                    return fastNode;
                if (stepCount == limit)
                {
                    stepCount = 0;
                    limit *= 2;
                    slowNode = fastNode; // teleport slowNode to fastNode
                }
            }
        }

        // ONLY works for list of distinct value nodes
        public static void PrintCircularList(LinkedListNode list, LinkedListNode startLoop)
        {
            while (list != null)
            {
                Console.Write(list.value + " -> ");
                list = list.next;
                if (list.value == startLoop.value) break;
            }
            Console.Write(list.value + " -> ");
            list = list.next;
            while (list != null)
            {
                if (list.value == startLoop.value) break;
                Console.Write(list.value + " -> ");
                list = list.next;
            }
            Console.WriteLine();
        }

        public static void Test()
        {
            /*
             * circular list: 1 -> 2 -> 3 -> 4 -> 5 
             *                          ^         |
             *                          |_________|                      
             */
            LinkedListNode list = new LinkedListNode(1);
            LinkedListNode node2 = new LinkedListNode(2);
            LinkedListNode node3 = new LinkedListNode(3);
            LinkedListNode node4 = new LinkedListNode(4);
            LinkedListNode node5 = new LinkedListNode(5);
            list.next = node2;
            node2.next = node3;
            node3.next = node4;
            node4.next = node5;
            node5.next = node3;
            LinkedListNode node = TortoiseAndHareCycleDetection(list);
            PrintCircularList(list, node);
            Console.WriteLine("Loop starts from: " + node.value);
            node = BrentCycleDetection(list);
            PrintCircularList(list, node);
            Console.WriteLine("Loop starts from: " + node.value);
        }

    }


    /// <summary>
    /// http://www.leetcode.com/2011/08/insert-into-a-cyclic-sorted-list.html
    /// Given a node from a cyclic linked list which has been sorted, write a function to 
    /// insert a value into the list such that it remains a cyclic sorted list. The given node 
    /// can be any single node in the list.
    /// </summary>
    public class InsertionInSortedCircularList
    {
        public static LinkedListNode Insert(LinkedListNode node, int val)
        {
            if (node == null) return new LinkedListNode(val); // null
            LinkedListNode p = node;
            LinkedListNode q = node.next;
            if (q.value == p.value)
            { // single node handled here
                LinkedListNode newNode = new LinkedListNode(val, p);
                p.next = newNode;
                return newNode;
            }
            int max;
            while (true)
            {   // three possible sinario to insert newNode
                // (1) p < val < q
                // (2) q < p < val, insert after the tail
                // (3) val < q < p, insert before the head
                max = Math.Max(p.value, q.value);
                if ((p.value < val && val < q.value)
                    || (max == p.value && p.value > val && q.value > val)
                    || (max == p.value && p.value < val && q.value < val))
                {
                    LinkedListNode newNode = new LinkedListNode(val, q);
                    p.next = newNode;
                    return newNode;
                }
                p = p.next;
                q = q.next;
            }
        }


        // print a sorted cyclic linkedlist from head to tail
        private static void print(LinkedListNode node)
        {
            if (node == null) return;
            if (node.next == null) Console.WriteLine(node.value);
            while (node.next.value > node.value)
            {
                node = node.next;
            }
            node = node.next;
            while (true)
            {
                Console.Write(node.value + " -> ");
                if (node.next.value < node.value) break;
                node = node.next;
            }
            Console.WriteLine();
        }

        public static void Test()
        {
            LinkedListNode node1 = new LinkedListNode(10);
            LinkedListNode node2 = new LinkedListNode(20);
            LinkedListNode node3 = new LinkedListNode(30);
            LinkedListNode node4 = new LinkedListNode(40);
            node1.next = node2;
            node2.next = node3;
            node3.next = node4;
            node4.next = node1;
            print(node1); // 10->20->30->40->
            LinkedListNode n = Insert(node2, 0);
            print(n); // 0->10->20->30->40->
            LinkedListNode n2 = Insert(node2, 15);
            print(n2);// 0->10->15->20->30->40->
            LinkedListNode n3 = Insert(node2, 50);
            print(n3);// 0->10->15->20->30->40->50->
        }

    }

    /// <summary>
    /// Decide if the given linkedlist is a palindrome sequence
    /// </summary>
    public class PalindromeLinkedList
    {
        // O(N) time and O(1) space
        public static bool IsPalindrome(LinkedListNode list)
        {
            if (list == null) return false;
            if (list.next == null) return true;
            // (1) get the length of the linkedlist
            int length = 0;
            LinkedListNode temp = list;
            for (; temp != null; temp = temp.next)
            {
                length++;
            }
            int mid = (length + 1) / 2;
            LinkedListNode rightHalf = list;
            for (; mid > 0; mid--)
            {
                rightHalf = rightHalf.next;
            }
            // (2) reverse the right half of the linkedlist in O(N/2) time
            LinkedListNode reversedRightHalf = ReverseLinkedList.ReverseByIterative(rightHalf);
            // (3) check if the left half and right half are equal
            for (int i = 0; i < length / 2; i++)
            {
                if (list.value != reversedRightHalf.value) return false;
                list = list.next;
                reversedRightHalf = reversedRightHalf.next;
            }
            return true;
        }

        public static void Test()
        {
            LinkedListNode list1 = new LinkedListNode(1, new LinkedListNode(2, new LinkedListNode(3, new LinkedListNode(3,
                new LinkedListNode(2, new LinkedListNode(1))))));
            LinkedListNode list2 = new LinkedListNode(1, new LinkedListNode(2, new LinkedListNode(3, new LinkedListNode(4,
                new LinkedListNode(3, new LinkedListNode(2, new LinkedListNode(1)))))));
            LinkedListNode list3 = new LinkedListNode(1, new LinkedListNode(2, new LinkedListNode(3, new LinkedListNode(4,
                new LinkedListNode(2, new LinkedListNode(1))))));
            LinkedListNode list4 = new LinkedListNode(1);
            LinkedListNode list5 = new LinkedListNode(1, new LinkedListNode(2));
            LinkedListNode.Print(list1);
            Console.WriteLine(IsPalindrome(list1)); // true
            LinkedListNode.Print(list2);
            Console.WriteLine(IsPalindrome(list2)); // true
            LinkedListNode.Print(list3);
            Console.WriteLine(IsPalindrome(list3)); // false
            LinkedListNode.Print(list4);
            Console.WriteLine(IsPalindrome(list4)); // true
            LinkedListNode.Print(list5);
            Console.WriteLine(IsPalindrome(list5)); // false
        }

    }

    /// <summary>
    /// Given a complex linkedlist, every node has a next pointer and a sibling pointer (points to a random node in the list)
    /// Clone the complex linkedlist
    /// </summary>
    public class ComplexLinkedListNodeClone
    {
        public class ComplexNode
        {
            public int value;
            public ComplexNode next;
            public ComplexNode sibling;
            public ComplexNode(int value)
            {
                this.value = value;
            }
            public ComplexNode(int value, ComplexNode next)
            {
                this.value = value;
                this.next = next;
            }
        }

        // pitfalls: (i) remember to set the null pointer of the last node
        //           (ii) not every node has a sibling
        public static ComplexNode clone(ComplexNode start)
        {
            if (start == null || start.next == null) return start;
            // 1. copy every nodes with next pointer and value
            ComplexNode head = start;
            while (start != null)
            {
                ComplexNode copy = new ComplexNode(start.value, start.next);
                start.next = copy;
                start = copy.next;
            }
            // 2. set all the sibling pointers
            start = head;
            while (start != null)
            {
                ComplexNode copy = start.next;
                if (start.sibling != null)
                    copy.sibling = start.sibling.next;
                start = copy.next;
            }
            // 3. seperate the whole list into two lists
            start = head;
            ComplexNode clone = null;
            while (start.next.next != null)
            {
                ComplexNode copy = start.next;
                if (clone == null)
                    clone = copy;
                start.next = copy.next;
                copy.next = copy.next.next;
                start = start.next;
            }
            start.next = null; // set the pointer of last node
            return clone;
        }


        public static void printList(ComplexNode start)
        {
            ComplexNode head = start;
            while (start != null)
            {
                Console.Write(start.value + " ");
                start = start.next;
            }
            Console.WriteLine();
            while (head != null)
            {
                if (head.sibling != null)
                {
                    Console.WriteLine(head.value + " -> " + head.sibling.value);
                }
                head = head.next;
            }
        }

        public static void Test()
        {
            /*   --------- 
                 |       |
             A - B - C - D - E
             |   |   |       |
             ----|----       |
                 |           | 
                 -------------
             */
            ComplexNode E = new ComplexNode(5);
            ComplexNode D = new ComplexNode(4, E);
            ComplexNode C = new ComplexNode(3, D);
            ComplexNode B = new ComplexNode(2, C);
            ComplexNode A = new ComplexNode(1, B);
            A.sibling = C;
            D.sibling = B;
            B.sibling = E;
            printList(A);
            ComplexNode clonedList = clone(A);
            printList(clonedList);
        }

    }


}
