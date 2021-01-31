using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float currentHealth;
    [SerializeField] Light2D playerLight;
    

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        playerLight.intensity = currentHealth / 100;

        if (Input.GetKeyDown(KeyCode.R)) 
        { 
            LoseHealth(25);
            Debug.Log(currentHealth);
        }
    }

    public void PickupHealth(float health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth) { currentHealth = maxHealth; }
    }

    public void LoseHealth(float health)
    {
        currentHealth -= health;
        if (currentHealth <= 0) { /*Trigger end game*/ }
    }
}
