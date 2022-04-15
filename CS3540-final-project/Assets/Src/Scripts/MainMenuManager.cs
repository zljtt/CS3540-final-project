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

    private void Awake() {
        mouseSlider.maxValue = 500;
        mouseSlider.minValue = 10;
        mouseSlider.value = MouseLook.mouseSensitiviy;
        currentMouseValue = MouseLook.mouseSensitiviy;
        mouseText.text = "" + MouseLook.mouseSensitiviy;
    }

    private void Update() {
        currentMouseValue = mouseSlider.value;
        mouseText.text = "" + currentMouseValue;
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(isGamePaused) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }
    }

    public void updateSetting() {
        MouseLook.mouseSensitiviy = currentMouseValue;
    }

    public void cancelSetting() {
        mouseSlider.value = MouseLook.mouseSensitiviy;
        currentMouseValue = MouseLook.mouseSensitiviy;
    }

    public void StartGame() {
        isGamePaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainMenu() {
        print("load scene");
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame() {
        Application.Quit();
    }

    void PauseGame() {
        isGamePaused = true;
        Time.timeScale = 0.0f;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame() {
        isGamePaused = false;
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        tutorialMenu.SetActive(false);
        settingMenu.SetActive(false);
    }
}
