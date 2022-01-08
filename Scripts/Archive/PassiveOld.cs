using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
    State Machine State
    Passive state follows movement orders of IController.
    Meanwhile it scans for enemies.
*/
public class PassiveOld: MachineState
{
    [SerializeField] private StateMachine ai; // From this gameobject
    [SerializeField] private MovementModule movement; // From this gameobject

    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<StateMachine>();
        movement = GetComponent<MovementModule>();
    }
    public override void Init()
    {
        // Only in chase/combat mode we want our stopping distance to be more than zero
        movement.navMeshAgent.stoppingDistance = 0;

        // parentUnit will calculate relative position in formation
        // Then it calls SoldierAI.Move() to set Nav Mesh Agents destination
        movement.Move();

        // AliveOrDead will check what controller we are using and return
        // enemies list based on that
        try
        {
            FindEnemy(AliveOrDead.Instance.GetEnemies(movement.controller));
        }
        catch (MissingReferenceException)
        {
            //ignore
        }
        
    }
    void FindEnemy(Transform[] enemies)
    { 
        // Finds first enemy in range not closest
        // chaseDistance is distance from center of fighter formation
        // Center is calculated as average of x, y and z axis
        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.position);
            if (distance < movement.stats.chaseDistance)
            {
                movement.target = enemy;
                ai.state = StateMachine.State.Chase;
                return;
            }
            // Some units may not chase
            else if(distance < movement.stats.engagementDistance)
            {
                ai.state = StateMachine.State.Fight;
                movement.target = enemy;
            }
        }
    }
}
