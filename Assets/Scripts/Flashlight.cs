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
	Light2D light;
    public float startIntensity = 1f;
    public float decaySpeed = 0.1f;


	// Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
    }

	// Update is called once per frame
	void Update()
	{
		for (int i = 0; i < hitPts.Length; i++)
		{
			RaycastHit2D h = Physics2D.Raycast(transform.position, (hitPts[i].position - transform.position), hitDistance);
			Debug.DrawRay(transform.position, (hitPts[i].position - transform.position) * hitDistance);
			if(h.collider != null){
				GoopEnemy enemy = h.collider.gameObject.GetComponent<GoopEnemy>();
				if(enemy){
					enemy.inLight = true;
					enemy.currLightTimer = 3f;
				}
			}
		}

		if(light.intensity > 0f){
            light.intensity -= Time.deltaTime * decaySpeed;
        } else if(light.intensity == 0f){
			tempHitDistance = hitDistance;
			hitDistance = 0f;
		}
	}

	public void Refresh(){
        light.intensity = startIntensity;
		hitDistance = tempHitDistance;
    }
}
