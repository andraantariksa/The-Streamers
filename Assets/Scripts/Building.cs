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

    public bool isWaterHot = false;

    public float minChangingDuration;
    public float maxChangingDuration;
    float changeTimer = 0.0f;

    void Update()
    {
        UpdateMood();
        UpdateWater();
    }

    void UpdateMood()
    {
        if (changeTimer > 0.0f)
        {
            changeTimer -= Time.deltaTime;
        }
        else
        {
            isWaterHot = !isWaterHot;
            changeTimer = Random.Range(minChangingDuration, maxChangingDuration);
        }
    }

    void UpdateWater()
    {
        if (isConnected)
        {   
            if (isWaterHot)
            {
                hotWaterAmount += (5 * Time.deltaTime);
            }
            else
            {
                coldWaterAmount += (5 * Time.deltaTime);    
            }
        }
        else
        {
            if (isWaterHot)
            {
                hotWaterAmount -= (2 * Time.deltaTime);
            }
            else
            {
                coldWaterAmount -= (2 * Time.deltaTime);    
            }
        }
        hotWaterAmount = Mathf.Clamp(hotWaterAmount, 0, hotWaterCapacity);
        coldWaterAmount = Mathf.Clamp(coldWaterAmount, 0, coldWaterCapacity);

        Debug.Log(hotWaterAmount);
        Debug.Log(coldWaterAmount);
    }
}
