using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeSphere : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect; // Asigna aquí tu sistema de partículas
    [SerializeField] float explosionForce = 700f;
    [SerializeField] float explosionRadius = 5f;
    private GameObject player;
    private GameObject companion;
    private SphereCollider sphereCollider;
    public bool explode;

    private void Awake()
    {
        explode = false;
        player = GameObject.FindGameObjectWithTag("Player");
        sphereCollider = GetComponent<SphereCollider>();
    }
    void Update()
    {
        companion = GameObject.FindGameObjectWithTag("Companion");
        if (companion == null) { return; }

        if (explode) Explode();
    }

    void Explode()
    {
        // Instanciar el efecto de explosión
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // Obtener todos los colliders en el radio de la explosión
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        
        Destroy(gameObject);
        Destroy(companion);
        //new positioon to player

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereCollider.radius*10);
    }

}
