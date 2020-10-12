using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth = 100f;


    void Update()
    {
        if (currentHealth <= 0)
            Destroy(this.gameObject);
    }
}
