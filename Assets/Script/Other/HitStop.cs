using System.Collections;
using UnityEngine;

public class HitStop : MonoBehaviour
{
	public static HitStop instances;
	private Coroutine coroutine;
	
	private void Awake() { instances = this; }

	public void Initiate(float t)
	{
		if (coroutine == null)
			coroutine = StartCoroutine(HitStopCoroutine(t));
	}
	
	private IEnumerator HitStopCoroutine(float t)
	{
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(t);
		Time.timeScale = 1;
		coroutine = null;
	}
}
