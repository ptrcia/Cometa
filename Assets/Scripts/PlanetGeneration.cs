using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlanetGeneration : MonoBehaviour
{
    public int sphereCount;
    public int satelliteCount;
    public int maxRadius;
    public GameObject[] spheres;
    public Material[] matsPlanets;
    public Material[] matsSatellites;
    public Material[] trailMat;


    [SerializeField] GameObject player;
    private void Awake()
    {
        spheres = new GameObject[sphereCount];
    }
    private void Start()
    {
        spheres = CreateSpheres(sphereCount, maxRadius);
    }
    public GameObject[] CreateSpheres(int count, int radius)
    {
        var sphs = new GameObject[count];
        var sphereToCopy = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        for (int i = 0; i< count; i++)
        {
            var sp = GameObject.Instantiate(sphereToCopy);
            CreateSatellites(sp);

            //Name
            sp.name = "Planet - " + i.ToString();

            //Position
            sp.transform.position = player.transform.position +
                new Vector3(Random.Range(-maxRadius, maxRadius), //X
                //Random.Range(-10, 10), // y altura
                Random.Range(-maxRadius, maxRadius),
                Random.Range(-maxRadius, maxRadius)); //z

            //Scale
            sp.transform.localScale *= Random.Range(5, 20);

            //Collider
            SphereCollider collider = sp.GetComponent<SphereCollider>();

            //Material
            sp.GetComponent<Renderer>().material = matsPlanets[Random.Range(0, matsPlanets.Length)];

            //Gravity
            GravityField gravityField = sp.AddComponent<GravityField>();
            gravityField.optionPlanetSize = (GravityField.planetSize)Random.Range(0, 3);
            gravityField.PlanetSize(gravityField.optionPlanetSize);

            //Colliders Children
            GameObject orbitSphere = new GameObject("ColliderOrbital");
            orbitSphere.transform.SetParent(sp.transform);
            orbitSphere.transform.localPosition = Vector3.zero;
            orbitSphere.transform.localRotation = Quaternion.identity;
            orbitSphere.tag = "Planet";
            SphereCollider colliderOrbit = orbitSphere.AddComponent<SphereCollider>();
            colliderOrbit.isTrigger = false;
            colliderOrbit.radius = Random.Range(collider.radius +10, collider.radius +20);


            GameObject attractionSphere = new GameObject("ColliderAtraction");
            attractionSphere.transform.SetParent(sp.transform);
            attractionSphere.transform.localPosition = Vector3.zero;
            attractionSphere.transform.localRotation = Quaternion.identity;
            attractionSphere.tag = "PlanetAttractionField";
            SphereCollider colliderAtraction = attractionSphere.AddComponent<SphereCollider>();
            colliderAtraction.isTrigger = true;
            colliderAtraction.radius = Random.Range(collider.radius + 30, collider.radius + 60);     

        }

        //GameObject.Destroy(sphereToCopy);
        GameObject.Destroy(sphereToCopy);
        Debug.Log("Sphere to Copy about to be destroyed: " + sphereToCopy.name);


        return spheres;
    }
    void CreateSatellites(GameObject planet)
    {
        satelliteCount = Random.Range(1, 6);

        for (int i = 0; i < satelliteCount; i++)
        {
            GameObject satellite = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            satellite.transform.SetParent(planet.transform);
            //Name
            satellite.name = "Satellite - " + i.ToString();

            //Position
            Vector3 satellitePosition = Random.onUnitSphere * Random.Range(1.0f, 3.0f);  // Distancia aleatoria desde el planeta
            satellite.transform.localPosition = satellitePosition;

            //Scale
            satellite.transform.localScale = Vector3.one * Random.Range(0.1f, 0.2f);

            //RotateAroundPoint
            RotateAroundPoint rotateAroundPoint = satellite.AddComponent<RotateAroundPoint>();
            rotateAroundPoint.pivotObject = satellite.transform.parent.gameObject;

            //Material
            //satellite.GetComponent<Renderer>().material = matsSatellites[Random.Range(0, matsPlanets.Length)];

            satellite.GetComponent<Renderer>().material = matsSatellites[Random.Range(0, matsSatellites.Length)];

            //Trail
            TrailRenderer tr = satellite.AddComponent<TrailRenderer>(); //adcomponent o getcomoponent?
            tr.time = 1;
            tr.startWidth = 0.1f;
            tr.endWidth = 0;
            tr.material = trailMat[Random.Range(0, trailMat.Length)];
            tr.startColor = new Color(1, 1, 0, 0.1f);
            tr.endColor = new Color(0, 0, 0, 0);
            tr.startWidth = 5;
            tr.endWidth = 0;

        }
    }
}
