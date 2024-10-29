using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instanciate;

    [Header("Player Settings")]
    [SerializeField] GameObject player;
    [SerializeField] Camera gameCamera;
    //[SerializeField] Camera menuCamera;
    [SerializeField] GameObject canvasInGame;
    PlanetController planetController;
    PlayerMovement playerMovement;
    CameraFollow cameraFollow;
    [SerializeField] AudioClip ambientSpace;


    [Header("Main Menu")]
    [SerializeField] GameObject MainMenuCanvas;

    [Header("Pause Menu")]
    [SerializeField] GameObject pauseCanvas;

    [Header("Final Act Menu")]
    [SerializeField] GameObject toContinueCanvas;
    [SerializeField] AudioClip hospital;
    private bool finalAct;

    [Header("End Game")]
    [SerializeField] GameObject gameOverCanvas;


    private void Awake()
    {
        finalAct = false;
        DontDestroyOnLoad(gameObject);
        if(instanciate == null)
        {
            instanciate = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        planetController = player.GetComponent<PlanetController>();
        playerMovement = player.GetComponent<PlayerMovement>();
        cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }
    private void Start()
    {
        SetUp();

    }

    private void Update()
    {
        if (Input.GetKeyUp("1")) SceneManager.LoadScene(0);
        if (Input.GetKeyUp("2")) SceneManager.LoadScene(1);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseCanvas.activeInHierarchy)
            {              
                ResumeGame();
            }
            else if(!pauseCanvas.activeInHierarchy && !MainMenuCanvas.activeInHierarchy)
            {
                PauseGame();
            }
        }

        if(finalAct && Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Ha pulsado la A");
            SceneManager.LoadScene(1);
            AudioManager.instance.PlayMusic(hospital);
        }

    }

    public void FinalAct()
    {
        finalAct=true;
        StartCoroutine(PauseBeforeAction(5));
    }

    public void SetUp()
    {
        MainMenuCanvas.SetActive(true);
        playerMovement.enabled = false;
       cameraFollow.enabled = false;
    }

    public void PlayGame()
    {
        MainMenuCanvas.SetActive(false);
        playerMovement.enabled = true;
        canvasInGame.SetActive(true);
        cameraFollow.enabled = true;
        
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

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        AudioManager.instance.PlayMusic(null);

    }
    public void ExitGame()
    {
        PauseGame();
        Application.Quit();
        Debug.Log("Quitting...");

    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void URL(string url)
    {
        Application.OpenURL(url);
    }

    IEnumerator PauseBeforeAction(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        toContinueCanvas.SetActive(true);

    }
}
