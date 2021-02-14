using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceMediator : MonoBehaviour
{
    public void PlayOneShot(AudioClip audioClip)
    {
        AudioSafeScreenTransition.Instance.PlayOneShot(audioClip);
    }
}
