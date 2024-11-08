//player = GameObject.FindGameObjectWithTag("Player");
//DistanceMantainer


using UnityEngine;

public class DistanceMantainer : MonoBehaviour
{
    public GameObject player;
    public float minDistancePercentage = 0.8f; // 80% de la distancia inicial
    private float initialDistance;
    private float minDistance;
    private Vector3 previousPlayerPosition;
    private float timeApproaching;
    private float timeThreshold = 3.0f; // Tiempo en segundos
    private bool isApproaching = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // Calcula la distancia inicial
        initialDistance = Vector3.Distance(transform.position, player.transform.position);
        minDistance = initialDistance * minDistancePercentage;
        previousPlayerPosition = player.transform.position;
        Debug.Log("Initial Distance: " + initialDistance);
        Debug.Log("Minimum Distance (80%): " + minDistance);
    }

    private void Update()
    {
        if (player == null) return;

        // Calcula la distancia actual con el player
        float currentDistance = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log("Current Distance: " + currentDistance);

        // Verifica si el jugador se ha acercado más del 20%
        if (currentDistance <= minDistance)
        {
            // Compara la posición actual del jugador con la posición anterior para determinar si se está acercando
            Vector3 directionToPlayer = (player.transform.position - previousPlayerPosition).normalized;
            float dotProduct = Vector3.Dot(directionToPlayer, (transform.position - player.transform.position).normalized);

            if (dotProduct > 0)
            {
                timeApproaching += Time.deltaTime;
                isApproaching = true;
                Debug.Log("Approaching Time: " + timeApproaching);
            }
            else
            {
                timeApproaching = 0f;
                isApproaching = false;
                Debug.Log("Player not approaching");
            }

            // Si el jugador ha estado acercándose por más de 'timeThreshold' segundos, reajustar la posición
            if (timeApproaching >= timeThreshold)
            {
                Vector3 direction = (transform.position - player.transform.position).normalized;
                transform.position = player.transform.position + direction * initialDistance;
                timeApproaching = 0f; // Resetear el contador de tiempo
                Debug.Log("Position Adjusted");
            }
        }
        else
        {
            timeApproaching = 0f; // Resetear el contador de tiempo si el jugador se aleja
            Debug.Log("Player too far");
        }

        // Actualiza la posición anterior del jugador
        previousPlayerPosition = player.transform.position;

        // Mantén la distancia inicial si el jugador no se acerca más del 20%
        if (!isApproaching)
        {
            Vector3 directionToPlayer = (transform.position - player.transform.position).normalized;
            transform.position = player.transform.position + directionToPlayer * initialDistance;
            Debug.Log("Maintaining Distance");
        }
    }
}
