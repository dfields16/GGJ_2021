using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
	bool following = false;
	GameObject parent;

	void FixedUpdate()
	{
		if (following)
		{
			transform.position = parent.transform.Find("HoldPoint").transform.position;
		}
	}

	void OnTriggerEnter2D(Collider2D collider2D)
	{
		parent = collider2D.gameObject;
		following = true;
		parent.GetComponent<PlayerMovement>().holdingOrb = true;
		parent.GetComponent<PlayerMovement>().SetOrb(gameObject);

	}
}
