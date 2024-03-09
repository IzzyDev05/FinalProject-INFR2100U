namespace INFR2100U.Graph;
using INFR2100U.Country;
using INFR2100U.Continent;

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
    /// Adds new item to Graph.
    /// </summary>
    /// <param name="node">The key which will be a node in the graph.</param>
    /// <param name="adjacency">The value which will be a list of adjancent nodes.</param>
    public void Add(T node, List<T> adjacency)
    {
        adjacencyList.Add(node, adjacency);
    }

    /// <summary>
    /// Removes item from Graph.
    /// </summary>
    /// <param name="node">Node to remove.</param>
    public void Remove(T node)
    {
        adjacencyList.Remove(node);
    }

    /// <summary>
    /// Addes a Node as a Key to the Dictionary which a List of adjacent nodes can be assigned
    /// </summary>
    /// <param name="node">Node of type T that will be added to the Graph</param>
    public void AddNode(T node)
    {
        if (adjacencyList.ContainsKey(node) == false)
        {
            adjacencyList[node] = new List<T>();
        } 
    }

    /// <summary>
    /// Adds the connection between two Nodes. This creates a bi-directional connection.
    /// </summary>
    /// <param name="startNode">The first node that the edge or connection will be drawn from.</param>
    /// <param name="endNode">The node that the edge or connection will end on.</param>
    public void AddEdge(T startNode, T endNode)
    {
        if (adjacencyList.ContainsKey(startNode) == false)
        {
            adjacencyList[startNode].Add(endNode);
        }
        
        if (adjacencyList.ContainsKey(endNode) == false) 
        {
            adjacencyList[endNode].Add(startNode);
        }
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
    /// <exception cref="KeyNotFoundException"></exception>
    public List<T> this[T node]
    {
        get
        {
            if (adjacencyList.ContainsKey(node))
            {
                return adjacencyList[node];
            }

            throw new KeyNotFoundException();

        }

        set
        {
            adjacencyList[node] = value;
        }
    }

    /// <summary>
    /// Get the value associated with the specified Key.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public List<T> this[string name] 
    {
        get
        {
            foreach (T node in adjacencyList.Keys)
            {
                if (node is Country country && country.countryName == name || node is Continent continent && continent.continentName == name)
                {
                    return adjacencyList[node];
                }
            }

            throw new KeyNotFoundException();
        }

        set 
        {
            foreach (T node in adjacencyList.Keys)
            {
                if (node is Country country && country.countryName == name || node is Continent continent && continent.continentName == name)
                {
                    adjacencyList[node] = value;
                }
            }

            throw new KeyNotFoundException();
        }
    }

    /// <summary>
    /// Returns the first node in the Graph
    /// </summary>
    public T FirstNode { get { return adjacencyList.First().Key; } }

    public T FindNode(string name)
    {
        foreach(T node in adjacencyList.Keys)
        {
            if (node is Country country && country.countryName == name || node is Continent continent && continent.continentName == name)
            {
                return node;
            }
        }

        throw new KeyNotFoundException();
    }

    public Dictionary<T, List<T>>.KeyCollection GetAllKeys { get { return adjacencyList.Keys; } }
    public Dictionary<T, List<T>>.ValueCollection GetAllValues { get { return adjacencyList.Values; } }
    #endregion


    #region TEST_&_DELETE
    // EXISTS ONLY TO CHECK OUT WHAT NEW FUNCTIONS NEEDS TO BE ADDED.
    private void Test()
    {
        
    }
    #endregion
}