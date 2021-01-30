using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller2D;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool flashlight = false;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(Input.GetButtonDown("Jump")){
            jump = true;
        }

        if(Input.GetButtonDown("Crouch")){
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")){
            crouch = false;
        }

        if(Input.GetMouseButtonDown(0)){
            flashlight = !flashlight;
        }
    }

    void FixedUpdate(){
        // Move our character
        controller2D.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        controller2D.Flashlight(flashlight);
        jump = false;
    }
}
