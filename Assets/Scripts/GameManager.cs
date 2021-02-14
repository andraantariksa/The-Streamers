using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject thingsAboveGroundObj;
    List<Building> buildings = new List<Building>();

    void Start()
    {
        PopulateBuildings();
    }

    void Update()
    {
        
    }

    void PopulateBuildings()
    {
        var buildingComponents = thingsAboveGroundObj.GetComponentsInChildren<Building>();
        foreach (var building in buildingComponents)
        {
            if (building != null)
            {
                buildings.Add(building);
            }
        }
    }

    float TotalScore()
    {
        float moodTotal = 0;
        foreach (var building in buildings)
        {
            moodTotal += building.moodAmount;
        }

        return moodTotal;
    }
}
