using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScene : MonoBehaviour
{
    [SerializeField] GameObject dialogue;
    [SerializeField] Animator animOut;
    [SerializeField] Animator animIn;
    [SerializeField] Animator animCameraOut;
    [SerializeField] Animator animCameraIn;


    private void Awake()
    {
        animOut = GetComponent<Animator>();
        animIn = GetComponent<Animator>();
        animCameraOut = GetComponent<Animator>();
        animCameraIn = GetComponent<Animator>();


    }

    private void Start()
    {
        StartCoroutine(SceneController());
    }
    IEnumerator SceneController()
    {
        yield return new WaitForSeconds(2);

        if (animOut != null)
        {
            animOut.Play("ConsoleOut");
        }

        if (animCameraOut != null)
        {
            animCameraOut.Play("ConsoleOut");
        }

        dialogue.SetActive(true);

        yield return new WaitForSeconds(5);
        //el niño se va
        yield return new WaitForSeconds(2);

        if (animOut != null)
        {
            animOut.Play("ConsoleIn");
        }
        if (animCameraIn != null)
        {
            animCameraIn.Play("ConsoleIn");
        }


        yield return new WaitForSeconds(2);
        //la imagen cmabia y esta orbitando a un planeta 2 cometas
        yield return new WaitForSeconds(5);
        //salen los creditos a un lado

    }
}
