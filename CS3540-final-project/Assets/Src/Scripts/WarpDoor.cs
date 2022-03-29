using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpDoor : MonoBehaviour
{
    public string teleportScene;
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        if ((FindObjectOfType<LevelManager>() == null || FindObjectOfType<LevelManager>().GetStatus() == LevelManager.STATUS.LOOT) && other.CompareTag("Player"))
        {
            SceneManager.LoadScene(teleportScene);
        }
    }
}
