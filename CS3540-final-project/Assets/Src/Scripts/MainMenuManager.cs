using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Slider mouseSlider;
    public Text mouseText;
    float currentMouseValue;
    public GameObject pauseMenu;
    public GameObject settingMenu;
    public GameObject tutorialMenu;
    public static bool isGamePaused = false;
    public Transform playerPosition;
    private void Awake()
    {
        mouseSlider.maxValue = 500;
        mouseSlider.minValue = 10;
        mouseSlider.value = MouseLook.mouseSensitiviy;
        currentMouseValue = MouseLook.mouseSensitiviy;
        mouseText.text = "" + MouseLook.mouseSensitiviy;
    }

    private void Update()
    {
        currentMouseValue = mouseSlider.value;
        mouseText.text = "" + currentMouseValue;
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu != null)
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void updateSetting()
    {
        MouseLook.mouseSensitiviy = currentMouseValue;
    }

    public void cancelSetting()
    {
        mouseSlider.value = MouseLook.mouseSensitiviy;
        currentMouseValue = MouseLook.mouseSensitiviy;
    }

    public void StartGame()
    {
        isGamePaused = false;
        Time.timeScale = 1.0f;
        PlayerData playerData = LevelManager.playerData;
        if (playerData.currentLevel == null || playerData.currentLevel == "")
        {
            SceneManager.LoadScene("Castle");
        }
        else
        {
            SceneManager.LoadScene(playerData.currentLevel);
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadMainMenu()
    {
        print("load scene");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1.0f;
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
        if (tutorialMenu != null)
            tutorialMenu.SetActive(false);
        if (settingMenu != null)
            settingMenu.SetActive(false);
    }
}
