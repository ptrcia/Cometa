using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityField : MonoBehaviour
{
    public enum planetSize { small, medium, large }
    public planetSize optionPlanetSize;

    public float rotationSpeed;
    public float gravity;


    void Start()
    {
        PlanetSize(optionPlanetSize);
    }

    public void PlanetSize(planetSize planetSize)
    {
        switch (planetSize)
        {
            case planetSize.small:
                rotationSpeed = 50.0f;
                gravity = 3f;
                break;
            case planetSize.medium:
                rotationSpeed = 25.0f;
                gravity = 9.81f;
                break;
            case planetSize.large:
                rotationSpeed = 10.0f;
                gravity = 15f;
                break;
            default:
                rotationSpeed = 1.0f;
                gravity = 9.81f;
                break;
        }
    }

}
