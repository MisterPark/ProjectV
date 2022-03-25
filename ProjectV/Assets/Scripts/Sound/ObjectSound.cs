using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSound : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audio;
    [SerializeField] float Volume;
    private void OnEnable()
    {
        if(audio==null)
        {
            audio = GetComponent<AudioSource>();
        }
        audio.volume = SoundManager.Instance.masterVolumeSFX * Volume;
    }
}
