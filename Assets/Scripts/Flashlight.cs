using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
	public Transform[] hitPts;
	public LayerMask layerMask;
	public float hitDistance = 3f;
	float tempHitDistance;

	// burnout
	Light2D light2D;
	public float startIntensity = 1f;
	public float decaySpeed = 0.1f;

	public float scareTime = 3f;
	// Start is called before the first frame update
	void Start()
	{
		light2D = GetComponent<Light2D>();
		light2D.intensity = 0;
	}

	// Update is called once per frame
	void Update()
	{
		if(light2D.intensity <= 0) return;

		for (int i = 0; i < hitPts.Length; i++)
		{
			RaycastHit2D h = Physics2D.Raycast(transform.position, (hitPts[i].position - transform.position).normalized, hitDistance, layerMask);
			Debug.DrawLine(transform.position, hitPts[i].position, Color.yellow);
			if (h.collider != null)
			{
				GoopEnemy enemy = h.collider.gameObject.GetComponent<GoopEnemy>();
				if (enemy)
				{
					enemy.inLight = true;
					enemy.currLightTimer = scareTime;
				}
			}
		}

		if (light2D.intensity > 0f)
		{
			light2D.intensity -= Time.deltaTime * decaySpeed;
		}
	}

	public void Refresh()
	{
		light2D.intensity = startIntensity;
	}
}
