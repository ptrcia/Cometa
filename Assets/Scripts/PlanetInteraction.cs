using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInteraction : MonoBehaviour
{
    public float rotationSpeed = 20f;
    public GameObject pivotObject;
    PlayerMovement playerMovement;
    CameraFollow cameraFollow;
    private bool isOrbiting = false; // Para saber si el jugador está en órbita

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            Debug.Log("Player collision with planet");
            pivotObject = collision.gameObject; // Guardar el planeta como objeto pivote
            playerMovement.enabled = false; // Desactivar movimiento del jugador
            cameraFollow.objectToFollow = pivotObject;
            isOrbiting = true; // Activar órbita
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Player exit collision with planet");

        playerMovement.enabled = true; // Desactivar movimiento del jugador
        pivotObject = null;//no hay objeto de pivote
        cameraFollow.objectToFollow = this.gameObject;
        isOrbiting = false;
    }

    private void Update()
    {
        if (isOrbiting && pivotObject != null)
        {
            transform.RotateAround(pivotObject.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);// Rotar alrededor del planeta en cada frame
        }
    }
}
