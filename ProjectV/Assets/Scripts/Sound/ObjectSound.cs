using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSound : MonoBehaviourEx
{
    
    private AudioSource audioSource;
    [SerializeField] float Volume;
    protected override void OnEnable()
    {
        base.OnEnable();
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        audioSource.volume = SoundManager.Instance.masterVolumeSFX * Volume;
    }
}
