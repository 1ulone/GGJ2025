using UnityEngine;

public class CameraCon : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private float camSpeed = 10;

	private readonly Vector2 minPos = new Vector2(-60, -2);
	private readonly Vector2 maxPos = new Vector2(42, 40);
	
	private void FixedUpdate()
	{
		Vector2 targetPos = target.position;
		Vector2 clampPos = new Vector2(Mathf.Clamp(transform.position.x, minPos.x, maxPos.x), Mathf.Clamp(transform.position.y, minPos.y, maxPos.y));
		Vector2 smoothPos = Vector2.Lerp(clampPos, targetPos, Time.fixedDeltaTime * camSpeed);
		transform.position = new Vector3(smoothPos.x, smoothPos.y, -10);
	}
}
