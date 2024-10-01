using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergyManagement : MonoBehaviour
{
    [Header("Energy")]
    [SerializeField] float currentEnergy = 50;
    [SerializeField] float maxEnergy = 100;
    [SerializeField] float energyRate = 1; 

    public bool isFullEnergised;
    [SerializeField] TextMeshProUGUI energyText;

    PlanetInteraction planetInteraction;
    PlayerMovement playerMovement;


    private void Awake()
    {
        planetInteraction = GetComponent<PlanetInteraction>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        isFullEnergised = false;
    }

    private void Update()
    {
        Energy();
        energyText.text = "Energy -> " + (int)currentEnergy;

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
                StartCoroutine(DecreaseEnergy());
            }else if (currentEnergy <= 0)
            {
                //Game over
                Debug.Log("Energy is empty");
            }
        }

    }

    IEnumerator DecreaseEnergy()
    {
        while (!planetInteraction.isOrbiting && currentEnergy > 0)
        {
            currentEnergy -= energyRate*Time.deltaTime * (playerMovement.currentSpeed * 0.01f);
            yield return new WaitForSeconds(1);
            isFullEnergised = false;
        }

    }
    IEnumerator IncreaseEnergy()
    {
        while (planetInteraction.isOrbiting && currentEnergy < maxEnergy)
        {
            currentEnergy += energyRate * Time.deltaTime;
            yield return new WaitForSeconds(1);
        }

        if (currentEnergy >= maxEnergy)
        {
            currentEnergy = maxEnergy;
            isFullEnergised = true;
        }

    }
}
