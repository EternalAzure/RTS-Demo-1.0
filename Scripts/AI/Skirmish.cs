using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    State Machine State
    Skirmish will run around but not engage.
*/
public class Skirmish : MachineState
{
    [SerializeField] private AI ai;
    [SerializeField] private List<AIController> controllers = new List<AIController>();


    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<AI>();
        controllers = ai.GetControllers();
    }
    public override void Init()
    {

    }
}