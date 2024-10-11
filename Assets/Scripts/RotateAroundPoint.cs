using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    [SerializeField] public float rotationSpeed; 
    [SerializeField] public GameObject pivotObject;

    private Vector3 randomAxis;

    private void Start()
    {
        rotationSpeed = Random.Range(20, 50);
        randomAxis = Random.onUnitSphere; // Eje aleatorio para rotar alrededor de una esfera completa.

    }

    private void Update()
    {
        //transform.RotateAround(pivotObject.transform.position, new Vector3(0, 1, 0), rotationSpeed*Time.deltaTime);
        transform.RotateAround(pivotObject.transform.position, randomAxis, rotationSpeed * Time.deltaTime);

    }
}
