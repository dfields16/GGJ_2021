using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
	bool following = false;
	GameObject parent;


	public float pulseSpeed = 1f;
	public float size = 0.75f;

	void Update(){
		transform.localScale = Vector3.one * size * (2 + Mathf.Sin(Time.time * pulseSpeed));
	}

	void FixedUpdate()
	{
		if (following)
		{
			transform.position = parent.transform.Find("HoldPoint").transform.position;
		}
	}

    void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.gameObject.tag == "Player")
        {
            parent = collider2D.gameObject;
            following = true;
            parent.GetComponent<PlayerMovement>().holdingOrb = true;
            parent.GetComponent<PlayerMovement>().SetOrb(gameObject);
        }
    }
}
