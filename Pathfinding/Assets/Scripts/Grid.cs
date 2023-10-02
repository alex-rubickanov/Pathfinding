using System;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int height;
    [SerializeField] private int width;

    [SerializeField] private GameObject nodePrefab;

    private void Start()
    {
        GenerateGrid(height, width);
    }

    private void GenerateGrid(int width, int height)
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Node node = Instantiate(nodePrefab, new Vector3(y, 0, x), Quaternion.identity, transform).GetComponent<Node>();
                node.SetCoordinates(x, y);
            }
        }
    }
}
