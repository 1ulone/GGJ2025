using UnityEngine;

public class BubbleSystem : MonoBehaviour
{
	[SerializeField] private float bubbleSize = 2f;
	public bool onPop { get; private set; }

	private void Start()
	{
		transform.localScale = Vector3.one * bubbleSize;
	}

	public void UpdateBubble(float currHealth)
	{
		bubbleSize = currHealth / 50;
		transform.localScale = Vector3.one * bubbleSize;

		if (bubbleSize <= 0.1f)
		{
			onPop = true;
			GetComponent<SpriteRenderer>().enabled = false;
		}
	}
}
