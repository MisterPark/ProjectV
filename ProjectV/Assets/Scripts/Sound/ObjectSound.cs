using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSound : MonoBehaviour
{
    
    private AudioSource audioSource;
    [SerializeField] float Volume;
    private void OnEnable()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.volume = SoundManager.Instance.masterVolumeSFX * Volume;
    }
}
