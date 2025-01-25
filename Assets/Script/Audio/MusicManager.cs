using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{

    [Header("Music input:")]

    public Music[] musics;

    public static MusicManager musicManager;

    private void Awake()
    { 

        

        foreach (Music music in musics)
        {
            music.musicSource = gameObject.AddComponent<AudioSource>();
            music.musicSource.clip = music.musicClip;

            music.musicSource.volume = music.volume;
            music.musicSource.pitch = music.pitch;
            music.musicSource.loop = music.loop;
            music.musicSource.outputAudioMixerGroup = music.musicMixGroup;

            MusicController.musicData = musics;
        }
    }

}


[System.Serializable]
public class Music
{
    [Tooltip("Input the title of the Sound used for identification of the music")]
    public string musicName;
    [Tooltip("Input the clip of the Sound used for clip that used for this music")]
    public AudioClip musicClip;

    [Tooltip("Setting the start volume")]
    [Range(0f, 1f)]
    public float volume;
    [Tooltip("Setting the start pitch")]
    [Range(0f, 3f)]
    public float pitch;

    [Tooltip("Turn the loop of the song (true/false) with (false) default")]
    public bool loop;

    [HideInInspector] public AudioSource musicSource;

    [Tooltip("Input the Mixer that used for this music.\nused when the music need the audio mixing")]
    public AudioMixer musicMixer;
    [Tooltip("Input the MixerGroup that used for this music.\nused so the music can be managed in AudioMixer")]
    public AudioMixerGroup musicMixGroup;
    [Tooltip("Input the Snapshot in AudioMixer.\nInput the Snapshot that turning on the music(Snapshot that have a highest music volume in AudioMixer)")]
    public AudioMixerSnapshot musicOn;
    [Tooltip("Input the Snapshot in AudioMixer.\nInput the Snapshot that turning off the music(Snapshot that have a lowest music volume in AudioMixer)")]
    public AudioMixerSnapshot musicOff;
}
