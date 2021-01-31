using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsoluteFollow : MonoBehaviour
{
	public Transform player;
	public bool useOffset = false;
	public bool lockX, lockY, lockZ;
	private Vector3 m_offset = Vector3.zero;
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
		transform.position = nPos;
	}

	void Update()
	{
		Vector3 mPos = transform.position;
		Vector3 pPos = player.position;
		Vector3 diff = useOffset ? m_offset : Vector3.zero;
		Vector3 nPos = new Vector3(lockX ? mPos.x : pPos.x + diff.x, lockY ? mPos.y : pPos.y + diff.y, lockZ ? mPos.z : pPos.z + diff.z);
		transform.position = nPos;
	}
}
