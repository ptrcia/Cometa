using System.Collections;
using TMPro;
using UnityEngine;

public class TextReveal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private float revealSpeed = 0.05f; // interveal

    private void Start()
    {
        StartCoroutine(RevealText());
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private IEnumerator RevealText()
    {
        textMeshPro.maxVisibleCharacters = 0;
        int totalCharacters = textMeshPro.text.Length;

        for (int i = 0; i <= totalCharacters; i++)
        {
            textMeshPro.maxVisibleCharacters = i;
            yield return new WaitForSeconds(revealSpeed); 
        }
    }
}
