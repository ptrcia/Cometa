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
    //public bool isSpecialPlanetEvoked;
    public bool isInvoked;
    [SerializeField] GameObject specialPlanetFirstAct;
    [SerializeField] int distanceSpawnSpecialPlanet;
    [SerializeField] GameObject companionPrefab;
    [SerializeField] int distanceMaintainSpecialPlanet = 800;
    public bool firstActEnded;
    private Vector3 lastPosition;
    private GameObject specialPlanet;


    [Header("Second Act")]
    [SerializeField] int secondAct;
    [SerializeField] bool secondActPrepared;
    [SerializeField] bool secondActStarted;

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
        secondActPrepared = false;
        secondAct = 5;
    }

    private void Update()
    {
        Debug.Log("orbited: " + planetsOrbited.Count + "/" + secondAct);
        PlanetCount();
        FirstAct();
        KeepDistanceSpecialPlanet();

        if (firstActEnded && !secondActPrepared)
        {
            Debug.Log("Road to segundo acto");
            
            isLost = false;
            planetGeneration.canGenerate = true;
            planetGeneration.Generate();
            Debug.Log(planetGeneration.sphereCount);
            secondActPrepared = true;
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
           /* if (obj.name == "SpecialPlanetFirstAct")
            {
                continue;
            }*/
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
            //planetGeneration.sphereCount = 0;
            planetGeneration.canGenerate = false;

            //Spawn Special Planet
            if (!isInvoked)
            {
                Invoke("GenerateSpecialPlanet", 5);
                //isSpecialPlanetEvoked = true;
                isInvoked = true;
            }

            //PENSAR 
            //Make planets materials tranparent
           //FadeAllPlanets(8f);

            //Delete satellites & planets
            //StartCoroutine(DeleteSatellites(5));

            //The change to Act2 is controlled by PlanetInteraction Collidar

        }
    }

    private void SecondAct()
    {

        //spawn planeta raro
        if (planetsOrbited.Count == secondAct) //??
        {
            Debug.Log("Evento Segundo Acto");
            

        }


    }
    public void DestroyAllPlanets()
    {
        List<GameObject> planetsToRemove = new List<GameObject>();

        foreach (GameObject planet in planetGeneration.generatedPlanets)
        {
            if (planet != null)
            {
                Destroy(planet);
                planetsToRemove.Add(planet);
            }
        }

        foreach (GameObject planet in planetsToRemove)
        {
            planetGeneration.generatedPlanets.Remove(planet);
        }
        planetGeneration.generatedPlanets.RemoveAll(planet => planet == null);

        Debug.Log("Todos los planetas han sido destruidos.");
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
        planetInteraction.holdSpace.SetActive(false);
        planetInteraction.releaseSpace.SetActive(false);
        DestroyAllPlanets();
        
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

    //esto no funciona porque los creo después
    private IEnumerator AppearFromTranslucent(GameObject planet, float fadeDuration)
    {
        Material planetMaterial = planet.GetComponentInParent<Renderer>().material;
        Color originalColor = planetMaterial.color;
        float startAlpha = originalColor.a;
        float targetAlpha = 1f;
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

    public void SpawnCompanion()
    {
        Instantiate(companionPrefab, Vector3.zero, Quaternion.identity);
    }
}

