using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInteraction : MonoBehaviour
{

    public enum planetSize{small , medium, large}
    public planetSize optionPlanetSize;

    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject pivotObject = null;
    [SerializeField] Vector3 directionOfRotation;

    public bool isOrbiting = false; // Para saber si el jugador está en órbita

    PlayerMovement playerMovement;
    CameraFollow cameraFollow;
    Rigidbody rb;
    EnergyManagement energyManagement;
    

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        rb = GetComponent<Rigidbody>();
        energyManagement = GetComponent<EnergyManagement>();

    }

    private void Start()
    {
        PlanetSize(optionPlanetSize);
        directionOfRotation = new Vector3(0, 0, 1);
    }

    public void PlanetSize(planetSize planetSize)
    {
        switch (planetSize)
        {
            case planetSize.small:
                rotationSpeed = 50.0f;
                break;
            case planetSize.medium:
                rotationSpeed = 25.0f;
                break;
            case planetSize.large:
                rotationSpeed = 10.0f;
                break;
            default:
                rotationSpeed = 1.0f;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            Debug.Log("Player collision with planet");
            GenerateRandomDirection();

            pivotObject = collision.gameObject; // Guardar el planeta como objeto pivote
            playerMovement.currentSpeed = 0;
            
            rb.useGravity = false;
            rb.velocity = Vector3.zero;

            cameraFollow.objectToFollow = pivotObject;
            
            isOrbiting = true; // Activar estado de orbitar
        }

        if (collision.gameObject.CompareTag("PlanetAttractionField"))
        {
            //pivotObject = collision.gameObject;
            //añadirle campo de atracion gravitacional
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            cameraFollow.objectToFollow = this.gameObject;

            pivotObject = null;
            isOrbiting = false;
        }
    }

    private void Update()
    {
        if (isOrbiting && pivotObject != null)
        {
            
            transform.RotateAround(pivotObject.transform.position, directionOfRotation, (rotationSpeed + playerMovement.currentSpeed) * Time.deltaTime);// Rotar alrededor del planeta en cada frame
        }
        else if (isOrbiting && energyManagement.isFullEnergised && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space has been pressed");
            //no funciona

            /*if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space has been pressed");
            }*/

        }
    }

    private void GenerateRandomDirection()
    {
        int randomNumber = Random.Range(0, 5);
        switch (randomNumber)
        {
            case 0:
                directionOfRotation = new Vector3(1, 1, 0);
                break;
            case 1:
                directionOfRotation = new Vector3(0, 0, 1);
                break;
            case 2:
                directionOfRotation = new Vector3(1, 0, 1);
                break;
            case 3:
                directionOfRotation = new Vector3(0, 1, 0);
                break;
            case 4:
                directionOfRotation = new Vector3(0, 1, 1);
                break;
            case 5:
                directionOfRotation = new Vector3(1, 0, 0);
                break;

        }
    }
}
