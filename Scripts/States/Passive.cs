using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    State Machine State
    Passive state follows movement orders of IController.
    Meanwhile it scans for enemies.
*/
public class Passive : MachineState
{
    public IController controller; // MultiSpawner.Spawn()
    public GameMaster gm; // MultiSpawner.Spawn()
    bool isAI = true;
    [SerializeField] private StateMachine ai; // From this gameobject
    [SerializeField] private MovementModule movement; // From this gameobject


    // Start is called before the first frame update
    void Start()
    {
        ai = GetComponent<StateMachine>();
        movement = GetComponent<MovementModule>();
        if (movement.controller.GetType().Equals(typeof(PlayerController))) isAI = false;
    }
    public override void Init()
    {
        // When called without parameter, 
        //it will assign destination according to controllers will
        movement.Move();

        // Location of this gameobject in 3 dimensions
        Vector3 location = transform.position;

        // Distance enemies will be seeked within
        float range = movement.stats.chaseDistance;

        movement.target = null;
        movement.target = gm.GetEnemy(location, range, isAI);
        if(movement.target != null) ai.state = StateMachine.State.Chase;
    }
}
