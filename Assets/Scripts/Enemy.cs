using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public bool inLight = false;

	public float currLightTimer = 0f;

	[SerializeField] protected float hitDamage = 20f;
	[SerializeField] protected float knockbackForce = 50f;

	protected void OnCollision(Collision2D c){
		if(c.gameObject.tag == "Player")
        {
			GameObject player = c.gameObject;
			Debug.Log("damage");
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

}
