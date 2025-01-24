using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IDamageable
{
	[SerializeField] private float dashSpeed;
	[SerializeField] private float speed = 10;
	[SerializeField] private LayerMask playerMask;
	[SerializeField] private float playerRadius;

	[SerializeField] private float attackPercentage = 50;
	[SerializeField] private float dodgePercentage = 80;

	private float health = 100;
	private float dashTimer;
	private float angle;

	private bool onPlayer, circleAround, onHurt;

	private PlayerController player;
	private Rigidbody2D rb;
	private Vector3 positionOffset;
	private SpriteRenderer rend;

	private void Awake()
	{
		player = FindObjectOfType<PlayerController>();
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		onPlayer = Physics2D.OverlapCircle(transform.position, playerRadius, playerMask);

		if (dashTimer > 0)
		{
			dashTimer -= Time.deltaTime;
			if (dashTimer <= 0)
				rb.velocity = Vector2.zero;
			return;
		}
	
		if (onPlayer)
		{
			if (dashTimer <= 0)
			{
				if (!circleAround)
				{
					angle = Mathf.Rad2Deg * (Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x));
					Debug.Log(angle);
					circleAround = true;
				}
			}

			attackPercentage += Time.deltaTime*5f;
			if (attackPercentage > 75)
				Attack(player.transform.position - transform.position);

			if (dodgePercentage > 75 && player.onAttack)
			{
				if (attackPercentage > 85)
					Attack(player.transform.position - transform.position);
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

		Attack(ddir.normalized);
	}

	private void Attack(Vector2 dir)
	{
		if (dashTimer > 0)
			return;

		attackPercentage = 50;
		dashTimer = 0.75f;
		circleAround = false;
		rb.AddForce(dir * dashSpeed, ForceMode2D.Impulse);
	}

	public void GetHurt()
	{
		if (onHurt)
			return;

		StartCoroutine(Hitflash());
		onHurt = true;
	}

	private IEnumerator Hitflash()
	{
		int i = 10;
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

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, playerRadius);
	}
}
