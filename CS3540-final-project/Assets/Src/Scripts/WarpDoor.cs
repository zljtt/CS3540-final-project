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
        if (other.CompareTag("Player"))
        {
            if (FindObjectOfType<LevelManager>() == null)
            {
                SceneManager.LoadScene(teleportScene);
            }
            else if (FindObjectOfType<LevelManager>().GetStatus() == LevelManager.STATUS.LOOT)
            {
                SceneManager.LoadScene(teleportScene);
            }
        }

    }
}
