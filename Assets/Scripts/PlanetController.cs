using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public bool isLost;
    public bool isInvoked;
    public int firstAct;

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

        currentPlanet = planetInteraction.pivotObject.transform.parent.gameObject;

        foreach (GameObject obj in planetsOrbited)
        {
            Debug.Log("Planeta en lista: " + obj.name);
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

            //Spawn Special Planet
            if (!isInvoked)
            {
                //Invoke("GenerateSpecialPlanet", 5); //los demas fallos tiene qu ever con esto
                isInvoked = true;
            }

            //Make planets materials tranparent
            //StartCoroutine(FadePlanets());  //esto tampoco funciona 

            //Destroy Planets
            //planetInteraction.destructionTrigger.radius = 10;
        }
        text.text = "Planet count -> " + planetsOrbited.Count;
    }
    public void GenerateSpecialPlanet()
    {
        GameObject instance = Instantiate(specialPlanetFirstAct);
        instance.transform.position = player.transform.position + new Vector3(100, 0, 0);
    }
    IEnumerator FadePlanets()
    {
        GameObject[] planets = GameObject.FindGameObjectsWithTag("Planet");

        float fadeDuration = 3.0f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alphaValue = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); 
            foreach (GameObject planet in planets)
            {
                Renderer renderer = planet.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Color color = renderer.material.color;
                    color.a = alphaValue; 
                    renderer.material.color = color; 
                }
            }

            yield return null;
        }
    }
}
