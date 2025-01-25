using System.Collections;
using UnityEngine;
using CameraShake;

public class PlayerController : MonoBehaviour, IDamageable
{
	public bool onAttack { get; private set; }

	[SerializeField] private int defaultDamage = 10;
	[SerializeField] private float defaultSpeed = 10;
	[SerializeField] private float dashMultiplier = 20;
	[SerializeField] private float bubbleRadius = 3;
	[SerializeField] private float friction = 0.5f;
	[SerializeField] private float acceleration = 10;
	[SerializeField] private LayerMask enemyMask;
	[SerializeField] private BubbleSystem bubble;

	private Rigidbody2D rb;
	private Vector2 dir;
	private SpriteRenderer rend;
	private Animator anim;
	private Coroutine routines;

	private bool onHurt;
	private float dampValue, speed, dashTimer;
	private float defaultDashTimer = 1.25f;
	private int facingDirection;
	private string state;
	private float animTimer;
	private int damage;
	private int maxhealth = 100;

	public int health { get; private set; }
	
	//input
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rend = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		onHurt = false;
		speed = defaultSpeed;
		dashTimer = 0;
		health = maxhealth;
		facingDirection = -1;
		damage = defaultDamage;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other == null)
			return;
	}

	private void Update()
	{
		RotateFaceDirection();
		HandleAnimation();

		float xx = Input.GetAxisRaw("Horizontal");
		float yy = Input.GetAxisRaw("Vertical");
		dir = new Vector2(xx, yy);
		Vector2 refVel = Vector2.zero;
		friction = (float)health / 200;


		if (routines == null && rb.velocity != Vector2.zero && !bubble.onPop)
			routines = StartCoroutine(SpawnBubble());

		if (dashTimer > 0)
		{

			dashTimer -= Time.deltaTime;
			onAttack = true;

			Collider2D col = Physics2D.OverlapCircle(transform.position, bubbleRadius, enemyMask);
			if(col != null)
			{
				if (col.gameObject.TryGetComponent<IDamageable>(out IDamageable d))
					d.GetHurt(damage);
			}
		}
		else 
		{
			onAttack = false;
			if (dir != Vector2.zero)
			{
				rb.velocity += dir * acceleration * Time.deltaTime;
				rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
			}
			else
			{
				rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, friction * Time.deltaTime);
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
			DashAttack();
	}

	private void HandleAnimation()
	{
		if (onHurt)
		{
			ChangeAnimation("cat-hurt");
			return;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			ChangeAnimation("cat-dash");
			return;
		}

		ChangeAnimation(dir != Vector2.zero ? "cat-idle" : "cat-walk");
	}

	private	IEnumerator SpawnBubble()
	{
		yield return new WaitForSeconds(0.1f);
		Pool.instances.Create("bubble-eff", transform.position, Quaternion.identity);
		routines = null;
	}

	public void Footstep()
		=> SFX.instances.PlayAudio("walk");

	private void DashAttack()
	{
		if (dashTimer > 0)
			return;

		ChangeAnimation("cat-dash");
		dashTimer = defaultDashTimer;
		rb.velocity = Vector2.zero;

		if (dir != Vector2.zero)
			rb.AddForce(dir * dashMultiplier, ForceMode2D.Impulse);
		else 
			rb.AddForce(Vector2.right * dashMultiplier, ForceMode2D.Impulse);

		SFX.instances.PlayAudio("dash");
	}

	private void ChangeAnimation(string n)
	{
		if (state != n)
			anim.Play(state);
		state = n;
	}

	public void GetHurt(int damage = 10)
	{
		if (onHurt)
			return;

		if (onAttack)
			return;

		SFX.instances.PlayAudio("hurt");
		CameraShaker.Presets.Explosion2D(6, 8, 0.5f);
		HitStop.instances.Initiate(0.2f);
		health -= damage;
		bubble.UpdateBubble(health);
		HPSystem.Instance.UpdateHealth(health);
		ChangeAnimation("cat-hurt");

		StartCoroutine(Hitflash());
		onHurt = true;
	}

	public void UpdateDamage(int dmg)
	{
		damage += dmg; 
	}

	public void UpdateMaxhealth(int h)
	{
		maxhealth += h;
		health = maxhealth;
		HPSystem.Instance.UpdateMaxhealth(maxhealth);
		HPSystem.Instance.UpdateHealth(health);
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

	private void RotateFaceDirection()
	{
		if ((dir.normalized.x == 1 || dir.normalized.x == -1) && dir.normalized.x != facingDirection)
		{
			transform.Rotate(0, 180, 0);
			facingDirection *= -1;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 0.6f);
	}
}
