using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextI18n : MonoBehaviour
{
    I18n i18n = I18n.Instance;
    [SerializeField]
    string keyI18n;

    void Start()
    {
        GetComponent<Text>().text = i18n.__(keyI18n);
    }
}
