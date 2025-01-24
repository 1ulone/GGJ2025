using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
	public bool onAttack { get; private set; }

	[SerializeField] private float defaultSpeed = 10;
	[SerializeField] private float dashMultiplier = 20;
	[SerializeField] private float defaultDampValue = 0.2f;
	[SerializeField] private float bubbleRadius = 3;
	[SerializeField] private LayerMask enemyMask;

	private Rigidbody2D rb;
	private Vector2 dir;
	private SpriteRenderer rend;

	private bool onHurt;
	private float dampValue, speed, dashTimer;
	private float defaultDashTimer = 0.75f;
	public float health { get; private set; }
	
	//input
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rend = GetComponent<SpriteRenderer>();
		onHurt = false;
		dampValue = defaultDampValue;
		speed = defaultSpeed;
		dashTimer = 0;
		health = 100;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other == null)
			return;
	}

	private void Update()
	{
		if (dashTimer > 0)
		{

			dashTimer -= Time.deltaTime;
			onAttack = true;


			Collider2D col = Physics2D.OverlapCircle(transform.position, bubbleRadius, enemyMask);
			if(col != null)
			{
				if (col.gameObject.TryGetComponent<IDamageable>(out IDamageable d))
					d.GetHurt();
			}
		}
		else 
		{
			onAttack = false;
		}

		if (Input.GetKeyDown(KeyCode.Space))
			DashAttack();
	}

	private void FixedUpdate()
	{
		float xx = Input.GetAxisRaw("Horizontal");
		float yy = Input.GetAxisRaw("Vertical");
		dir = new Vector2(xx, yy);
		Vector2 refVel = Vector2.zero;
		dampValue = health / 500;
		
		if (dashTimer > 0)
			return;
		
		rb.velocity = Vector2.SmoothDamp(rb.velocity, dir * defaultSpeed, ref refVel, dampValue);
	}

	private void DashAttack()
	{
		if (dashTimer > 0)
			return;

		dashTimer = defaultDashTimer;
		rb.velocity = Vector2.zero;

		if (dir != Vector2.zero)
			rb.AddForce(dir * dashMultiplier, ForceMode2D.Impulse);
		else 
			rb.AddForce(Vector2.right * dashMultiplier, ForceMode2D.Impulse);
	}

	public void GetHurt(int damage = 10)
	{
		if (onHurt)
			return;

		if (onAttack)
			return;

		HitStop.instances.Initiate(0.2f);
		health -= damage;
		HPSystem.Instance.UpdateHealth(health);

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
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, 0.6f);
	}
}
