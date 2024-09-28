using UnityEngine;

public class SphereStretchEffect : MonoBehaviour
{
    public float stretchFactor = 2.0f;  // Factor de estiramiento (cu�nto se alarga)
    public float smoothness = 5.0f;     // Suavidad de la deformaci�n
    private Mesh mesh;
    private Vector3[] originalVertices, deformedVertices;
    private Vector3 previousPosition;

    void Start()
    {
        // Obtener la malla original de la esfera
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = mesh.vertices;
        deformedVertices = new Vector3[originalVertices.Length];
        previousPosition = transform.position; // Almacena la posici�n inicial del objeto
    }

    void Update()
    {
        // Calcula la direcci�n de movimiento
        Vector3 velocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;

        // Si hay movimiento, aplicar la deformaci�n
        if (velocity.magnitude > 0.01f)
        {
            ApplyStretch(-velocity.normalized);  // Deforma en la direcci�n opuesta al movimiento
        }
        else
        {
            // Si no hay movimiento, volver a la forma original
            ResetVertices();
        }
    }

    void ApplyStretch(Vector3 direction)
    {
        // Recorrer todos los v�rtices de la malla y deformarlos
        for (int i = 0; i < originalVertices.Length; i++)
        {
            // Calcula la direcci�n desde el centro hacia cada v�rtice
            Vector3 vertexDirection = originalVertices[i].normalized;

            // Proyecta el v�rtice en la direcci�n opuesta al movimiento
            float dotProduct = Vector3.Dot(vertexDirection, direction);
            if (dotProduct > 0)  // Si el v�rtice est� en la parte "trasera"
            {
                // Estirar los v�rtices en la direcci�n opuesta
                deformedVertices[i] = originalVertices[i] + direction * stretchFactor * dotProduct;
            }
            else
            {
                // Mantener los v�rtices en la parte "frontal" sin deformaci�n
                deformedVertices[i] = Vector3.Lerp(deformedVertices[i], originalVertices[i], Time.deltaTime * smoothness);
            }
        }

        // Actualiza la malla con los nuevos v�rtices
        mesh.vertices = deformedVertices;
        mesh.RecalculateNormals();  // Recalcular normales para mantener el sombreado correcto
        mesh.RecalculateBounds();   // Recalcular los l�mites de la malla
    }

    void ResetVertices()
    {
        // Volver a la forma original cuando no hay movimiento
        for (int i = 0; i < originalVertices.Length; i++)
        {
            deformedVertices[i] = Vector3.Lerp(deformedVertices[i], originalVertices[i], Time.deltaTime * smoothness);
        }

        // Actualiza la malla con los v�rtices originales
        mesh.vertices = deformedVertices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}
