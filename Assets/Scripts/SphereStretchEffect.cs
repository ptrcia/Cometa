using UnityEngine;

public class SphereStretchEffect : MonoBehaviour
{
    public float stretchFactor = 2.0f;  // Factor de estiramiento (cuánto se alarga)
    public float smoothness = 5.0f;     // Suavidad de la deformación
    private Mesh mesh;
    private Vector3[] originalVertices, deformedVertices;
    private Vector3 previousPosition;

    void Start()
    {
        // Obtener la malla original de la esfera
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        deformedVertices = new Vector3[originalVertices.Length];
        previousPosition = transform.position; // Almacena la posición inicial del objeto
    }

    void Update()
    {
        // Calcula la dirección de movimiento
        Vector3 velocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;

        // Si hay movimiento, aplicar la deformación
        if (velocity.magnitude > 0.01f)
        {
            ApplyStretch(-velocity.normalized);  // Deforma en la dirección opuesta al movimiento
        }
        else
        {
            // Si no hay movimiento, volver a la forma original
            ResetVertices();
        }
    }

    void ApplyStretch(Vector3 direction)
    {
        // Recorrer todos los vértices de la malla y deformarlos
        for (int i = 0; i < originalVertices.Length; i++)
        {
            // Calcula la dirección desde el centro hacia cada vértice
            Vector3 vertexDirection = originalVertices[i].normalized;

            // Proyecta el vértice en la dirección opuesta al movimiento
            float dotProduct = Vector3.Dot(vertexDirection, direction);
            if (dotProduct > 0)  // Si el vértice está en la parte "trasera"
            {
                // Estirar los vértices en la dirección opuesta
                deformedVertices[i] = originalVertices[i] + direction * stretchFactor * dotProduct;
            }
            else
            {
                // Mantener los vértices en la parte "frontal" sin deformación
                deformedVertices[i] = Vector3.Lerp(deformedVertices[i], originalVertices[i], Time.deltaTime * smoothness);
            }
        }

        // Actualiza la malla con los nuevos vértices
        mesh.vertices = deformedVertices;
        mesh.RecalculateNormals();  // Recalcular normales para mantener el sombreado correcto
        mesh.RecalculateBounds();   // Recalcular los límites de la malla
    }

    void ResetVertices()
    {
        // Volver a la forma original cuando no hay movimiento
        for (int i = 0; i < originalVertices.Length; i++)
        {
            deformedVertices[i] = Vector3.Lerp(deformedVertices[i], originalVertices[i], Time.deltaTime * smoothness);
        }

        // Actualiza la malla con los vértices originales
        mesh.vertices = deformedVertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
