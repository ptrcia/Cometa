using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplodeSphere : MonoBehaviour
{
    [Header("Boolean")]
    public bool explode;

    [Header("Explosion")]
    [SerializeField] GameObject explosionEffect; // Asigna aquí tu sistema de partículas
    [SerializeField] GameObject companion;
    [SerializeField] GameObject fadeOutPanel;
    [SerializeField] AudioClip explosionClip;

    private GameObject player;
    private SphereCollider sphereCollider;
    private int impulseForce = 70;

    MeshRenderer meshRenderer;
    Rigidbody playerRb;
    PlayerMovement playerMovement;
    EnergyManagement energyManagement;

    private void Awake()
    {
        explode = false;
        meshRenderer = GetComponent<MeshRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
        playerMovement = player.GetComponent<PlayerMovement>();
        energyManagement = player.GetComponent<EnergyManagement>();
        fadeOutPanel = Camera.main.transform.Find("Canvas").gameObject;
    }
    void Update()
    {
        companion = GameObject.FindGameObjectWithTag("Companion");
        if (companion == null) { return; }

        if (explode) Explode();
    }

    void Explode()
    {
        AudioManager.instance.PlaySound(explosionClip);

        // Start sfx
        explosionEffect.SetActive(true);
        Instantiate(explosionEffect, transform.position, transform.rotation);

        //Impulse player
        if (playerRb != null)
        {
            Vector3 forceDirection = transform.forward;
            playerRb.AddForce(forceDirection * impulseForce, ForceMode.Impulse);
            playerMovement.currentSpeed = impulseForce;
        }

        playerMovement.canMove = false;
        energyManagement.energySlider.enabled = false;        
        Destroy(companion);
        DestroyAllPlanets();

        meshRenderer.enabled = false;

        StartCoroutine(PauseBeforeAction(3f));
      
    }


    void DestroyAllPlanets()
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

        foreach (GameObject planet in planets)
        {
            Destroy(planet);
        }
    }
    IEnumerator PauseBeforeAction(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //sonar audios de paso 
        fadeOutPanel.SetActive(true);
        GameManager.instanciate.FinalAct();
    }

}
