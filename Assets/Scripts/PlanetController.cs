using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;

public class PlanetController : MonoBehaviour
{

    [Header("Planets Orbited")]
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] List<GameObject> planetsOrbited = new List<GameObject>();

    [Header("First Act")]
    public int firstAct;
    public bool isLost;
    public bool isSpecialPlanetEvoked;
    public bool isInvoked;
    [SerializeField] GameObject specialPlanetFirstAct;
    [SerializeField] int distanceSpawnSpecialPlanet;
    [SerializeField] GameObject companionPrefab;
    [SerializeField] int distanceMaintainSpecialPlanet = 800;
    [SerializeField] bool firstActEnded;
    private Vector3 lastPosition;
    private GameObject specialPlanet;


    [Header("Second Act")]
    [SerializeField] int secondAct;

    [SerializeField] PlanetGeneration planetGeneration;
    [SerializeField] EnergyManagement energyManagement;
     PlanetInteraction planetInteraction;

    private SphereCollider sphereColliderPlayer;
    private bool nameIsDifferent;
    private GameObject currentPlanet; // Planet currently being orbited

    private void Awake()
    {
        planetInteraction = player.GetComponent<PlanetInteraction>();
        sphereColliderPlayer = player.GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
        isLost = false;
        isInvoked = false;
        firstActEnded = false;
    }

    private void Update()
    {
        PlanetCount();
        FirstAct();
        KeepDistanceSpecialPlanet();

        if (firstActEnded)
        {
            isLost = false;
            planetGeneration.sphereCount = 15;
            planetInteraction.isBeingAttracted = true;
            SecondAct();
        }

    }
    public int PlanetCount()
    {
        nameIsDifferent = true;

        if (planetInteraction.pivotObject == null || planetInteraction == null)
        {
            return 0;
        }

        currentPlanet = planetInteraction.pivotObject.transform.parent.gameObject;

        for (int i = planetsOrbited.Count - 1; i >= 0; i--)
        {
            GameObject obj = planetsOrbited[i];

            if (obj == null)
            {
                planetsOrbited.RemoveAt(i);

                continue;
            }
            if (obj.name == "SpecialPlanetFirstAct")
            {
                continue;
            }
            if (obj == currentPlanet)
            {
                nameIsDifferent = false;
                break;
            }
        }

        if (planetInteraction.isOrbiting && nameIsDifferent)
        {
            planetsOrbited.Add(currentPlanet);
        }

        text.text = "Planet count -> " + planetsOrbited.Count;

        return planetsOrbited.Count;
    }

    private void FirstAct()
    {
        if (planetsOrbited.Count == firstAct)
        {
            isLost = true;
            planetGeneration.sphereCount = 0;
            planetInteraction.isBeingAttracted = false;


            //Spawn Special Planet
            if (!isInvoked)
            {
                Invoke("GenerateSpecialPlanet", 5);
                isSpecialPlanetEvoked = true;
                isInvoked = true;
            }

            //Make planets materials tranparent
           FadeAllPlanets(8f);

            //Delete satellites
            StartCoroutine(DeleteSatellites(5)); //deberia eliminarlos??????

            //Destroy Planets
            StartCoroutine(DestroyPlanets(10));

            //firstActEnded = true;
        }
    }

    private void SecondAct()
    {
        //if (planetsOrbited.Count == secondAct)

    }

    private void KeepDistanceSpecialPlanet()
    {
        if (specialPlanet != null && !energyManagement.companionExist)
        {
            specialPlanet.transform.position = player.transform.position + new Vector3(distanceMaintainSpecialPlanet, 0, 0);
            lastPosition = specialPlanet.transform.position;
        }
    }
    public void GenerateSpecialPlanet()
    {

        //Special Planet will be kept out of reach until the companion came.
        specialPlanet = Instantiate(specialPlanetFirstAct);

        if (specialPlanet == null) // Solo generar si no ha sido generado ya
        {
            specialPlanet = Instantiate(specialPlanetFirstAct);

            if (!energyManagement.companionExist)
            {
                // Mantén la distancia inicial
                specialPlanet.transform.position = player.transform.position + new Vector3(distanceMaintainSpecialPlanet, 0, 0);
                lastPosition = specialPlanet.transform.position;
            }
            else
            {
                // Si ya existe el companion, el planeta se mantiene en su última posición
                specialPlanet.transform.position = lastPosition;
            }
        }
        //specialPlanet.transform.position = player.transform.position + new Vector3(player.transform.position.x+ distanceSpawnSpecialPlanet, 0, 0);
    }

    public IEnumerator DeleteSatellites(int secondsToWait)
    {
        
        GameObject[] satellites = GameObject.FindGameObjectsWithTag("Satellite");
        if (satellites.Length == 0)
        {
            yield break; 
        }

        foreach (GameObject satellite in satellites)
        {
            Destroy(satellite);
            yield return new WaitForSeconds(secondsToWait);
        }
        
    }
    public void FadeAllPlanets(float duration)
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");
        if (planets.Length == 0)
        {
            return;
        }

        foreach (GameObject planet in planets)
        {
            StartCoroutine(FadeToTranslucent(planet, duration));
        }
    }

    private IEnumerator FadeToTranslucent(GameObject planet, float fadeDuration)
    {
        Material planetMaterial = planet.GetComponentInParent<Renderer>().material;
        Color originalColor = planetMaterial.color;
        float startAlpha = originalColor.a;
        float targetAlpha = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);

            planetMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

            yield return null;
        }

        planetMaterial.color = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);
    }

    private IEnumerator DestroyPlanets(int secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        sphereColliderPlayer.radius = 0.5f;

        yield return new WaitForSeconds(secondsToWait-7);

        sphereColliderPlayer.radius = 1000f;
        planetInteraction.isOrbiting = false;
    }

    public void SpawnCompanion()
    {
        Instantiate(companionPrefab, Vector3.zero, Quaternion.identity);
    }
}

