using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoukkaPassive : Passive
{
    [SerializeField] List<Transform> objectives;

    void Start()
    {
        adversary = GameObject.FindGameObjectWithTag("MultiSpawner").GetComponent<MultiSpawner>();
        objectives = GameObject.FindGameObjectWithTag("Environment").GetComponent<Environment>().GetObjectives();
    }

    protected override void Travel()
    {
        Vector3 newTarget = FindClosestObjective(objectives);
        agent.destination = newTarget;
        target = newTarget;
        agent.stoppingDistance = config.fightDistance;
    }

    protected override void FindEnemies()
    {
        // Find enemies
        Vector3 location = transform.position;
        float range = config.chaseDistance;

        Transform enemy = adversary.GetEnemy(location, range);
        if (enemy == null) return;

        // Set next state
        ai.state = StateMachine.State.Chase;
        this.SetTarget(enemy.position);
        chase.SetTarget(enemy);
    }

    Vector3 FindClosestObjective(List<Transform> objectives)
    {
        Transform closest = objectives[0];
        for (int i = 0; i < objectives.Count; i++)
        {
            if (objectives[i].GetComponent<Conquest>() == null) continue;
            if (Vector3.Distance(transform.position, objectives[i].position) < Vector3.Distance(transform.position, closest.position))
            {
                closest = objectives[i];
            }
        }

        return closest.position;
    }
}
