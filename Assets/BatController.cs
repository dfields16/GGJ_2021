using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatController : MonoBehaviour
{
	public GameObject player;

	public bool agro;

	private Vector2 startingPos;
	public float patrolDistance = 8f;
	private Vector2 patrolPosition;

	public float flySpeed = 2f;

	public LayerMask diveLayers;

	[SerializeField] private SpriteRenderer spriteRenderer;
	void Start()
	{
		if (!player)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
		startingPos = transform.position;
		patrolPosition = transform.position;
		patrolPosition.x += patrolDistance;
	}

	// Update is called once per frame
	void Update()
	{
		Vector2 pos = transform.position;
		RaycastHit2D hit = Physics2D.Raycast(pos, ((Vector2)player.transform.position - pos), Vector2.Distance(pos, player.transform.position), diveLayers);
		if (agro && !hit)
		{
			Vector3 targ = player.transform.position;
			targ.z = 0f;
			targ = targ - (Vector3)pos;

			float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * flySpeed);
			spriteRenderer.flipY = (transform.eulerAngles.z > 180f && transform.eulerAngles.z < 270f);
			transform.position = Vector2.Lerp(pos, player.transform.position, Time.deltaTime * flySpeed / 2f);

		}
		else
		{
			//Patrol
			float distanceDiff = Vector2.Distance(pos, patrolPosition);
			if (distanceDiff < 0.01f)
			{
				patrolPosition = (patrolPosition.Equals(startingPos)) ? new Vector2(startingPos.x + patrolDistance, startingPos.y) : startingPos;
				spriteRenderer.flipX = !spriteRenderer.flipX;
				distanceDiff = patrolDistance;
			}
			transform.position = Vector2.Lerp(pos, patrolPosition, (Time.deltaTime * flySpeed) / distanceDiff);
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag == "Player")
		{
			agro = true;
		}
	}

}

