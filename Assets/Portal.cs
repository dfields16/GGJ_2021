using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    public float suckSpeed = 10f;
    bool collided = false;
    Collider2D player;
    public Transform targetPosition;
    public Canvas canvas;
    public bool level2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(collided && !level2){
            player.GetComponent<Rigidbody2D>().gravityScale = 0;
            player.transform.position = Vector2.MoveTowards(player.transform.position, targetPosition.position, suckSpeed * Time.deltaTime);
            canvas.GetComponent<UIController>().FlashWhite(true);
            Invoke("LoadNextLevel", 1.0f);
        }
        
    }

    void LoadNextLevel(){
        SceneManager.LoadScene("Level2");
    }

    void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.gameObject.tag == "Player")
        {
            player = collider2D;
            collided = true;
        }
    }
}
