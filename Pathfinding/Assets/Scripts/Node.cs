using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int x, y;
    private Node parent;

    [SerializeField] private TextMeshProUGUI coordinates;
    
    [SerializeField] private TextMeshProUGUI fCost;
    [SerializeField] private TextMeshProUGUI gCost;
    [SerializeField] private TextMeshProUGUI hCost;
    
    private int f; // sum of g and h
    
    private int g; // to start point
    private int h; // to goal point

    private void Start()
    {
        coordinates.text = $"{x.ToString()}, {y.ToString()}";
    }


    public void SetCoordinates(int newX, int newY)
    {
        x = newX;
        y = newY;
    }

    public void SetupVariables(Node startNode, Node goalNode)
    {
        int g = (Mathf.Abs(startNode.x - x) + Mathf.Abs(startNode.y - y)) * 10;
        gCost.text = g.ToString();
        int h = (Mathf.Abs(goalNode.x - x) + Mathf.Abs(goalNode.y - y)) * 10;
        hCost.text = h.ToString();
        int f = g + h;
        fCost.text = f.ToString();
    }

    public void SetParent(Node node)
    {
        parent = node;
    }
}
