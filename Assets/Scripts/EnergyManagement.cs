using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EnergyManagement : MonoBehaviour
{
    [Header("Energy")]
    public float currentEnergy = 50;
    public float maxEnergy = 100;
    public bool isFullEnergised;
    public float energyRate = 1;


    [Header("Companion")]
    public bool companionExist;

    [Header("UI")]
    [SerializeField] Slider energySlider;
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] Image fillImageSlider;

    PlanetInteraction planetInteraction;
    PlayerMovement playerMovement;
    CompanionInteraction companionInteraction;
    [SerializeField] PlanetController planetController;


    private void Awake()
    {
        planetInteraction = GetComponent<PlanetInteraction>();
        playerMovement = GetComponent<PlayerMovement>();
        companionInteraction = GetComponent<CompanionInteraction>();
    }
    private void Start()
    {
        isFullEnergised = false;
    }

    private void Update()
    {
        Energy();
        energyText.text = "Energy -> " + (int)currentEnergy;
        energySlider.value = currentEnergy;

    }
    public void Energy()
    {
        if (planetInteraction.isOrbiting)
        {
            if (!isFullEnergised)
            {
                StartCoroutine(IncreaseEnergy());
            }
        }
        else 
        {
            if (currentEnergy > 0)
            {
                playerMovement.canMove = true;
                StartCoroutine(DecreaseEnergy());

            }

            //INVOKED TODO BIEN???????????
            else if (currentEnergy <= 0 && !planetController.isInvoked)
            {
                //GameManager.instanciate.GameOver();
                Debug.Log("It's game over.");
            }
            else if (currentEnergy <= 0 && planetController.isInvoked)
            {
                playerMovement.canMove = false;               
                if(!companionExist)
                {
                    companionExist = true;
                    planetController.SpawnCompanion();
                    energyRate = 0;
                    //IMPORTANTE
                }
            }
        }

        IEnumerator DecreaseEnergy()
        {
            while (!planetInteraction.isOrbiting && currentEnergy > 0)
            {
                currentEnergy -= energyRate * Time.deltaTime * (playerMovement.currentSpeed * 0.01f);

                // New Alpha
                float newAlpha = Mathf.Clamp(currentEnergy / maxEnergy, 0, 1);
                fillImageSlider.color = new Color(1, 1, 1, newAlpha);  // Usamos 1 para RGB y el alpha dinámico

                yield return new WaitForSeconds(1);
                isFullEnergised = false;
            }

        }

        IEnumerator IncreaseEnergy()
        {

            WaitForSeconds wait = new WaitForSeconds(1);

            while (planetInteraction.isOrbiting && currentEnergy < maxEnergy)
            {
                currentEnergy += energyRate * Time.deltaTime;
                //Debug.Log("currentEnergy: " + currentEnergy + "// energyRate: " + energyRate + "// time: " + Time.deltaTime);
                //imprimir

                //alpha based on the actual energy value
                float newAlpha = Mathf.Clamp(currentEnergy / maxEnergy, 0, 1);
                fillImageSlider.color = new Color(1, 1, 1, newAlpha);


                yield return wait;
            }

            if (currentEnergy >= maxEnergy)
            {
                currentEnergy = maxEnergy;
                isFullEnergised = true;
                fillImageSlider.color = new Color(1, 1, 1, 1);
            }

        }

    }
}
