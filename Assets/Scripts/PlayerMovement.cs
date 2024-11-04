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
    public bool canMove = true;
    private float inputVertical;
    private float inputHorizontal;


    [Header("Audio")]
    [SerializeField] private float minPitch = 0.5f; 
    [SerializeField] private float maxPitch = 2.0f;  
    [SerializeField] private float fadeSpeed = 2.0f; 
    [SerializeField] AudioClip accelerationSound;

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
        if(!canMove)
        {
            inputVertical = 0;
            inputHorizontal = 0;
        }
        else
        {
            inputVertical = Input.GetAxis("Vertical");
            inputHorizontal = Input.GetAxis("Horizontal");
        }

        if (inputVertical != 0 || inputHorizontal != 0)
        {
            currentSpeed += acceleration * Time.fixedDeltaTime;

            /*float targetVolume = Mathf.Lerp(AudioManager.instance.SFXSource.volume, 1f, Time.fixedDeltaTime * 2f);
            AudioManager.instance.SetSFXVolume(targetVolume);

            if (!AudioManager.instance.SFXSource.isPlaying)
            {
                AudioManager.instance.PlaySound(accelerationSound);
                AudioManager.instance.SFXSource.loop = true;
            }*/

        }
        else
        {
            currentSpeed -= deceleration * Time.fixedDeltaTime;         

            /*
            float targetVolume = Mathf.Lerp(AudioManager.instance.SFXSource.volume, 0.3f, Time.fixedDeltaTime * 2f);
            AudioManager.instance.SetSFXVolume(targetVolume);*/
        }

        if (currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
        else if (currentSpeed < 0)
        {
            currentSpeed = 0;
        }

        Vector3 velocity = transform.forward * currentSpeed;

        GetComponent<Rigidbody>().velocity = velocity;
    }
    public Vector3 GetVelocityVector()
    {
        return transform.forward * currentSpeed;
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
