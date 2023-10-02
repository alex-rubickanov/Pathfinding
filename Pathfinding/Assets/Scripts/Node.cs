using System;
using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
    private int x, y;
    [SerializeField] private TextMeshProUGUI coordinates;
    
    private int f;
    
    private int g;
    private int h;

    private void Start()
    {
        coordinates.text = $"{x.ToString()}, {y.ToString()}";
    }


    public void SetCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
