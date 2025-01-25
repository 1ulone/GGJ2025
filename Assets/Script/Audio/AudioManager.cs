using UnityEngine;
using System;
using System.Collections;
/// <summary>
/// Setting up the FadeState for the rest of the Code
/// </summary>

/// <summary>
/// AudioManager that manage all Ingame audio
/// </summary>
public class AudioManager : MonoBehaviour
{
    private AudioSource _dataMusic;
    /// <summary>
    /// Singletone AudioManager
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        _dataMusic = GetComponent<AudioSource>();
        _dataMusic.Play(0);
    }
}





















