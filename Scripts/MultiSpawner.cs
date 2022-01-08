using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    MultiSpawner spawns many units of many kind with many members.
    This class not only spawns but configures things.
    Prefab is prefabricated gameobject with grapchics, sounds, scripts, colliders and etc.
    PlayerPreferences is Unity provided way of storing data.
*/
public class MultiSpawner : MonoBehaviour
{
    public GameObject ai;
    public List<Transform> spawnList_AI; // List of spawnpoints
    public List<Transform> spawnList_Player; // List of spawnpoints
    public List<GameObject> playerPrefabs;  //List of units to spawn. All kinds of units
    public List<GameObject> AIPrefabs; // Can have same type of unit many times over
    public GameMaster gameMaster; // Helps to find enemies
    public  int unitSize = 9; // Number of fighters in one unit

    private void Start()
    {
        if (spawnList_AI == null) spawnList_AI = new List<Transform>();
        if (spawnList_Player == null) spawnList_Player = new List<Transform>();

        // Spawn Player
        int i = 0;
        IController[] playerControllers = new IController[playerPrefabs.Count];
        foreach (GameObject prefab in playerPrefabs)
        {
            PlayerController newPlayerController = gameObject.AddComponent<PlayerController>();
            playerControllers[i] = newPlayerController;
            
            //unitSize = PlayerPrefs.GetInt(prefab.name, 9); // TODO
            SpawnQuareFormation(newPlayerController, prefab, spawnList_Player[i].position, unitSize);
            i++;
        }
        gameMaster.SetPlayerUnits(playerControllers);

        // Spawn AI 
        i = 0;
        IController[] aiControllers = new IController[AIPrefabs.Count];
        foreach (GameObject prefab in AIPrefabs)
        {
            AIController newAIController = gameObject.AddComponent<AIController>();
            aiControllers[i] = newAIController;
            
            //unitSize = PlayerPrefs.GetInt(prefab.name, 9); // TODO
            SpawnQuareFormation(newAIController, prefab, spawnList_AI[i].position, unitSize);
            i++;

            // Battle AI
            ai.GetComponent<AI>().AddController(newAIController);
        }
        gameMaster.SetAIUnits(aiControllers);
    }

    void SpawnQuareFormation(IController controller, GameObject model, Vector3 position, int unitsize)
    {
        // List for controller to control fighters
        Transform[] fighters = new Transform[unitsize];

        int rows = Mathf.FloorToInt(Mathf.Sqrt(unitSize));
        int columns = Mathf.CeilToInt(unitSize / rows);

        float z = 0;
        float x = 0;
        for (int i = 0; i < unitSize; i++)
        {
            if (x >= rows)
            {
                 x = 0; 
                 z += 1.2f;
            }
            Vector3 spawnPos = new Vector3(position.x + x, position.y, position.z + z);
            GameObject fighter = Instantiate(model, spawnPos, Quaternion.identity) as GameObject;
            fighters[i] = fighter.transform;

            ConfigureFighter(fighters[i], model, controller, gameMaster);
            x += 1.2f;
        }
        controller.SetList(fighters);
    }

    void ConfigureFighter(Transform fighter, GameObject model, IController controller, GameMaster gameMaster)
    {
        fighter.name = model.name;
        fighter.GetComponent<MovementModule>().controller = controller;
        fighter.GetComponent<Passive>().gm = gameMaster;
        fighter.GetComponent<Passive>().controller = controller;
        fighter.GetComponent<Chase>().controller = controller;
    }
}
