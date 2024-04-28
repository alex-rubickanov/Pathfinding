using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class BHPathfinding : MonoBehaviour {

    public Transform seeker, target;
    BHGrid grid;

    void Awake() {
        grid = GetComponent<BHGrid>();
    }

    void Start() {
        Stopwatch sw = Stopwatch.StartNew();
        FindPath(seeker.position,target.position);
        sw.Stop();
        Debug.Log("Elapsed time: " + sw.ElapsedMilliseconds + " ms");
    }

    void FindPath(Vector3 startPos, Vector3 targetPos) {
        BHNode startNode = grid.NodeFromWorldPoint(startPos);
        BHNode targetNode = grid.NodeFromWorldPoint(targetPos);

        List<BHNode> openSet = new List<BHNode>();
        HashSet<BHNode> closedSet = new HashSet<BHNode>();
        openSet.Add(startNode);

        while (openSet.Count > 0) {
            BHNode node = openSet[0];
            for (int i = 1; i < openSet.Count; i ++) {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost) {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode) {
                RetracePath(startNode,targetNode);
                return;
            }

            foreach (BHNode neighbour in grid.GetNeighbours(node)) {
                if (!neighbour.walkable || closedSet.Contains(neighbour)) {
                    continue;
                }

                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    void RetracePath(BHNode startNode, BHNode endNode) {
        List<BHNode> path = new List<BHNode>();
        BHNode currentNode = endNode;

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;

    }

    int GetDistance(BHNode nodeA, BHNode nodeB) {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14*dstY + 10* (dstX-dstY);
        return 14*dstX + 10 * (dstY-dstX);
    }
}