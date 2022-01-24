using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    State Machine State
    Travels and seeks adversaries.
*/
public class DefaultPassive : Passive
{
    [SerializeField] private ReactionsToHostilities movement; // From this gameobject
    
    void Start()
    {
        adversary = GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>();
        movement = GetComponent<ReactionsToHostilities>();
    }

    protected override void Travel()
    {
        agent.destination = target;
    }

    protected override void FindEnemies()
    {
        Vector3 location = transform.position;
        float range = config.chaseDistance;

        Transform enemy = adversary.GetEnemy(location, range);
        if (enemy == null) return;
        ai.state = StateMachine.State.Chase;
        chase.SetTarget(enemy);
    }
}
