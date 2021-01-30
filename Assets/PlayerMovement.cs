using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

    private Controls controls;
    private Vector3 moveVelocity = Vector3.zero;
    private Vector3 axisLimit = Vector3.one;

    public CharacterController2D controller2D;
    public float jumpHeight = 3f;

    public float runSpeed = 40f;
    bool jump = false;
    bool crouch = false;
    bool flashlight = true;

    void Awake()
	{
		controller2D = GetComponent<CharacterController2D>();

		controls = new Controls();
		controls.Enable();
		controls.Player.Move.performed += (x) => onMove(x.ReadValue<Vector2>());
		// controls.Player.Look.performed += (x) => onLook(x.ReadValue<Vector2>());
		controls.Player.Move.canceled += (x) => onMove(Vector2.zero);
		// controls.Player.Look.canceled += (x) => onLook(Vector2.zero);
		controls.Player.Jump.performed += (x) => onJump();
        // TODO how to do only while holding?
        // controls.Player.Crouch.perform += (x) => onCrouch();
        controls.Player.ToggleFlashlight.performed += (x) => onToggleFlashlight();

	}

    private void onLook(Vector2 lookAxis){

    }

    private void onMove(Vector2 input){
        moveVelocity = new Vector3(input.x * axisLimit.x, moveVelocity.y * axisLimit.y, 0f);
    }

    private void onJump(){
        jump = true;
    }

    private void onCrouch(){
        crouch = true;
    }

    private void onToggleFlashlight(){
        flashlight = !flashlight;
    }

    // Update is called once per frame
    void Update()
    {
        PerformMove();
        // PerformLook();
    }

    void PerformMove(){
        // Move our character

        Debug.Log("MOVE VELOCITY: " + moveVelocity);
        controller2D.Move(moveVelocity.x * runSpeed * Time.fixedDeltaTime, crouch, jump);
        controller2D.Flashlight(flashlight);
        
        jump = false;
    }
}
