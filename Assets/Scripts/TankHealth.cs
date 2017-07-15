using System;
using UnityEngine;

public class TankHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    GameObject deathEffect;

    public event Action OnDeath;

    public void TakeDamage()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);

        gameObject.SetActive(false);

        if (OnDeath != null)
            OnDeath();        
    }
}
