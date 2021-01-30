using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public float speed = 1f;
	void Update()
	{
		Vector3 rot = transform.eulerAngles;
		rot.z += Time.deltaTime * speed;
		transform.eulerAngles = rot;
	}
}
