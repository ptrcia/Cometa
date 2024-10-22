using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionInteraction : MonoBehaviour
{
    public bool isCollidingCompanion = false;
    private GameObject companion;
    EnergyManagement energyManagement;
    PlanetInteraction planetInteracion;
    PlayerMovement playerMovement;

    private void Awake()
    {
        energyManagement = GetComponent<EnergyManagement>();
        planetInteracion = GetComponent<PlanetInteraction>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        FindCompanion();
    }

    private void FindCompanion()
    {
        companion = GameObject.FindGameObjectWithTag("Companion");

        if (companion == null) { return; }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CompanionEnergy")) //el collider
        {
            isCollidingCompanion = true;
            StartCoroutine(IncreaseEnergy());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CompanionEnergy")) //el collider
        {
            isCollidingCompanion = false;
        }
    }

    IEnumerator IncreaseEnergy()
    {
        WaitForSeconds wait = new WaitForSeconds(1);

        while (isCollidingCompanion)
        {
            if (energyManagement.currentEnergy < energyManagement.maxEnergy)
            {
                energyManagement.currentEnergy += 100 * Time.deltaTime;

                if (energyManagement.currentEnergy >= energyManagement.maxEnergy)
                {
                    energyManagement.currentEnergy = energyManagement.maxEnergy;
                    energyManagement.isFullEnergised = true;
                }
            }
            else
            {
                energyManagement.currentEnergy = energyManagement.maxEnergy;
                break;
            }

            yield return wait;
        }
    }


}
