using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
	public float timer = 1f;
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		timer -= Time.deltaTime;
		if(timer < 0) Destroy(gameObject);
	}
}
