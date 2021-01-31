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


	void Start()
	{
		if(!player){
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
		if(agro){

		}else{
			//Patrol
			float distanceDiff = Vector2.Distance(pos, patrolPosition);
			if(distanceDiff < 0.1f){
				patrolPosition = (patrolPosition.Equals(startingPos)) ? new Vector2(startingPos.x + patrolDistance, startingPos.y) : startingPos;
				Vector3 rot = transform.eulerAngles;
				rot.y += 180;
				transform.eulerAngles = rot;
			}
			transform.position = Vector2.Lerp(pos, patrolPosition, (Time.deltaTime * flySpeed) / distanceDiff);
		}
	}

	void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.tag == "Player"){
			agro = true;
		}
	}

}


