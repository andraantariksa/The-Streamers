using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Piping class
public class Piping : MonoBehaviour
{
    public ISet<Vector2> nodes = new HashSet<Vector2>();
    // Path is bidirectional, check both key and value
    public HashSet<KeyValuePair<Vector3Int, Vector3Int>> path = new HashSet<KeyValuePair<Vector3Int, Vector3Int>>();

    public Building[] buildingList;
    public Vector3Int pdamCoordinate;

    public void DeletePathRouteFrom(Vector3Int from)
    {
        // Optimize?
        path.RemoveWhere(x => x.Key == from);
    }

    public HashSet<Vector3Int> GetNeighbors(Vector3Int node)
    {
        var neighbors = new HashSet<Vector3Int>();
        foreach (var dest in path)
        {
            if (dest.Key == node)
            {
                if (IsPathExists(dest.Key, dest.Value))
                {
                    neighbors.Add(dest.Value);
                }
            }
        }
        return neighbors;
    }

    public bool IsPathExists(Vector3Int from, Vector3Int to)
    {
        byte res = 0;
        foreach (var pair in path)
        {
            if ((pair.Key == from && pair.Value == to) || (pair.Key == to && pair.Value == from))
            {
                if (res == 1)
                {
                    return true;
                }
                else
                {
                    res++;
                }
            }
        }

        return false;
    }

    public bool IsConnected(Vector3Int startPos, Vector3Int targetPos)
    {
        var queue = new Queue<Vector3Int>();
        var exploredNodes = new HashSet<Vector3Int>();

        queue.Enqueue(startPos);

        while (queue.Count > 0)
        {
            var currentPos = queue.Dequeue();
            if (currentPos == targetPos)
            {
                return true;
            }

            var neighbors = GetNeighbors(currentPos);
            foreach (Vector3Int neighbor in neighbors)
            {
                if (!exploredNodes.Contains(neighbor))
                {
                    exploredNodes.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }

        return false;
    }

    public bool IsStartingNodeConnectedTo(ISet<Vector3Int> starts, Vector3Int target)
    {
        foreach (Vector3Int start in starts)
        {
            var isConnected = IsConnected(start, target);
            if (isConnected == true)
            {
                return true;
            }
        }

        return false;
    }

    public void OnPipeChange()
    {
        CheckBuildingConnectivity();
        // print(path);
        // foreach (var item in path)
        // {
        //     Debug.Log(item.Key);
        // }
        // Debug.Log(IsConnected(new Vector3Int(1, 1, 0), new Vector3Int(0, -3, 0)));
    }

    void Start() 
    {
        CheckBuildingConnectivity();
    }

    public void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Debug.Log(GetComponent<Tilemap>().WorldToCell(worldPosition));
        // }
    }

    void CheckBuildingConnectivity()
    {
        foreach (Building building in buildingList)
        {
            building.isConnected = IsConnected(pdamCoordinate, building.gridCoordinate);
            Debug.Log(building.isConnected);
        }
    }
}
