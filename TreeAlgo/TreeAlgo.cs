using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace TreeAlgo
{

    /// <summary>
    /// binary tree data structure
    /// </summary>
    public class TreeNode
    {
        // public fields
        public int value;
        public TreeNode left, right;

        // constructors
        public TreeNode(int value)
        {
            this.value = value;
        }
        public TreeNode(int value, TreeNode left, TreeNode right)
        {
            this.value = value;
            this.left = left;
            this.right = right;
        }
    }

    /// <summary>
    /// binary tree, each node has a parent pointer
    /// </summary>
    public class ThreePointerTreeNode
    {
        public int value;
        public ThreePointerTreeNode left, right, parent;
        public ThreePointerTreeNode(int value)
        {
            this.value = value;
        }
        public ThreePointerTreeNode(int value, ThreePointerTreeNode left,
            ThreePointerTreeNode right, ThreePointerTreeNode parent)
        {
            this.value = value;
            this.left = left;
            this.right = right;
            this.parent = parent;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class IsTreeBalanced
    {
        // recursive find depth
        public static int Depth(TreeNode root)
        {
            if (root == null) return 0;
            return Math.Max(Depth(root.left), Depth(root.right)) + 1;
        }

        // a variance of definition: A binary tree in which the depth 
        // of every leaf never differ by more than 1  
        public static bool IsBalaned_2(TreeNode root)
        {
            return false;
        }



        // this is the most strict commonly definition of balanced tree, same as balance in "self-balancing tree"
        // such balanced tree has the minimum height
        // 
        // height balanced if (1) tree is empty, or
        // (2) its left subtree and right subtree are balanced 
        // (3) the max and min depth of both subtrees can be more than 1
        // O(2^N) time and O(N) space
        public static bool IsBalanced(TreeNode root)
        {
            if (root == null) return true;
            if (!IsBalanced(root.left) || !IsBalanced(root.right)) return false;
            return Math.Max(MaxDepth(root.left), MaxDepth(root.right))
                - Math.Min(MinDepth(root.left), MinDepth(root.right)) <= 1;
        }
        private static int MaxDepth(TreeNode root)
        {
            if (root == null) return 0;
            return Math.Max(MaxDepth(root.left), MaxDepth(root.right)) + 1;
        }
        public static int MinDepth(TreeNode root)
        {
            if (root == null) return 0;
            //if (root != null && root.left == null && root.right == null) return 1;
            return Math.Min(MinDepth(root.left), MinDepth(root.right)) + 1;
        }


        // recursive version 1, time O(2^N), space O(N)
        // height balanced if (1) tree is empty, or 
        // (2) its left and right children are balanced, and
        // (3) the difference of height of its left and right tree <= 1
        public static bool IsBalanced_loose_definition(TreeNode root)
        {
            return (root == null) ||
             (IsBalanced_loose_definition(root.left) && IsBalanced_loose_definition(root.right)
            && Math.Abs(Depth(root.left) - Depth(root.right)) <= 1);
        }
        // recursive version 2, time O(N), space O(N)
        // optimized version
        // combine the Depth() and Isbalanced()
        public static bool IsBalanced_loose_definition(TreeNode root, out int height)
        {
            if (root == null)
            {
                height = 0;
                return true;
            }
            int hLeft = 0;
            int hRight = 0;
            bool bal = IsBalanced_loose_definition(root.left, out hLeft)
                && IsBalanced_loose_definition(root.right, out hRight);
            height = Math.Max(hLeft, hRight) + 1;
            return bal && Math.Abs(hLeft - hRight) <= 1;
        }



        public static void Test()
        {
            /* This tree is NOT balanced!
             *        1
             *       / \
             *      2   3
             *     / \ / 
             *    4  5 6 
             *      /
             *     7
             */
            TreeNode tree1 = new TreeNode(1, new TreeNode(2,
                new TreeNode(4, null, null), new TreeNode(5,
                new TreeNode(7, null, null), null)),
                new TreeNode(3, new TreeNode(6, null, null), null));



            /* This tree is NOT balanced!
             *        1
             *       / \
             *      2   3
             *     /   / \
             *    4   5   6
             *   /    /    \
             *  7    8      9
             */
            TreeNode tree2 = new TreeNode(1, new TreeNode(2,
                new TreeNode(4, new TreeNode(7, null, null), null), null),
                new TreeNode(3, new TreeNode(5, new TreeNode(8, null, null), null),
                    new TreeNode(6, null, new TreeNode(9, null, null))));



            Console.WriteLine("Depth() = " + MinDepth(tree1)); // 4
            Console.WriteLine("IsBalanced_loose_definition() = " + IsBalanced_loose_definition(tree1)); // true
            Console.WriteLine("IsBalanced()= " + IsBalanced(tree1)); // false
            int depth = 0;
            Console.WriteLine("IsBalanced(out int height) = " + IsBalanced_loose_definition(tree1, out depth)); // true
            Console.WriteLine("Depth() = " + MinDepth(tree2)); // 4
            Console.WriteLine("IsBalanced_loose_definition() = " + IsBalanced_loose_definition(tree2)); // false
            Console.WriteLine("IsBalanced()= " + IsBalanced(tree2)); // false
            depth = 0;
            Console.WriteLine("IsBalanced(out int height) = " + IsBalanced_loose_definition(tree2, out depth)); // false


        }

    }

    /// <summary>
    /// [easy]: recursive perorder | recursive inorder | recursive postorder | iterative preorder | iterative level-order
    /// [medium]: iterative inorder |
    /// [hard]: iterative postorder | iterative inorder(without stack)
    /// </summary>
    public class TreeTraversal
    {
        // Recursive
        public static void PreorderTraverse(TreeNode root) // DFS
        {
            if (root != null)
            {
                Console.Write(root.value + " ");
                PreorderTraverse(root.left);
                PreorderTraverse(root.right);
            }
        }
        public static void InorderTraverse(TreeNode root)
        {
            if (root != null)
            {
                InorderTraverse(root.left);
                Console.Write(root.value + " ");
                InorderTraverse(root.right);
            }
        }
        public static void PostorderTraverse(TreeNode root)
        {
            if (root != null)
            {
                PostorderTraverse(root.left);
                PostorderTraverse(root.right);
                Console.Write(root.value + " ");
            }
        }

        // BFS
        public static void LevelTraverse(TreeNode root)
        {
            if (root != null)
            {
                Queue<TreeNode> queue = new Queue<TreeNode>();
                queue.Enqueue(root);
                while (queue.Count > 0)
                {
                    TreeNode node = queue.Dequeue();
                    Console.Write(node.value + " ");
                    if (node.left != null) queue.Enqueue(node.left);
                    if (node.right != null) queue.Enqueue(node.right);
                }
            }
        }


        // push right substree before left subtree into the stack
        // space O(N)
        public static void PreorderTraverse_iterative(TreeNode root)
        {
            if (root != null)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(root);
                while (stack.Count > 0)
                {
                    TreeNode node = stack.Pop();
                    Console.Write(node.value + " ");
                    if (node.right != null) stack.Push(node.right);
                    if (node.left != null) stack.Push(node.left);
                }
            }
        }

        // Stack + HashSet
        // use a HashSet to keep all the visited nodes
        // not space efficient, O(N) space
        public static void InorderTraverse_iterative(TreeNode root)
        {
            if (root != null)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(root);
                HashSet<TreeNode> visited = new HashSet<TreeNode>();
                while (stack.Count > 0)
                {
                    TreeNode node = stack.Peek();
                    if (visited.Contains(node)) // node is already visited
                    {
                        stack.Pop();
                        Console.Write(node.value + " ");
                        if (node.right != null)
                            stack.Push(node.right); // search on right subtree
                    }
                    else // node is not visited
                    {
                        visited.Add(node);
                        if (node.left != null)
                            stack.Push(node.left); // search on left subtree
                    }
                }
            }
        }

        // Stack + HashSet
        // use a HashSet to keep all the visited nodes
        // space O(N)
        public static void PostorderTraverse_iterative(TreeNode root)
        {
            if (root != null)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                stack.Push(root);
                HashSet<TreeNode> visited = new HashSet<TreeNode>();
                while (stack.Count > 0)
                {
                    TreeNode node = stack.Peek();
                    if (node.left != null && !visited.Contains(node.left))
                    {
                        stack.Push(node.left);
                    }
                    else if (node.right != null && !visited.Contains(node.right))
                    {
                        stack.Push(node.right);
                    }
                    else
                    {
                        stack.Pop();
                        Console.Write(node.value + " ");
                        visited.Add(node);
                    }
                }
            }
        }

        // Using only stack to iteratively preorder traverse tree
        public static void PreorderTraverse_iterative_only_stack(TreeNode root)
        {
            if (root != null)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                TreeNode current = root;
                while (true)
                {
                    if (current != null) // not a leaf node, so search the left subtree
                    {
                        Console.Write(current.value + " ");
                        stack.Push(current);
                        current = current.left;
                    }
                    else // just visited a leaf node
                    {
                        if (stack.Count == 0)
                        {
                            break; // finished
                        }
                        else // pop node from stack
                        {
                            current = stack.Pop();
                            current = current.right;
                        }
                    }
                }
            }
        }

        // http://www.leetcode.com/2010/04/binary-search-tree-in-order-traversal.html
        // Use only stack to iterative inorder traverse tree
        // The last traversed node must not have a right child.
        public static void InorderTraverse_iterative_only_stack(TreeNode root)
        {
            if (root != null)
            {
                Stack<TreeNode> stack = new Stack<TreeNode>();
                TreeNode current = root;
                while (true)
                {
                    if (current != null) // not a leaf node, so search the left subtree
                    {
                        stack.Push(current);
                        current = current.left;
                    }
                    else // just visited a leaf node
                    {
                        if (stack.Count == 0)
                        {
                            break; // finished
                        }
                        else // pop node from stack
                        {
                            current = stack.Pop();
                            Console.Write(current.value + " ");
                            current = current.right;
                        }
                    }
                }
            }

        }


        public static void PostorderTraverse_iterative_only_stack(TreeNode root)
        {
            if (root != null)
            {
                // TODO
            }

        }


        public static void Test()
        {

            /* This tree is NOT balanced!
             *        1
             *       / \
             *      2   3
             *     / \ / 
             *    4  5 6 
             *      /
             *     7
             */
            TreeNode tree1 = new TreeNode(1, new TreeNode(2,
                new TreeNode(4, null, null), new TreeNode(5,
                new TreeNode(7, null, null), null)),
                new TreeNode(3, new TreeNode(6, null, null), null));


            /* This tree is NOT balanced!
             *        1
             *       / \
             *      2   3
             *     /   / \
             *    4   5   6
             *   /    /    \
             *  7    8      9
             */
            TreeNode tree2 = new TreeNode(1, new TreeNode(2,
                new TreeNode(4, new TreeNode(7, null, null), null), null),
                new TreeNode(3, new TreeNode(5, new TreeNode(8, null, null), null),
                    new TreeNode(6, null, new TreeNode(9, null, null))));

            // tree traversal: DFS vs. BFS, recursive vs. iterative
            Console.Write("Preorder recursive: ");
            PreorderTraverse(tree1);
            Console.WriteLine();
            Console.Write("Preorder iterative: ");
            PreorderTraverse_iterative(tree1);
            Console.WriteLine();
            Console.Write("Preorder iterative: ");
            PreorderTraverse_iterative_only_stack(tree1);
            Console.WriteLine();

            Console.Write("Inorder recursive: ");
            InorderTraverse(tree1);
            Console.WriteLine();
            Console.Write("Inorder iterative: ");
            InorderTraverse_iterative(tree1);
            Console.WriteLine();
            Console.Write("Inorder iterative: ");
            InorderTraverse_iterative_only_stack(tree1);
            Console.WriteLine();

            Console.Write("Postorder recursive: ");
            PostorderTraverse(tree1);
            Console.WriteLine();
            Console.Write("Postorder iterative: ");
            PostorderTraverse_iterative(tree1);
            Console.WriteLine();

            Console.Write("Traverse by level: ");
            LevelTraverse(tree1);
            Console.WriteLine();


        }


    }

    /// <summary>
    /// Threaded binary tree
    /// Inorder iterative traversal without using stacks
    /// </summary>
    public class MorrisTraversal
    {

        // inorder iterative traversal without using stacks, Morris Traverse (threaded binary tree)
        // O(N log N) time, O(1) space
        // http://www.geeksforgeeks.org/archives/6358
        /*
            1. Initialize current as root 
            2. While current is not NULL
               If current does not have left child
                  a) Make current as right child of the rightmost node in current's left subtree
                  b) Go to this left child, i.e., current = current->left
               Else
                  a) Print current’s data
                  b) Go to the right, i.e., current = current->right
         */
        public static void MorrisTraverse(TreeNode root)
        {
            TreeNode current = root;
            TreeNode prev = null;
            while (current != null)
            {
                if (current.left != null)
                {
                    prev = current.left;
                    while (prev.right != null) // find the prev node, takes O(log N) time
                    {
                        if (prev.right == current) break; // encounter thread, stop
                        prev = prev.right;
                    }

                    if (prev.right == null) // thread is not yet added
                    {
                        prev.right = current; // add thread
                        current = current.left; // search left subtree
                    }
                    else // thread is already there
                    {
                        prev.right = null; // remove thread
                        Console.Write(current.value + " ");
                        current = current.right; // search right subtree
                    }
                }
                else // left subtree is null, search right subtree
                {
                    Console.Write(current.value + " ");
                    current = current.right;
                }
            }
        }


        public static void Test()
        {
            /* This tree is NOT balanced!
             *        1
             *       / \
             *      2   3
             *     / \ / 
             *    4  5 6 
             *      /
             *     7
             */
            TreeNode tree1 = new TreeNode(1, new TreeNode(2,
                new TreeNode(4, null, null), new TreeNode(5,
                new TreeNode(7, null, null), null)),
                new TreeNode(3, new TreeNode(6, null, null), null));

            MorrisTraverse(tree1);
            Console.WriteLine();
        }

    }

    /// <summary>
    /// Question 4
    /// Print *all* the paths starts from root to a leaf, which the sum of the values of each
    /// node on the path is N
    /// </summary>
    public class RootToLeafPathWithSum
    {
        // recursion with an extra stack, O(N) time and space
        // similar to standard DFS
        public static void FindPathOfSum(TreeNode root, int N)
        {
            FindPathOfSum(root, N, new List<TreeNode>(), 0);
        }
        // 1. push a node onto the stack
        // 2. if the node is leave and sum is N, print the path
        // 3. do the same on the left child and right child, recursively
        // 4. when finished, pop the node
        private static void FindPathOfSum(TreeNode root, int N, List<TreeNode> path, int tempSum)
        {
            if (root == null) return; // base case
            // preorder traverse the tree
            path.Add(root);
            tempSum += root.value;
            if (root.left == null && root.right == null && tempSum == N)
            { // print stack
                TreeNode[] array = path.ToArray();
                for (int i = 0; i < array.Length; i++)
                    Console.Write(array[i].value + " ");
                Console.WriteLine();
            }
            // traverse the left child and right child            
            FindPathOfSum(root.left, N, path, tempSum);
            FindPathOfSum(root.right, N, path, tempSum);
            // when finished, pop this node
            tempSum -= root.value;
            path.RemoveAt(path.Count - 1);
        }


        // iterative version
        // two stacks: one stack for DFS traversal,
        //             another stack for current path
        public static void FindPathOfSum_iterative(TreeNode root, int N)
        {
            if (root == null) return;
            Stack<TreeNode> stack = new Stack<TreeNode>();
            Stack<TreeNode> path = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                TreeNode node = stack.Pop();
                N -= node.value;
                path.Push(node);
                if (node.left == null && node.right == null)
                {
                    if (N == 0) // we found a valid path, print it
                    {
                        TreeNode[] array = path.ToArray();
                        for (int i = array.Length - 1; i >= 0; i--)
                            Console.Write(array[i].value + " ");
                        Console.WriteLine();
                    }
                    do // very tricky: remove nodes from path when there are pop off
                    {
                        N += path.Pop().value;
                    } while (stack.Count > 0
                        && path.Peek().right != stack.Peek());
                }
                if (node.right != null)
                    stack.Push(node.right);
                if (node.left != null)
                    stack.Push(node.left);
            }
        }


        public static void Test()
        {
            /*
             *        1
             *       / \
             *      2   3
             *     / \ / \
             *    4  5 6  6
             *      / / 
             *     2 7
             */
            TreeNode root = new TreeNode(1, new TreeNode(2,
                new TreeNode(4), new TreeNode(5,
                new TreeNode(2), null)),
                new TreeNode(3, new TreeNode(6, new TreeNode(7), null), new TreeNode(6)));

            FindPathOfSum(root, 10);
            FindPathOfSum_iterative(root, 10);

        }

    }

    /// <summary>
    /// Leetcode
    /// Find the max sum of a path from root to a leaf
    /// </summary>
    public class RootToLeafPathMaxSum
    {
        private static int maxSum;
        public static int MaxPathSum_recursive(TreeNode root)
        {
            maxSum = int.MinValue;
            MaxPathSum_recursive(root, 0);
            return maxSum;
        }

        // recursive approach
        private static void MaxPathSum_recursive(TreeNode root, int sum)
        {
            if (root == null) return;
            sum += root.value;
            if (root.left == null && root.right == null)
            {
                if (sum > maxSum) maxSum = sum;
            }
            if (root.left != null) MaxPathSum_recursive(root.left, sum);
            if (root.right != null) MaxPathSum_recursive(root.right, sum);
        }

        // one stack for DFS, one stack for path
        public static int MaxPathSum(TreeNode root)
        {
            if (root == null) return 0;
            Stack<TreeNode> stack = new Stack<TreeNode>();
            Stack<TreeNode> path = new Stack<TreeNode>();
            int sum = 0;
            int max = int.MinValue;
            stack.Push(root);
            while (stack.Count > 0)
            {
                TreeNode node = stack.Pop();
                path.Push(node);
                sum += node.value;
                if (node.left == null && node.right == null)
                {
                    if (sum > max)
                        max = sum;
                    do
                    {
                        TreeNode p = path.Pop();
                        sum -= p.value;
                    } while (stack.Count > 0 && path.Peek().right != stack.Peek());
                }
                if (node.right != null) stack.Push(node.right);
                if (node.left != null) stack.Push(node.left);
            }
            return max;
        }


        public static void Test()
        {
            /*
             *        1
             *       / \
             *      2   2
             *     / \ / \
             *    6  5 5  9
             *      / / 
             *     2 3
             */
            TreeNode root = new TreeNode(1, new TreeNode(2,
                new TreeNode(6), new TreeNode(5,
                new TreeNode(2), null)),
                new TreeNode(2, new TreeNode(5, new TreeNode(3), null), new TreeNode(9)));

            Console.WriteLine(MaxPathSum(root)); // 12
            Console.WriteLine(MaxPathSum_recursive(root)); // 12   
        }
    }



    /// <summary>
    /// Question 6
    /// Check if an array is the postorder traversal of a BST
    /// </summary>
    public class PostorderTraversalArray
    {
        // this is an inefficient implementation, O(N) space, don't use this!!
        // root is the last element, divide the array into two subtree, recursively validate postorder property
        public static bool isPostorder(int[] a)
        {
            if (a.Length == 1) return true;
            int root = a[a.Length - 1];
            List<int> leftTree = new List<int>();
            List<int> rightTree = new List<int>();
            bool flag = false;
            for (int i = 0; i < a.Length - 1; i++)
            {
                if (a[i] < root && !flag) // left subtree must be < root
                {
                    leftTree.Add(a[i]);
                }
                else if (a[i] >= root) // right subtree must be >= root
                {
                    rightTree.Add(a[i]);
                    flag = true;
                }
                else return false;
            }
            bool isLeftPostorder, isRightPostorder;
            if (leftTree.Count > 0)
                isLeftPostorder = isPostorder(leftTree.ToArray());
            else
                isLeftPostorder = true; // left subtree is null
            if (rightTree.Count > 0)
                isRightPostorder = isPostorder(rightTree.ToArray());
            else
                isRightPostorder = true; // right subtree is null

            return isLeftPostorder && isRightPostorder;
        }

        // recursion, O(N) time and O(1) space
        // 1. a[high] is the root
        // 2. first element that is greater than root is the beginning of right subtree
        // 3. make sure all elements in right subtree is greater than root
        // 4. recursively check left subtree and right subtree
        private static bool isPost(int[] a, int low, int high)
        {
            if (low >= high) return true; // base case
            int p = low;
            while (p <= high)
            {
                if (a[p] < a[high]) p++;
                else break; // p is at the first of right subtree
            }
            int q = p;
            while (p < high)
            {
                if (a[p] > a[high]) p++;
                else return false; // right subtree is not satisfied
            }
            return isPost(a, low, q - 1) && isPost(a, q, high - 1);
        }
        public static bool isPost(int[] a)
        {
            return isPost(a, 0, a.Length - 1);
        }

        public static void Test()
        {
            /*
             *        4
             *       / \
             *      2   6
             *     / \ / \
             *    1  3 5  7
             */
            int[] a = new int[] { 1, 3, 2, 5, 7, 6, 4 };
            bool result = isPost(a);
            Console.Write("array: ");
            foreach (int i in a) Console.Write(i + " ");
            if (result)
                Console.WriteLine("is BST by postorder traversal");
            else
                Console.WriteLine("is not BST by postorder traversal");
            /*
             *        4
             *       / \
             *      2   6
             *     / \ / \
             *    1  3 7  5
             */
            int[] a2 = new int[] { 1, 3, 2, 7, 5, 6, 4 };
            result = isPost(a2);
            Console.Write("array: ");
            foreach (int i in a2) Console.Write(i + " ");
            if (result)
                Console.WriteLine("is BST by postorder traversal");
            else
                Console.WriteLine("is not BST by postorder traversal");

        }
    }

    /// <summary>
    /// Question 11
    /// Mirror the tree, recursively and iteratively
    /// </summary>
    public class Mirror
    {
        // 1. swap the left subtree and right subtree of the root
        // 2. recursively mirror the left subtree and right subtree
        public static TreeNode MirrorRecursive(TreeNode root)
        {
            if (root == null) return null;
            TreeNode temp = root.left;
            root.left = root.right;
            root.right = temp;
            root.left = MirrorRecursive(root.left);
            root.right = MirrorRecursive(root.right);
            return root;
        }

        // DFS
        // for every node, swap the left subtree and the right subtree
        public static TreeNode MirrorIterative(TreeNode root)
        {
            if (root == null) return null;
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                TreeNode node = stack.Pop();
                TreeNode temp = node.left;
                node.left = node.right;
                node.right = temp;
                if (node.left != null)
                    stack.Push(node.left);
                if (node.right != null)
                    stack.Push(node.right);
            }
            return root;
        }

        public static void Test()
        {
            /*
             *        4                 4
             *       / \               /  \
             *      2   6     ->      6    2   
             *     / \ / \           /\    /\
             *    1  3 5  7         7  5  3  1
             */
            TreeNode root = new TreeNode(4, new TreeNode(2,
                new TreeNode(1, null, null), new TreeNode(3, null, null)),
                new TreeNode(6, new TreeNode(5, null, null), new TreeNode(7, null, null)));
            Console.Write("original: ");
            TreeTraversal.LevelTraverse(root); // 4 2 6 1 3 5 7
            root = MirrorIterative(root);
            Console.Write("\nMirroring: "); // 4 6 2 7 5 3 1
            TreeTraversal.LevelTraverse(root);
            root = MirrorRecursive(root);
            Console.Write("\nMirroring twice: "); // 4 2 6 1 3 5 7
            TreeTraversal.LevelTraverse(root);
        }

    }

    /// <summary>
    /// Question 48
    /// Find the last common ancestor of two nodes in a given tree
    /// </summary>
    public class CommonAncestor
    {
        // traverse the tree by level (BFS), find the lowest level of common ancester
        // O(N^2) time
        public static TreeNode LastCommonAncestor(TreeNode root, TreeNode node1, TreeNode node2)
        {
            if (root == null || node1 == null || node2 == null) return null;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            TreeNode result = root;
            while (queue.Count > 0)
            {
                TreeNode node = queue.Dequeue();
                if (IsAncester(node, node1) && IsAncester(node, node2))
                    result = node;
                queue.Enqueue(node.left);
                queue.Enqueue(node.right);
            }
            return result;
        }
        // check if node1 is the ancestor of node2, O(N) time
        private static bool IsAncester(TreeNode node1, TreeNode node2)
        {
            if (node1 == null || node2 == null) return false;
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(node1);
            while (stack.Count > 0)
            {
                TreeNode node = stack.Pop();
                if (node.value == node2.value) return true;
                stack.Push(node.left);
                stack.Push(node.right);
            }
            return false;
        }

        public static void Test()
        {
            /*
             *        4
             *       / \
             *      2   6
             *     / \ / \
             *    1  3 7  5
             */
            TreeNode node1 = new TreeNode(1);
            TreeNode node3 = new TreeNode(3);
            TreeNode node7 = new TreeNode(7);
            TreeNode node5 = new TreeNode(5);
            TreeNode node2 = new TreeNode(2, node1, node3);
            TreeNode node6 = new TreeNode(6, node7, node5);
            TreeNode node4 = new TreeNode(4, node2, node6);

            TreeNode common1 = LastCommonAncestor(node4, node1, node5);
            Console.WriteLine("LastCommonAncestor is " + common1); // 4
            TreeNode common2 = LastCommonAncestor(node4, node1, node3);
            Console.WriteLine("LastCommonAncestor is " + common2); // 2
            TreeNode common3 = LastCommonAncestor(node4, node1, node6);
            Console.WriteLine("LastCommonAncestor is " + common3); // 4
            TreeNode common4 = LastCommonAncestor(node4, node1, node2);
            Console.WriteLine("LastCommonAncestor is " + common4); // 2
        }
    }

    /// <summary>
    /// Question 50
    /// Determine if one tree is the substructure of the other tree
    /// </summary>
    public class Substructure
    {

        public static bool IsSubstructure(TreeNode node1, TreeNode node2)
        {


            return false;
        }



    }


    public class BinarySearchTree
    {
        // iterative inorder traverse the tree, the array of traversal should be sorted if is BST
        // optimized for space
        // O(N) time, O(1) space 
        public static bool IsBST_Iterative(TreeNode root)
        {
            if (root == null) return false;
            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode current = root;
            int tempValue = int.MinValue;
            while (true)
            {
                if (current != null) // not a leaf, search the left subtree
                {
                    stack.Push(current);
                    current = current.left;
                }
                else
                {
                    if (stack.Count == 0) // finished traversal
                        break;
                    else
                    {
                        current = stack.Pop();
                        if (current.value < tempValue) return false;
                        else tempValue = current.value;
                        current = current.right;
                    }
                }
            }
            return true;
        }

        // O(N) time and O(N) space
        // recursive check BST property, narrowing the range of (min, max)
        public static bool IsBST_Recursive(TreeNode root)
        {
            return IsBST_Recursive(root, int.MinValue, int.MaxValue);
        }
        private static bool IsBST_Recursive(TreeNode root, int min, int max)
        {
            if (root == null) return true;
            // check the value of node is in range (min, max)
            if (root.value < min || root.value > max) return false;
            // check the left and right child
            return IsBST_Recursive(root.left, min, root.value - 1)
                && IsBST_Recursive(root.right, root.value, max);
        }

        public static void Test()
        {
            /*  binary tree
             *     10
             *    /  \
             *   8   12   
             *  /\   /\
             * 7  9 11 13 
             */
            TreeNode tree1 = new TreeNode(10, new TreeNode(8, new TreeNode(7, null, null),
                new TreeNode(9, null, null)), new TreeNode(12, new TreeNode(11, null, null), new TreeNode(13, null, null)));

            /*  not binary tree
             *     10
             *    /  \
             *   8   12
             *  /\   /\
             * 7  6 11 13 
             */
            TreeNode tree2 = new TreeNode(10, new TreeNode(8, new TreeNode(7, null, null),
             new TreeNode(6, null, null)), new TreeNode(12, new TreeNode(11, null, null), new TreeNode(13, null, null)));

            Debug.Assert(IsBST_Iterative(tree1));
            Debug.Assert(!IsBST_Iterative(tree2));
            Debug.Assert(IsBST_Recursive(tree1));
            Debug.Assert(!IsBST_Recursive(tree2));
        }

    }

    /// <summary>
    /// http://www.geeksforgeeks.org/archives/20174
    /// Convert a Binary tree to a BST without changing the structure of the Binary tree
    /// </summary>
    public class BinaryTreeToBST
    {
        // O(N) time and space
        // 1. in-order traverse, 
        // 2. sort the result, 
        // 3. reconstruct the BST by in-order traversal
        public static TreeNode BSTConversion(TreeNode root)
        {
            if (root == null) return null;
            List<int> inorder = new List<int>();
            inorder = InorderTraversal(root, inorder);
            inorder.Sort();
            index = 0;
            return buildBST(root, inorder);
        }
        // recursive inorder traversal
        private static List<int> InorderTraversal(TreeNode root, List<int> list)
        {
            if (root == null) return list;
            if (root.left != null)
            {
                list = InorderTraversal(root.left, list);
            }
            list.Add(root.value);
            if (root.right != null)
            {
                list = InorderTraversal(root.right, list);
            }
            return list;
        }
        // recursive inorderly modify each node of the tree
        private static int index;
        private static TreeNode buildBST(TreeNode root, List<int> inorder)
        {
            if (root == null) return root;
            if (root.left != null)
            {
                buildBST(root.left, inorder);
            }
            root.value = inorder[index++];
            if (root.right != null)
            {
                buildBST(root.right, inorder);
            }
            return root;
        }

        public static void Test()
        {
            /*
             *   10          8
             *   / \        / \
             *  2   7  =>  4   10
             *  /\        / \     
             * 8  4      2   7    
             * 
             */
            TreeNode tree = new TreeNode(10, new TreeNode(2, new TreeNode(8, null, null),
                new TreeNode(4, null, null)), new TreeNode(7, null, null));
            TreeNode tree2 = BSTConversion(tree);
            TreeTraversal.InorderTraverse(tree2);
            Console.WriteLine();

            /*
                      10               15  
                     /  \             / \
                    30   15    ->   10   20
                   /      \         /     \
                  20       5       5      30
             */
            tree = new TreeNode(10, new TreeNode(30, new TreeNode(20), null),
                                    new TreeNode(15, null, new TreeNode(5)));
            tree2 = BSTConversion(tree);
            TreeTraversal.InorderTraverse(tree2);
            Console.WriteLine();
        }

    }

    /// <summary>
    /// http://www.geeksforgeeks.org/archives/22502
    /// Given an array, Check if all the non-leaf node in the BST has only one child (looks like a list shape)
    /// </summary>
    public class SpecailBST
    {
        // O(N) time
        // root of BST should be either greater than both first sucessor and last sucessor
        // or smaller than those two
        public static bool IsListLikeBST_version1(int[] a)
        {
            if (a.Length <= 2) return true;
            for (int i = 0; i < a.Length - 1; i++)
            {
                int root = a[i];
                int firstSucessor = a[i + 1];
                int lastSucessor = a[a.Length - 1];
                if (root < firstSucessor != root < lastSucessor)
                    return false;
            }
            return true;
        }

        // most efficient, O(N) time, only go throught the list once 
        // 1. set the last two nodes as the min value and max value
        // 2. update the max/min value when iterate the array backwardly
        // 3. if a node has a value between (min, max), return false
        public static bool IsListLikeBST_version2(int[] a)
        {
            if (a.Length <= 2) return true;
            int min = a[a.Length - 1];
            int max = a[a.Length - 2];
            if (min > max) // make sure min is smaller than max
            {
                int temp = min;
                min = max;
                max = temp;
            }
            for (int i = a.Length - 3; i >= 0; i--)
            {
                if (a[i] < min)
                    min = a[i];
                else if (a[i] > max)
                    max = a[i];
                else return false;
            }
            return true;
        }

        public static void Test()
        {
            /*
             *    20
             *   /
             * 10
             *   \
             *   11
             *     \
             *     13
             *     /
             *   12
             */
            int[] a = { 20, 10, 11, 13, 12 };
            Debug.Assert(IsListLikeBST_version1(a));
            Debug.Assert(IsListLikeBST_version2(a));
            int[] a2 = { 20, 10, 11, 13, 100 };
            Debug.Assert(!IsListLikeBST_version1(a2));
            Debug.Assert(!IsListLikeBST_version2(a2));
        }

    }


    /// <summary>
    /// Merge two BST, print all the nodes inorder in the merged BST tree, O(M+N) time, O(lg M + lg N) space
    /// </summary>
    public class MergeBST
    {
        // TODO
        public static void MergeAndPrint(TreeNode bst1, TreeNode bst2)
        {

        }

        public static void Test()
        {
            /*
             *   3        4
             *  / \  +   / \  => print "1 2 3 4 5 6"
             * 1   5    2   6
             */
            TreeNode tree1 = new TreeNode(3, new TreeNode(1, null, null), new TreeNode(5, null, null));
            TreeNode tree2 = new TreeNode(4, new TreeNode(2, null, null), new TreeNode(6, null, null));
            MergeAndPrint(tree1, tree2);
            Console.WriteLine();
        }

    }

    /// <summary>
    /// Leetcode
    /// Print binary tree by level order with number of levels
    /// </summary>
    public class LevelOrderTraversal
    {

        // use a queue for current level nodes, another queue for next level nodes
        public static void PrintLevelOrder_by_two_queue(TreeNode root)
        {
            if (root == null) return;
            Queue<TreeNode> currentLevel = new Queue<TreeNode>();
            Queue<TreeNode> nextLevel = new Queue<TreeNode>();
            currentLevel.Enqueue(root);
            int level = 1;
            Console.Write("Level-" + level + ": ");
            while (currentLevel.Count > 0)
            {
                TreeNode node = currentLevel.Dequeue();
                Console.Write(node.value + " ");
                if (node.left != null)
                    nextLevel.Enqueue(node.left);
                if (node.right != null)
                    nextLevel.Enqueue(node.right);
                if (currentLevel.Count == 0)
                {
                    level++;
                    if (nextLevel.Count > 0)
                        Console.Write("\nLevel-" + level + ": ");
                    else
                        Console.WriteLine();
                    currentLevel = nextLevel;
                    nextLevel = new Queue<TreeNode>();
                }
            }
        }

        // a slightly variant version: use only one queue and a counter 
        // to keep track of number of nodes of current level
        public static void PrintLevelOrder_by_one_queue(TreeNode root)
        {
            if (root == null) return;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            int count = 1;
            int level = 1;
            Console.Write("Level-" + level + ": ");
            while (queue.Count > 0)
            {
                TreeNode node = queue.Dequeue();
                count--;
                Console.Write(node.value + " ");
                if (node.left != null)
                    queue.Enqueue(node.left);
                if (node.right != null)
                    queue.Enqueue(node.right);
                if (count == 0)
                {
                    level++;
                    if (queue.Count > 0)
                        Console.Write("\nLevel-" + level + ": ");
                    else
                        Console.WriteLine();
                    count = queue.Count; // update counter
                }
            }
        }

        // a variant: bottom-up level order traversal 
        public static void PrintLevelOrder_BottomUp(TreeNode root)
        {
            if (root == null) return;
            List<List<TreeNode>> result = new List<List<TreeNode>>();
            Queue<TreeNode> currentLevel = new Queue<TreeNode>();
            Queue<TreeNode> nextLevel = new Queue<TreeNode>();
            List<TreeNode> list = new List<TreeNode>();
            currentLevel.Enqueue(root);
            while (currentLevel.Count > 0)
            {
                TreeNode node = currentLevel.Dequeue();
                list.Add(node);
                if (node.left != null)
                    nextLevel.Enqueue(node.left);
                if (node.right != null)
                    nextLevel.Enqueue(node.right);
                if (currentLevel.Count == 0)
                {
                    result.Add(list);
                    list = new List<TreeNode>();
                    currentLevel = nextLevel;
                    nextLevel = new Queue<TreeNode>();
                }
            }
            for (int i = result.Count - 1; i >= 0; i--) // reversely print the List<List<TreeNode>>
            {
                List<TreeNode> li = result[i];
                Console.Write("Level-" + (i + 1) + ": ");
                for (int j = 0; j < li.Count; j++)
                {
                    Console.Write(li[j].value + " ");
                }
                Console.WriteLine();
            }
        }

        public static void Test()
        {
            /* 
            *        1
            *       / \
            *      2   3
            *     / \ / 
            *    4  5 6 
            *      /
            *     7
            */
            TreeNode tree1 = new TreeNode(1, new TreeNode(2,
                new TreeNode(4, null, null), new TreeNode(5,
                new TreeNode(7, null, null), null)),
                new TreeNode(3, new TreeNode(6, null, null), null));
            /*
             * Level-1: 1
             * Level-2: 2 3
             * Level-3: 4 5 6
             * Level-4: 7
             */
            PrintLevelOrder_by_two_queue(tree1);
            Console.WriteLine();
            PrintLevelOrder_by_one_queue(tree1);
            Console.WriteLine();

            /*
             * Level-4: 7
             * Level-3: 4 5 6
             * Level-2: 2 3
             * Level-1: 7
             */
            PrintLevelOrder_BottomUp(tree1);
            Console.WriteLine();
        }

    }



    /// <summary>
    /// Leetcode
    /// Given a binary tree, print out the tree in zig zag level order (ie, from left to right, 
    /// then right to left for the next level and alternate between). 
    /// Output a newline after the end of each level.
    /// </summary>
    public class ZigZagLevelOrderTraversal
    {
        // one stack store nodes of current level, another stack store nodes of next level, 
        // and a bool indicates order of printing
        // (1)(i) when traverse from left to right on the current level, push left child prior than right child
        //     onto next level will makes traverse from right to left on the next level
        //    (ii) vice visa for traverse from right to left on the current level
        // (2) when the current level stack is empty, swap the current level and next level
        public static void ZigZagTraversal(TreeNode root)
        {
            if (root == null) return;
            Stack<TreeNode> currentLevel = new Stack<TreeNode>();
            Stack<TreeNode> nextLevel = new Stack<TreeNode>();
            bool leftToRight = true;
            currentLevel.Push(root);
            while (currentLevel.Count > 0)
            {
                TreeNode node = currentLevel.Pop();
                Console.Write(node.value + " ");
                if (leftToRight)
                {
                    if (node.left != null) nextLevel.Push(node.left);
                    if (node.right != null) nextLevel.Push(node.right);
                }
                else
                {
                    if (node.right != null) nextLevel.Push(node.right);
                    if (node.left != null) nextLevel.Push(node.left);
                }
                if (currentLevel.Count == 0)
                { // current level is finished, update a few variables
                    Console.WriteLine();
                    leftToRight = !leftToRight;
                    currentLevel = nextLevel;
                    nextLevel = new Stack<TreeNode>();
                }
            }
        }

        public static void Test()
        {
            /* 
             *        1
             *       / \
             *      2   3
             *     / \ / 
             *    4  5 6 
             *      /
             *     7
             */
            TreeNode tree1 = new TreeNode(1, new TreeNode(2,
                new TreeNode(4, null, null), new TreeNode(5,
                new TreeNode(7, null, null), null)),
                new TreeNode(3, new TreeNode(6, null, null), null));
            /*
             * 1
             * 3 2
             * 4 5 6
             * 7
             */
            ZigZagTraversal(tree1);

        }
    }

    /// <summary>
    /// Construct a binary tree from its preorder and inorder traversal
    /// </summary>
    public class ConstructTreeFromPreorderInorder
    {

        public static TreeNode BuildFromPreorderInorder(int[] preorder, int[] inorder)
        {
            return BuildFromPreorderInorder(preorder, inorder, 0, preorder.Length - 1, 0);
        }

        private static TreeNode BuildFromPreorderInorder(int[] preorder, int[] inorder,
            int inLeft, int inRight, int preIndex)
        {
            if (inLeft > inRight) return null;
            TreeNode node = new TreeNode(preorder[preIndex]);
            if (inLeft == inRight) return node;
            int inIndex = GetInorderIndex_by_hashmap(inorder, node.value);
            node.left = BuildFromPreorderInorder(preorder, inorder, inLeft, inIndex - 1,
                preIndex + 1);
            node.right = BuildFromPreorderInorder(preorder, inorder, inIndex + 1, inRight,
                preIndex + (inIndex - inLeft));
            return node;
        }
        private static Dictionary<int, int> inorderMap = null;
        private static int GetInorderIndex_by_hashmap(int[] inorder, int value)
        {
            if (inorderMap == null)
            {
                inorderMap = new Dictionary<int, int>();
                for (int i = 0; i < inorder.Length; i++)
                {
                    inorderMap[inorder[i]] = i;
                }
            }
            return inorderMap[value];
        }


        public static TreeNode BuildFromPreorderInorder_2(int[] preorder, int[] inorder)
        {
            return BuildFromPreorderInorder_2(preorder, inorder, 0, preorder.Length - 1, 0);
        }
        private static TreeNode BuildFromPreorderInorder_2(int[] preorder, int[] inorder,
            int inLeft, int inRight, int preIndex)
        {
            if (inLeft > inRight) return null;
            TreeNode node = new TreeNode(preorder[preIndex]);
            if (inLeft == inRight) return node;
            int inIndex = GetInorderIndex_by_search(inorder, node.value);
            node.left = BuildFromPreorderInorder(preorder, inorder, inLeft, inIndex - 1,
                preIndex + 1);
            node.right = BuildFromPreorderInorder(preorder, inorder, inIndex + 1, inRight,
                preIndex + (inIndex - inLeft));
            return node;
        }
        private static int GetInorderIndex_by_search(int[] inorder, int value)
        {
            for (int i = 0; i < inorder.Length; i++)
            {
                if (inorder[i] == value) return i;
            }
            return -1; // error
        }

        public static void Test()
        {
            /* 
            *        1
            *       / \
            *      2   3
            *     / \   \ 
            *    4  5    6 
            *      /      \
            *     7        8
            */
            //int[] inorder = { 4, 2, 7, 5, 1, 3, 6, 8 };
            //int[] preorder = { 1, 2, 4, 5, 7, 3, 6, 8 };
            int[] inorder = { 3, 1, 6, 5, 0, 2, 5, 7 };
            int[] preorder = { 0, 1, 3, 4, 6, 2, 5, 7 };
            TreeNode tree1 = DeserializePreInorder(preorder, inorder);
            LevelOrderTraversal.PrintLevelOrder_by_one_queue(tree1);
            TreeNode tree2 = DeserializePreInorder(preorder, inorder);
            LevelOrderTraversal.PrintLevelOrder_by_one_queue(tree2);
        }


        public static TreeNode DeserializePreInorder(int[] preorder, int[] inorder)
        {
            int[] mapping = new int[inorder.Length];
            for (int i = 0; i < inorder.Length; i++)
            {
                mapping[inorder[i]] = i;
            }
            int start = 0;
            return DeserializePreInorder(preorder, inorder, mapping, 0, inorder.Length, ref start);
        }

        // 1. (low, high) is the index range of the current root,
        //    root.leftChild is (low, dividerIndex) and root.rightChild is (dividerIndex + 1, high)
        // 2. readPointer is the current root pointer in inorder mapping
        public static TreeNode DeserializePreInorder(int[] preorder, int[] inorder,
            int[] mapping, int low, int high, ref int readPointer)
        {
            if (low == high)
            {
                return null;
            }
            TreeNode root = new TreeNode(preorder[readPointer++]);
            int dividerIndex = mapping[root.value];
            root.left = DeserializePreInorder(preorder, inorder, mapping, low, dividerIndex, ref readPointer);
            root.right = DeserializePreInorder(preorder, inorder, mapping, dividerIndex + 1, high, ref readPointer);
            return root;
        }
    }

    /// <summary>
    /// Construct a binary tree from its postorder and inorder traversal
    /// </summary>
    public class ConstructTreeFromInorderPostorder
    {



        public static void Test()
        {

        }

    }






}
