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


    private void Start()
    {
        nodes = new List<Node>();
        GenerateGrid();
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
            }
        }
    }



    public Node GetNode(Vector2Int gridPosition)
    {
        int index = gridPosition.x + gridPosition.y * width;
        return nodes[index];
    }
}
