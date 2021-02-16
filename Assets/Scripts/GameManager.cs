using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    GameObject upperViewObj;
    [SerializeField]
    GameObject textScoreObj;
    [SerializeField]
    GameObject textResultScoreObj;
    List<Building> buildings = new List<Building>();
    // Timer timer = new Timer();

    void Start()
    {
        PopulateBuildings();
    }

    void Update()
    {
        // timer.TimerUpdate(Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (upperViewObj.GetComponent<Transform>().transform.position.x < 10.0f)
            {
                upperViewObj.GetComponent<Transform>().transform.position = new Vector3(1000.0f, 0.0f, 0.0f);
            }
            else
            {
                upperViewObj.GetComponent<Transform>().transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            }
        }
        textScoreObj.GetComponent<Text>().text = string.Format("{0:0.0}%", MeanScore());
    }

    float MeanScore()
    {
        return TotalScore() / (float)buildings.Count;
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

    public void BackToLevelSelector()
    {
        SceneManager.LoadScene("LevelSelectorScene", LoadSceneMode.Single);
    }
}
