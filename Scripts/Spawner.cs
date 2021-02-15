using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform spawnpointRed;
    [SerializeField] private Transform spawnpointBlue;

    [SerializeField] private GameObject modelBlue;
    [SerializeField] private GameObject modelRed;
    [SerializeField] private GameObject unitBlue;
    [SerializeField] private GameObject unitRed;

    private int r;
    private int b;

    private void Start()
    {
        r = PlayerPrefs.GetInt("red", 9);
        b = PlayerPrefs.GetInt("blue", 9);
        Spawn(unitRed, modelRed, spawnpointRed.position, r);
        Spawn(unitBlue, modelBlue, spawnpointBlue.position, b);
    }

    private GameObject Spawn(GameObject unit, GameObject model, Vector3 position, int unitsize)
    {
        GameObject newUnit = Instantiate(unit);
        newUnit.name = unit.name;
        newUnit.transform.position = position;
        List<Transform> soldiers = new List<Transform>();

        for (int x = 0; x < Mathf.CeilToInt(Mathf.Sqrt(unitsize)); x++) //max x is 3
        {
            for (int z = 0; z < Mathf.FloorToInt(Mathf.Sqrt(unitsize)); z++) ////max z is 3
            {
                Vector3 spawnPos = new Vector3(position.x + x, position.y, position.z + z);
                GameObject soldier = Instantiate(model, spawnPos, Quaternion.identity) as GameObject;
                soldier.name = model.name;
                soldiers.Add(soldier.transform);
                soldier.GetComponent<Soldier>().SetUC(newUnit.GetComponent<IUnitController>());
            }
        }
        newUnit.GetComponent<IUnitController>().SetSoldiers(soldiers);
        return newUnit;
    }

}
