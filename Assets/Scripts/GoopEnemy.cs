using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GoopEnemy : Enemy
{
	[HideInInspector] public GameObject player;
	public Rigidbody2D rb;

	public float jumpForce = 5f;

	private float distanceToGround;

	[SerializeField] private float agroDistance = 8f;
	private bool agro = false;
	private bool isGrounded;


	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		distanceToGround = GetComponent<Collider2D>().bounds.extents.y;
	}


	void Update()
	{
		currHitCoolDown -= Time.deltaTime;

		if (!agro && Vector2.Distance(player.transform.position, transform.position) < agroDistance)
		{
			agro = true;
		}
		else if (agro)
		{
			if (inLight)
			{
				if (isGrounded)
				{
					rb.velocity = new Vector2(rb.velocity.x, 0);
					rb.AddForce(((Vector2)(-1 * (player.transform.position - transform.position).normalized) + (Vector2.up)) * jumpForce);
				}
				currLightTimer -= Time.deltaTime;
			}
			else
			{
				if (isGrounded)
				{
					rb.velocity = new Vector2(rb.velocity.x, 0);
					rb.AddForce(((Vector2)((player.transform.position - transform.position).normalized) + (Vector2.up)) * jumpForce);
				}
			}

			if (currLightTimer < 0)
			{
				inLight = false;
			}
		}
	}

	void OnCollisionStay2D(Collision2D c)
	{
		OnCollisionEnter2D(c);
	}
	void OnCollisionEnter2D(Collision2D c)
	{
		OnCollision(c);
		if (LayerMask.LayerToName(c.gameObject.layer) == "Platforms")
		{
			isGrounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D c)
	{
		if (LayerMask.LayerToName(c.gameObject.layer) == "Platforms")
		{
			isGrounded = false;
		}
	}
}
