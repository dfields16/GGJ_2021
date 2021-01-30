using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
	public Transform player;
	public bool useOffset = false;
	public bool lockX, lockY, lockZ;

	private Vector3 offset = Vector3.zero;
	void Start()
	{
		if(!player){
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
		if(useOffset){
			offset = transform.position - player.transform.position;
		}
	}

	void Update()
	{
		Vector3 mPos = transform.position;
		Vector3 pPos = player.position;
		transform.position = new Vector3(lockX ? mPos.x : pPos.x + offset.x, lockY ? mPos.y : pPos.y + offset.y, lockZ ? mPos.z : pPos.z + offset.z);
	}
}
