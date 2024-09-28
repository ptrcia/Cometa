using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedRot;

    public float maxSpeed;
    public float acceleration;
    public float deceleration;
    public float currentSpeed;


    private void Update()
    {
        if (Time.timeScale == 0) {
            return; 
        }

        transform.Rotate(Vector3.up * speedRot * Input.GetAxis("Mouse X"));
        transform.Rotate(Vector3.right * -speedRot * Input.GetAxis("Mouse Y"));

        Vector3 rot = transform.rotation.eulerAngles;
        rot.z = 0;

        if (rot.x > 50 && rot.x < 180){
            rot.x = 50; 
        }
        if (rot.x < 310 && rot.x > 180){
            rot.x = 310; 
        }

        transform.rotation = Quaternion.Euler(rot);

    }
    private void FixedUpdate()
    {

        // Input del jugador
        float inputVertical = Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");

        // Aceleraci�n cuando hay input
        if (inputVertical != 0 || inputHorizontal != 0)
        {
            currentSpeed += acceleration * Time.fixedDeltaTime;
        }
        else
        {
            // Desaceleraci�n cuando no hay input
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

        // Mover el objeto seg�n la velocidad actual
        Vector3 velocity = transform.forward * currentSpeed +
                           transform.right * currentSpeed;

        GetComponent<Rigidbody>().velocity = velocity;
    }
}
