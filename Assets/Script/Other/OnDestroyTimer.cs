using UnityEngine;

public class OnDestroyTimer : MonoBehaviour
{
	public float timer = 1;
	
	private void OnEnable()
	{
		Invoke("destroyEnd", timer);
	}

	private void destroyEnd()
	{
		Pool.instances.Destroy(this.gameObject);
	}
}
