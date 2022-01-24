using UnityEngine;

public class MoukkaChase : Chase
{
    protected override void Approach()
    {
        agent.destination = target.position;
    }

    protected override void TurnBack()
    {
        // Moukka will never back off
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
