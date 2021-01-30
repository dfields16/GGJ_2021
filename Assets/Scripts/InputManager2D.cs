using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class InputManager2D : MonoBehaviour
{
	private Controls controls;
	private CharacterController charController;
	private Vector2 moveVelocity = Vector3.zero;
	private Vector2 lookVelocity = Vector3.zero;

    private Vector3 axisLimit = Vector3.one;
	private bool canJump = true;

    [SerializeField] private Transform head;
	public float lookSensitivity = 2f;
	public float moveSpeed = 5f;
	public float jumpHeight = 3f;
	public float gravity = -9.8f;

    // Start is called before the first frame update
    void Awake()
    {
        charController = GetComponent<CharacterController>();

		controls = new Controls();
		controls.Enable();
		controls.Player.Move.performed += (x) => onMove(x.ReadValue<Vector2>());
		controls.Player.Look.performed += (x) => onLook(x.ReadValue<Vector2>());
		controls.Player.Move.canceled += (x) => onMove(Vector2.zero);
		controls.Player.Look.canceled += (x) => onLook(Vector2.zero);
		controls.Player.Jump.performed += (x) => onJump();
    }

    private void onLook(Vector2 lookAxis)
	{
		lookVelocity = lookAxis * Time.deltaTime * lookSensitivity;
	}

	private void onMove(Vector2 input)
	{
		moveVelocity = new Vector3(input.x * axisLimit.x, moveVelocity.y * axisLimit.y, 0f);
	}

	private void onJump()
	{
		if (canJump)
		{
			moveVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravity) * axisLimit.y;
			canJump = false;
			//Animator Here
		}
	}

    // Update is called once per frame
    void Update()
    {
        PerformMove();
		PerformLook();
    }

    private void PerformLook()
	{
		// transform.Rotate(0f, lookVelocity.x, 0f);
		head.Rotate(lookVelocity.y, 0f, 0f);
	}

    private void PerformMove()
	{
		float deltaTime = Time.deltaTime;
		//Move
		Vector3 right = charController.transform.right;
		Vector3 forward = charController.transform.forward;
		right.y = 0f;
		forward.y = 0f;
        if(charController.isGrounded){
            Debug.Log("IS GROUNDED: " + charController.isGrounded);
        }
		// Vector3 moveXZ = right.normalized * moveVelocity.x + forward.normalized * moveVelocity.z;
		charController.Move(right.normalized * moveVelocity.x * moveSpeed * deltaTime);

		//Jump
		moveVelocity.y += gravity * deltaTime; 
		charController.Move(moveVelocity * deltaTime);
		if (charController.isGrounded && moveVelocity.y < 0)
		{
			moveVelocity.y = 0f;
			canJump = true;
			//Jump Animator Here
		}
	}

    public void moveOnAxis(bool x, bool y, bool z){
		axisLimit = new Vector3(Convert.ToInt16(x),Convert.ToInt16(y),Convert.ToInt16(z));
	}
}
