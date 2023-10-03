using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
    private int x, y;
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

    public void SetupVariables(Vector2Int startNodePos, Vector2Int finishNodePos)
    {
        int g = (Mathf.Abs(startNodePos.x - x) + Mathf.Abs(startNodePos.y - y)) * 10;
        gCost.text = g.ToString();
        int h = (Mathf.Abs(finishNodePos.x - x) + Mathf.Abs(finishNodePos.y - y)) * 10;
        hCost.text = h.ToString();
        int f = g + h;
        fCost.text = f.ToString();
    }
}
