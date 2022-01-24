using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Responsibilities:
    -Spawn CPU fighters in waves (enemies)

    SpawnLoop() will loop over waves (list). Each Wave (class) has groups of enemies.
    SpawnWave() will spawn one wave by iteration over WaveUnit (class).
    SpawnLoop() calls SpawnWave once per iteration.
    Both SpawnLoop() and SpawnWave() will avoid busy waiting between iterations.
    Wave and WaveUnit classes have no functionality, only data.
    SpawnFormation() prevents enemies spawning on top of each other.

    Unlike Player controlled fighters, enemies are not bound to any groups.

    Prefab is prefabricated gameobject with grapchics, sounds, scripts, colliders and etc.
*/

public class WaveSpawner : Adversary
{
    [SerializeField] private List<Wave> waves; // in Unitity Editor
    public float sleepTime = 2f;
    public Transform waypoint; // in Editor. Center of fortress
    public Adversary adversary;
    Formations formations = new Formations();

    void Start()
    {
        adversary = GameObject.FindGameObjectWithTag("MultiSpawner").GetComponent<MultiSpawner>();
        StartCoroutine(SpawnLoop());
        if (waves == null) waves = new List<Wave>();
    }

    IEnumerator SpawnLoop()
    {
        if (waves.Count == 0)
        {
            StopCoroutine(SpawnLoop());
            yield break;
        }

        // SPAWN FIRST WAVE
        StartCoroutine(SpawnWave(0));
        yield return null;

        // SPAWN REST
        for (int wave = 1; wave < waves.Count; wave++)
        {
            // Avoid busy waiting between waves
            while(fighters.Count != 0)
            {
                yield return new WaitForSeconds(sleepTime);
            }
            
            // Spawn new wave after previous has died out
            StartCoroutine(SpawnWave(wave));
        }

        // WAIT FOR LAST WAVE TO DIE
        yield return new WaitUntil(() => fighters.Count == 0);
        EndGame();

    }

    IEnumerator SpawnWave(int waveIndex)
    {
        List<WaveUnit> currentWave = waves[waveIndex].wave;

        float lastSpawnTime = Time.time;
        float interval = 0f; // This initial value is time before first enemies spawn
        for (int i = 0; i < currentWave.Count; i++)
        {
            // Avoid busy waiting between groups
            if (Time.time < lastSpawnTime + interval) yield return new WaitForSeconds(0.5f);

            SpawnFormation(currentWave[i].prefab, currentWave[i].spawnpoint.position, currentWave[i].amount);

            // Update time for next unit in this wave
            lastSpawnTime = Time.time;
            interval = currentWave[i].interval;
        }
        
        yield break;
    }

    void SpawnFormation( GameObject model, Vector3 position, int amount)
    {
        Vector3[] positions = formations.Square(position, amount);

        for (int i = 0; i < amount; i++)
        {
            GameObject fighter = Instantiate(model, positions[i], Quaternion.identity) as GameObject;
            fighters.Add(fighter.transform);
            ConfigureFighter(fighter.transform, model);
        }
    }

    void ConfigureFighter(Transform fighter, GameObject model)
    {
        fighter.name = model.name;
        fighter.GetComponent<Passive>().SetAdversaries(adversary);
        fighter.GetComponent<ReactionsToHostilities>().onDeathEvent += RemoveSelf;
    }

    private void EndGame()
    {
        Time.timeScale = 0; // Stop game
        GameObject endScreen = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(1).gameObject;
        endScreen.SetActive(true);
        endScreen.transform.GetChild(0).GetComponent<Text>().text = "You Won";
    }


    [System.Serializable]
    private class Wave
    {
        // Wrapper class for Unity editor
        // Unity can't display any "lists of lists"
        public List<WaveUnit> wave;

        public Wave(List<WaveUnit> wave)
        {
            this.wave = wave;
        }
    }
    
    [System.Serializable]
    private class WaveUnit
    {
        // All accessed and set in Unity Editor
        public GameObject prefab;
        public int amount;
        public Transform spawnpoint;
        public float interval;
        public Transform waypoint;
        
    }
}
