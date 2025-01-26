using UnityEngine;
using System.Collections;
using CameraShake;

public class Enemy : MonoBehaviour, IDamageable
{
	[SerializeField] private float dashSpeed;
	[SerializeField] private float speed = 10;
	[SerializeField] private LayerMask playerMask;
	[SerializeField] private float playerRadius;

	[SerializeField] private float defaultattackPercentage = 50;
	[SerializeField] private float defaultdodgePercentage = 80;
	[SerializeField] private float defaultHealth = 100;

	[SerializeField] private BubbleSystem bubble;

	private float health, attackPercentage, dodgePercentage;
	private float dashTimer;
	private float angle;
	private string state;

	private bool onPlayer, circleAround, onHurt;

	private PlayerController player;
	private Rigidbody2D rb;
	private Vector3 positionOffset;
	private SpriteRenderer rend;
	private Animator anim;

	private void Awake()
	{
		player = FindObjectOfType<PlayerController>();
		rb = GetComponent<Rigidbody2D>();
		rend = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();

		health = defaultHealth;
		attackPercentage = defaultattackPercentage;
		dodgePercentage = defaultdodgePercentage;
	}

	private void Update()
	{
		onPlayer = Physics2D.OverlapCircle(transform.position, playerRadius, playerMask);

		if (dashTimer > 0)
		{
			dashTimer -= Time.deltaTime;
			Collider2D col = Physics2D.OverlapCircle(transform.position, playerRadius, playerMask);
			if(col != null && !player.onAttack)
			{
				if (col.gameObject.TryGetComponent<IDamageable>(out IDamageable d))
					d.GetHurt();
			}
			if (dashTimer <= 0)
				rb.velocity = Vector2.zero;
			return;
		}
	
		ChangeAnimation("enemy-walk");
		if (onPlayer)
		{
			if (dashTimer <= 0)
			{
				if (!circleAround)
				{
					angle = Mathf.Rad2Deg * (Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x));
					circleAround = true;
				}
			}

			attackPercentage += Time.deltaTime*10f;
			if (attackPercentage > 55)
				StartCoroutine(Attack(player.transform.position - transform.position));

			if (dodgePercentage > 75 && player.onAttack)
			{
				if (attackPercentage > 75)
					StartCoroutine(Attack(player.transform.position - transform.position));
				else 
					Dodge();
			}
		}
		else 
		{
			Vector2 refVel = Vector2.zero;
			Vector2 dir = player.transform.position - transform.position;
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
			circleAround = false;
		}
		if (routines == null && !bubble.onPop)
			routines = StartCoroutine(SpawnBubble());
	}

	private void LateUpdate()
	{
		if (!circleAround)
			return;

		positionOffset.Set(Mathf.Cos(angle) * playerRadius*1.5f, Mathf.Sin(angle) * playerRadius*1.5f, 0);
		transform.position = Vector3.MoveTowards(transform.position, player.transform.position + positionOffset, Time.deltaTime * speed);
		angle += Time.deltaTime;
	}

	private void Dodge()
	{
		Vector2 ddir = Vector2.zero;
		ddir = new Vector2(ddir.x, Random.Range(-1,1));
		if (player.transform.position.x < transform.position.x)
			ddir = new Vector2(-1, ddir.y); else 
		if (player.transform.position.x > transform.position.x)
			ddir = new Vector2(1, ddir.y);
		if (ddir.y == 0)
			ddir = new Vector2(ddir.x, 1);

		StartCoroutine(Attack(ddir.normalized));
	}

	private void ChangeAnimation(string n)
	{
		if (state != n)
			anim.Play(state);
		state = n;
		Debug.Log(state);
	}

	private Coroutine routines;
	private	IEnumerator SpawnBubble()
	{
		Pool.instances.Create("bubble-eff", transform.position, Quaternion.identity);
		yield return new WaitForSeconds(0.1f);
		routines = null;
	}

	private IEnumerator Attack(Vector2 dir)
	{
		if (dashTimer > 0)
			yield return null;

		dashTimer = 2.75f;
		rb.velocity = Vector2.zero;
		ChangeAnimation("enemy-prepare");
		yield return new WaitForSeconds(2f);

		ChangeAnimation("enemy-idle");
		attackPercentage = 50;
		circleAround = false;
		SFX.instances.PlayAudio("dash");
		rb.AddForce(dir * dashSpeed, ForceMode2D.Impulse);
	}

	public void GetHurt(int damage = 10)
	{
		if (onHurt)
			return;

		health -= damage;

		if (health < -10)
			StartCoroutine(Die());

		
		bubble.UpdateBubble(health);
		Pool.instances.Create("bubble-explosion", transform.position, Quaternion.identity);
		CameraShaker.Presets.Explosion2D(10, 10, 0.75f);
		HitStop.instances.Initiate(0.2f);
		StartCoroutine(Hitflash());
		HPSystem.Instance.UpdateHealthE(health);
		onHurt = true;
	}

	private IEnumerator Hitflash()
	{
		int i = 5;
		while(i > 0)
		{	
			rend.enabled = false;
			yield return new WaitForSeconds(0.05f);
			rend.enabled = true;
			i--;
			yield return new WaitForSeconds(0.05f);
		}

		onHurt = false;
	}

	private IEnumerator Die()
	{
		yield return new WaitForSeconds(1f);
		SFX.instances.PlayAudio("levelup");
		UpgradeSystem.Instance.ShowUpgradeMenu();
//		SceneManager.LoadScene(1);
	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, playerRadius);
	}
}
