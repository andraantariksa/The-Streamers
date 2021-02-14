using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

// Piping class
public class Piping : MonoBehaviour
{
    public HashSet<KeyValuePair<Vector3Int, Vector3Int>> allowedPath = new HashSet<KeyValuePair<Vector3Int, Vector3Int>>();
    // Path is bidirectional, check both key and value
    public HashSet<KeyValuePair<Vector3Int, Vector3Int>> path = new HashSet<KeyValuePair<Vector3Int, Vector3Int>>();
    public Dictionary<Vector3Int, IPipe> tiles = new Dictionary<Vector3Int, IPipe>();
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

    public HashSet<Vector3Int> GetNeighborsWithRestriction(Vector3Int node)
    {
        var neighbors = new HashSet<Vector3Int>();
        foreach (var dest in path)
        {
            if (dest.Key == node)
            {
                if (IsPathExistsWithRestriction(dest.Key, dest.Value))
                {
                    neighbors.Add(dest.Value);
                }
            }
        }
        return neighbors;
    }

    public bool IsPathExistsWithRestriction(Vector3Int from, Vector3Int to)
    {
        bool thereIsRestriction = false;
        foreach (var pair in allowedPath)
        {
            if (pair.Key == from)
            {
                thereIsRestriction = true;
                break;
            }
        }

        if (thereIsRestriction)
        {
            bool allowed = false;
            foreach (var pair in allowedPath)
            {
                if (pair.Key == from && pair.Value == to)
                {
                    allowed = true;
                    break;
                }
            }
            if (!allowed)
            {
                return false;
            }
        }

        byte res = 0;
        foreach (var pair in path)
        {
            if (
                (pair.Key == from && pair.Value == to) ||
                (pair.Key == to && pair.Value == from))
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

    public bool IsPathExists(Vector3Int from, Vector3Int to)
    {
        byte res = 0;
        foreach (var pair in path)
        {
            if (
                (pair.Key == from && pair.Value == to) ||
                (pair.Key == to && pair.Value == from))
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

        ClearHotPipe();
        SetupHotPipeTransformer();
    }

    void PopulateTiles()
    {
        tiles.Clear();
        var childrensTransform = GetComponentsInChildren<IPipe>();
        foreach (var children in childrensTransform)
        {
            var gridPos = GetComponent<Tilemap>().WorldToCell(children.worldPos());
            // Debug.Log(gridPos);
            tiles.Add(gridPos, children);
        }
    }

    void Start() 
    {
        PopulateTiles();

        CheckBuildingConnectivity();

        ClearHotPipe();
        SetupHotPipeTransformer();
    }

    void SetupHotPipeTransformer()
    {
        foreach (var pipe in tiles)
        {
            var hotWaterDirs = pipe.Value.GetHotWaterDir();
            if (hotWaterDirs != null)
            {
                foreach (var dir in hotWaterDirs)
                {
                    TransfromRegularWaterToHotWater(pipe.Key, pipe.Key + dir);
                }
            }
        }
    }

    public void ClearHotPipe()
    {
        foreach (var tile in tiles)
        {
            tile.Value.SetHotWaterPipe(false);
        }
    }

    public void TransfromRegularWaterToHotWater(Vector3Int from, Vector3Int to)
    {
        allowedPath.Add(new KeyValuePair<Vector3Int, Vector3Int>(from, to));

        Fill((pipe) => {
            pipe.SetHotWaterPipe(true);

            return pipe;
        }, from); // Pipa si pemanas juga isinya air
    }

    public void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    Debug.Log(GetComponent<Tilemap>().WorldToCell(worldPosition));
        // }
    }

    void Fill(Func<IPipe, IPipe> action, Vector3Int startPos)
    {
        var queue = new Queue<Vector3Int>();
        var exploredNodes = new HashSet<Vector3Int>();

        queue.Enqueue(startPos);
        var tile = tiles[startPos];
        action(tile);

        while (queue.Count > 0)
        {
            var currentPos = queue.Dequeue();
            var neighbors = GetNeighborsWithRestriction(currentPos);
            foreach (Vector3Int neighbor in neighbors)
            {
                if (!exploredNodes.Contains(neighbor))
                {
                    tile = tiles[neighbor];
                    action(tile);

                    exploredNodes.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }
    }

    void CheckBuildingConnectivity()
    {
        foreach (Building building in buildingList)
        {
            // building.isConnected = IsConnected(pdamCoordinate, building.gridCoordinate);
            // Debug.Log(building.isConnected);
        }
    }
}
