using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LanguageSelectorUI : MonoBehaviour
{
    Mgl.I18n i18n = Mgl.I18n.Instance;
    static string[] locales = new string[] { "en-US", "id-ID" };
    static byte localeIdx = 0;
    [SerializeField]
    Sprite[] flags;

    void Start()
    {
        UpdateButton();
    }

    public void UpdateButton()
    {
        var locale = Mgl.I18n.GetLocale();
        for (byte i = 0; i < locales.Length; ++i)
        {
            if (locale == locales[i])
            {
                GetComponent<Image>().sprite = flags[i];
                return;
            }
        }
    }

    public void ToggleLanguage()
    {
        localeIdx = (byte)((localeIdx + 1) % locales.Length);
        Mgl.I18n.SetLocale(locales[localeIdx]);
        
        SceneManager.LoadScene("MainMenuScene"); // Reload the scene to reload the language
    }
}
