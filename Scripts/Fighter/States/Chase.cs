using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

/*
    State Machine State
    Chase state will chase after enemies until it has chased 
    far enough or is close enough to figh.
*/
public class Chase : MachineState
{
    public PlayerController controller; // in MultiSpawner
    [SerializeField] protected StateMachine ai; // From this gameobject
    [SerializeField] protected FighterConfig config; // in Unity Editor
    [SerializeField] protected Transform target; // in Passive
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected Fight fight;
    public float distance;

    void Awake()
    {
        ai = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        fight = GetComponent<Fight>();
    }

    public override void Init()
    {
        if (target == null)
        {
            ai.state = StateMachine.State.Passive;
            return;
        }

        // Right stopping distance matches attack animation
        agent.stoppingDistance = config.fightDistance;

        Approach();

        TurnBack();

        Engage();
    }

    protected virtual void Approach()
    {

    }

    protected virtual void TurnBack()
    {
        
    }

    protected virtual void Engage()
    {
        
    }

    public void SetTarget(Transform target)
    {
        try
        {
            this.target = target;
        }
        catch (MissingReferenceException)
        {
            throw;
        }
    }
}
