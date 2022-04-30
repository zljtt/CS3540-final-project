using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverBankLevelManager : CombatManager
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
            new SpawnInfo(5, "Prefabs/Character/HumanKnight", spawnPoints[0].position, spawnPoints[0].rotation),
            new SpawnInfo(6, "Prefabs/Character/HumanKnight", spawnPoints[1].position, spawnPoints[1].rotation),
            new SpawnInfo(7, "Prefabs/Character/HumanArcher", spawnPoints[2].position, spawnPoints[2].rotation),
            new SpawnInfo(12, "Prefabs/Character/HumanKnight", spawnPoints[0].position, spawnPoints[0].rotation),
            new SpawnInfo(13, "Prefabs/Character/HumanKnight", spawnPoints[1].position, spawnPoints[1].rotation),
            new SpawnInfo(14, "Prefabs/Character/HumanArcher", spawnPoints[2].position, spawnPoints[2].rotation),
            new SpawnInfo(25, "Prefabs/Character/HumanKnight", spawnPoints[0].position, spawnPoints[0].rotation),
            new SpawnInfo(26, "Prefabs/Character/HumanMage", spawnPoints[0].position, spawnPoints[0].rotation),
            new SpawnInfo(27, "Prefabs/Character/HumanKnight", spawnPoints[1].position, spawnPoints[1].rotation),
            new SpawnInfo(28, "Prefabs/Character/HumanArcher", spawnPoints[2].position, spawnPoints[2].rotation),
        };
    }

}
