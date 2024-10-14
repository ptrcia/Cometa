using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instanciate;

    [SerializeField] GameObject player;


    EnergyManagement energyManagement;

    private void Awake()
    {
        if(instanciate == null)
        {
            instanciate = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        energyManagement = player.GetComponent<EnergyManagement>();  
    }

    public void GameOver()
    {
        Debug.Log("Energy is empty");
        //fading
        //UI texto y boton

    }
}
