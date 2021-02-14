using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings
{
    static private GameSettings instance = new GameSettings();

    static public GameSettings Instance
    {
        get
        {
            return instance;
        }
    }

    public float volume = 1.0f;
    public bool isSoundOn = true;
}
