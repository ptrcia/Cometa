using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public enum planetSize { small, medium, large }
    public planetSize optionPlanetSize;

    public float rotationSpeed;
    public float gravity;

    public int satellites;
    void Start()
    {
        PlanetSize(optionPlanetSize);
    }
    public void PlanetSize(planetSize planetSize)
    {
        switch (planetSize)
        {
            case planetSize.small:
                rotationSpeed = 80.0f;
                gravity = 10000f;
                break;
            case planetSize.medium:
                rotationSpeed = 60.0f;
                gravity = 20000f;
                break;
            case planetSize.large:
                rotationSpeed = 50.0f;
                gravity = 30000f;
                break;
            default:
                rotationSpeed = 50.0f;
                gravity = 10000f;
                break;
        }
    }
}
