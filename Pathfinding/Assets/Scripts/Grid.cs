using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private Vector2Int getNodeTest;
    
    [SerializeField] private GameObject nodePrefab;

    [SerializeField] private List<Node> nodes;

    [SerializeField] private Vector2Int startPos;
    [SerializeField] private Vector2Int goalPos;
    

    private void Start()
    {
        nodes = new List<Node>();
        GenerateGrid();

        GetNode(startPos).GetComponent<MeshRenderer>().material.color = Color.green;
        GetNode(goalPos).GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void GenerateGrid()
    {
        for(int y = 0; y < width; y++)
        {
            for(int x = 0; x < height; x++)
            {
                Node node = Instantiate(nodePrefab, new Vector3(x, 0, y), Quaternion.identity, transform).GetComponent<Node>();
                
                nodes.Add(node);
                
                node.SetCoordinates(x, y);
                node.SetupVariables(startPos, goalPos);
            }
        }
    }

    
    
    public Node GetNode(Vector2Int gridPosition)
    {
        try
        {
            int index = gridPosition.x + gridPosition.y * width;

            return nodes[index];
        }
        catch (Exception e)
        {
            Debug.LogError("WRITE A VECTOR WITH X LESS THAN WIDTH AND Y LESS THAN HEIGHT!");
            Debug.LogError("AND BOTH OF THEM SUPPOSED TO BE GREATER THAN 0 YOU STUPIDO");
            return null;
        }
    }
    
}
