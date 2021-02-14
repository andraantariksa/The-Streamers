using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSelectorUI : MonoBehaviour
{
    private Canvas canvas;
    private Mgl.I18n i18n = Mgl.I18n.Instance;

    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    public void SelectLanguage(string locale)
    {
        Mgl.I18n.SetLocale(locale);
    }

    void SetCanvasHidden(bool isHidden)
    {
        canvas.enabled = !isHidden;
    }

    public void StartSelectLanguage()
    {
        SetCanvasHidden(false);
    }

    public void EndSelectLanguage()
    {
        SetCanvasHidden(true);
    }
}
