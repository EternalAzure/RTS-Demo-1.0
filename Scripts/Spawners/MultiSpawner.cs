using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Responsibilities:
    -Spawn player controlled units
    -Declare game over

    _SpawnAllUnits() calls SpawnSquareFormation() for every unit to be spawned.
    SpawnFormation() prevents enemies spawning on top of each other.
    Positions() calculate unique positions in square formation.

    Prefab is prefabricated gameobject with grapchics, sounds, scripts, colliders and etc.
*/
public class MultiSpawner : Adversary
{
    public List<Transform> spawnpoints;
    public List<GameObject> playerPrefabs; // Will be replaced with other data.
    public Adversary adversary;
    public  int unitSize = 9;
    Formations formations = new Formations();

    private void Start()
    {
        adversary = GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>();
        
        _SpawnAllUnits();
        StartCoroutine(EndGame());
    }

    public void _SpawnAllUnits()
    {
        if (spawnpoints == null)
        {
            Debug.Log("No spawnpoints set for player. No player units spawned");
            return;
        }

        for (int i = 0; i < playerPrefabs.Count; i++)
        {
            SpawnFormation(playerPrefabs[i], spawnpoints[i].position, unitSize); // Call once per every unit
        }
    }

    void SpawnFormation(GameObject model, Vector3 position, int unitsize)
    {
        PlayerController controller = gameObject.AddComponent<PlayerController>();
        controller.onDeathEvent += RemoveSelf;
        Transform[] unit = new Transform[unitsize]; // List for controller to control fighters
        Vector3[] positions = formations.Square(position, unitSize);

        for (int i = 0; i < unitSize; i++)
        {
            GameObject fighter = Instantiate(model, positions[i], Quaternion.identity) as GameObject;
            fighters.Add(fighter.transform); // Add to list inherited from Adversary
            ConfigureFighter(fighter, model, controller);
            unit[i] = fighter.transform;
            controller.destination = position; // Do we need this? Destination is set only when mouse1 is clicked
        }

        controller.SetList(unit);
    }

    void ConfigureFighter(GameObject fighter, GameObject model, PlayerController controller)
    {
        fighter.name = model.name;
        fighter.GetComponent<Passive>().SetAdversaries(adversary);
        fighter.GetComponent<Chase>().controller = controller; // Use DefaultChase instead
        fighter.gameObject.layer = 6; // Set layer as 'Selectable'
    }

    IEnumerator EndGame()
    {
        yield return new WaitUntil(() => fighters.Count == 0);
        
        Time.timeScale = 0; // Stop game
        GameObject endScreen = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).gameObject;
        endScreen.SetActive(true);
        endScreen.transform.GetChild(0).GetComponent<Text>().text = "You Lose";
    }
}
