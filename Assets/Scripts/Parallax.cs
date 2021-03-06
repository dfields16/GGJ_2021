using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
	private float length, startPos;
	public float speed = 1f;
	public bool parallaxEnabled = true;
	public GameObject cam;
	void Start()
	{
		if(!cam) cam = Camera.main.gameObject;
		startPos = transform.position.x;
		length = GetComponent<SpriteRenderer>().bounds.size.x;
	}

	void Update()
	{
		float temp = (cam.transform.position.x * (1 - speed));
		float dist = (cam.transform.position.x * (speed));
		transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
		if (temp > startPos + length) startPos += length;
		else if (temp < startPos - length) startPos -= length;
	}
}
