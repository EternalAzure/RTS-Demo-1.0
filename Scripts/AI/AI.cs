using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class AI : MonoBehaviour
{
    public Transform waypointN;
    public Transform waypointE;
    public Transform waypointS;
    public Transform waypointW;
    public Transform waypointC;
    public List<AIController> controllers = new List<AIController>();

    public enum State 
    {
        Skirmish,
        Assault,
        Regroup,
    }
    public State state;
    public MachineState skirmish; // From this gameobject
    public MachineState assault; // From this gameobject
    public MachineState regroup; // From this gameobject

    public void AddController(AIController controller)
    {
        controllers.Add(controller);
    }
    public List<AIController> GetControllers()
    {
        return controllers;
    }

    void Start()
    {
        waypointN = transform.GetChild(0);
        waypointE = transform.GetChild(1);
        waypointS = transform.GetChild(2);
        waypointW = transform.GetChild(3);
        waypointC = transform.GetChild(4);

        skirmish = GetComponent<Skirmish>();
        assault = GetComponent<Assault>();
        regroup = GetComponent<Regroup>();

        state = State.Skirmish;
    }

    void Update()
    {
        switch (state)
        {
            default:
            case State.Skirmish:
            // No engage, position
                skirmish.Init();
                break;
            case State.Assault:
            // Engage
                assault.Init();
                break;
            case State.Regroup:
            // Retreat
                regroup.Init();
                break;
        }
    }
}
