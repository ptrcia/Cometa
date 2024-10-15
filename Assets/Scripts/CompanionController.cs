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
    }

    void Update()
    {
        transform.position = player.transform.position + offset;
    }

    IEnumerator UpdateOffsetRandomly()
    {
        while (true)
        {
            offset = new Vector3(Random.Range(-2f, 2f), Random.Range(-1f, 1f), Random.Range(-2f, 2f));
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }

}
