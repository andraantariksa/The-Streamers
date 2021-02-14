using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Timer
{
    public float TimeLeft;
    public bool Running;

    public Timer(float timeLeft, bool running)
    {
        TimeLeft = timeLeft;
        Running = running;
    }

    public void TimerUpdate(float dt)
    {
        if (Running)
        {
            if (TimeLeft > 0.0f)
            {
                TimeLeft -= dt;
            }
            else
            {
                TimeLeft = 0;
                Running = false;
            }
        }
    }
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject thingsAboveGroundObj;
    List<Building> buildings = new List<Building>();
    // Timer timer = new Timer();

    void Start()
    {
        PopulateBuildings();
    }

    void Update()
    {
        // timer.TimerUpdate(Time.deltaTime);
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
