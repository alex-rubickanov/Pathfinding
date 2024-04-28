using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class AHPathfinding : MonoBehaviour {

    public Transform seeker, target;
	
    AHGrid grid;

    void Awake() {
        grid = GetComponent<AHGrid>(); 
    }

    void Start()
    {
        Stopwatch sw = Stopwatch.StartNew();
        FindPath(seeker.position,target.position);
        sw.Stop();
        Debug.Log("Elapsed time: " + sw.ElapsedMilliseconds + " ms");
    }

    void FindPath(Vector3 startPos, Vector3 targetPos) {

        AHNode startNode = grid.NodeFromWorldPoint(startPos);
        AHNode targetNode = grid.NodeFromWorldPoint(targetPos);

        AHHeap<AHNode> openSet = new AHHeap<AHNode>(grid.MaxSize);
        HashSet<AHNode> closedSet = new HashSet<AHNode>();
        openSet.Add(startNode);

        while (openSet.Count > 0) {
            AHNode currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if (currentNode == targetNode) {
                RetracePath(startNode,targetNode);
                return;
            }

            foreach (AHNode neighbour in grid.GetNeighbours(currentNode)) {
                if (!neighbour.walkable || closedSet.Contains(neighbour)) {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                    else {
                        //openSet.UpdateItem(neighbour);
                    }
                }
            }
        }
    }

    void RetracePath(AHNode startNode, AHNode endNode) {
        List<AHNode> path = new List<AHNode>();
        AHNode currentNode = endNode;

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;

    }

    int GetDistance(AHNode nodeA, AHNode nodeB) {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14*dstY + 10* (dstX-dstY);
        return 14*dstX + 10 * (dstY-dstX);
    }


}