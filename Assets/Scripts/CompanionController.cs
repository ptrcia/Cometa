using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        StartCoroutine(UpdateOffsetRandomly());
        //quiero qu evaya con suavidad
    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }

    IEnumerator UpdateOffsetRandomly()
    {
        while (true)
        {
            offset = new Vector3(
                Random.Range(-10f, 10f), 
                Random.Range(-5f, 5f), 
                Random.Range(-10f, 10f));
            yield return new WaitForSeconds(Random.Range(1f, 5f));
        }
    }

}
