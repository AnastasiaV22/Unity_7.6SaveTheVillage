using Unity.VisualScripting;
using UnityEngine;

public class UIMoving : MonoBehaviour
{

    private UIMoving() { }
    private static UIMoving instance;

    public static UIMoving GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject endGameCanvas;

    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject aboutPanel;
    [SerializeField] GameObject settingsPanel;

    [SerializeField] GameObject pausePanel;
    
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject buttonEndGamePanel;

    
    private void Start()
    {
        MainMenuOpen();
    }
    public void MainMenuOpen()
    {
        mainCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        endGameCanvas.SetActive(false);

        mainMenuPanel.SetActive(true);
        aboutPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void OnSettingsButtonClick()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OnAboutButtonClick()
    {
        mainMenuPanel.SetActive(false);
        aboutPanel.SetActive(true);
    }

    public void OnNewGameStartButtonClick()
    {
        Debug.Log("newgame");
        mainCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        endGameCanvas.SetActive(false);

        pausePanel.SetActive(false);
    }

    
    public void OnPauseClick()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        Debug.Log("ChangePause");

    }

    public bool IsPauseButtonClick()
    {
        return pausePanel.activeSelf;
    }

    public void ShowWinEndGame()
    {

        endGameCanvas.SetActive(true);
        winPanel.SetActive(true);
        losePanel.SetActive(false);
        buttonEndGamePanel.SetActive(true);
        
    }

    public void ShowLoseEndGame() {

        endGameCanvas.SetActive(true);
        losePanel.SetActive(true);
        winPanel.SetActive(false);
        buttonEndGamePanel.SetActive(true);

    }


}