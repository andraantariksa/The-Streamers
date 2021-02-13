using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LanguageSelector : MonoBehaviour
{
    [SerializeField]
    string LanguageID;
    I18n i18n = I18n.Instance;

    public void OnClick()
    {
        Debug.Log(LanguageID);
        I18n.SetLocale(LanguageID);
    }
}
