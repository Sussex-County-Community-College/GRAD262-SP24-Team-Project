using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public float health = 10;
    public UnityEvent<float> onTakeDamage;

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health < 0)
        {
            health = 0;
        }
        onTakeDamage.Invoke(health);
    }
}
