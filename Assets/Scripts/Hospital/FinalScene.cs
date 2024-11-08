using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinalScene : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] GameObject dialogue;
    [SerializeField] GameObject button;
    [SerializeField] TextMeshProUGUI credits;
    [SerializeField] TMP_FontAsset fontDelius;
    [SerializeField] TMP_FontAsset fontPlaywrite;

    [Header("Images")]
    [SerializeField] GameObject newScene;
    [SerializeField] GameObject panelCredits;

    [Header("Footsteps")]
    [SerializeField] GameObject footstep1;
    [SerializeField] GameObject footstep2;
    [SerializeField] GameObject footstep3;
    [SerializeField] GameObject footstep4;
    [SerializeField] GameObject footstep5;
    [SerializeField] GameObject footstep6;
    [SerializeField] GameObject footstep7;
    [SerializeField] GameObject footstep8;
    [SerializeField] GameObject footstep9;
    [SerializeField] GameObject footstep10;
    [SerializeField] GameObject footstep11;
    [SerializeField] GameObject footstep12;


    [Header("Animations")]
    [SerializeField] Animator animOut;
    [SerializeField] Animator animIn;
    [SerializeField] Animator animCameraOut;
    [SerializeField] Animator animCameraIn;
    [SerializeField] Animator doorAnim;

    [Header("Audio")]
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip steps;
    [SerializeField] AudioClip door;

    bool isPressed = false;
    bool canPress = false;
    private string language;
    bool isEnded = false;


    private void Start()
    {
        language = PlayerPrefs.GetString("Language", "Spanish");

        StartCoroutine(SceneController());
        doorAnim.enabled = false;
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

        if (animOut != null)animOut.Play("ConsoleOut");
        if (animCameraOut != null)animCameraOut.Play("CameraOut");

        dialogue.SetActive(true);
        yield return new WaitForSeconds(5);
        button.SetActive(true);
        canPress = true;
        while (!isPressed) yield return null;


        Debug.Log("Ha pulsado la A");
        button.SetActive(false);
        dialogue.SetActive(false);

        yield return new WaitForSeconds(1);

        Debug.Log("Pasos");
        source.PlayOneShot(steps);

        #region Steps
        footstep1.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        footstep1.SetActive(false);
        footstep2.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        footstep2.SetActive(false);
        footstep3.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        footstep3.SetActive(false);
        footstep4.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        footstep4.SetActive(false);
        footstep5.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        footstep5.SetActive(false);
        footstep6.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        footstep6.SetActive(false);
        footstep7.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        footstep7.SetActive(false);
        footstep8.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        footstep8.SetActive(false);
        footstep9.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        footstep9.SetActive(false);
        footstep10.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        footstep10.SetActive(false);
        footstep11.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        footstep11.SetActive(false);
        footstep12.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        footstep12.SetActive(false);
        #endregion

        yield return new WaitForSeconds(0.5f);
        source.PlayOneShot(door);
        doorAnim.enabled = true; //animaicon de abrir
        yield return new WaitForSeconds(2.5f);

        Debug.Log("Dentro");

        if (animIn != null)animIn.Play("ConsoleIn");
        if (animCameraIn != null)animCameraIn.Play("CameraIn");

        newScene.SetActive(true);

        yield return new WaitForSeconds(5);
        panelCredits.SetActive(true);
        StartCoroutine(ChangeText2());

    }

    IEnumerator ChangeText2()
    {
        credits.fontSize = 60;
        credits.font = fontPlaywrite;
        credits.text = language == "Spanish" ? "Cometa" : "Cometa";
        yield return new WaitForSeconds(5);

        credits.fontSize = 50;
        credits.font = fontDelius;
        credits.text = language == "Spanish" ? "Patricia S. Gracia Artero\n2024" : "Patricia S. Gracia Artero\n2024";
        yield return new WaitForSeconds(5);

        credits.fontSize = 40;
        credits.text = language == "Spanish"
            ? "Todos los recursos utilizados y más información en Itch.io y Github"
            : "All resources used and more info on Itch.io and Github";
        yield return new WaitForSeconds(5);

        credits.text = language == "Spanish"
            ? "Gracias a Stega Academy y a mis compañeros"
            : "Thanks to Stega Academy and my teammates";
        yield return new WaitForSeconds(8);

        credits.text = language == "Spanish" ? "¡Gracias por jugar!" : "Thank you for playing!";

        isEnded = true;
        button.SetActive(true);
    }
}
