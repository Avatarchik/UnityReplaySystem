using UnityEngine;
using System;

public class CannonShell : MonoBehaviour 
{
    [SerializeField]
    float speed = 20;

    [SerializeField]
    GameObject deathEffect;

    [SerializeField]
    GameObject bounceEffect;

    private new Rigidbody rigidbody;
    private bool nextCollisionIsDeath;

    public event Action OnDeath;

	void Start () 
	{
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = transform.forward * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        var idamageable = collision.gameObject.GetComponent<IDamageable>();

        if (idamageable != null)
        {
            idamageable.TakeDamage();
            Die();
        }
        else if (nextCollisionIsDeath)
        {
            Die();
        }
        else
        {
            nextCollisionIsDeath = true;
            Instantiate(bounceEffect, transform.position, Quaternion.identity);
            rigidbody.velocity = Vector3.Reflect(transform.forward, collision.contacts[0].normal).normalized * speed;
        }
    }

    void Die()
    {
        // Disconnect any children particle effects
        foreach (Transform child in transform)
        {
            ParticleSystem childSystem = child.GetComponent<ParticleSystem>();

            if (childSystem != null)
            {
                child.parent = null;
                var emission = childSystem.emission;
                emission.enabled = false;
            }
        }

        Instantiate(deathEffect, transform.position, Quaternion.identity);

        if (OnDeath != null)
            OnDeath();

        Destroy(gameObject);
    }
}
