using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
	public Transform player;
	public bool useOffset = false;
	public bool lockX, lockY, lockZ;
	public Vector3 offset = Vector3.zero;
	private Vector3 m_offset = Vector3.zero;

	private bool smoothing = false;
	void Awake()
	{
		if(!player){
			player = GameObject.FindGameObjectWithTag("Player").transform;
		}
		if(useOffset){
			m_offset = transform.position - player.transform.position;
		}
		Vector3 mPos = transform.position;
		Vector3 pPos = player.position;
		Vector3 diff = useOffset ? m_offset : Vector3.zero;
		Vector3 nPos = new Vector3(lockX ? mPos.x : pPos.x + diff.x, lockY ? mPos.y : pPos.y + diff.y, lockZ ? mPos.z : pPos.z + diff.z);
		nPos += offset;
		transform.position = nPos;
	}

	void Update()
	{
		Vector3 mPos = transform.position;
		Vector3 pPos = player.position;
		Vector3 diff = useOffset ? m_offset : Vector3.zero;
		Vector3 nPos = new Vector3(lockX ? mPos.x : pPos.x + diff.x, lockY ? mPos.y : pPos.y + diff.y, lockZ ? mPos.z : pPos.z + diff.z);
		nPos += offset;

		float dist = Vector3.Distance(nPos, mPos);
		if(dist > .5f || smoothing){
			smoothing = dist > .01f;
			transform.position = Vector3.Lerp(transform.position, nPos, (Time.deltaTime * 10f) / dist);
		}else{
			smoothing = false;
			transform.position = nPos;
		}
	}
}
