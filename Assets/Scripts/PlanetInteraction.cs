using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetInteraction : MonoBehaviour
{
    [SerializeField] GameObject pivotObject = null;
    [SerializeField] Vector3 directionOfRotation;
    
    public bool isOrbiting = false; // Para saber si el jugador está en órbita

    PlayerMovement playerMovement;
    CameraFollow cameraFollow;
    Rigidbody rb;
    EnergyManagement energyManagement;
    GravityField gravityField;
    

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
        rb = GetComponent<Rigidbody>();
        energyManagement = GetComponent<EnergyManagement>();
        gravityField = GetComponent<GravityField>();
    }

    private void Start()
    {
        directionOfRotation = new Vector3(0, 0, 1);  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            //Hacer que vaya cambiando la dirección!!!
            GenerateRandomDirection();

            pivotObject = collision.gameObject; // Guardar el planeta como objeto pivote
            Debug.Log("Player collision with: " + pivotObject.transform.parent.name);

            playerMovement.enabled = false;
            playerMovement.currentSpeed = 0;
            
            rb.useGravity = false;
            rb.velocity = Vector3.zero;

            //cameraFollow.objectToFollow = pivotObject;  //ya veremos
            
            isOrbiting = true; // Activar estado de orbitar
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            cameraFollow.objectToFollow = this.gameObject;
            playerMovement.enabled = true;

            pivotObject = null;
            isOrbiting = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlanetAttractionField")) //el collider
        {
            //añadirle campo de atracion gravitacional
            //no funciona

            pivotObject = other.gameObject;
            //Debug.Log("Player enter the gravity field of the: " + pivotObject.transform.parent.name);

            ProcessGravity();
        }
    }

    private void Update()
    {
        if (isOrbiting && pivotObject != null)
        {
            //Debug.Log("Pivote: "+ pivotObject.name + " del " + pivotObject.transform.parent.name);

            transform.RotateAround(pivotObject.transform.position, directionOfRotation, 
                (pivotObject.transform.parent.GetComponent<GravityField>().rotationSpeed 
                + playerMovement.currentSpeed)  * Time.deltaTime);// Rotar alrededor del planeta en cada frame
        }

        if (isOrbiting && energyManagement.isFullEnergised)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Mantiene pulsado: " + KeyCode.Space);
                //cameraFollow.cameraDistance = 20f;
                //cameraFollow.objectToFollow = this.gameObject;
                pivotObject.transform.parent.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                Debug.Log("Suelta: " + KeyCode.Space);
                //cameraFollow.cameraDistance = 20f;
                //rb.AddForce(new Vector3(0, 0, transform.position.z * 500)); //no funciona
                rb.AddForce(transform.forward * 500f, ForceMode.Force);

                pivotObject.transform.parent.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    void ProcessGravity()
    {
         Vector3 diff = transform.position - pivotObject.transform.position;
         rb.AddForce(-diff.normalized * pivotObject.transform.parent.GetComponent<GravityField>().gravity * (rb.mass)); //gravity
         Debug.DrawRay(transform.position, diff.normalized, Color.yellow);
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
