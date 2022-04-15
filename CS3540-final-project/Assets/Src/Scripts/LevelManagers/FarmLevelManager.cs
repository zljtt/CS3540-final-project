using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmLevelManager : LevelManager
{
    public Transform spawnPoint;
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
            new SpawnInfo(5, "Prefabs/Character/HumanKnight", spawnPoint.position, spawnPoint.rotation),
            new SpawnInfo(10, "Prefabs/Character/HumanKnight", spawnPoint.position, spawnPoint.rotation),
            new SpawnInfo(15, "Prefabs/Character/HumanKnight", spawnPoint.position, spawnPoint.rotation),
            new SpawnInfo(20, "Prefabs/Character/HumanKnight", spawnPoint.position, spawnPoint.rotation),
        };
    }

}
