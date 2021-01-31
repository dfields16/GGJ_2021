using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flashlight : Enemy
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
	public ParticleSystem DestructionEffect;

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
			Debug.DrawLine(transform.position,(h.point != null) ? (Vector3)h.point : hitPts[i].position, Color.yellow);
			if (h.collider != null)
			{
				Enemy enemy = h.collider.gameObject.GetComponent<Enemy>();
				if (enemy)
				{
					enemy.inLight = true;
					enemy.currLightTimer = scareTime;
					h.collider.gameObject.transform.localScale = h.collider.gameObject.transform.localScale / 1.01f;

					ParticleSystem explosionEffect = Instantiate(DestructionEffect) as ParticleSystem;
					explosionEffect.transform.position = h.collider.gameObject.transform.position;
					explosionEffect.loop = false;

					explosionEffect.Play();
					Destroy(h.collider.gameObject, 5f);
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
