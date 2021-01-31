using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    [SerializeField] Collider2D tilemapCollider;

    // Start is called before the first frame update
    void Start()
    {
        if (!tilemapCollider) tilemapCollider= FindObjectOfType<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tilemapCollider.enabled = true;
        }
    }
}

