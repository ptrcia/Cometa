using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateAroundFinal : MonoBehaviour
{
    [SerializeField]  float rotationSpeed;
    [SerializeField]  GameObject pivotObject;

    private Vector3 randomAxis;

    private void Start()
    {
        randomAxis = new Vector3(0, 0,1);
        //randomAxis = Random.onUnitSphere;
    }

    private void Update()
    {
        transform.RotateAround(pivotObject.transform.position, randomAxis, rotationSpeed * Time.deltaTime);
    }
}
