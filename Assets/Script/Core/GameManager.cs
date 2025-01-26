using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instances;
	public static bool stage1;
	public static bool stage2;
	public static bool stage3;

	public int health;
	public int damage;

	private void Awake()
	{
        if (instances != null && instances != this)
        {
            Destroy(gameObject); 
            return;
        }

		instances = this;
		DontDestroyOnLoad(this.gameObject);
	}
}
