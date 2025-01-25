using UnityEngine;
using System;
using System.Collections;
/// <summary>
/// Setting up the FadeState for the rest of the Code
/// </summary>
public enum FadeState
{
    FadeIn,
    FadeOut
}
/// <summary>
/// AudioManager that manage all Ingame audio
/// </summary>
public class AudioManager : MonoBehaviour
{
    
    public static AudioManager audioManagerControl;


    /// <summary>
    /// Singletone AudioManager
    /// </summary>
    void Awake()
    {
        if (audioManagerControl == null)
        {
            audioManagerControl = this;
        }
        else
        {
            return;
        }
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        MusicController.Play("battle");
    }
}



/// <summary>
/// Control Music Effect and Function
/// </summary>
class MusicController : MonoBehaviour
{
    public static MusicManager musicManager;
    public static Music[] musicData;
    /// <summary>
    /// Define music that want to play
    /// </summary>
    /// <param name="music_name"></param>
    public static void Play(string music_name)
    {
        Music musicFinder = Array.Find<Music>(musicData, musicFinder => musicFinder.musicName == music_name);
        if (music_name == null)
        {
            Debug.LogError("'" + music_name + "'" + " could not be found in Music List inside Music Manager");
        }
        musicFinder.musicSource.clip = musicFinder.musicClip;

        musicFinder.musicSource.Play();


    }


    /// <summary>
    /// Define music that want to stop playing
    /// </summary>
    /// <param name="music_name"></param>
    public static void Stop(string music_name)
    {

        Music musicFinder = Array.Find<Music>(musicData, musicFinder => musicFinder.musicName == music_name);

        if (music_name == null)
        {
            Debug.LogError("'" + music_name + "'" + " could not be found in Music List Inside Music Manager");
        }

        musicFinder.musicSource.clip = musicFinder.musicClip;
        musicFinder.musicSource.Stop();
    }

    /// <summary>
    /// Make Fade Effect on music by declear the name fade state and fade time
    /// </summary>
    /// <param name="music_name"></param>
    /// <param name="fadeState"></param>
    /// <param name="fadeTime"></param>
    public static void FadeEffect(string music_name, FadeState fadeState, float fadeTime)
    {
        Music musicFinder = Array.Find<Music>(musicData, musicFinder => musicFinder.musicName == music_name);
        switch (fadeState)
        {
            case FadeState.FadeIn:
                musicFinder.musicOn.TransitionTo(fadeTime);
                break;
            case FadeState.FadeOut:
                musicFinder.musicOff.TransitionTo(fadeTime);
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Crossfade usually used in ambience bgm
    /// </summary>
    /// <param name="music1"></param>
    /// <param name="music2"></param>
    /// <param name="fadeSpeed1"></param>
    /// <param name="fadeSpeed2"></param>
    public static void CrossFade(string music1, string music2, float fadeSpeed1, float fadeSpeed2)
    {
        FadeEffect(music1, FadeState.FadeOut, fadeSpeed1);
        Stop(music1);
        FadeEffect(music2, FadeState.FadeIn, fadeSpeed2);
        Play(music2);
    }
}



/// <summary>
/// Sound Controller that control function in sound all in game
/// </summary>
class SoundController
{

    /// <summary>
    /// Play Sound clip by refrence the AudioClip and GameObject
    /// </summary>
    /// <param name="soundClip"></param>
    /// <param name="soundGameObject"></param>
    public static void Play(AudioClip soundClip, GameObject soundGameObject)
    {
        
        AudioSource.PlayClipAtPoint(soundClip, soundGameObject.transform.position);
    }
}






















