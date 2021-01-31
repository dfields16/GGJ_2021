using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinBox : MonoBehaviour
{

	public GameMenu gameMenu;
    public Canvas canvas;

    bool collided = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(collided){
            canvas.GetComponent<UIController>().FlashWhite(true);
            Invoke("LoadMenu", 1.0f);

        }        
    }

    void OnTriggerEnter2D(Collider2D collider2D){
        // GameObject.Instantiate(gameMenu.gameObject, Vector3.zero, Quaternion.identity);
        // GameMenu.menu.isPlayerDead(false);

        collided = true;
    }

    void LoadMenu(){
        SceneManager.LoadScene(0);
    }
}
