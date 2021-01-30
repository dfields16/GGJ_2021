using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
	private Vector2 goTo;
	public float timer = .75f;
	public float speed = 1f;
	private float currTimer = .75f;

	public float killTimer = 1f;

	void Start()
	{
		killTimer += Random.Range(0, killTimer);
		speed = Random.Range(speed / 2, speed * 1.5f);
		currTimer = timer;
		goTo = transform.position;
		goTo.x += Random.Range(2.5f, 5f);
		goTo.y += Random.Range(-3f, 4f);
	}

	// Update is called once per frame
	void Update()
	{
		currTimer -= Time.deltaTime;
		killTimer -= Time.deltaTime;
		if (currTimer < 0)
		{
			goTo = transform.position;
			goTo.x += Random.Range(-2f, 3f);
			goTo.y += Random.Range(-3f, 4f);
			currTimer = timer;
		}

		transform.position = Vector2.Lerp(transform.position, goTo, (Time.deltaTime * speed) / Vector2.Distance(transform.position, goTo));


		if (killTimer < 0)
		{
			Destroy(gameObject);
		}
	}
}
