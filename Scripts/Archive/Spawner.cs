using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This is archived file and server no purpose in final game
*/
public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform spawnpointRed;
    [SerializeField] private Transform spawnpointBlue;
    // Prefab of any fighter. Should contain IController component
    // suitable for human 
    [SerializeField] private GameObject modelBlue;
    // Prefab of any fighter. Should contain IController component
    // suitable for AI 
    [SerializeField] private GameObject modelRed;
    [SerializeField] private AIController unitRed;
    [SerializeField] private SimpleController unitBlue;

    // Has tools for arbiting combat and managing game state
    // Game Master could get list of soldiers straight from PlayerPrefs!!!
    [SerializeField] private AliveOrDead gm;
    private int AIUnitSize = 9;
    private int playerUnitSize = 9;

    private void Start()
    {
        //AIUnitSize = PlayerPrefs.GetInt("red", 9);
        //playerUnitSize = PlayerPrefs.GetInt("blue", 9);
        Transform[] aiUnits = Spawn(unitRed, modelRed, spawnpointRed.position, AIUnitSize);
        Transform[] playerUnits = Spawn(unitBlue, modelBlue, spawnpointBlue.position, playerUnitSize);

        gm.SetPlayer(playerUnits);
        gm.SetAI(aiUnits);
    }

    private Transform[] Spawn(IController controller, GameObject model, Vector3 position, int unitsize)
    {
        Transform[] soldiers = new Transform[unitsize];
        int i = 0;
        for (int x = 0; x < Mathf.CeilToInt(Mathf.Sqrt(unitsize)); x++) //max x is 3
        {
            for (int z = 0; z < Mathf.FloorToInt(Mathf.Sqrt(unitsize)); z++) ////max z is 3
            {
                Vector3 spawnPos = new Vector3(position.x + x, position.y, position.z + z);
                GameObject soldier = Instantiate(model, spawnPos, Quaternion.identity) as GameObject;
                soldier.name = model.name;
                soldier.GetComponent<MovementModule>().controller = controller;
                soldiers[i] = soldier.transform;
                i++;
            }
        }
        controller.SetList(soldiers);
        return soldiers;
    }

}
