using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    bool following = false;
    GameObject parent;

    [SerializeField] float healthValue = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(following){
            transform.position = parent.transform.Find("HoldPoint").transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D){
        parent = collider2D.gameObject;
        following = true;
        parent.GetComponent<PlayerMovement>().holdingOrb = true;
        parent.GetComponent<PlayerMovement>().SetOrb(gameObject);
        parent.GetComponent<PlayerHealth>().PickupHealth(healthValue);
    }
}
