using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject pivotObject;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.RotateAround(pivotObject.transform.position, new Vector3(0, 1, 0), rotationSpeed*Time.deltaTime);
    }
}
