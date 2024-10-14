using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public bool isLost;
    public bool isInvoked;
    public int firstAct;

    [SerializeField] int distanceSpecialPlanet;
    [SerializeField] PlanetGeneration planetGeneration;
    [SerializeField] PlanetInteraction planetInteraction;
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> planetsOrbited = new List<GameObject>();
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject specialPlanetFirstAct;


    private int inicialSphereCount;
    private float inicialDestroyRadius;
    private bool nameIsDifferent;
    private GameObject currentPlanet; // Planet currently being orbited


    private void Awake()
    {
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
    }
    public void PlanetCount()
    {
        nameIsDifferent = true;

        if (planetInteraction.pivotObject == null)
        {
            return;
        }
        if(planetInteraction == null)
        {
            return;
        }

        currentPlanet = planetInteraction.pivotObject.transform.parent.gameObject;

        foreach (GameObject obj in planetsOrbited)
        {
            Debug.Log("Planeta en lista: " + obj.name);
            if (obj == null)
            {
                Debug.LogWarning("El objeto ha sido destruido y será eliminado de la lista.");
                planetsOrbited.Remove(obj);
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




        if (planetsOrbited.Count == firstAct)
        {
            isLost = true;
            planetGeneration.sphereCount = 0;
            planetInteraction.isOrbiting = false;
            planetInteraction.isBeingAttracted = false; 

            //Spawn Special Planet
            if (!isInvoked)
            {
                Invoke("GenerateSpecialPlanet", 5);
                isInvoked = true;
            }

            //Make planets materials tranparent
            FadeAllPlanets(8f);

            //Delete satellites
            StartCoroutine(DeleteSatellites(5)); //deberia eliminarlos??????

            //Destroy Planets
            StartCoroutine(DestroyPlanets(10));

        }
        text.text = "Planet count -> " + planetsOrbited.Count;
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

    }
}

