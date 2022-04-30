using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffLevelManager : CombatManager
{
    protected override void OnPrepareStart()
    {
        Debug.Log("Prepare stage starts!");
    }
    protected override void OnCombatStart()
    {
        Debug.Log("Combat stage starts!");
        // enable all enemies
        List<GameObject> units = new List<GameObject>();
        units.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        units.AddRange(GameObject.FindGameObjectsWithTag("Ally"));
        foreach (GameObject obj in units)
        {
            if (obj.GetComponent<UnitBehavior>() != null)
            {
                obj.GetComponent<UnitBehavior>().ChangeState(UnitBehavior.State.ALERT);
            }
        }
    }
    protected override void OnCombatEnd()
    {
        Debug.Log("Combat stage ends!");
    }

    protected override List<SpawnInfo> GetSpawns()
    {
        return new List<SpawnInfo>
        {
            new SpawnInfo(1, "Prefabs/Character/HumanWarrior", spawnPoints[0].position, spawnPoints[0].rotation),
            new SpawnInfo(3, "Prefabs/Character/HumanWarrior", spawnPoints[0].position, spawnPoints[0].rotation),
            new SpawnInfo(5, "Prefabs/Character/HumanWarrior", spawnPoints[0].position, spawnPoints[0].rotation),
            new SpawnInfo(7, "Prefabs/Character/HumanWarrior", spawnPoints[0].position, spawnPoints[0].rotation),
            new SpawnInfo(10, "Prefabs/Character/HumanMage", spawnPoints[1].position, spawnPoints[1].rotation),
            new SpawnInfo(10, "Prefabs/Character/HumanMage", spawnPoints[2].position, spawnPoints[2].rotation),
            new SpawnInfo(21, "Prefabs/Character/HumanWarrior", spawnPoints[0].position, spawnPoints[0].rotation),
            new SpawnInfo(23, "Prefabs/Character/HumanWarrior", spawnPoints[0].position, spawnPoints[0].rotation),
            new SpawnInfo(25, "Prefabs/Character/HumanMage", spawnPoints[1].position, spawnPoints[1].rotation),
            new SpawnInfo(25, "Prefabs/Character/HumanMage", spawnPoints[2].position, spawnPoints[2].rotation),
        };
    }

}
