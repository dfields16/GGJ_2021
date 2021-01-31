using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbTrigger : MonoBehaviour
{
    public OrbPathing orbPathing;
    public Transform nextPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (!orbPathing) orbPathing = FindObjectOfType<OrbPathing>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            orbPathing.MarkNextWaypoint(nextPosition);
            Destroy(gameObject);
        }
    }
}
