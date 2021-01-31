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

	[SerializeField] float hitDamage = 20f;
	[SerializeField] float knockbackForce = 50f;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		distanceToGround = GetComponent<Collider2D>().bounds.extents.y;
	}


	void Update()
	{
		if(!agro && Vector2.Distance(player.transform.position, transform.position) < agroDistance){
			agro = true;
		}else if(agro){
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

			if(currLightTimer < 0){
				inLight = false;
			}
		}
	}


	void OnCollisionEnter2D(Collision2D c){
		if(LayerMask.LayerToName(c.gameObject.layer) == "Platforms"){
			isGrounded = true;
		}
		if(c.gameObject.tag == "Player")
        {
			GameObject player = c.gameObject;
			player.GetComponent<PlayerHealth>().LoseHealth(hitDamage);
			// TODO Trigger Damage Taking Audio
			if(transform.position.x > player.transform.position.x)
            {
				player.GetComponent<Rigidbody2D>().velocity = new Vector2(-knockbackForce, 5f);
			}
            else
            {
				player.GetComponent<Rigidbody2D>().velocity = new Vector2(knockbackForce, 5f);
			}
        }
	}
	void OnCollisionExit2D(Collision2D c){
		if(LayerMask.LayerToName(c.gameObject.layer) == "Platforms"){
			isGrounded = false;
		}
	}
}
