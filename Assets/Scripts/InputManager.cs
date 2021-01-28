using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	private Controls controls;
	void Awake()
	{
		controls = new Controls();
		controls.Enable();
		controls.Player.Move.performed += (x) => onMove(x.ReadValue<Vector2>());
	}

	private void onMove(Vector2 input){
		Debug.Log(input);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
