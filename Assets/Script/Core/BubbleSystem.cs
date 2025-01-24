using UnityEngine;

public class BubbleSystem : MonoBehaviour
{
	public float getSmoothDamp(float health)
	{
		return health / 500;
	}
}
