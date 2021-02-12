using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing
{
    public ISet<Vector2> nodes = new HashSet<Vector2>();
    // Path is bidirectional, check both key and value
    public IDictionary<Vector2, Vector2> path = new Dictionary<Vector2, Vector2>();

    IList<Vector2> GetNeighbors(Vector2 node)
    {
        var neighbors = new List<Vector2>();
        foreach (KeyValuePair<Vector2, Vector2> pair in path)
        {
            if (pair.Key == node)
            {
                neighbors.Add(pair.Value);
            }
            else if (pair.Value == node)
            {
                neighbors.Add(pair.Key);
            }
        }
        return neighbors;
    }

    bool IsConnected(Vector2 startPos, Vector2 targetPos)
    {
        var queue = new Queue<Vector2>();
        var exploredNodes = new HashSet<Vector2>();

        queue.Enqueue(startPos);

        while(queue.Count > 0)
        {
            var currentPos = queue.Dequeue();
            if (currentPos == targetPos)
            {
                return true;
            }

            var neighbors = GetNeighbors(currentPos);
            foreach(Vector2 neighbor in neighbors)
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

    bool IsStartingNodeConnectedTo(ISet<Vector2> starts, Vector2 target)
    {
        foreach (Vector2 start in starts)
        {
            var isConnected = IsConnected(start, target);
            if (isConnected == true)
            {
                return true;
            }
        }

        return false;
    }
}
