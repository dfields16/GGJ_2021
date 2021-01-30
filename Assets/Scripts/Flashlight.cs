using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
	public Transform[] hitPts;
	public LayerMask layerMask;
	public float hitDistance = 3f;
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
	}
}
