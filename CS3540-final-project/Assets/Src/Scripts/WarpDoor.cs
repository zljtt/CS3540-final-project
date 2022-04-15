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
        Debug.Log("enter1");
        if (other.CompareTag("Player"))
        {
            Debug.Log("teleport1");
            if (FindObjectOfType<CombatManager>() == null)
            {
                SceneManager.LoadScene(teleportScene);
            }
            else if (FindObjectOfType<CombatManager>().GetStatus() == CombatManager.STATUS.LOOT)
            {
                Debug.Log("teleport");
                SceneManager.LoadScene(teleportScene);
            }
        }

    }
}
