using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/*
    State Machine State
    Chase state will chase after enemies until it has chased 
    far enough or is close enough to figh.
*/
public class Chase : MachineState
{
    
    public IController controller; // MultiSpawner.Spawn()
    [SerializeField] private MovementModule movement; // From this gameobject
    [SerializeField] private StateMachine ai; // From this gameobject
    private Vector3 target; // From MovementModule

    void Start()
    {
        ai = GetComponent<StateMachine>();
        movement = GetComponent<MovementModule>();
    }
    public override void Init()
    {
        try
        {
            // Target can die and disappear causing exception
            if (movement.target == null)
            {
                movement.target = null;
                ai.state = StateMachine.State.Passive;
                return;
            }

            // Target is enemy that is close enough to chase
            target = movement.target.position;
        }
        catch (MissingReferenceException)
        {
            movement.target = null;
            ai.state = StateMachine.State.Passive;
            throw;
        }
        
        // Only in chase/combat mode we want our stopping distance to be more than zero
        movement.navMeshAgent.stoppingDistance = movement.stats.stoppingDistance;

        // Chase target by setting targets position as destination for Nav Mesh Agent
        if(movement.stats.stoppingDistance < Vector3.Distance(transform.position, target))
        {
            movement.Move(target);
        }
        
        // If too far away from friends, stop chasing
        // Note that last fighter alive will never be too far away from itself
        if (Vector3.Distance(transform.position, controller.GetCenter()) > movement.stats.chaseDistance)
        {
            ai.state = StateMachine.State.Passive;
            movement.target = null;
            return;
        }
        try
        {
            // If close enough to fight, fight
            if (Vector3.Distance(transform.position, target) < movement.stats.engagementDistance)
            {
                // Change state of state machine
                ai.state = StateMachine.State.Fight;
            }
        }
        catch (MissingReferenceException)
        {
            movement.target = null;
            ai.state = StateMachine.State.Passive;
            throw;
        }
    }
}
