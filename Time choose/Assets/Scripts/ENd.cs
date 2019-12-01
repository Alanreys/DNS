using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENd : MonoBehaviour
{
    public AudioSource audio_end;

    private void Start()
    {
        if (PlayerPrefs.GetString("Sound") != "no")
        {
            audio_end.Play();
        }
    }
}
