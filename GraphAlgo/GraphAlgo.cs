using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphAlgo
{

    /// <summary>
    /// Graph data structure
    /// </summary>
    public class Graph
    {
        // public fields
        public int V { get; set; }
        public List<int>[] adj { get; set; } // adjacency list

        // public constructors
        public Graph(int V)
        {
            this.V = V;
            adj = new List<int>[V];
            for (int i = 0; i < V; i++)
            {
                adj[i] = new List<int>();
            }
        }

        // public methods
        public void addEdge(int v, int w)
        {
            adj[v].Add(w);
        }
    }

    /// <summary>
    /// Graph traversal
    /// </summary>
    public class GraphTraversal
    {
        // single source DFS search
        public static void DFS(Graph g, int v)
        {
            DFS(g, v, new bool[g.V]);
        }

        private static void DFS(Graph g, int v, bool[] visited)
        {
            visited[v] = true;
            Console.Write(v + " ");
            foreach (int node in g.adj[v])
            {
                if (!visited[node])
                    DFS(g, node, visited);
            }
        }


        public static void Test()
        {

            Graph g1 = new Graph(4);
            g1.addEdge(0, 1);
            g1.addEdge(0, 2);
            g1.addEdge(1, 2);
            g1.addEdge(2, 0);
            g1.addEdge(2, 3);
            g1.addEdge(3, 3);
            DFS(g1, 2); // 2 -> 0 -> 1 -> 3
        }
    }

    /// <summary>
    /// Grapha Node 
    /// </summary>
    public class GraphNode
    {
        public int value { get; set; }
        public List<GraphNode> neighbors { get; set; }
        public GraphNode() { }
        public GraphNode(int value)
        {
            this.value = value;
            this.neighbors = new List<GraphNode>();
        }
        public void DFS()
        {
            HashSet<GraphNode> visited = new HashSet<GraphNode>();
            Stack<GraphNode> stack = new Stack<GraphNode>();
            stack.Push(this);
            while (stack.Count > 0)
            {
                GraphNode node = stack.Pop();
                Console.Write(node.value + " ");
                foreach (GraphNode neighbor in node.neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        stack.Push(neighbor);
                        visited.Add(neighbor);
                    }
                }
            }
            Console.WriteLine();
        }

    }

    /// <summary>
    /// Clone a graph. Input is a Node pointer. Return the Node pointer of the cloned graph.
    /// </summary>
    public class CloneGraph
    {

        public static GraphNode Clone(GraphNode graph)
        {
            if (graph == null) return null;
            Dictionary<GraphNode, GraphNode> visited = new Dictionary<GraphNode, GraphNode>();
            Queue<GraphNode> queue = new Queue<GraphNode>();
            queue.Enqueue(graph);
            GraphNode copy = new GraphNode(graph.value);
            visited[graph] = copy;
            while (queue.Count > 0)
            {
                GraphNode node = queue.Dequeue();
                foreach (GraphNode neighbor in node.neighbors)
                {
                    if (visited.ContainsKey(neighbor)) // not exisited
                    {
                        GraphNode n = new GraphNode();
                        visited[node].neighbors.Add(n);
                        visited[neighbor] = n;
                        queue.Enqueue(neighbor);
                    }
                    else
                    {
                        visited[node].neighbors.Add(visited[neighbor]);
                    }
                }
            }
            return copy;
        }


        public static void Test()
        {
            GraphNode node1 = new GraphNode(1);
            GraphNode node2 = new GraphNode(2);
            GraphNode node3 = new GraphNode(3);
            GraphNode node4 = new GraphNode(4);
            GraphNode node5 = new GraphNode(5);
            node1.neighbors.Add(node2);
            node1.neighbors.Add(node3);
            node2.neighbors.Add(node4);
            node2.neighbors.Add(node5);
            node3.neighbors.Add(node5);
            node3.neighbors.Add(node4);
            node1.DFS();
            GraphNode copy = Clone(node1);
            copy.DFS();
        }

    }

}
