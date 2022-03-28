using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnUnitBehavior : MonoBehaviour
{
    public GameObject unitPrefab;
    public float spawnDistance = 10f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1") && FindObjectOfType<LevelManager>().GetStatus() == LevelManager.STATUS.PREPARE)
        {
            SpawnUnit(unitPrefab);
        }
    }

    void SpawnUnit(GameObject unitToSpawn)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
        {
            if (Vector3.Distance(transform.position, hit.point) > 0.5f)
            {
                GameObject unit = Instantiate(unitToSpawn, hit.point, transform.parent.transform.rotation);
            }
        }
    }
}
