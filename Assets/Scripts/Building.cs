using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Tilemaps;

public class Building : MonoBehaviour
{
    public float hotWaterAmount;
    public float coldWaterAmount;
    public float hotWaterCapacity;
    public float coldWaterCapacity;

    public Vector3Int gridCoordinate;

    public bool isConnected = false;

    void Update()
    {
        if (isConnected)
        {
            coldWaterAmount += (5 * Time.deltaTime);
        }
        else
        {
            coldWaterAmount -= (2 * Time.deltaTime);
        }
        
        coldWaterAmount = Mathf.Clamp(coldWaterAmount, 0, coldWaterCapacity);

        Debug.Log(coldWaterAmount);
    }
}
