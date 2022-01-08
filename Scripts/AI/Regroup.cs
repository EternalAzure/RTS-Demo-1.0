using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    State Machine State
    Regroup will retreat and gather forces
*/
public class Regroup : MachineState
{
    [SerializeField] private AI ai;
    [SerializeField] private List<AIController> controllers = new List<AIController>();

    void Start()
    {
        ai = GetComponent<AI>();
        controllers = ai.GetControllers();
    }
    public override void Init()
    {

    }
}