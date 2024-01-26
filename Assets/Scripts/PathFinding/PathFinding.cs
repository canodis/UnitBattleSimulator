using System;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{
    public List<Node> FindPath(Vector3Int startPosition, Vector3Int endPosition)
    {
        Node startNode, targetNode, currentNode;
        List<Node> openSet = new();
        HashSet<Node> closedSet = new();
        int distance;

        if (startPosition == endPosition)
            return new List<Node>();

        startNode = CellMap.Instance.getNode(startPosition.x, startPosition.y);
        targetNode = CellMap.Instance.getNode(endPosition.x, endPosition.y);
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                if (closedSet.Contains(neighbour))
                    continue;
                distance = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (distance < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = distance;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour)) openSet.Add(neighbour);
                }
            }
        }
        return null;
    }

    public GameObject CheckTargetCell(Vector3Int startPosition, ref Vector3Int endPosition)
    {
        GameObject targetObject = null;

        if (startPosition == endPosition)
            return null;
        int targetIndex = GameManager.Instance.gridData.GetObjectIndex(endPosition);
        if (targetIndex != -1)
        {
            GameObject obj = GameManager.Instance.objectManager.GetObject(targetIndex);
            IAttackable attackableObject = obj.GetComponent<IAttackable>();
            if (attackableObject != null)
            {
                targetObject = obj;
            }
        }
        if (CellMap.Instance.IsNodeAvailable(endPosition) == false)
        {
            endPosition = GetClosestNeighbourCell(startPosition, endPosition);
        }
        return targetObject;
    }

    private int GetDistance(Node currentNode, Node neighbour)
    {
        int distx, disty;

        distx = Mathf.Abs(currentNode.position.x - neighbour.position.x);
        disty = Mathf.Abs(currentNode.position.y - neighbour.position.y);

        return distx > disty ? (14 * disty + 10 * (distx - disty)) : (14 * distx + 10 * (disty - distx));
    }

    private List<Node> GetNeighbours(Node currentNode)
    {
        List<Node> neighbours = new();

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                Vector3Int neighbourPosition = new Vector3Int(currentNode.position.x + i, currentNode.position.y + j);
                if (CellMap.Instance.IsNodeAvailable(neighbourPosition))
                {
                    neighbours.Add(CellMap.Instance.getNode(neighbourPosition.x, neighbourPosition.y));
                }
            }
        }
        return neighbours;
    }

    List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path;
        Node currentNode;

        path = new List<Node>();
        currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    private Vector3Int GetClosestNeighbourCell(Vector3Int startPosition, Vector3Int targetPosition)
    {
        PlacementData placementData = GameManager.Instance.gridData.GetObjectPlacementData(targetPosition);
        if (placementData == null)
        {
            return targetPosition;
        }
        ObjectData objectData = GameManager.Instance.FindObjectDataWithId(placementData.Id);
        if (objectData == null)
        {
            return targetPosition;
        }
        List<Vector3Int> neighbourCells = GameManager.Instance.gridData.GetObjectsNeighbourCells(placementData.CellPosition, objectData.Size);
        Vector3Int closestCell = targetPosition;
        float minDistance = float.MaxValue;
        foreach (Vector3Int neighbourCell in neighbourCells)
        {
            if (neighbourCell == startPosition)
                return neighbourCell;
            if (GameManager.Instance.gridData.IsCellEmpty(neighbourCell))
            {
                float distance = Vector3Int.Distance(startPosition, neighbourCell);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestCell = neighbourCell;
                }
            }
        }
        return closestCell;
    }

}