using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteComponent : MonoBehaviour
{
	public Component component;
	void Start()
	{
		Destroy(component);
	}
}
