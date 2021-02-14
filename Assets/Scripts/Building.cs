using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    public float hotWaterAmount;
    public float coldWaterAmount;
    public float hotWaterCapacity;
    public float coldWaterCapacity;

    public Vector3Int gridCoordinate;

    public bool isConnected = false;

    public bool isUsedWaterHot = false;

    public float minChangingDuration;
    public float maxChangingDuration;
    float changeTimer = 0.0f;

    public Slider hotSlider;
    public Slider coldSlider;
    public Slider moodSlider;
    public Image tempIndicator;
    public Sprite coldIndicator;
    public Sprite hotIndicator;

    public PipeStraight connectedPipe;

    void Start() 
    {
        changeTimer = Random.Range(minChangingDuration, maxChangingDuration);
        UpdateIndicator();
    }

    void Update()
    {
        UpdateMood();
        UpdateWater();
        UpdateSlider();
    }

    void UpdateMood()
    {
        if (changeTimer > 0.0f)
        {
            changeTimer -= Time.deltaTime;
        }
        else
        {
            isUsedWaterHot = !isUsedWaterHot;
            changeTimer = Random.Range(minChangingDuration, maxChangingDuration);
            UpdateIndicator();
        }
    }

    void UpdateWater()
    {
        // if (isConnected)
        // {   
        //     if (isUsedWaterHot)
        //     {
        //         hotWaterAmount += (5 * Time.deltaTime);
        //     }
        //     else
        //     {
        //         coldWaterAmount += (5 * Time.deltaTime);    
        //     }
        // }
        // else
        // {
        //     if (isUsedWaterHot)
        //     {
        //         hotWaterAmount -= (2 * Time.deltaTime);
        //     }
        //     else
        //     {
        //         coldWaterAmount -= (2 * Time.deltaTime);    
        //     }
        // }


        if (isUsedWaterHot)
        {
            hotWaterAmount -= (2 * Time.deltaTime);
        }
        else
        {
            coldWaterAmount -= (2 * Time.deltaTime);
        }

        if (isConnected)
        {
            if (connectedPipe.isHot)
            {
                hotWaterAmount += (5 * Time.deltaTime);
            }
            else
            {
                coldWaterAmount += (5 * Time.deltaTime);
            }
        }

        hotWaterAmount = Mathf.Clamp(hotWaterAmount, 0, hotWaterCapacity);
        coldWaterAmount = Mathf.Clamp(coldWaterAmount, 0, coldWaterCapacity);

        // Debug.Log(hotWaterAmount);
        // Debug.Log(coldWaterAmount);
    }

    void UpdateSlider()
    {
        hotSlider.value = (hotWaterAmount / hotWaterCapacity);
        coldSlider.value = (coldWaterAmount / coldWaterCapacity);
    }

    void UpdateIndicator()
    {
        if (isUsedWaterHot)
        {
            tempIndicator.sprite = hotIndicator;
        }
        else
        {
            tempIndicator.sprite = coldIndicator;
        }
    }

    // void CheckWaterTemp()
    // {
    //     isUsedWaterHot = connectedPipe.isHot;
    // }
}
