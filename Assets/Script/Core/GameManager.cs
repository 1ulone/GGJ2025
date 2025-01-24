using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instances;

	public float health { get; private set; }
	public float exp { get; private set; }
	public int level { get; private set; }

	private float maxExp;
	private float defaultMaxExp = 100;
	private float defaultDifferencesExp = 10;

	private PlayerController player;

	private void Awake()
	{
		instances = this;
		player = FindFirstObjectByType<PlayerController>();
	}
	
	private void Start()
	{
		health = 100;
		exp = 0;

		maxExp = defaultMaxExp;
//		ExpSystem.Instance.ChangeMaxExp(maxExp);
//		ExpSystem.Instance.UpdateEXP(0);
	}

	public void UpdateExp(float getExp)
	{
		exp += getExp;
//		ExpSystem.Instance.UpdateEXP(exp);

		if (exp >= maxExp)
			LevelUp();
	}

	public void UpdateHealth(float values)
	{
		if (values < 0)
			player.GetHurt();

		health += values;
//		HealthSystem.Instance.UpdateHealth(health);
		if (health <= 0)	
			{/*GameOver */}
	}

	public void LevelUp()
	{
		level++;
		
		if (exp > maxExp)
			exp -= maxExp;
		else 
			exp = 0;

		maxExp = defaultMaxExp + (level - 1)* defaultDifferencesExp;	

//		ExpSystem.Instance.UpdateEXP(exp);
//		ExpSystem.Instance.ChangeMaxExp(maxExp);
//		ExpSystem.Instance.UpdateLevel(level);

//		UpgradeSystem.Instance.showUpgradeMenu();
	}
}
