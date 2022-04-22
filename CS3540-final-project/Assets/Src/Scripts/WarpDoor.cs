using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpDoor : MonoBehaviour
{
    public List<string> teleportScene;
    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int index = Random.Range(0, teleportScene.Count);
            if (FindObjectOfType<CombatManager>() == null)
            {
                if (teleportScene[0] == "Castle")
                {
                    StoreManager.nextLoadRefill = true;
                }
                SceneManager.LoadScene(teleportScene[index]);

            }
            else if (FindObjectOfType<CombatManager>().GetStatus() == CombatManager.STATUS.LOOT)
            {
                if (teleportScene[0] == "Castle")
                {
                    StoreManager.nextLoadRefill = true;
                }
                SceneManager.LoadScene(teleportScene[index]);
            }
        }

    }
}
