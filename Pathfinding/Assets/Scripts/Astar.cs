using System;
using UnityEngine;

public class Astar : MonoBehaviour
{
    private Node startNode;
    private Node goalNode;

    private Node currentNode;
    
    private Node[] path;
    private Node[] neighbours;
}
