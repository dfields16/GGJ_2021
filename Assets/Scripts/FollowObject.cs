using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
	public Transform player;
	public bool lockX, lockY, lockZ;
	void Start()
	{
		if(!player){
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
	}

	void Update()
	{
		Vector3 mPos = transform.position;
		Vector3 pPos = player.position;
		transform.position = new Vector3(lockX ? mPos.x : pPos.x, lockY ? mPos.y : pPos.y, lockZ ? mPos.z : pPos.z);
	}
}
