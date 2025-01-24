using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
	public static SFX instances;
	[SerializeField] private List<SFXClip> sfxs;
	private Dictionary<string, AudioClip> clipDictionary;
	private AudioSource source;

	private void Awake()
	{
		instances = this;

		clipDictionary = new Dictionary<string, AudioClip>();

		source = GetComponent<AudioSource>();
		foreach(SFXClip s in sfxs)
			clipDictionary.Add(s.sTag.ToLower(), s.clip);
	}

	public void PlayAudio(string tag)
	{
		if (!clipDictionary.ContainsKey(tag.ToLower()))
			return;

		source.PlayOneShot(clipDictionary[tag.ToLower()]);
	}
}

[System.Serializable]
public class SFXClip
{
	public string sTag;
	public AudioClip clip;
}
