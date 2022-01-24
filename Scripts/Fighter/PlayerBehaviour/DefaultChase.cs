using UnityEngine;

/*
    State Machine State
    Travels and seeks adversaries.
*/
public class DefaultChase : Chase
{
    protected override void Approach()
    {
        agent.destination = target.position;
        distance = Vector3.Distance(transform.position, target.position);
    }

    protected override void TurnBack()
    {
        // If too far away from friends, stop chasing
        // Note that last fighter alive will never be too far away from itself
        if (config.chaseDistance < Vector3.Distance(transform.position, controller.GetCenter()))
        {
            ai.state = StateMachine.State.Passive;
            return;
        }
    }

    protected override void Engage()
    {
        try
        {
            // If close enough to fight, fight
            if (Vector3.Distance(transform.position, target.position) <= config.fightDistance)
            {
                ai.state = StateMachine.State.Fight;
                fight.SetTarget(target);
            }
        }
        catch (MissingReferenceException)
        {
            ai.state = StateMachine.State.Passive;
            throw;
        }
    }
}