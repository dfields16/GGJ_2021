using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public bool inLight = false;

	public float currLightTimer = 0f;

	[SerializeField] protected float hitDamage = 20f;
	[SerializeField] protected float knockbackForce = 50f;
	[SerializeField] protected float hitCooldown = .8f;
	protected float currHitCoolDown = .8f;

	protected void OnCollision(Collision2D c)
	{
		if (c.gameObject.tag == "Player")
		{
			if(currHitCoolDown < 0){
				currHitCoolDown = hitCooldown;
				GameObject player = c.gameObject;
				player.GetComponent<PlayerHealth>().LoseHealth(hitDamage);
				// TODO Trigger Damage Taking Audio
				if (transform.position.x > player.transform.position.x)
				{
					player.GetComponent<Rigidbody2D>().velocity = new Vector2(-knockbackForce, 5f);
				}
				else
				{
					player.GetComponent<Rigidbody2D>().velocity = new Vector2(knockbackForce, 5f);
				}
			}
		}
	}

}
