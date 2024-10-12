using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public bool isLost;
    public int firstAct;

    [SerializeField] PlanetGeneration planetGeneration;
    [SerializeField] PlanetInteraction planetInteraction;
    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> planetsOrbited = new List<GameObject>(); 
    [SerializeField] TextMeshProUGUI text;

    private int inicialSphereCount;
    private float inicialDestroyRadius;
    private bool nameIsDifferent;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isLost = false;
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
        foreach (GameObject obj in planetsOrbited)
        {
            if (obj.name == planetInteraction.pivotObject.name)
            {
                nameIsDifferent = false;
                break;
            }
        }

        if (planetInteraction.isOrbiting && nameIsDifferent)
        {
            planetsOrbited.Add(planetInteraction.pivotObject);
        }

        if(planetsOrbited.Count > firstAct)
        {
            isLost = true;
            //planetGeneration.sphereCount = 0;

            //spawn Special Planet

            //hacer que el material de todos los planetas se vaya haciendo transparente

            //destruitlos planetas
            //planetInteraction.destructionTrigger.radius = 10;
        }

        text.text = "Planet count -> " + planetsOrbited.Count;
    }
}
