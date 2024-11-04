using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPLanets : MonoBehaviour
{
    [SerializeField] GameObject player;
    private float boundarySize = 1000;
    private void Start()
    {
        player = this.gameObject;
    }
    void Update()
    {
        DestroyDistantPlanets(player, boundarySize); // 'player' es el objeto principal
    }
    void DestroyDistantPlanets(GameObject player, float distanceThreshold)
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

        foreach (GameObject planet in planets)
        {
            float distance = Vector3.Distance(planet.transform.position, player.transform.position);

            if (distance > distanceThreshold)
            {
                Destroy(planet);
            }

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 boundaryCenter = transform.position;

        Gizmos.DrawWireCube(boundaryCenter, new Vector3(boundarySize * 2, boundarySize * 2, boundarySize * 2));
    }
}
