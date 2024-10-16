using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [Header("First Act")]
    public int firstAct;
    public bool isLost;    
    public bool isSpecialPlanetEvoked;
    public bool isInvoked;
    [SerializeField] GameObject specialPlanetFirstAct;
    [SerializeField] int distanceSpecialPlanet;
    [SerializeField] GameObject companionPrefab;

    [Header("Planets Orbited")]
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] List<GameObject> planetsOrbited = new List<GameObject>();

     [SerializeField] PlanetGeneration planetGeneration;
     PlanetInteraction planetInteraction;

    private int inicialSphereCount;
    private float inicialDestroyRadius;
    private bool nameIsDifferent;
    private GameObject currentPlanet; // Planet currently being orbited


    private void Awake()
    {
        planetInteraction = player.GetComponent<PlanetInteraction>();
        player = GameObject.FindGameObjectWithTag("Player");
        isLost = false;
        isInvoked = false;
    }
    private void Start()
    {

        inicialSphereCount = planetGeneration.sphereCount;
        inicialDestroyRadius = planetInteraction.destructionTrigger.radius;
    }
    private void Update()
    {
        PlanetCount();
        FirstAct();
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

        //SECOND ACT
        //if()
        text.text = "Planet count -> " + planetsOrbited.Count;

        return planetsOrbited.Count;
    }

    public void FirstAct()
    {
        //FIRST ACT
        if (planetsOrbited.Count == firstAct)
        {
            isLost = true;
            planetGeneration.sphereCount = 0;
            //planetInteraction.isOrbiting = false;
           planetInteraction.isBeingAttracted = false;
            //planetInteraction.isOrbiting = true;
            //planetInteraction.isBeingAttracted = true;

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
        }
    }

    public void GenerateSpecialPlanet()
    {
        GameObject instance = Instantiate(specialPlanetFirstAct);
        instance.transform.position = player.transform.position + new Vector3
            (player.transform.position.x+ distanceSpecialPlanet, 0, 0);
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
        planetInteraction.destructionTrigger.radius = 10;

        planetInteraction.destructionTrigger.radius = 1000;
        planetInteraction.isOrbiting = false;

    }

    public void SpawnCompanion()
    {
        Instantiate(companionPrefab, Vector3.zero, Quaternion.identity);
    }
}

