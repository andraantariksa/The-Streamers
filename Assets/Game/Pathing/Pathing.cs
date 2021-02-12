using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathing : MonoBehaviour
{
    ArrayList<Vector2> starting;

    void Start()
    {
        
    }

    int EuclideanDistance(Vector2 a, Vector2 b)
    {
        return (int) Mathf.Sqrt(Mathf.Pow(node.x - goal.x, 2) +
            Mathf.Pow(node.y - goal.y, 2) +
            Mathf.Pow(node.z - goal.z, 2));
    }

    void GetShortestPathAStar(Node target)
    { 
        for (Node source: starting)
        {

        }
    }

    bool IsRootConnectedTo(Node target)
    {
        Priority
        return false;
    }
}
