using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbTrigger : MonoBehaviour
{
    OrbPathing orbPathing;

    // Start is called before the first frame update
    void Start()
    {
        orbPathing = FindObjectOfType<OrbPathing>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            orbPathing.MakeOrbReady();
            Destroy(gameObject);
        }
    }
}
