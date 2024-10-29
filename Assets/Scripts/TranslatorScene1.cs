using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class TranslatorScene1 : MonoBehaviour
{
    [Header("ToContinue")]
    [SerializeField] TextMeshProUGUI continueText;
    [SerializeField] TextMeshProUGUI doctor;

    private void Start()
    {
        string language = PlayerPrefs.GetString("Language", "Spanish"); // Predeterminado a "English" si no se encuentra

        if (language == "Spanish")
        {
            SetSpanishText();
        }
        else if (language == "English")
        {
            SetEnglishText();
        }
    }
    public void SetSpanishText()
    {
        continueText.text = "Pulsa   A   para continuar";
        doctor.text = "Tu madre se ha recuperado y está perfecta.\r\n\r\nPor cierto, creo que tu nuevo hermanito pequeño quiere saludarte.\r\n\r\n¡Vamos ve!";

    }
    public void SetEnglishText()
    {
        continueText.text = "Press   A   to continue           ";
        doctor.text = "Your mother has recovered and is doing fine.\r\n\r\nBy the way, I think your new little brother wants to say hello.\r\n\r\nGo on, go!";
    }

}
