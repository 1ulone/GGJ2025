using UnityEngine;
using CameraShake;

public class BoxBehaviour : MonoBehaviour, IDamageable
{
	private Animator anim;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		int t = Random.Range(0, 5);
		string state = "box";

		switch (t)
		{
			case 0: state = "box"; break;
			case 1: state = "box4"; break;
			case 2: state = "box2"; break;
			case 3: state = "box3"; break;
		}

		anim.Play(state);
		anim.speed = 0;
	}

	public void GetHurt(int damage)
	{
		anim.speed = 2;
		GetComponent<BoxCollider2D>().enabled = false;
		SFX.instances.PlayAudio("boxbreak");
		CameraShaker.Presets.Explosion2D(5, 5, 0.5f);
	}
}
