using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

/*
    State Machine State
    Stunned state stops all actions.
*/
public class KnockedBack : MachineState
{
    [SerializeField] protected StateMachine ai; // From this gameobject
    [SerializeField] protected FighterConfig config; // in Unity Editor
    [SerializeField] protected NavMeshAgent agent;

    void Awake()
    {
        ai = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
    }

    public override void Init()
    {
        
    }
}