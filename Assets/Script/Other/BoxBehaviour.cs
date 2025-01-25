using UnityEngine;

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
			case 1: state = "box1"; break;
			case 2: state = "box2"; break;
			case 3: state = "box3"; break;
		}

		anim.Play(state);
		anim.speed = 0;
	}

	public void GetHurt(int damage)
	{
		anim.speed = 1;
		GetComponent<BoxCollider2D>().enabled = false;
		HitStop.instances.Initiate(0.25f);
	}
}
