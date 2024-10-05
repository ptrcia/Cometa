using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{ 
    public GameObject objectToFollow;
    public float cameraDistance = 10.0f;

    void LateUpdate()
    {
        transform.position = objectToFollow.transform.position - objectToFollow.transform.forward * cameraDistance;
        transform.LookAt(objectToFollow.transform.position);
        transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
    }
}
