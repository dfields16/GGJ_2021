using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float currentHealth;
    Light playerLight;

    // Start is called before the first frame update
    void Start()
    {
        playerLight = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        playerLight.intensity = currentHealth / 100;
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
