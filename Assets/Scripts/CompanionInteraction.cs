using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionInteraction : MonoBehaviour
{
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
        }
    }

}
