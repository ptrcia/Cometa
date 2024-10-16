using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CompanionInteraction : MonoBehaviour
{
    bool isCollidingCompanion= false;
    private GameObject companion;
    EnergyManagement energyManagement;

    private void Awake()
    {
        energyManagement = GetComponent<EnergyManagement>();
    }
    private void Update()
    {
        FindCompanion();
    }

    private void FindCompanion()
    {
        companion = GameObject.FindGameObjectWithTag("Companion");
        if (companion == null)
        {
            return;
        }
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
            energyManagement.currentEnergy += 100 * Time.deltaTime;
            //imprimir
            yield return wait;
        }

        if (energyManagement.currentEnergy >= energyManagement.maxEnergy)
        {
            energyManagement.currentEnergy = energyManagement.maxEnergy;
            energyManagement.isFullEnergised = true;
            //UI iluminar y pulsar barra
        }

    }


}
