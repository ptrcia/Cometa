using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    [SerializeField] public float rotationSpeed;
    [SerializeField] public GameObject pivotObject;

    private void Start()
    {
        rotationSpeed = Random.Range(20, 50);
    }

    private void Update()
    {
        transform.RotateAround(pivotObject.transform.position, new Vector3(0, 1, 0), rotationSpeed*Time.deltaTime);
    }
}
