using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    GameObject settingsCanvas;

    public void OnClickPlay()
    {
        SceneManager.LoadScene("LevelSelectorScene", LoadSceneMode.Single);
    }

    public void OnClickSettings()
    {
        settingsCanvas.SetActive(true);
    }

    public void OnClickCloseSettings()
    {
        settingsCanvas.SetActive(false);
    }
}
