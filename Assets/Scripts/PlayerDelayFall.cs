using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDelayFall : MonoBehaviour
{
	[SerializeField] private GameObject player;
	private float destroyTimer = 1.2f;
	void Start()
	{
		if (!player) player = GameObject.FindGameObjectWithTag("Player");
		player.GetComponent<Rigidbody2D>().gravityScale = .4f;
	}

	// Update is called once per frame
	void Update()
	{
		destroyTimer -= Time.deltaTime;
		if (destroyTimer < 0)
		{
			player.GetComponent<Rigidbody2D>().gravityScale = 1f;
			Destroy(this);
		}
	}
}
