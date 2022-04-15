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
    }

    public void updateSetting() {
        MouseLook.mouseSensitiviy = currentMouseValue;
    }

    public void cancelSetting() {
        mouseSlider.value = MouseLook.mouseSensitiviy;
        currentMouseValue = MouseLook.mouseSensitiviy;
    }

    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
