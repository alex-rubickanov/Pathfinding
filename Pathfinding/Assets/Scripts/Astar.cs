using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    [SerializeField] private Vector2Int startPos;
    [SerializeField] private Vector2Int goalPos;

    private Grid grid;

    private Node startNode;
    private Node goalNode;
    private Node currentNode;

    private int currentDistance;
    
    private List<Node> path;
    private Node[] neighbours;

    private List<Node> openList;
    private List<Node> closeList;


    private int CalculateDistance(Node startNode, Node goalNode)
    {
        return Mathf.Abs(startNode.x - goalNode.x) + Mathf.Abs(startNode.y - goalNode.y);
    }

    private Node[] GetNeighbours(Node node)
    {
        Node[] neighbours = new Node[4];

        Node topNode = grid.GetNode(new Vector2Int(node.x, node.y + 1));
        Node rightNode = grid.GetNode(new Vector2Int(node.x + 1, node.y));
        Node botNode = grid.GetNode(new Vector2Int(node.x, node.y - 1));
        Node leftNode = grid.GetNode(new Vector2Int(node.x - 1, node.y));

        neighbours[0] = topNode;
        neighbours[1] = rightNode;
        neighbours[2] = botNode;
        neighbours[3] = leftNode;

        return neighbours;
    }

    private void Awake()
    {
        grid = GetComponent<Grid>();

        path = new List<Node>();
        openList = new List<Node>();
        closeList = new List<Node>();
    }

    private void Start()
    {
        List<Node> finalPath;
        finalPath = FindPath(startPos, goalPos);

       for(int i = 0; i < finalPath.Count; i++)
        {
            finalPath[i].GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }

    public List<Node> FindPath(Vector2Int startPos, Vector2Int goalPos)
    {
        startNode = grid.GetNode(startPos);
        goalNode = grid.GetNode(goalPos);

        currentNode = startNode;
        path.Add(currentNode);
        currentDistance = CalculateDistance(startNode, goalNode);

        while(currentNode != goalNode)
        {
            neighbours = GetNeighbours(currentNode);
            foreach(Node node in neighbours)
            {
                if(node != null)
                {
                    if(!closeList.Contains(node))
                    {
                        int newDistance = CalculateDistance(node, goalNode);
                        if(newDistance < currentDistance || !openList.Contains(node))
                        {
                            node.SetupVariables(startNode, goalNode);
                            node.SetParent(currentNode);
                            currentDistance = newDistance;
                            path.Add(node);
                        }
                    }
                }
            }
        }
        return path;
    }
}

// pick a node
// get neighbours
// calculate f cost
// pick node with the lowest f cost 
// get neighbours 
// calculate f cost