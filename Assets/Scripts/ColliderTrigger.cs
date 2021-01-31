using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    [SerializeField] Collider2D tilemapCollider;
    [SerializeField] Orb orb;
    [SerializeField] PlayerMovement movementScript;

    // Start is called before the first frame update
    void Start()
    {
        if (!tilemapCollider) tilemapCollider= FindObjectOfType<Collider2D>();
        if (!orb) orb = FindObjectOfType<Orb>();
        if (!movementScript) movementScript = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tilemapCollider.enabled = true;
            orb.TriggerCinematicGrow();
            StartCoroutine(StopMovement());
        }
    }

    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(0.5f);
        movementScript.enabled = false;
    }
}

