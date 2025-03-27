using Unity.VisualScripting;
using UnityEngine;

public class UIMoving : MonoBehaviour
{

    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject endGameCanvas;

    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject aboutPanel;
    [SerializeField] GameObject settingsPanel;

    [SerializeField] GameObject pausePanel;

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
    }
}