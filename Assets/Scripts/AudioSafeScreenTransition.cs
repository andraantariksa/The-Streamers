using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSafeScreenTransition : MonoBehaviour
{
    private static AudioClip audioClipPersistence;
    private static AudioSafeScreenTransition instance = null;
    public static AudioSafeScreenTransition Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayOneShot(AudioClip audioClip)
    {
        audioClipPersistence = audioClip;
        instance.GetComponent<AudioSource>().PlayOneShot(audioClipPersistence);
    }
}
