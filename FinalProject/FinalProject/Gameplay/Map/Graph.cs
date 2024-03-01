namespace INFR2100U.Graph;

public class Graph<T>
{
    // Dictionary is have a Key as the Node and the List will hold the list of adjacent nodes.
    private Dictionary<T, List<T>> adjacencyList;

    public Graph()
    {
        adjacencyList = new Dictionary<T, List<T>>();
    }

    #region GRAPH_FUNCTIONS
    /// <summary>
    /// Addes a Node as a Key to the Dictionary which a List of adjacent nodes can be assigned
    /// </summary>
    /// <param name="node">Node of type T that will be added to the Graph</param>
    public void AddNode(T node)
    {
        adjacencyList[node] = new List<T>();
    }

    /// <summary>
    /// Adds the connection between two Nodes. This creates a bi-directional connection.
    /// </summary>
    /// <param name="startNode">The first node that the edge or connection will be drawn from.</param>
    /// <param name="endNode">The node that the edge or connection will end on.</param>
    public void AddEdge(T startNode, T endNode)
    {
        adjacencyList[startNode].Add(endNode);
        adjacencyList[endNode].Add(startNode);
    }

    /// <summary>
    /// Get the list of neighboring nodes or the list of connected nodes.
    /// </summary>
    /// <param name="Node"></param>
    /// <returns></returns>
    public List<T> GetNeighbors(T Node)
    {
        return adjacencyList[Node];
    }

    /// <summary>
    /// Returns the first node in the Graph
    /// </summary>
    public T FirstNode { get { return adjacencyList.First().Key; } }

    public Dictionary<T, List<T>>.KeyCollection GetKey { get { return adjacencyList.Keys; } }
    public Dictionary<T, List<T>>.ValueCollection GetValue { get { return adjacencyList.Values; } }
    #endregion

    #region HELPERS
    /// <summary>
    /// Add support for ForEach statement.
    /// </summary>
    /// <returns></returns>
    public IEnumerator<T> GetEnumerator()
    {
        foreach (T Node in adjacencyList.Keys)
        {
            yield return Node;
        }
    }

    /// <summary>
    /// Get the value associated with the specified Key.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    public List<T> this[T node]
    {
        get
        {
            if (adjacencyList.ContainsKey(node))
            {
                return adjacencyList[node];
            }
            else
            {
                return new List<T>();
            }
        }
    }
    #endregion

    #region TEST_&_DELETE
    // EXISTS ONLY TO CHECK OUT WHAT NEW FUNCTIONS NEEDS TO BE ADDED.
    private void Test()
    {

    }
    #endregion
}