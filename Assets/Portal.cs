using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public float suckSpeed = 0.5f;
    bool collided = false;
    Collider2D player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(collided){

            player.transform.position += new Vector3(0f, suckSpeed * Time.deltaTime, 0f);

        }
    }

    void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.gameObject.tag == "Player")
        {
            player = collider2D;
            collided = true;
        }
    }
}
