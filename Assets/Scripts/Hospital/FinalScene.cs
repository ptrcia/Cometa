using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class FinalScene : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] GameObject dialogue;
    [SerializeField] GameObject button;
    [SerializeField] TextMeshProUGUI credits;
    [SerializeField] TMP_FontAsset fontDelius;
    [SerializeField] TMP_FontAsset fontPlaywrite;

    [Header("Images")]
    [SerializeField] GameObject imgBackgroundConsole;
    [SerializeField] GameObject imgCometa;
    [SerializeField] GameObject panelCredits;

    [Header("Animations")]
    [SerializeField] Animator animOut;
    [SerializeField] Animator animIn;
    [SerializeField] Animator animCameraOut;
    [SerializeField] Animator animCameraIn;

    [Header("Audio")]
    [SerializeField] AudioClip steps;

    bool isPressed = false;
    bool canPress = false;
    bool isEnded = false;


    private void Start()
    {
        StartCoroutine(SceneController());
    }

    private void Update()
    {
        if (canPress && Input.GetKeyDown(KeyCode.A))
        {
            isPressed = true;
        }
        if(isEnded && Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene(0);
        }
    }
    IEnumerator SceneController()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Fuera");
        dialogue.SetActive(true);

        if (animOut != null)
        {
            animOut.Play("ConsoleOut");
        }

        if (animCameraOut != null)
        {
            animCameraOut.Play("CameraOut");
        }

        dialogue.SetActive(true);
        yield return new WaitForSeconds(5);
        button.SetActive(true);
        canPress = true;
        while (!isPressed)
        {
            yield return null;
        }

        Debug.Log("Ha pulsado la A");
        button.SetActive(false);
        dialogue.SetActive(false);

        yield return new WaitForSeconds(1);

        Debug.Log("Pasos");
        //AudioManager.instance.PlaySound(steps);
        yield return new WaitForSeconds(2);

        Debug.Log("Dentro");

        if (animIn != null)
        {
            animIn.Play("ConsoleIn");
        }
        if (animCameraIn != null)
        {
            animCameraIn.Play("CameraIn");
        }

        imgBackgroundConsole.SetActive(true);
        imgCometa.SetActive(true);

        yield return new WaitForSeconds(4);
        panelCredits.SetActive(true);
        StartCoroutine(ChangeText());

    }

    IEnumerator ChangeText()
    {
        credits.fontSize = 60;
        credits.font = fontPlaywrite;
        credits.text = "Cometa";
        yield return new WaitForSeconds(2);
        credits.fontSize = 50;
        credits.text = "Patricia S. Gracia Artero\n2024";
        credits.font = fontDelius;
        yield return new WaitForSeconds(2);
        credits.fontSize = 40;
        credits.text = "Thank you for playing!";


        isEnded = true;
        button.SetActive(false);

    }
}
