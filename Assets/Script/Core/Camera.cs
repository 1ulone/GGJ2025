using UnityEngine;

public class Camera : MonoBehaviour
{
	[SerializeField] private Transform target;
	[SerializeField] private float camSpeed = 10;
	
	private void FixedUpdate()
	{
		Vector2 targetPos = target.position;
		Vector2 smoothPos = Vector2.Lerp(transform.position, targetPos, Time.fixedDeltaTime * camSpeed);
		transform.position = new Vector3(smoothPos.x, smoothPos.y, -10);
	}
}
