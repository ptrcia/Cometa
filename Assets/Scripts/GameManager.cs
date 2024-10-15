using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instanciate;

    [Header("Player Settings")]
    [SerializeField] GameObject player;
    [SerializeField] Camera gameCamera;
    [SerializeField] Camera menuCamera;
    PlanetController planetController;

    [Header("Main Menu")]
    [SerializeField] GameObject MainMenuCanvas;

    [Header("Pause Menu")]
    [SerializeField] GameObject pauseCanvas;

    [Header("Game Over Menu")]
    [SerializeField] GameObject panelFading;
    [Header("End Game Menu")]


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
        planetController = player.GetComponent<PlanetController>();
        gameCamera = Camera.main;
    }
    private void Start()
    {
        SetUp();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseCanvas.activeInHierarchy)
            {              
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void SetUp()
    {
        MainMenuCanvas.SetActive(true);
        planetController.enabled = false;
        menuCamera.enabled = true;
        gameCamera.enabled = false;
    }

    public void PlayGame()
    {
        MainMenuCanvas.SetActive(false);
        planetController.enabled = true;
        menuCamera.enabled= false;
        gameCamera.enabled = true;
    }
    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }
    public void EndGame()
    {

    }

    public void GameOver()
    {
        Debug.Log("Energy is empty");
        
        StartCoroutine(FadingPanel(5)); //comprobar que funciona
        //UI texto y boton
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void URL(string url)
    {
        Application.OpenURL(url);
    }

    IEnumerator FadingPanel(float fadeDuration)
    {
        panelFading.SetActive(true);

        Color originalColor = panelFading.GetComponent<Image>().tintColor;
        float startAlpha = originalColor.a;
        float targetAlpha = 250f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);

            panelFading.GetComponent<Image>().tintColor = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);

            yield return null;
        }

        panelFading.GetComponent<Image>().tintColor = new Color(originalColor.r, originalColor.g, originalColor.b, targetAlpha);
    }
}
