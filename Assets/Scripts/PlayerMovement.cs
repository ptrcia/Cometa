using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speedRot;
    [SerializeField] float maxSpeed;
    [SerializeField] float acceleration;
    [SerializeField] float deceleration;
    public float currentSpeed;

    PlanetInteraction planetInteraction;

    private void Awake()
    {
        planetInteraction = GetComponent<PlanetInteraction>();
    }

    private void Update()
    {
        if (Time.timeScale == 0) {
            return; 
        }

        RotateView();

        if (!planetInteraction.isOrbiting)
        {
            Move();
        }

    }

    private void Move()
    {
        // Input del jugador
        float inputVertical = Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");

        // Aceleración cuando hay input
        if (inputVertical != 0 || inputHorizontal != 0)
        {
            currentSpeed += acceleration * Time.fixedDeltaTime;
        }
        else
        {
            // Desaceleración cuando no hay input
            currentSpeed -= deceleration * Time.fixedDeltaTime;
        }

        // Limitar velocidad
        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
        else if (currentSpeed < 0)
        {
            currentSpeed = 0;
        }

        // Mover el objeto según la velocidad actual
        Vector3 velocity = transform.forward * currentSpeed;

        GetComponent<Rigidbody>().velocity = velocity;
    }

    private void RotateView()
    {
        transform.Rotate(Vector3.up * speedRot * Input.GetAxis("Mouse X"));
        transform.Rotate(Vector3.right * -speedRot * Input.GetAxis("Mouse Y"));

        Vector3 rot = transform.rotation.eulerAngles;
        rot.z = 0;

        if (rot.x > 50 && rot.x < 180)
        {
            rot.x = 50;
        }
        if (rot.x < 310 && rot.x > 180)
        {
            rot.x = 310;
        }

        transform.rotation = Quaternion.Euler(rot);
    }

}
