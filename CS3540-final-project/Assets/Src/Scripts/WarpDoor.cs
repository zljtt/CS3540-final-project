using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpDoor : MonoBehaviour
{
    public List<string> normalScene;
    public List<string> hardScene;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (LevelManager.playerData.playerLevel >= 2)
            {
                normalScene.AddRange(hardScene);
            }
            int index = Random.Range(0, normalScene.Count);
            if (FindObjectOfType<CombatManager>() == null)
            {
                if (normalScene[0] == "Castle")
                {
                    StoreManager.nextLoadRefill = true;
                }
                SceneManager.LoadScene(normalScene[index]);
            }
            else if (FindObjectOfType<CombatManager>().GetStatus() == CombatManager.STATUS.LOOT)
            {
                if (normalScene[0] == "Castle")
                {
                    StoreManager.nextLoadRefill = true;
                }
                SceneManager.LoadScene(normalScene[index]);
            }
        }

    }
}
