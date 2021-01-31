using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinBox : MonoBehaviour
{

	public GameMenu gameMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider2D){
        // GameObject.Instantiate(gameMenu.gameObject, Vector3.zero, Quaternion.identity);
        // GameMenu.menu.isPlayerDead(false);

        SceneManager.LoadScene(0);

    }
}
