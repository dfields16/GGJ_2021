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

	// Grab variables
	public bool holdingOrb = false;
	RaycastHit2D hit;
	public float orbCollectionDistance = 1f;
	public float orbHealValue = 50f;
	public Transform holdPoint;
	public ParticleSystem OrbDestructionEffect;
	[SerializeField] private GameObject pauseMenu;

	// Animation
	public Animator animator;
	GameObject curOrb;

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
		controls.Player.Grab.performed += (x) => onGrab();
		controls.Player.Pause.performed += (x) => onPause();

	}
	private void onPause()
	{
		if (GameMenu.menu)
		{
			GameMenu.menu.closeWindow();
		}
		else
		{
			GameObject.Instantiate(pauseMenu, Vector3.zero, Quaternion.identity);
			GameMenu.menu.isPlayerDead(false);
		}
	}
	private void onLook(Vector2 lookAxis)
	{

	}

	private void onMove(Vector2 input)
	{
		moveVelocity = new Vector3(input.x * axisLimit.x, moveVelocity.y * axisLimit.y, 0f);
	}

	private void onJump()
	{
		jump = true;
	}

	private void onCrouch()
	{
		crouch = true;
	}

	private void onToggleFlashlight()
	{
		if (holdingOrb)
		{
			ParticleSystem explosionEffect = Instantiate(OrbDestructionEffect) as ParticleSystem;
			// explosionEffect.transform.position = hit.collider.gameObject.transform.position;
			explosionEffect.transform.position = curOrb.transform.position;
			explosionEffect.loop = false;
			explosionEffect.Play();
			// Destroy(hit.collider.gameObject, 0.2f);

			if(curOrb.GetComponent<SpatialAudio2D>()){
				curOrb.GetComponent<SpatialAudio2D>().Remove();
			}else{
				Destroy(curOrb.gameObject, 0.2f);
			}

			AudioManager.PlayOrbCollectionAudio();
			holdingOrb = false;
			curOrb = null;

			GetComponent<PlayerHealth>().PickupHealth(orbHealValue);
			controller2D.FlashlightRefresh();
		}
	}

	public void FlashlightRefresh()
	{
		controller2D.FlashlightRefresh();
	}

	public void SetOrb(GameObject orb)
	{
		curOrb = orb;
	}

	private void onGrab()
	{
		if (!holdingOrb)
		{

			// hold orb

			// Physics2D.queriesStartInColliders = false;
			hit = Physics2D.Raycast(transform.position, Vector3.right * transform.localScale.x, orbCollectionDistance);

			if (hit.collider && hit.collider.tag == "Orb")
			{

				holdingOrb = true;
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		PerformMove();
		// PerformLook();
		// if(holdingOrb){
		//     hit.collider.gameObject.transform.position = holdPoint.position;
		// }
		animator.SetFloat("Speed", Mathf.Abs(moveVelocity.x));
		animator.SetFloat("JumpSpeed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y));
	}

	// public Vector3 GetMoveVelocity(){
	// 	return moveVelocity;
	// }

	void PerformMove()
	{
		// Move our character
		if (moveVelocity.x != 0 && controller2D.isGrounded())
		{
			AudioManager.PlayFootstepAudio();
		}
		controller2D.Move(moveVelocity.x * runSpeed * Time.fixedDeltaTime, crouch, jump);
		//   controller2D.FlashlightRefresh(flashlight);

		jump = false;
	}
}
