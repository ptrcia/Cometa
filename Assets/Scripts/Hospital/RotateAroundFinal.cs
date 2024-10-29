using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateAroundFinal : MonoBehaviour
{
    public  float rotationSpeed;
    public GameObject pivotObject;
    public Vector3 axis;

    private void Update()
    {
        transform.RotateAround(pivotObject.transform.position, axis, rotationSpeed * Time.deltaTime);
    }
}
