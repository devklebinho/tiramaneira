using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ResumeMusic()
    {
        audioSource.Play();
    }
    public void StopMusic()
    {
        audioSource.Pause();
    }
}
