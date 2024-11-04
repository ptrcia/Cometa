using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class TranslatorScene0 : MonoBehaviour
{
    string language;

    [Header("Main Menu")]
    [SerializeField] TextMeshProUGUI start;
    [SerializeField] TextMeshProUGUI sound;
    [SerializeField] TextMeshProUGUI controls;
    [SerializeField] TextMeshProUGUI exit;

    [Header("Controls")]
    [SerializeField] TextMeshProUGUI accelerate;
    [SerializeField] TextMeshProUGUI prepareImpulse;
    [SerializeField] TextMeshProUGUI impulse;
    [SerializeField] TextMeshProUGUI mouse;

    [Header("Audio")]
    [SerializeField] TextMeshProUGUI audioText;

    [Header("InGame")]
    [SerializeField] TextMeshProUGUI hold;
    [SerializeField] TextMeshProUGUI release;

    [Header("ToContinue")]
    [SerializeField] TextMeshProUGUI continueText;
    [SerializeField] TextMeshProUGUI doctor;

    [Header("Pause")]
    [SerializeField] TextMeshProUGUI pause;
    [SerializeField] TextMeshProUGUI continuePause;
    [SerializeField] TextMeshProUGUI menuPause;
    [SerializeField] TextMeshProUGUI exitPause;

    [Header("Game Over")]
    [SerializeField] TextMeshProUGUI gameover;
    [SerializeField] TextMeshProUGUI menuOver;
    [SerializeField] TextMeshProUGUI exitOver;

    private void Start()
    {
        SetSpanishText();
    }

    public void SetSpanishText()
    {
        PlayerPrefs.SetString("Language", "Spanish");
        language = PlayerPrefs.GetString("Language", "Spanish");
        PlayerPrefs.Save();

        start.text = "Empezar";
        sound.text = "Audio";
        controls.text = "Controles";
        exit.text = "Salir";

        accelerate.text = "Acelerar   ->    W";
        prepareImpulse.text = "Prepara impulso en órbita -> mantener espacio";
        impulse.text = "Impulsar -> Soltar espacio";
        mouse.text = "Rotar -> mover Ratón";

        audioText.text = "Máster" + "\n" + "\n" + "Musica" + "\n" + "\n" + "Efectos especiales";

        hold.text = "mantén espacio";
        release.text = "soltar espacio";

        continueText.text = "Pulsa   A   para continuar";
        doctor.text = "Hola pequeño\r\n\r\n¿Me puedes atender un momentito?";

        pause.text = "P   usa";
        continuePause.text = "Continuar";
        menuPause.text = "Menú";
        exitPause.text = "Salir";

        gameover.text = "Te quedaste sin energía...";
        menuOver.text = "Menú";
        exitOver.text = "Salir";
    }
    public void SetEnglishText()
    {
        PlayerPrefs.SetString("Language", "English");
        language = PlayerPrefs.GetString("Language", "English");
        PlayerPrefs.Save();

        start.text = "Start";
        sound.text = "Audio";
        controls.text = "Controls";
        exit.text = "Exit";

        accelerate.text = "Accelerate->   W";
        prepareImpulse.text = "Prepare orbital thrust -> hold space";
        impulse.text = "Impulse -> Release space";
        mouse.text = "Rotate -> move Mouse";

        audioText.text = "Master" + "\n" +"\n" + "Music" + "\n"+ "\n" + "Sound Effects";

        hold.text = "hold space";
        release.text = "release space";

        continueText.text = "Press   A   to continue           ";
        doctor.text = "Hello little one\r\n\r\nCan I have your attention for a moment?";

        pause.text = "P   use";
        continuePause.text = "Continue";
        menuPause.text = "Main menu";
        exitPause.text = "Exit";

        gameover.text = "You ran out of energy...";
        menuOver.text = "Main menu";
        exitOver.text = "Exit";
    }
}
