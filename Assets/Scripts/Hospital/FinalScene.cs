using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScene : MonoBehaviour
{
    [SerializeField] GameObject dialogue;


    private void Start()
    {
        StartCoroutine(SceneController());
    }
    IEnumerator SceneController()
    {
        yield return new WaitForSeconds(2);
        //dejar la consola a un lado y mirar al medico
        dialogue.SetActive(true);
        yield return new WaitForSeconds(5);
        //el niño se va
        yield return new WaitForSeconds(2);
        //cam,biar la posicion de la camara a la consola y hace zoom
        yield return new WaitForSeconds(2);
        //la imagen cmabia y esta orbitando a un planeta 2 cometas
        yield return new WaitForSeconds(5);
        //salen los creditos a un lado

    }
}
