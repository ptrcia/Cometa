using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    [SerializeField] float smoothSpeed  = 0.3f;

    private GameObject player;
    private Vector3 offset;
    private Vector3 targetPosition;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        StartCoroutine(UpdateOffsetRandomly());
    }
    void FixedUpdate()
    {
        targetPosition = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }

    IEnumerator UpdateOffsetRandomly()
    {
        while (true)
        {
            offset = new Vector3(
                Random.Range(-5, 5), 
                Random.Range(-5f, 5f), 
                Random.Range(-5, 5));
            yield return new WaitForSeconds(Random.Range(1f, 5f));
        }
    }

}
