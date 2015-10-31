using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringAlgo
{

    /// <summary>
    /// given string pattern (of length M) to match string txt (of length N), return the matched index, 
    /// or return -1 if not matched
    /// 
    /// (1) brute force: nested loops, exhaustively search, O(MN) time and O(1) space
    /// 
    /// (2) boyer-moore: search from right towards left, build skip table 
    ///                 best-case O(N/M) time, worse case O(MN) time, improved version of brute force           
    ///                 
    /// (3) rabin-karp: 
    /// 
    /// (4) KMP: 
    /// </summary>
    public class SubstringSearching
    {
        // Solution 1a
        // brute-force way, O(MN) time
        // nested for loop, outter loop O(N) and inner loop O(M)
        public static int BruteForceSearch(string pat, string txt)
        {
            if (string.IsNullOrEmpty(pat) || string.IsNullOrEmpty(txt)
                || txt.Length < pat.Length) return -1; // not found
            for (int i = 0; i <= txt.Length - pat.Length; i++)
            {
                int j;
                for (j = 0; j < pat.Length; j++)
                {
                    if (txt[i + j] != pat[j]) break;
                }
                if (j == pat.Length) return i; // found it!
            }
            return -1; // not found
        }

        // Solution 1b
        // alternative implementation of brute-force, O(MN) time
        public static int ImprovedBruteForceSearch(string pat, string txt)
        {
            if (string.IsNullOrEmpty(pat) || string.IsNullOrEmpty(txt)
                || txt.Length < pat.Length) return -1; // not found
            int i, j;
            for (i = 0, j = 0; i < txt.Length && j < pat.Length; i++)
            {
                if (txt[i] == pat[j]) j++;
                else
                {
                    i -= j; j = 0;
                }
            }
            if (j == pat.Length) return i - pat.Length; // found it!
            else return -1; // not found
        }

        // Solution 2
        // preprocessing step, build skip table, O(M) time and O(M) space
        // skip table holds the rightmost occurrence of the every char 
        private static int[] BoyerMooreSkipTable(string pat)
        {
            // rightmost occurrence array
            int[] right = new int[256]; // assuming ASCII char
            // initialize the array into -1
            for (int i = 0; i < 256; i++) right[i] = -1;
            // rightmost occurrence array, right[x] how far to skip for x
            for (int j = 0; j < pat.Length; j++) right[pat[j]] = j;
            return right;
        }

        // O(MN) time worstcase
        // for i, every iteration i += skip instead of i++
        public static int BoyerMooreSearch(string pat, string txt)
        {
            int[] right = BoyerMooreSkipTable(pat);
            int skip;
            for (int i = 0; i <= txt.Length - pat.Length; i += skip)
            { // does pattern match the text at position i
                skip = 0;
                for (int j = pat.Length - 1; j >= 0; j--) // backward
                {
                    if (pat[j] != txt[i + j])
                    {
                        skip = j - right[txt[i + j]];
                        if (skip < 1) skip = 1; // skip at least is 1
                        break;
                    }
                }
                if (skip == 0) return i; // found it
            }
            return -1; // not found
        }

        private static void RabinKarp(string pat)
        {

        }

        public static int RabinKarpSearch(string pat, string txt)
        {


            return 0;
        }


        // KMP DFA, O(M) time
        private static int[] KMPDFA(string pat)
        {
            int p = 0; // pointer for pattern matching 
            int[] dfa = new int[pat.Length];
            for (int i = 1; i < pat.Length; i++)
            {
                if (pat[i] == pat[p])
                { // matched, set dfa[i] to be dfa[p], increment p by 1
                    dfa[i] = dfa[p];
                    p++;
                }
                else
                { // not matched, set dfa[i] be p+1, p back to dfa[p]
                    dfa[i] = p + 1;
                    p = dfa[p];
                }
            }
            return dfa;
        }

        // O(N)
        public static int KMPSearch(string pat, string txt)
        {
            int[] dfa = KMPDFA(pat);
            int j = 0; // pointer on pat
            for (int i = 0; i < txt.Length; i++)
            {
                if (txt[i] == pat[j]) // if matched, incremented the pointer
                    j++;
                else // otherwise, look up in the dfa array
                    j = dfa[j];
                if (j == pat.Length) // check if it found
                    return i - pat.Length + 1; // found it!
            }
            return -1; // not found
        }

        public static void BruteForceSearchTest()
        {
            string pat = "ABRA";
            string txt = "ABACADABRAC";
            Console.WriteLine(SubstringSearching.BruteForceSearch(pat, txt));
        }

        public static void ImprovedBruteForceSearchTest()
        {
            string pat = "ABRA";
            string txt = "ABACADABRAC";
            Console.WriteLine(SubstringSearching.ImprovedBruteForceSearch(pat, txt));
        }

        public static void BoyerMooreSearchTest()
        {
            string pat = "ABRA";
            string txt = "ABACADABRAC";
            Console.WriteLine(SubstringSearching.BoyerMooreSearch(pat, txt));
        }

        public static void RabinKarpTest()
        {
            string pat = "ABRA";
            string txt = "ABACADABRAC";
            Console.WriteLine(SubstringSearching.RabinKarpSearch(pat, txt));
        }

        public static void KMPTest()
        {
            string pat = "ABRA";
            string txt = "ABACADABRAC";
            Console.WriteLine(SubstringSearching.KMPSearch(pat, txt));
        }
    }

    /// <summary>
    /// reverse the order of the words in a given paragraph
    // eg: "I enjoy coding in C#" -> "C# in coding enjoy I"
    /// </summary>
    public class ReverseWords
    {

        // solution: first reverse the whole paragraph, then reverse each word
        public static string ReverseWordsInParagraph(string s)
        {
            if (s == null) return s;
            char[] chs = s.ToArray();
            Array.Reverse(chs); // reverse string in C# is verbose
            s = new string(chs);
            string[] rev = s.Split(' ');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < rev.Length; i++)
            {
                chs = rev[i].ToArray();
                Array.Reverse(chs);
                sb.Append(new string(chs));
                if (i < rev.Length - 1) sb.Append(" ");
            }
            return sb.ToString();
        }

        public static void Test()
        {
            string s = "I enjoy coding in C#";
            Console.WriteLine(s);
            Console.WriteLine(ReverseWords.ReverseWordsInParagraph(s));
        }

    }


    /// <summary>
    /// Implementation of Trie
    /// </summary>
    public class Trie
    {

        public class TrieNode
        {
            public int value;
            public Dictionary<int, TrieNode> children; // character as the key, TrieNode as the value
            public bool isTerminal;
            public TrieNode(int value)
            {
                this.value = value;
                this.children = new Dictionary<int, TrieNode>();
                this.isTerminal = false;
            }
        }

        // O(Length of string) time
        public static TrieNode Add(string word, TrieNode root)
        {
            if (string.IsNullOrEmpty(word)) return root;
            TrieNode currentNode = root;
            for (int i = 0; i < word.Length; i++)
            {
                if (currentNode.children.ContainsKey(word[i])) // word[i] already exists             
                {
                    currentNode = currentNode.children[word[i]];
                    if (i == word.Length - 1)
                    {
                        currentNode.isTerminal = true;
                    }
                }
                else
                { // word[i] does not exists
                    TrieNode newNode = new TrieNode(word[i]);
                    if (i == word.Length - 1)
                    {
                        newNode.isTerminal = true;
                    }
                    currentNode.children.Add(word[i], newNode);
                    currentNode = newNode;
                }
            }
            return root;
        }

        // O(depth of trie) time
        public static bool Search(string word, TrieNode root)
        {
            if (string.IsNullOrEmpty(word)) return false;
            TrieNode currentNode = root;
            for (int i = 0; i < word.Length; i++)
            {
                if (!currentNode.children.ContainsKey(word[i]))
                    return false;
                currentNode = currentNode.children[word[i]];
                if (i == word.Length - 1 && !currentNode.isTerminal)
                    return false;
            }
            return true;
        }


        public static void Test()
        {
            TrieNode root = new TrieNode(0); // trie root doesn't have any value
            string[] ss = { "the", "there", "their", "bye", "by" };
            string[] sss = { "b", "them", "theirs", "thei" };
            for (int i = 0; i < ss.Length; i++)
                root = Add(ss[i], root);
            for (int i = 0; i < ss.Length; i++)
                Console.WriteLine("Search(\"{0}\") = {1}", ss[i], Search(ss[i], root));
            for (int i = 0; i < sss.Length; i++)
                Console.WriteLine("Search(\"{0}\") = {1}", sss[i], Search(sss[i], root));
        }

    }



}
